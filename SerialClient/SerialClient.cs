using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace SerialClient
{
    public partial class Client : Form
    {
        //Main Databse
        private RealTimeDB DB;
        //private Serial_API ASI;
        private DataRecorder Recorder = null;
        private DataRecord2txt Recordertxt = null;
        private DataPlotter Plt = null;
         

        // For Serial Plotter
        System.Threading.Timer updateTimerForPlot;  
        Dictionary<String, List<Tuple<Single, Single>>> Buffer = new Dictionary<String, List<Tuple<Single, Single>>>();  // 存入DB前的暫存空間
        float[] dataflow = new float[13];   // 儲存serial上的數據

        //Serial Port Communication Stuff
        SerialPort ComPort;

        Dictionary<String, String> EOLChars = new Dictionary<String, String>();

        //Form Stuff
        bool Autoscroll = true;
        bool isProcessing = false;  //是否正在執行顯示接受到的數據
        bool isDataReceivedSubscribed = false;
        bool isOpenSerialPlotter = false;

        //委派事件
        public event EventHandler SerialFormClosing;

        public Client(SerialPort com, RealTimeDB realTimeDB)
        {
            InitializeComponent();

            //畫面大小
            this.Resize += new EventHandler(FrmResize);

            //定義物件初始狀態
            DB = realTimeDB;     //  SQL對象初始化
            ComPort = com;       //  串口初始化
            //ASI = new Serial_API(DB);   // ASI對象初始化
            this.Text = ComPort.PortName;   // 視窗名稱為com

            //新增每一行的結尾符號選項
            EOLChars.Add("None", "");
            EOLChars.Add("Newline", "\n");
            EOLChars.Add("Carraige Regurn", "\r");
            EOLChars.Add("Both NL and CR", "\n\r");
            //結尾符號載入Combox物件內
            cmb_eol.DataSource = new BindingSource(EOLChars, null);
            cmb_eol.DisplayMember = "Key";
            cmb_eol.ValueMember = "Value";

            //初始化 Buffer
            Buffer.Add("Accelerometer_X", new List<Tuple<Single, Single>>());
            Buffer.Add("Accelerometer_Y", new List<Tuple<Single, Single>>());
            Buffer.Add("Accelerometer_Z", new List<Tuple<Single, Single>>());
            Buffer.Add("AngularVelocity_X", new List<Tuple<Single, Single>>());
            Buffer.Add("AngularVelocity_Y", new List<Tuple<Single, Single>>());
            Buffer.Add("AngularVelocity_Z", new List<Tuple<Single, Single>>());
            Buffer.Add("Quaternion_W", new List<Tuple<Single, Single>>());
            Buffer.Add("Quaternion_X", new List<Tuple<Single, Single>>());
            Buffer.Add("Quaternion_Y", new List<Tuple<Single, Single>>());
            Buffer.Add("Quaternion_Z", new List<Tuple<Single, Single>>());
            Buffer.Add("Displacement_X", new List<Tuple<Single, Single>>());
            Buffer.Add("Displacement_Y", new List<Tuple<Single, Single>>());

        }

        
        #region 控件事件程序
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newPlotterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isOpenSerialPlotter = true;
            
            //  開啟計時器，更新點雲顯示
            AutoResetEvent autoEvent = new AutoResetEvent(false);   //  同步基元類型，用於多執行緒間同步
            TimerCallback scanCallBack = UpdateData;
            updateTimerForPlot = new System.Threading.Timer(scanCallBack, dataflow, 0, 28);
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    Plt = new DataPlotter(DB);
                    Plt.DataPlotClosing += new EventHandler(Plt_DataPlotClosing);
                    Plt.Show();
                }));
            }
            else
            {
                Plt = new DataPlotter(DB);
                Plt.DataPlotClosing += new EventHandler(Plt_DataPlotClosing);
                Plt.Show();
            }
        }

        private void Plt_DataPlotClosing(object sender, EventArgs e)
        {
            isOpenSerialPlotter = false;
            updateTimerForPlot.Dispose();
            Plt.Dispose();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            try
            {
                ComPort.Write(tb_send.Text + cmb_eol.SelectedValue);
                tb_send.Clear();
            }
            catch
            {
                MessageBox.Show("Send失敗，還未與COM連線成功", "操作錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            rtb_serialdata.Clear();
        }

        private void requestSymbolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ASI.getSymbols(ComPort);
        }

        private void restartDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DB.Reset();
            //ASI.getSymbols(ComPort);
        }

        private void dataRecord2CsvtoolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if ((Recorder == null) || Recorder.IsDisposed)
            {
                Recorder = new DataRecorder(DB);
                Recorder.Show();
            }
            else
            {
                Recorder.BringToFront();
            }

        }

        private void dataRecord2TxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Recordertxt == null) || Recordertxt.IsDisposed)
            {
                Recordertxt = new DataRecord2txt();
                Recordertxt.Show();
                Recordertxt.BringToFront();
            }
            else
            {
                Recordertxt.BringToFront();
            }
        }

        private void cb_autoscroll_CheckedChanged(object sender, EventArgs e)
        {
            Autoscroll = cb_autoscroll.Checked;
        }

        /// <summary>
        /// 暫停讀取串口數據
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pause_Click(object sender, EventArgs e)
        {
            if (isDataReceivedSubscribed)
            {
                ComPort.DataReceived -= ComPort_DataReceived;
                this.btn_Pause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
                this.btn_Pause.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Bold);
                isDataReceivedSubscribed = false;
            }
            else
            {
                try
                {
                    ComPort.DiscardInBuffer();
                    ComPort.DataReceived += new SerialDataReceivedEventHandler(ComPort_DataReceived);
                    this.btn_Pause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
                    this.btn_Pause.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Regular);
                    isDataReceivedSubscribed = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("暫停失敗： " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        #endregion

        #region Form 事件

        /// <summary>
        /// 視窗與控件大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmResize(object sender, EventArgs e)
        {

            this.rtb_serialdata.Width = this.Size.Width - 25;
            this.tb_send.Width = this.Size.Width - btn_send.Size.Width - 30;
            cb_autoscroll.Location = new System.Drawing.Point(7, this.Height - 76);
            cbk_FilterASI.Location = new System.Drawing.Point(cb_autoscroll.Right + 5, cb_autoscroll.Top);
            cmb_eol.Location = new System.Drawing.Point(this.Width - 25 - cmb_eol.Width, cb_autoscroll.Top - 2);
            lbl_eol.Location = new System.Drawing.Point(cmb_eol.Left - 5 - lbl_eol.Width, cmb_eol.Top + 2);
            this.rtb_serialdata.Height = cb_autoscroll.Top - tb_send.Bottom - 10;

        }

        /// <summary>
        /// 載入視窗時執行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_Load(object sender, EventArgs e)
        {
            ComPort.DataReceived += new SerialDataReceivedEventHandler(ComPort_DataReceived);
            isDataReceivedSubscribed = true;
        }

        /// <summary>
        /// 關閉此專案時執行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ComPort != null || ComPort.IsOpen)
            {
                ComPort.DataReceived -= ComPort_DataReceived;
                if (SerialFormClosing != null)
                {
                    SerialFormClosing?.Invoke(this, e);
                }
            }
            else
            {
                MessageBox.Show("關閉視窗會出錯", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        /*
        public void SetControlText(Control control, string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<Control, string>(SetControlText), new object[] { control, text });
            }
            else
            {
                control.Text = text;
            }
        }*/
        //舊的DataReceived
        //private void ComPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{

        //    if (rtb_serialdata.IsDisposed || rtb_serialdata.Disposing)
        //    {
        //        return;
        //    }

        //    if (ComPort.IsOpen == true)
        //    {
        //        while (ComPort.BytesToRead > 0)
        //        {

        //            //Add Byte read to idata array
        //            byte inByte = (byte)ComPort.ReadByte();
        //            CurrentLine.Add(inByte);

        //            //See If we reached an End of Line (Carriage Return + New Line), then see if its ASI data or Test Data
        //            //可自定義結尾符，通常情況為兩者(回車+換行)都有，因此資料型式為 "data + \t + \n"，每一個元素均為 1 byte
        //            if (CurrentLine.Count() > 2)
        //            {
        //                if ((CurrentLine[CurrentLine.Count - 1] == (byte)'\n') && (CurrentLine[CurrentLine.Count - 2] == (byte)'\r'))
        //                {
        //                    //bool isASIData = false;

        //                    ////Reached End of Line, is it ASI data?
        //                    //if (CurrentLine.Count() > 5)  
        //                    //{
        //                    //    if ((CurrentLine[0] == (byte)'#') && (CurrentLine[1] == (byte)'A') && (CurrentLine[2] == (byte)'S') && (CurrentLine[3] == (byte)'I') && (CurrentLine[4] == (byte)':'))
        //                    //    {
        //                    //        isASIData = true;
        //                    //    }
        //                    //}

        //                    //Process the Data
        //                    //if (isASIData)
        //                    //{
        //                    //    ASIData.Add(CurrentLine);

        //                    //    //If not filtering out ASI messages then add to the text box.
        //                    //    if (!(cbk_FilterASI.Checked))
        //                    //    {
        //                    //        rtb_serialdata.Invoke(new Action(() =>
        //                    //        {
        //                    //            //Convert Data to a String
        //                    //            ASCIIEncoding encoding = new ASCIIEncoding();
        //                    //            string StrData = encoding.GetString(CurrentLine.ToArray());

        //                    //            //Add it to the Text Box
        //                    //            rtb_serialdata.AppendText(StrData);
        //                    //            if (cb_autoscroll.Checked)
        //                    //            {
        //                    //                rtb_serialdata.ScrollToCaret();
        //                    //            }
        //                    //        }));
        //                    //    }

        //                    //    //Process the ASI data
        //                    //    ASI.ProcessMessage(CurrentLine);

        //                    //    //Clear Current Line Data
        //                    //    CurrentLine.Clear();
        //                    //}
        //                    //else
        //                    //{

        //                    //Convert Data to a String
        //                    ASCIIEncoding encoding = new ASCIIEncoding();
        //                    string StrData = encoding.GetString(CurrentLine.ToArray());

        //                    //Add it to the Text Data List
        //                    //TextData.Add(StrData);

        //                    //Add it to the Text Box
        //                    //將任務載入UI介面受限於"相同執行緒"的條件，以Invoke外掛程序給UI執行緒比較保險

        //                    if (!rtb_serialdata.IsDisposed && InvokeRequired)
        //                    {
        //                        //try
        //                        //{
        //                        if (rtb_serialdata.IsHandleCreated)
        //                        {
        //                            Invoke(new Action(() =>
        //                            {
        //                                rtb_serialdata.AppendText(StrData);
        //                                if (cb_autoscroll.Checked)
        //                                {
        //                                    rtb_serialdata.ScrollToCaret();
        //                                }
        //                            }));
        //                        }
        //                        //}
        //                        //catch
        //                        //{
        //                        //    // MessageBox.Show("SerialClient關閉", "通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                        //}

        //                    }

        //                    //Clear Current Line Data
        //                    CurrentLine.Clear();

        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 串口資料流事件任務
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ComPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (ComPort.IsOpen)
            {
                if (ComPort.BytesToRead > 0)
                {
                    try
                    {
                        string buf = ComPort.ReadExisting();

                        //判斷是否正在執行顯示
                        if (!isProcessing)
                        {
                            isProcessing = true;
                            Task.Run(() =>
                            {
                                ProcessReceivedData(buf, dataflow);
                            });

                        }
                    }
                    catch { MessageBox.Show("讀取不到緩存資料，可能鮑率誤選", "SerialClient開啟失敗", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                }
                else { return; }

            }

        }

        /// <summary>
        /// 處理擷取到的資料流
        /// </summary>
        /// <param name="buf"></param>
        private void ProcessReceivedData(string buf, float[] dataArray)
        {
            string[] lines = buf.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i=0; i < lines.Length; i++)
            {
                string line = lines[i];

                AppendTextToUI(line);
                SaveDataToTxtFile(line);
                if (isOpenSerialPlotter)
                {
                    StringToDoubleData(line, dataArray);
                }
                
            }

            isProcessing = false;
        }

        /// <summary>
        /// 儲存資料流上擷取到的數據
        /// </summary>
        /// <param name="line"></param>
        private void SaveDataToTxtFile(string line)
        {
            if (Recordertxt != null && !Recordertxt.IsDisposed && Recordertxt.RecordStatus)
            {
                Recordertxt.SaveData2TxtFile(line + Environment.NewLine);
            }
        }

        /// <summary>
        /// 顯示數據至Richbox上
        /// </summary>
        /// <param name="line"></param>
        private void AppendTextToUI(string line)
        {
            if (rtb_serialdata.InvokeRequired)
            {
                rtb_serialdata.Invoke(new Action<string>(AppendTextToUI), line);
            }
            else
            {
                rtb_serialdata.AppendText(line + Environment.NewLine);

                if (cb_autoscroll.Checked)
                {
                    rtb_serialdata.ScrollToCaret();
                }
            }
        }

        private void StringToDoubleData(String line, float[] dataArray)
        {
            string[] splitData = line.Split(',');

            if (splitData.Length != dataArray.Length) // **此處需數量正確, 最後一筆時間數據別忘了算進去**
            {
                return;
            }

            // 感測數據
            for (int j = 0; j < dataArray.Length; j++)
            {
                if (float.TryParse(splitData[j], out float value))
                {
                    dataArray[j] = value;
                }
                else
                {
                    dataArray[j] = 0.0f; // 轉換失敗，給出默認值
                }
            }

        }

        private void UpdateData(Object state)
        {
            float[] data = (float[])state; // 從 state 參數中提取 float[] 變量

            Single TimeSec = (Single)DB.GetTimeSec();

            int lck = DB.requestLock();
            if (lck >= 0)
            {
                //如果buffer內有存值，優先寫入資料庫裡
                if (Buffer[Buffer.Keys.First()].Count > 0)
                {
                    foreach (String key in Buffer.Keys)
                    {
                        DB.Write(key, Buffer[key]);
                        Buffer[key].Clear();
                    }
                }

                //Now write new data
                DB.Write("Accelerometer_X", TimeSec, data[0]);
                DB.Write("Accelerometer_Y", TimeSec, data[1]);
                DB.Write("Accelerometer_Z", TimeSec, data[2]);
                DB.Write("AngularVelocity_X", TimeSec, data[3]);
                DB.Write("AngularVelocity_Y", TimeSec, data[4]);
                DB.Write("AngularVelocity_Z", TimeSec, data[5]);
                DB.Write("Quaternion_W", TimeSec, data[6]);
                DB.Write("Quaternion_X", TimeSec, data[7]);
                DB.Write("Quaternion_Y", TimeSec, data[8]);
                DB.Write("Quaternion_Z", TimeSec, data[9]);
                DB.Write("Displacement_X", TimeSec, data[10]);
                DB.Write("Displacement_Y", TimeSec, data[11]);

                DB.returnLock(lck);
            }
            else
            {
                //Write data to buffer couldnt get a lock
                Buffer["Accelerometer_X"].Add(new Tuple<Single, Single>(TimeSec, data[0]));
                Buffer["Accelerometer_Y"].Add(new Tuple<Single, Single>(TimeSec, data[1]));
                Buffer["Accelerometer_Z"].Add(new Tuple<Single, Single>(TimeSec, data[2]));
                Buffer["AngularVelocity_X"].Add(new Tuple<Single, Single>(TimeSec, data[3]));
                Buffer["AngularVelocity_Y"].Add(new Tuple<Single, Single>(TimeSec, data[4]));
                Buffer["AngularVelocity_Z"].Add(new Tuple<Single, Single>(TimeSec, data[5]));
                Buffer["Quaternion_W"].Add(new Tuple<Single, Single>(TimeSec, data[6]));
                Buffer["Quaternion_X"].Add(new Tuple<Single, Single>(TimeSec, data[7]));
                Buffer["Quaternion_Y"].Add(new Tuple<Single, Single>(TimeSec, data[8]));
                Buffer["Quaternion_Z"].Add(new Tuple<Single, Single>(TimeSec, data[9]));
                Buffer["Displacement_X"].Add(new Tuple<Single, Single>(TimeSec, data[10]));
                Buffer["Displacement_Y"].Add(new Tuple<Single, Single>(TimeSec, data[11]));
            }

        }

    }
}
