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
using System.Drawing.Imaging;
using System.Runtime.InteropServices;



namespace _3D_Scan_software
{
    public partial class FrameCapture : Form
    {

        private SerialPort myPort;

        //儲存影像數據
        private StringBuilder receivedData = new StringBuilder();

        //晶片規格變量
        private int squal;
        private int pixelSUM;
        private int fr;
        private int lift;
        private int framePeriod_Max;
        private int framePeriod_Min;
        private int framePeriod_Shutter;
        int brightness = 0;
        private String Squal;
        private String PixelSUM;
        private String FR;
        private String Lift;
        private String FramePeriod;

        // 分析變量
        public const int AvarageNum = 50;
        int[] AvaPixSum = new int[AvarageNum + 1];  // 為了避免數據第一筆的錯誤，所以加一保持平均為50筆
        int[] AvaSqual = new int[AvarageNum + 1];
        int index = 0;
        int MaxAmplitude = 0;

        //委派事件
        public event EventHandler FrameCapFormClosing;

        public FrameCapture(SerialPort port)
        {
            InitializeComponent();
            this.myPort = port;
            myPort.DataReceived += new SerialDataReceivedEventHandler(MyPort_DataReceived);

        }

        private void FrameCapture_Load(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                if (!this.IsDisposed)
                {
                    this.lbl_AvgPixSum.ForeColor = System.Drawing.Color.DarkSlateGray;
                    this.lbl_AvgSqual.ForeColor = System.Drawing.Color.DarkSlateGray;
                    this.lbl_MaxAmplitude.ForeColor = System.Drawing.Color.DarkSlateGray;

                }
            }));
        }

        private void MyPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (myPort.BytesToRead > 0 && myPort.IsOpen)
            {
                //  讀取數據
                string rawData = myPort.ReadExisting();

                //  影像數據附加至receivedData
                receivedData.Append(rawData);

                //  數據處理並顯示
                ProcessReceivedData();
            }
        }

        private void ProcessReceivedData()
        {
            string data = receivedData.ToString();

            int endIndex = data.LastIndexOf('\n');
            if (endIndex >= 0)
            {
                string[] lines = data.Substring(0, endIndex).Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string line in lines)
                {
                    String[] dataArr = line.Trim().Split(' ');  //  切割字串

                    if (dataArr.Length == 900)
                    {
                        try
                        {
                            byte[] pix = new byte[900];

                            for (int i = 0; i < 900; i++)
                            {
                                if (byte.TryParse(dataArr[i].Trim(), out byte value))
                                {
                                    pix[i] = value;
                                }
                                else
                                {
                                    // 轉換失敗，記錄錯誤信息或進行相應處理
                                    Console.WriteLine("數據轉換錯誤: " + dataArr[i].Trim());
                                }
                            }

                            if (InvokeRequired)
                            {
                                pictureBox_FramePix.Invoke(new Action<byte[]>(DrawImage), pix);
                            }
                            else
                            {
                                DrawImage(pix);
                            }
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show("數據處理錯誤: " + ex.Message);
                        }
                    }
                    else
                    {
                        string[] tokens = line.Split('#');

                        foreach (string token in tokens)
                        {
                            string[] parts = token.Split(':');

                            if (parts.Length == 2)
                            {
                                string key = parts[0].Trim();
                                string value = parts[1].Trim();

                                switch (key)
                                {
                                    case "Squal":
                                        if (int.TryParse(value, out int squalValue))
                                        {
                                            Squal = key;
                                            squal = squalValue;
                                        }
                                        break;
                                    case "PixelSUM":
                                        if (int.TryParse(value, out int pixelSUMValue))
                                        {
                                            PixelSUM = key;
                                            pixelSUM = pixelSUMValue;
                                        }
                                        break;
                                    case "FrameRate":
                                        if (int.TryParse(value, out int frValue))
                                        {
                                            FR = key;
                                            fr = frValue;
                                        }
                                        break;
                                    case "Lift_Detec":
                                        if (int.TryParse(value, out int liftValue))
                                        {
                                            Lift = key;
                                            lift = liftValue;
                                        }
                                        break;
                                    case "Frame_period(Max/Min/Shutter)":
                                        FramePeriod = key;
                                        string[] FP = value.Split(',');
                                        int Max_Bound, Min_Bound, Shutter;
                                        if (int.TryParse(FP[0], out Max_Bound) && int.TryParse(FP[1], out Min_Bound) && int.TryParse(FP[2], out Shutter))
                                        {
                                            framePeriod_Max = Max_Bound;
                                            framePeriod_Min = Min_Bound;
                                            framePeriod_Shutter = Shutter;
                                        }
                                        break;
                                    case "Brightness_updated":
                                        if (int.TryParse(value, out int Laser_intensity))
                                        {
                                            brightness = Laser_intensity;
                                            
                                            Invoke(new Action(() =>
                                            {
                                                if (!this.IsDisposed)
                                                {
                                                    this.lbl_AvgPixSum.ForeColor = System.Drawing.Color.DarkSlateGray;
                                                    this.lbl_AvgSqual.ForeColor = System.Drawing.Color.DarkSlateGray;
                                                    this.lbl_MaxAmplitude.ForeColor = System.Drawing.Color.DarkSlateGray;

                                                }
                                            }));
                                            index = 0;
                                        }   
                                        break;
                                }
                            }
                        }
                        if(index != AvarageNum + 1)
                        {
                            CalAvarage();
                        }
                        // 更新標籤上的數據
                        UpdateDataLabels();

                    }
                }

                // 清除已處理的數據
                receivedData.Remove(0, endIndex + 1);
            }
        }

        /// <summary>
        /// 更新晶片規格的顯示數據
        /// </summary>
        private void UpdateDataLabels()
        {
            if (InvokeRequired)
            {
                if (!this.IsDisposed && this != null)
                {
                    Invoke(new Action(() =>
                    { 
                        try
                        {
                            if (this != null && !this.IsDisposed)
                            {
                                lbl_name1.Text = Squal;
                                lbl_name2.Text = PixelSUM;
                                lbl_name3.Text = FR;
                                lbl_name4.Text = Lift;
                                lbl_name5.Text = FramePeriod;
                                lbl_Value1.Text = squal.ToString();
                                lbl_Value2.Text = pixelSUM.ToString();
                                lbl_Value3.Text = fr.ToString();    
                                lbl_Value4.Text = lift.ToString();
                                lbl_Value5.Text = framePeriod_Max.ToString() + " / " + framePeriod_Min.ToString() + " / " + framePeriod_Shutter.ToString();
                                lbl_brightnessNum.Text = brightness.ToString();
                            }
                            
                        }
                        catch { };
                    }));
                }
            }
            else
            {
                try
                {
                    lbl_name1.Text = Squal;
                    lbl_name2.Text = PixelSUM;
                    lbl_name3.Text = FR;
                    lbl_name4.Text = Lift;
                    lbl_name5.Text = FramePeriod;
                    lbl_Value1.Text = squal.ToString();
                    lbl_Value2.Text = pixelSUM.ToString();
                    lbl_Value3.Text = fr.ToString();
                    lbl_Value4.Text = lift.ToString();
                    lbl_Value5.Text = framePeriod_Max.ToString() + " / " + framePeriod_Min.ToString() + " / " + framePeriod_Shutter.ToString();
                    lbl_brightnessNum.Text = brightness.ToString();
                }
                catch { };
            }
        }

        /// <summary>
        /// 繪製灰度點陣圖
        /// </summary>
        /// <param name="pix_"></param>
        private void DrawImage(byte[] pix_)
        {
            Bitmap bitmap = new Bitmap(600, 600);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            int index = 0;
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    byte colorScale = pix_[index];
                    Color grayColor = Color.FromArgb(colorScale, colorScale, colorScale);
                    SolidBrush brush = new SolidBrush(grayColor);   //  塗鴉筆參數設定
                    graphics.FillRectangle(brush, i * 20  , 600 - (j * 20 + 20), 20, 20);
                    index++;
                }
            }

            pictureBox_FramePix.Image = bitmap;
        }

        /// <summary>
        /// 表單關閉時執行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameCapture_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myPort != null && myPort.IsOpen)
            {
                myPort.DataReceived -= MyPort_DataReceived;
                myPort.DiscardInBuffer();  // 清空串口接收緩衝區
                if (FrameCapFormClosing != null)
                    FrameCapFormClosing?.Invoke(this, e);

            }
        }

        private void CalAvarage()
        {
            // 將當前的Squal和PixelSUM存入對應的陣列
            if(index > 0)
            {
                AvaPixSum[index] = pixelSUM;
                AvaSqual[index] = squal;
            }

            // 確保index在合理範圍內
            if (index < AvarageNum)
            {
                index++;
            }
            else
            {
                // 計算AvaPixSum和AvaSqual的平均值
                int avgPixSum = AvaPixSum.Sum() / AvarageNum;
                int avgSqual = AvaSqual.Sum() / AvarageNum;

                //取得最大振幅
               int MaxApli =  CalMaxAmplitude(AvaPixSum, avgPixSum);

                // 使用平均值更新Squal和PixelSUM
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        if (!this.IsDisposed)
                        {
                            lbl_AvgSqual.Text = avgSqual.ToString();
                            lbl_AvgPixSum.Text = avgPixSum.ToString();
                            lbl_MaxAmplitude.Text = MaxApli.ToString();
                            this.lbl_AvgPixSum.ForeColor = System.Drawing.Color.White;
                            this.lbl_AvgSqual.ForeColor = System.Drawing.Color.White;
                            this.lbl_MaxAmplitude.ForeColor = System.Drawing.Color.White;
                        }


                    }));
                }
                else
                {
                    lbl_AvgSqual.Text = avgSqual.ToString();
                    lbl_AvgPixSum.Text = avgPixSum.ToString();
                    lbl_MaxAmplitude.Text = MaxApli.ToString();
                    this.lbl_AvgPixSum.ForeColor = System.Drawing.Color.White;
                    this.lbl_AvgSqual.ForeColor = System.Drawing.Color.White;
                    this.lbl_MaxAmplitude.ForeColor = System.Drawing.Color.White;
                }

                index++;
            }
        }

        /// <summary>
        /// 求最大振幅
        /// </summary>
        /// <param name="data"></param>
        /// <param name="avg"></param>
        /// <returns></returns>
        private int CalMaxAmplitude(int[] data, int avg)
        {
            MaxAmplitude = 0;

            foreach (int value in data)
            {
                int amplitude = Math.Abs(value - avg);
                if (amplitude > MaxAmplitude && amplitude < 15)
                {
                    MaxAmplitude = amplitude;
                }
            }
            return MaxAmplitude;
        }

        /// <summary>
        /// 點擊 lbl_Value2 即可重新計算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_Value2_Click(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                if (!this.IsDisposed)
                {
                    this.lbl_AvgPixSum.ForeColor = System.Drawing.Color.DarkSlateGray;
                    this.lbl_AvgSqual.ForeColor = System.Drawing.Color.DarkSlateGray;
                    this.lbl_MaxAmplitude.ForeColor = System.Drawing.Color.DarkSlateGray;
                }
            }));
            index = 0;
        }

        private void lbl_Value1_Click(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                if (!this.IsDisposed)
                {
                    this.lbl_AvgSqual.ForeColor = System.Drawing.Color.DarkSlateGray;
                    this.lbl_AvgPixSum.ForeColor = System.Drawing.Color.DarkSlateGray;
                    this.lbl_MaxAmplitude.ForeColor = System.Drawing.Color.DarkSlateGray;
                }
            }));
            index = 0;
        }

        private void lbl_MaxAmplitude_Click(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                if (!this.IsDisposed)
                {
                    this.lbl_MaxAmplitude.ForeColor = System.Drawing.Color.DarkSlateGray;
                    this.lbl_AvgPixSum.ForeColor = System.Drawing.Color.DarkSlateGray;
                    this.lbl_AvgSqual.ForeColor = System.Drawing.Color.DarkSlateGray;
                }
            }));
            index = 0;
        }
    }
}
