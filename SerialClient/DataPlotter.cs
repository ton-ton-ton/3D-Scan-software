using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Research.DynamicDataDisplay;
using System.Text.RegularExpressions;
using System.Numerics;  // 複變
using MathNet.Numerics; // 傅立葉轉換
using MathNet.Numerics.IntegralTransforms;

using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot.Axes;
using FftSharp.Windows;
using FftSharp;
using System.IO;

namespace SerialClient
{
    public partial class DataPlotter : Form
    {

        //  圖表對象
        private PlotView RawDataPlot;
        private PlotView SpectrumPlot;
        PlotModel DataPlot;
        PlotModel SpectrumDataPlot;
        IWindow window;

        //  LowPass Filter(未使用)
        readonly float cutoffFrequency = 10.0f; // Set your desired cutoff frequency in Hz
        float sampleFrequency = 100.0f; // Set your sample frequency in Hz
        bool adaptive = true;
        LowPassFilter lp;

        //  Timer Stuff
        System.Threading.Timer updateTimer;     //  圖表更新

        //  SQL DataBase
        private RealTimeDB DB;
        private List<String> SignalNames;

        //  Form Stuff Variables(表單內容變量)
        bool ShowNames = true;   //  右邊選單的顯示狀態
        int lastSplitterDistance = 250;      //  Splitter的位置
        private double SampleRate =  200;     //  取樣頻率，影響 X 軸，須滿足Nyquist定理
        private float FrameRate = 0;         //  幀數
        Single XAxisLength = 10.0f;          //  時域 X 軸時間範圍, 10s
        bool isSelectedWindow = false;       //  窗體濾波狀態
        bool islogScale = false;             //  頻率 Y 軸尺度

        //  儲存圖表數據所需變量
        public List<Tuple<double, double>> StorageRawData = new List<Tuple<double, double>>();    //  給予外部讀取頻譜數據; //  供外部讀取數據的變量
        public List<Tuple<double, double>> StorageSpectrumnData = new List<Tuple<double, double>>();    //  給予外部讀取頻譜數據; //  供外部讀取數據的變量

        bool isSavingData = false;
        string dataLog_path = "../../../DataLog/";

        //  property(屬性)
        private bool isSpectrumVisible { get; set; } = false;

        //  delegate events(委派事件)
        public event EventHandler DataPlotClosing;

        public DataPlotter(RealTimeDB Database)
        {
            InitializeComponent();

            //  初始化圖表
            RawDataPlot = new PlotView();
            SpectrumPlot = new PlotView();

            //  數據圖
            RawDataPlot.Location = new Point(0, 0);
            RawDataPlot.Width = this.Width;
            RawDataPlot.Height = this.Height;
            DataPlot = new PlotModel()
            {
                //Title = "Serial Plotter",
                Background = OxyColors.White,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightMiddle,
                LegendBackground = OxyColor.FromAColor(100, OxyColors.White),
                LegendBorder = OxyColors.Black,
                IsLegendVisible = false,
            };
            DataPlot.PlotMargins = new OxyThickness(75, 15, 35, 65);
            LinearAxis(DataPlot, "Time (s)", "X");
            LinearAxis(DataPlot, "Amplitude (m/s²)", "Y");
            RawDataPlot.Model = DataPlot;
            this.Controls.Add(this.RawDataPlot);
            RawDataPlot.Parent = panel_DataPlot;  //  控制項(RawDataPlot)動態加入至視窗(Panel1)中的常見方法
            RawDataPlot.Update();

            //  頻譜圖
            SpectrumPlot.Location = new Point(0, 0);
            SpectrumPlot.Width = this.Width;
            SpectrumPlot.Height = this.Height;
            SpectrumDataPlot = new PlotModel()
            {
                //Title = "Spectrum Plotter",
                Background = OxyColors.White,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightMiddle,
                LegendBackground = OxyColor.FromAColor(100, OxyColors.White),
                LegendBorder = OxyColors.Black,
                IsLegendVisible = false,
            };
            SpectrumDataPlot.PlotMargins = new OxyThickness(75, 15, 35, 65);
            LinearAxis(SpectrumDataPlot, "Frequency (Hz)", "X");
            LinearAxis(SpectrumDataPlot, "Magnitude", "Y");
            SpectrumPlot.Model = SpectrumDataPlot;
            this.Controls.Add(this.SpectrumPlot);
            SpectrumPlot.Parent = panel_Spectrum;
            SpectrumPlot.Update();

            //  連接資料庫
            DB = Database;
            updateSignalNames();

            //  載入濾波窗體
            IWindow[] windows = FftSharp.Window.GetWindows();
            toolStripComboBox_WindowItems.Items.AddRange(windows);
            toolStripComboBox_WindowItems.SelectedIndex = windows.ToList().FindIndex(x => x.Name == "Null");

            lp = new LowPassFilter(cutoffFrequency, sampleFrequency, adaptive);

            //  註冊事件
            this.Resize += new EventHandler(FrmResize);

            // 創建用於顯示資料圖的計時器
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            TimerCallback tcb = Update;
            updateTimer = new System.Threading.Timer(tcb, autoEvent, 0, 1000/30);
            
            TB_XAxisTime.Text = XAxisLength.ToString();
            FrmResize(this, new System.EventArgs());

            //dgvplots.CellValueChanged += dgvplots_CellValueChanged;
        }

        #region Form Stuff
        private void FrmResize(object sender, System.EventArgs e)
        {
            Splitter.Size = new Size(this.Size.Width, this.Size.Height - toolStrip1.Size.Height);
            if (!(ShowNames))
            {
                if (Splitter.Width > 20)
                {
                    Splitter.SplitterDistance = this.Size.Width; // 將分割線移至form的最右邊
                }
                Splitter.Panel2Collapsed = true;
                Splitter.Panel2.Hide();
            }
            else
            {
                Splitter.Panel2Collapsed = false;
                Splitter.Panel2.Show();
                if (Splitter.Width > 20)
                {
                    Splitter.SplitterDistance = Math.Max(Splitter.Panel1MinSize, this.Size.Width - lastSplitterDistance);
                }
            }

            RawDataPlot.Width = panel_DataPlot.Width;
            SpectrumPlot.Width = panel_Spectrum.Width;
            RawDataPlot.Height = Splitter.Panel1.Height - 15;
            SpectrumPlot.Height = Splitter.Panel1.Height - 15;

            dgvplots.Width = Splitter.Panel2.Width;
            dgvplots.Columns[1].Width = dgvplots.Width - dgvplots.Columns[0].Width - 23;
            dgvplots.Height = Splitter.Panel2.Height;
        }

        private void SplitterMouseUp(object sender, MouseEventArgs e)
        {
            lastSplitterDistance = this.Size.Width - e.Location.X;
            FrmResize(this, new System.EventArgs());
        }

        private void TB_XAxisTime_KeyPress(Object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void TB_XAxisTime_Validating(Object sender, CancelEventArgs e)
        {
            String newText = TB_XAxisTime.Text;
            if (newText.Trim().Equals('*'))
            {

            }
            else if (Regex.Match(newText.Trim(), @"^(\d*\.\d*|\d+)$").Success)
            {
                try
                {
                    Single.TryParse(newText.Trim(), out XAxisLength);
                }
                catch (Exception)
                {

                }
            }
            else
            {
                e.Cancel = true;
            }

        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (ShowNames)
            {
                btn_ShowNames.Text = "◀";
                ShowNames = false;
                FrmResize(this, new System.EventArgs());
            }
            else
            {
                btn_ShowNames.Text = "▶";
                ShowNames = true;
                FrmResize(this, new System.EventArgs());

            }
        }

        private void btn_PauseRun_Click(object sender, EventArgs e)
        {
            if (btn_PauseRun.Checked)
            {
                btn_PauseRun.Text = "Run";
            }
            else
            {
                btn_PauseRun.Text = "Pause";
            }
        }

        private void btn_ShowLegend_Click(object sender, EventArgs e)
        {
            if (!BTN_ShowLegend.Checked)
            {
                BTN_ShowLegend.Text = " Show Legend";
                DataPlot.IsLegendVisible = false;
                DataPlot.InvalidatePlot(true);
                if (isSpectrumVisible)
                {
                    SpectrumDataPlot.IsLegendVisible = false;
                    SpectrumDataPlot.InvalidatePlot(true);
                }
            }
            else
            {
                BTN_ShowLegend.Text = " Hide Legend";
                DataPlot.IsLegendVisible = true;    //  顯示圖例
                DataPlot.InvalidatePlot(true);  //  更新圖表顯示
                if (isSpectrumVisible)
                {
                    SpectrumDataPlot.IsLegendVisible = true;
                    SpectrumDataPlot.InvalidatePlot(true);
                }
            }
        }

        private void dgvplots_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == VisibleSereis.Index && e.RowIndex >= 0) // 當下所在的列位置大於等於零
            {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dgvplots.Rows[e.RowIndex].Cells[0];
                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)dgvplots.Rows[e.RowIndex].Cells[1];

                bool isChecked = (bool)dgvplots.Invoke(new Func<bool>(() => (bool)checkBoxCell.EditingCellFormattedValue));
                if (isChecked)
                {
                    //// 在這裡進行 OxyPlot 的更新
                    //UpdateDataPlot(e.RowIndex, comboBoxCell);
                    //DataPlot.InvalidatePlot(true);
                    //RawDataPlot.Update();
                }

            }
            //if (e.ColumnIndex == VisibleSereis.Index && e.RowIndex >= 0) // 當下所在的列位置大於等於零
            //{
            //    DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dgvplots.Rows[e.RowIndex].Cells[0];
            //    DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)dgvplots.Rows[e.RowIndex].Cells[1];

            //    if (RawDataPlot != null)
            //    {
            //        DataPlot.InvalidatePlot(true);
            //        RawDataPlot.Update();

            //    }

            //}

            //if (e.ColumnIndex == VisibleSereis.Index && e.RowIndex >= 0) // 當下所在的列位置大於等於零
            //{
            //    DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dgvplots.Rows[e.RowIndex].Cells[0];
            //    DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)dgvplots.Rows[e.RowIndex].Cells[1];

            //    if (checkBoxCell != null && comboBoxCell != null)
            //    {
            //        if (!(bool)checkBoxCell.EditingCellFormattedValue) // 如果沒被勾選
            //        {
            //            if (this.dgvplots.InvokeRequired)
            //            {
            //                this.Invoke(new Action(() =>
            //                {
            //                    comboBoxCell.Value = null;
            //                    if (dgvplots.Rows.Count == 1)
            //                    {
            //                        // 如果只剩下一行，不允许删除该行
            //                        dgvplots.Rows[0].Cells[VisibleSereis.Index].Value = true;
            //                    }
            //                    else
            //                    {
            //                        // 删除当前行
            //                        dgvplots.Rows.RemoveAt(e.RowIndex);
            //                    }
            //                }));
            //            }
            //            else
            //            {
            //                comboBoxCell.Value = null;
            //                if (dgvplots.Rows.Count == 1)
            //                {
            //                    // 如果只剩下一行，不允许删除该行
            //                    dgvplots.Rows[0].Cells[VisibleSereis.Index].Value = true;
            //                }
            //                else
            //                {
            //                    // 删除当前行
            //                    dgvplots.Rows.RemoveAt(e.RowIndex);
            //                }
            //            }

            //        }
            //    }
            //}
        }
        private void UpdateOxyPlot()
        {
            // 獲取選取的參數名稱，假設它在 "ParamName" 欄位中
            string selectedParamName = dgvplots.Rows[dgvplots.CurrentCell.RowIndex].Cells[1].Value.ToString();

            // 在這裡根據選取的參數名稱計算數據，更新 OxyPlot 的顯示
            // 例如，使用與 "Spectrum" 中相似的程式碼計算頻譜，並更新 OxyPlot 的數據

            // 最後，重新繪製 OxyPlot
            SpectrumDataPlot.InvalidatePlot(true);
        }

        private void btn_ShowSpectrum_Click(object sender, EventArgs e)
        {
            if (!isSpectrumVisible)
            {
                this.btn_ShowSpectrum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));

                for (int row = 0; row < dgvplots.Rows.Count; row++)
                {
                    DataGridViewCheckBoxCell visibleCell = (DataGridViewCheckBoxCell)dgvplots.Rows[row].Cells[0];
                    DataGridViewComboBoxCell paramNameCell = (DataGridViewComboBoxCell)dgvplots.Rows[row].Cells[1];

                    if (!(paramNameCell.Value == null) && (DB.Signals().Contains(paramNameCell.Value.ToString())))
                    {
                        RemoveDataSeries(row);
                        // 移除數據圖,觸發AddChartSeries()
                        //var lineSeriesList = DataPlot.Series.OfType<LineSeries>().ToList();
                        //foreach (var lineSeries in lineSeriesList)
                        //{
                        //    DataPlot.Series.Remove(lineSeries);
                        //}
                    }
                }

                this.Invoke(new Action(() =>
                {
                    //  關閉圖例
                    if (BTN_ShowLegend.Checked) { SpectrumDataPlot.IsLegendVisible = true; }
                    else { SpectrumDataPlot.IsLegendVisible = false; }
                    isSpectrumVisible = true;   //  頻譜顯示狀態改變

                    SpectrumDataPlot.InvalidatePlot(true);
                }));
            }
            else
            {
                this.btn_ShowSpectrum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
                
                isSpectrumVisible = false;

                //for (int row = 0; row < dgvplots.Rows.Count; row++)
                //{
                //    DataGridViewCheckBoxCell visibleCell = (DataGridViewCheckBoxCell)dgvplots.Rows[row].Cells[0];
                //    DataGridViewComboBoxCell paramNameCell = (DataGridViewComboBoxCell)dgvplots.Rows[row].Cells[1];

                //    if (!(paramNameCell.Value == null) && (DB.Signals().Contains(paramNameCell.Value.ToString())))
                //    {
                //        LineSeries spectrumSeries = SpectrumDataPlot.Series.OfType<LineSeries>().FirstOrDefault(s => s.Tag.Equals(row));

                //        if (spectrumSeries != null)
                //        {
                //            SpectrumDataPlot.Series.Remove(spectrumSeries);
                //            SpectrumDataPlot.InvalidatePlot(true);
                //        }
                //    }
                //}
                var lineSeriesList_ = SpectrumDataPlot.Series.OfType<LineSeries>().ToList();
                foreach (var lineSeries in lineSeriesList_)
                {
                    SpectrumDataPlot.Series.Remove(lineSeries);
                }
                SpectrumDataPlot.InvalidatePlot(true);
            }
            
        }

        private void toolStripBtn_FPS_Click(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                if (toolStripBtn_FPS.Checked)
                {
                    label_FPS.Visible = false;
                    toolStripBtn_FPS.Checked = false;

                }
                else
                {
                    label_FPS.Visible = true;
                    toolStripBtn_FPS.Checked = true;
                }
            }));
        }

        private void toolStripbtn_SpectrumWindowed_Click(object sender, EventArgs e)
        {
            if (toolStripbtn_SpectrumWindowed.Checked)
            {
                toolStripbtn_SpectrumWindowed.Checked = false;

            }
            else
            {
                toolStripbtn_SpectrumWindowed.Checked = true;
            }
        }

        private void DataPlotter_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DataPlotClosing != null)
            {
                DataPlotClosing?.Invoke(this, e);
                updateTimer.Dispose();
            }
        }

        private void toolStripComboBox_WindowItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((String)toolStripComboBox_WindowItems.SelectedItem != "Null")
            {
                window = (IWindow)toolStripComboBox_WindowItems.SelectedItem;
                isSelectedWindow = true;
            }
            else
            {
                window = null;
                isSelectedWindow = false;
            }
        }

        /// <summary>
        /// 移除所有數據線段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_RemoveAllLines_Click(object sender, EventArgs e)
        {
            // 用于存储需要移除的行的索引
            List<int> rowsToRemove = new List<int>();

            for (int row = dgvplots.Rows.Count - 1; row >= 0; row--)
            {
                DataGridViewCheckBoxCell visibleCell = (DataGridViewCheckBoxCell)dgvplots.Rows[row].Cells[0];
                DataGridViewComboBoxCell paramNameCell = (DataGridViewComboBoxCell)dgvplots.Rows[row].Cells[1];

                if (!(paramNameCell.Value == null))
                {
                    String isChecked = (String)dgvplots.Invoke(new Func<String>(() => (String)visibleCell.Value));
                    if (isChecked == "T" && visibleCell != null)
                    {
                        // 添加要移除的行索引
                        rowsToRemove.Add(row);
                    }
                }
            }
            // 移除行
            this.Invoke(new Action(() =>
            {
                foreach (int rowIndexToRemove in rowsToRemove)
                {
                    dgvplots.Rows.RemoveAt(rowIndexToRemove);
                    RemoveDataSeries(rowIndexToRemove);
                }
            }));

            // 找到所有 LineSeries 並刪除它們
            var lineSeriesList = DataPlot.Series.OfType<LineSeries>().ToList();
            foreach (var lineSeries in lineSeriesList)
            {
                DataPlot.Series.Remove(lineSeries);
            }

            //  如果頻譜有顯示的話執行
            if (isSpectrumVisible)
            {
                var lineSeriesList_ = SpectrumDataPlot.Series.OfType<LineSeries>().ToList();
                foreach (var lineSeries in lineSeriesList_)
                {
                    SpectrumDataPlot.Series.Remove(lineSeries);
                }
                StorageSpectrumnData.Clear();   //刪除內存
                StorageRawData.Clear();
                SpectrumDataPlot.InvalidatePlot(true);
            }

            // 更新繪圖
            DataPlot.InvalidatePlot(true);
        }

        /// <summary>
        /// 更改 Y 軸表示的刻度(Log / Liner Scale)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripbtn_YAisScale_Click(object sender, EventArgs e)
        {
            if (isSpectrumVisible)
            {
                if (!islogScale)
                {
                    islogScale = true;
                    toolStripbtn_YAisScale.Text = "Liner scale";
                    var yAxes = SpectrumDataPlot.Axes.Where(axis => axis.Position == AxisPosition.Left).ToList();
                    foreach (var yAxis in yAxes)
                    {
                        SpectrumDataPlot.Axes.Remove(yAxis);
                    }
                    LinearAxis(SpectrumDataPlot, "Magnitude(dB)", "Y");
                }
                else
                {
                    islogScale = false;
                    toolStripbtn_YAisScale.Text = "Log scale";
                    var yAxes = SpectrumDataPlot.Axes.Where(axis => axis.Position == AxisPosition.Left).ToList();
                    foreach (var yAxis in yAxes)
                    {
                        SpectrumDataPlot.Axes.Remove(yAxis);
                    }
                    LinearAxis(SpectrumDataPlot, "Magnitude", "Y");
                }
            }
        }

        /// <summary>
        /// 圖表尺寸改變時執行的函式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_Spectrum_SizeChanged(object sender, EventArgs e)
        {
            //SpectrumPlot.Width = SpectrumPlot.Width;
            //SpectrumPlot.Height = SpectrumPlot.Height;
            //SpectrumDataPlot.InvalidatePlot(true);
        }

        #region Save Data
        private void toolStripBtn_SavingData_Click(object sender, EventArgs e)
        {
            if (isSpectrumVisible)
            {
                if (!isSavingData)
                {
                    toolStripBtn_SavingData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
                    toolStripBtn_SavingData.Font = new System.Drawing.Font("微軟正黑體", 7.8F, System.Drawing.FontStyle.Bold);

                    isSavingData = true;
                }
                else
                {
                    toolStripBtn_SavingData.BackColor = System.Drawing.Color.MidnightBlue;
                    toolStripBtn_SavingData.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    WriteToCSV();

                    isSavingData = false;
                }
            }
            else
            {
                if (!isSavingData)
                {
                    MessageBox.Show("儲存失敗：未開啟頻譜圖 ", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    toolStripBtn_SavingData.BackColor = System.Drawing.Color.MidnightBlue;
                    toolStripBtn_SavingData.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    isSavingData = false;
                }
            }

        }

        private void WriteToCSV()
        {
            //  儲存時域數據
            using (StreamWriter writer = new StreamWriter(dataLog_path + "RawData_" + DateTime.Now.ToString("yyyy-MM-dd HH_mm") + ".csv"))
            {
                foreach (var dataPoint in StorageRawData)
                {
                    writer.WriteLine($"{dataPoint.Item1},{dataPoint.Item2}");
                }
            }
            //  儲存頻譜數據
            using (StreamWriter writer = new StreamWriter(dataLog_path + "Specturm_" + DateTime.Now.ToString("yyyy-MM-dd HH_mm") + ".csv"))
            {
                foreach (var dataPoint in StorageSpectrumnData)
                {
                    writer.WriteLine($"{dataPoint.Item1},{dataPoint.Item2}");
                }
            }
        }
        #endregion

        #endregion

        #region Plotting Stuff

        /// <summary>
        /// 更新數據下拉選單
        /// </summary>
        public void updateSignalNames()
        {
            if (!(DB == null))
            {
                DataGridViewComboBoxColumn Col = (DataGridViewComboBoxColumn)dgvplots.Columns[1];

                Col.DataSource = null;
                SignalNames = DB.Signals();
                SignalNames.Sort(); // 排序 SignalNames
                Col.DataSource = SignalNames;
            }
        }
    
        /// <summary>
        /// 更新數據線段與圖表
        /// </summary>
        /// 
        private  void Update(Object stateInfo)
        {
            try
            {
                if (!(btn_PauseRun.Checked) && !(this.IsDisposed))
                {
                    
                        // 用於存储需要移除的行的索引
                        List<int> rowsToRemove = new List<int>();

                        //依據所有列的選項
                        for (int row = dgvplots.Rows.Count - 1; row >= 0; row--)
                        {
                            DataGridViewCheckBoxCell visibleCell = (DataGridViewCheckBoxCell)dgvplots.Rows[row].Cells[0];
                            DataGridViewComboBoxCell paramNameCell = (DataGridViewComboBoxCell)dgvplots.Rows[row].Cells[1];


                            if (!(paramNameCell.Value == null))
                            {
                            String isChecked = (String)dgvplots.Invoke(new Func<String>(() => (String)visibleCell.Value));
                                if (isChecked == "T" && visibleCell!= null)
                                {
                                    // 更新數據圖
                                    UpdateDataPlot(row, paramNameCell);
                                }
                                else
                                {
                                    // 添加要移除的行索引
                                    rowsToRemove.Add(row);
                                }

                            }
                        }
                        // 移除行
                        this.Invoke(new Action(() =>
                        {
                            foreach (int rowIndexToRemove in rowsToRemove)
                            {
                                dgvplots.Rows.RemoveAt(rowIndexToRemove);
                                RemoveDataSeries(rowIndexToRemove);
                            }
                        }));
                }
            }
            catch(Exception e) { Console.WriteLine("Error : " + e.ToString()); };
            
        }

        /// <summary>
        /// 更新圖表畫面
        /// </summary>
        /// <param name="row"></param>
        /// <param name="paramNameCell"></param>
        private void UpdateDataPlot(int row, DataGridViewComboBoxCell paramNameCell)
        {
            // 查找與當前row相同的數據序列，如未找到回傳null
            LineSeries existingSeries = DataPlot.Series.OfType<LineSeries>().FirstOrDefault(s => s.Tag.Equals(row));

            if (existingSeries == null)
            {
                // 創建新的數據序列並添加至序列集合內
                LineSeries newSeries = new LineSeries()
                {
                    Title = paramNameCell.Value.ToString().Trim(),
                    Tag = row
                };
                if (!this.IsDisposed)
                {
                    this.Invoke(new Action(() =>
                    {
                        AddChartSeries(newSeries);
                    }));
                }
            }
            else
            {
                if (!this.IsDisposed)
                {
                    this.Invoke(new Action(() =>
                    {
                        // 更新數據圖序列
                        UpdateSeriesData(existingSeries, paramNameCell.Value.ToString(), "DataPlot");

                        // 更新 Spectrum plot 的數據序列
                        if (isSpectrumVisible)
                        {
                            // 查找與當前row相同的 Spectrum 數據序列
                            LineSeries existingSpectrumSeries = SpectrumDataPlot.Series.OfType<LineSeries>().FirstOrDefault(s => s.Tag.Equals(row));
                            if (existingSpectrumSeries != null)
                            {
                               // 更新 Spectrum
                               UpdateSeriesData(existingSpectrumSeries, paramNameCell.Value.ToString(), "Spectrum");
                            }
                        }
                    }));
                }
            }
        }

        /// <summary>
        /// 移除數據線段
        /// </summary>
        /// <param name="row"></param>
        private void RemoveDataSeries(int row)
        {
            // 查找與當前row相同的數據序列
            LineSeries existingSeries = DataPlot.Series.OfType<LineSeries>().FirstOrDefault(s => s.Tag.Equals(row));

            if (existingSeries != null && !this.IsDisposed)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        RemovedChartSeries(existingSeries);
                    }));
                }
                else
                {
                    RemovedChartSeries(existingSeries);
                }
            }
        }

        /// <summary>
        /// 移除圖表中的數據線段圖
        /// </summary>
        /// <param name="Ser"></param>
        private void RemovedChartSeries(LineSeries Ser)
        {
            if (!this.IsDisposed)
            {
                // 從數據圖的序列集合中移除指定的序列
                DataPlot.Series.Remove(Ser);
                DataPlot.InvalidatePlot(true);

                var spectrumSeries = SpectrumDataPlot.Series.FirstOrDefault(s => s.Title == Ser.Title && s.Tag == Ser.Tag);
                if (spectrumSeries != null)
                {
                    SpectrumDataPlot.Series.Remove(spectrumSeries);
                    SpectrumDataPlot.InvalidatePlot(true);
                }

            }
        }

        /// <summary>
        /// 新增新的數據線段
        /// </summary>
        /// <param name="Ser"></param>
        private void AddChartSeries(LineSeries Ser)
        {
             LineSeries newSeries = CreateNewLineSeries(Ser);

             // 如果 DataPlot 的序列集合中不包含指定的序列，則添加該序列
             if (!DataPlot.Series.Contains(newSeries))
             {
                 DataPlot.Series.Add(newSeries);
             }

             // 如果頻譜可見，則創建新的序列並添加至 SpectrumDataPlot 的序列集合中
             if (isSpectrumVisible)
             {
                 LineSeries spectrumSeries = CreateNewLineSeries(Ser);
                 if (!SpectrumDataPlot.Series.Contains(spectrumSeries))
                 {
                     SpectrumDataPlot.Series.Add(spectrumSeries);
                 }
                SpectrumDataPlot.InvalidatePlot(true);
            }

            DataPlot.InvalidatePlot(true);
        }

        /// <summary>
        /// 更新數據線資料(時域 / 頻域)
        /// </summary>
        /// <param name="Ser"></param>
        /// <param name="signalName"></param>
        /// <param name="plot"></param>
        private void UpdateSeriesData(LineSeries Ser, string signalName, String plot)
        {
            int Lock = DB.requestLock();
            if (Lock >= 0)
            {
                List<Tuple<Single, Single>> data = DB.Read(signalName, Convert.ToSingle(DB.GetTimeSec() - XAxisLength), Convert.ToSingle(DB.GetTimeSec())); //  item1 = 時間; item2 = 數據
              
                switch (plot)
                {
                     case "DataPlot":
                        Ser.MarkerType = MarkerType.Circle;
                        Ser.MarkerSize = 2;
                        Ser.Title = signalName;
                        Ser.Points.Clear();

                        double[] RawData = new double[data.Count];
                        for (int i = 0; i < data.Count; i++)
                            RawData[i] = Convert.ToDouble(data[i].Item2);

                        //  加窗
                        double[] RawDataWindowed = new double[RawData.Length];
                        if (window != null) { RawDataWindowed = window.Apply(RawData); }
                        else { Array.Copy(RawData, RawDataWindowed, RawData.Length); }

                        List<DataPoint> serPoints = new List<DataPoint>();  // 創建數據點
                        List<Tuple<double, double>> StorageRawDataBuff = new List<Tuple<double, double>>(); // 創建暫存外部數據點

                        for (int i = 0; i < RawDataWindowed.Length; i++)
                        {
                            //   存入圖表序列
                            serPoints.Add(new DataPoint(data[i].Item1, RawDataWindowed[i]));   

                            //   外部讀取序列
                            if (isSavingData)
                            {
                                double time = data[i].Item1;
                                double amplitude = data[i].Item2;
                                StorageRawDataBuff.Add(new Tuple<double, double>(time, amplitude));
                            }
                        }
                        if (StorageRawDataBuff.Count > 0)
                            StorageRawData = StorageRawDataBuff;    //  給予全域儲存變量

                        Ser.Points.AddRange(serPoints); //  序列存入數據點

                        if (toolStripBtn_FPS.Checked)
                            CalculateSamplingRate(data); // 計算FPS

                        DataPlot.InvalidatePlot(true);
                        //UpdateXAxis(DataPlot);
                        break;

                     case "Spectrum":
                         Ser.MarkerType = MarkerType.Circle;
                         Ser.MarkerSize = 2;
                         Ser.Title = signalName;
                         Ser.Points.Clear();

                        //  建立數據點復本, float 轉為 double
                         double[] RawSignal = new double[data.Count];
                        for (int i = 0; i < data.Count; i++)
                            RawSignal[i] = Convert.ToDouble(data[i].Item2);

                        //  時域濾波
                         double[] RawSignalWindowed = new double[RawSignal.Length];
                        if (window != null) { RawSignalWindowed = window.Apply(RawSignal); }
                        else { Array.Copy(RawSignal, RawSignalWindowed, RawSignal.Length); }
                        double[] paddingSignal = ZeroPad(RawSignalWindowed);    //  數據長變為2的倍數，缺的補零

                        //  執行傅立葉轉換
                        Complex[] spectrum = new Complex[paddingSignal.Length];
                        for (int i = 0; i < paddingSignal.Length; i++)
                            spectrum[i] = new Complex(paddingSignal[i], 0);
                        Fourier.Forward(spectrum, FourierOptions.NoScaling);

                        //頻域濾波


                        //  計算頻譜強度
                        double[] frequencies = Fourier.FrequencyScale(spectrum.Length, SampleRate);
                        double[] Magnitude = new double[(spectrum.Length / 2)];
                        for (int i = 0; i < Magnitude.Length; i++)
                        {
                            Magnitude[i] = (2.0 / paddingSignal.Length) * Complex.Abs(spectrum[i]);  // 注意索引偏移
                            if (islogScale) Magnitude[i] = Math.Log(Magnitude[i]);  // Y 軸刻度改變時執行取 Log 運算
                        }

                        //  序列存入數據點
                        List<DataPoint> spectrumPoints = new List<DataPoint>();
                        List<Tuple<double, double>> StorageDataBuff = new List<Tuple<double, double>>();
                        for (int i = 0; i < Magnitude.Length; i++)
                        {
                            //  存入圖表序列
                            spectrumPoints.Add(new DataPoint(frequencies[i], Magnitude[i]));

                            //  外部讀取序列
                            if (isSavingData)
                            { 
                                double freq = frequencies[i];
                                double mag = Magnitude[i];
                                StorageDataBuff.Add(new Tuple<double, double>(freq, mag));
                            }
                        }
                        if(StorageDataBuff.Count > 0)
                            StorageSpectrumnData = StorageDataBuff;
                        Ser.Points.AddRange(spectrumPoints);

                        SpectrumDataPlot.InvalidatePlot(true);

                        //  給定固定頻率的輸入並輸出顯示頻譜，用於測試傅立葉轉換是否正確
                        //Ser.MarkerType = MarkerType.Circle;
                        //Ser.MarkerSize = 2;
                        //Ser.Points.Clear();
                        //int numsample = 6000;
                        //double samplerate = 1000;
                        //double[] fundamental = Generate.Sinusoidal(numsample, samplerate, 60, 10.0);
                        //double[] second = Generate.Sinusoidal(numsample, samplerate, 120, 1.0);
                        //double[] third = Generate.Sinusoidal(numsample, samplerate, 180, 15.0);

                        //Complex[] spectrum = new Complex[numsample];
                        //for (int i = 0; i < numsample; i++)
                        //{
                        //    spectrum[i] = new Complex(fundamental[i] + second[i] + third[i], 0.0);
                        //}
                        //Fourier.Forward(spectrum, FourierOptions.NoScaling);


                        //double[] frequencies = Fourier.FrequencyScale(spectrum.Length, samplerate);
                        //double[] amplitudes = new double[(spectrum.Length / 2)];
                        //for (int i = 0; i < amplitudes.Length; i++)
                        //{
                        //    amplitudes[i] = (2.0 / numsample) * Complex.Abs(spectrum[i]); // 注意索引偏移
                        //}

                        //List<DataPoint> spectrumPoints = new List<DataPoint>();
                        //for (int i = 0; i < amplitudes.Length; i++)
                        //{
                        //    spectrumPoints.Add(new DataPoint(frequencies[i], amplitudes[i]));
                        //}

                        //Ser.Points.AddRange(spectrumPoints);
                        //SpectrumDataPlot.InvalidatePlot(true);

                        break;
                }
                DB.returnLock(Lock);
            }

        }

        /// <summary>
        /// 複製線段資訊並回傳
        /// </summary>
        /// <param name="sourceSeries"></param>
        /// <returns></returns>
        private LineSeries CreateNewLineSeries(LineSeries sourceSeries)
        {
            //創建新的序列
            LineSeries newSeries = new LineSeries();
            newSeries.Title = sourceSeries.Title;
            newSeries.Tag = sourceSeries.Tag;

            // 將原始數據的點加入新的數據序列
            List<DataPoint> serPoints = new List<DataPoint>();
            foreach (var item in sourceSeries.Points)
            {
                serPoints.Add(new DataPoint(item.X, item.Y));
            }
            newSeries.Points.Clear();
            newSeries.Points.AddRange(serPoints);

            return newSeries;
        }

        /// <summary>
        /// 計算幀率
        /// </summary>
        /// <param name="signalData"></param>
        public void CalculateSamplingRate(List<Tuple<Single, Single>> signalData)
        {
            // 獲取數據列中的第一個和最後一個時間
            Single firstTimestamp = signalData[0].Item1;
            Single lastTimestamp = signalData[signalData.Count - 1].Item1;

            // 時間差
            Single timeDifference = lastTimestamp - firstTimestamp;

            // 獲取間格樣本數
            int sampleCount = signalData.Count;

            // 計算SampleRate
            FrameRate = sampleCount / timeDifference;

            this.Invoke(new Action(() =>
            {
                this.label_FPS.Text = FrameRate.ToString("F1");
            }));

        }

        /// <summary>
        /// 更新 X 軸的刻度
        /// </summary>
        //private void UpdateXAxis(PlotModel RawDataPlot)
        //{
        //    try
        //    {
        //        Find the SCaling
        //        double min = DB.GetTimeSec() - XAxisLength;
        //        if (min < 0)
        //        {
        //            min = 0;
        //        }

        //        Find the Bottom Axis
        //        OxyPlot.Axes.Axis Ax = null;
        //        for (int i = 0; i < RawDataPlot.Axes.Count; i++)
        //        {
        //            if (RawDataPlot.Axes[i].Position == OxyPlot.Axes.AxisPosition.Bottom)
        //            {
        //                Ax = RawDataPlot.Axes[i];
        //                break;
        //            }
        //        }
        //        if (!(Ax == null))
        //        {
        //            double oldMin = Ax.Minimum;
        //            double oldMax = Ax.Maximum;

        //            Update the axis range only if the minimum value has changed
        //            if (Math.Abs(oldMin - min) > double.Epsilon)
        //            {
        //                Ax.Minimum = min;
        //                Ax.Maximum = min + XAxisLength;
        //            }
        //        }
        //        RawDataPlot.InvalidatePlot(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error Message：" + ex.Message, "Fault", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        /// <summary>
        /// 設定 X 或 Y 軸的標號
        /// </summary>
        /// <param name="plot"></param>
        /// <param name="title"></param>
        /// <param name="axes"></param>
        private void LinearAxis(PlotModel plot, String title, String axes)
        {
            LinearAxis Axis = null;
            switch (axes)
            {
                case "X":
                    Axis = new LinearAxis()
                    {
                        Position = AxisPosition.Bottom,
                        Title = title,
                        MajorGridlineStyle = LineStyle.Solid,
                    };
                    break;
                case "Y":
                    Axis = new LinearAxis()
                    {
                        Position = AxisPosition.Left,
                        Title = title,
                        MajorGridlineStyle = LineStyle.Solid,
                    };
                    break;
            }
            plot.Axes.Add(Axis);
        }

        #endregion

        #region Utility function
        public static double[] ZeroPad(double[] input)
        {
            if (IsPowerOfTwo(input.Length))
                return input;

            int targetLength = 1;
            while (targetLength < input.Length)
                targetLength *= 2;

            int difference = targetLength - input.Length;
            double[] padded = new double[targetLength];
            Array.Copy(input, 0, padded, difference / 2, input.Length);

            return padded;
        }
        public static bool IsPowerOfTwo(int x)
        {
            return ((x & (x - 1)) == 0) && (x > 0);
        }

        private double[] FrequencyDomainIntegration(double[] frequencyDomainSignal, double samplingRate)
        {
            // 对频域信号进行积分操作
            double[] integratedSignal = new double[frequencyDomainSignal.Length];

            // 频域积分公式
            for (int i = 0; i < integratedSignal.Length; i++)
            {
                integratedSignal[i] = frequencyDomainSignal[i] / (2 * Math.PI * i / frequencyDomainSignal.Length);
            }

            // 乘以采样频率得到位移信号
            for (int i = 1; i < integratedSignal.Length; i++)
            {
                integratedSignal[i] *= (2 * Math.PI * i / samplingRate);
            }

            return integratedSignal;
        }

        public static (byte n, double wc) LowPass(double passbandFreq, double stopbandFreq, double passbandRipple, double stopbandAttenuation)
        {
            CheckFrequency(passbandFreq, nameof(passbandFreq));
            CheckFrequency(stopbandFreq, nameof(stopbandFreq));
            CheckDouble(passbandRipple, nameof(passbandRipple));
            CheckDouble(stopbandAttenuation, nameof(stopbandAttenuation));

            if (stopbandFreq < passbandFreq)
            {
                throw new ArgumentException("Stopband corner frequency must be greater than passband corner frequency.", nameof(stopbandFreq));
            }

            if (stopbandAttenuation < passbandRipple)
            {
                throw new ArgumentException("Stopband attenuation must be greater than passband ripple.", nameof(stopbandAttenuation));
            }

            var wwp = Math.Tan(Math.PI * passbandFreq / 2);
            var wws = Math.Tan(Math.PI * stopbandFreq / 2);

            var qp = Math.Log(Math.Pow(10, passbandRipple / 10) - 1);
            var qs = Math.Log(Math.Pow(10, stopbandAttenuation / 10) - 1);

            var n = (byte)Math.Ceiling((qs - qp) / (2 * (Math.Log(wws) - Math.Log(wwp))));

            var wwcp = Math.Exp(Math.Log(wwp) - (qp / 2 / n));

            return (n, Math.Atan(wwcp) * 2 / Math.PI);
        }

        internal static void CheckFrequency(double frequency, string parameterName)
        {
            CheckDouble(frequency, parameterName);
            if (frequency < 0 || frequency > 1)
            {
                throw new ArgumentException($"The {parameterName} frequency must be normalized to Nyquist rate.", parameterName);
            }
        }

        internal static void CheckDouble(double value, string parameterName)
        {
            if (double.IsInfinity(value) || double.IsNaN(value))
            {
                throw new ArgumentException($"The {parameterName} must be a finite value.", parameterName);
            }
        }


        #endregion

        
    }
}
