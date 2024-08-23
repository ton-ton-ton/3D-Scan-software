using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace SerialClient
{
    public partial class DataRecord2txt : Form
    {
        private string dataLog_path = "../../../DataLog/";
        private StreamWriter dataLog;
        uint pcNumbers = 0;
        bool _recordStatus = false;
        private Stopwatch SW = new Stopwatch();
        private bool isRunning = false;
        //設定timer
        System.Threading.Timer updateTimer;
        AutoResetEvent autoEvent = new AutoResetEvent(false);
        


        public bool RecordStatus 
        { 
            get { return _recordStatus;}
            set { _recordStatus = value;}
        }


        public DataRecord2txt()
        {
            InitializeComponent();
            SW.Reset();

            //固定大小
            this.MaximumSize = new Size(this.Width, this.Height);


        }

        private void TimerUpdate(Object stateInfo)
        {
            if (isRunning)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        var updateData = (SW.Elapsed.ToString(@"mm\:ss"), pcNumbers);
                        Update(updateData); //  更新顯示的運行時間與點雲數量
                    }));
                }
                else
                {
                    setClockText(SW.Elapsed.ToString(@"mm\:ss"));
                    setPointCloudNum(pcNumbers);
                }
            }
        }

        private void Update((string time, uint num) data)
        {
            setClockText(data.time);
            setPointCloudNum(data.num);
        }

        private void setClockText(string value)
        {
            if (TimeClock_lbl.InvokeRequired)
            {
                TimeClock_lbl.Invoke(new Action<string>(setClockText), new object[] { value });
            }
            else
            {
                TimeClock_lbl.Text = value;
            }
        }

        private void setPointCloudNum(uint num)
        {
            if (PCNumber_lbl.InvokeRequired)
            {
                PCNumber_lbl.Invoke(new Action<uint>(setPointCloudNum), new object[] { num });
            }
            else
            {
                PCNumber_lbl.Text = num.ToString();
            }
        }

        public void SaveData2TxtFile(String serialLine)
        {
            try
            {
                dataLog.Write(serialLine);

                // 獲取點雲數
                //pcNumbers += (uint)serialLine.ToString().Split('\n').Length;
                pcNumbers++;
            }
            catch { }
        }

        private void btn_Record_Click(object sender, EventArgs e)
        {
            if(!isRunning)
            {
                TimerCallback tcb = TimerUpdate;
                updateTimer = new System.Threading.Timer(tcb, autoEvent, 1000, 1000);   //  表單畫面更新頻率 1 Hz

                dataLog = new StreamWriter(dataLog_path + DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss_f") + ".csv");                
                _recordStatus = true;
                SW.Start();
                btn_Record.Text = "Stop and Save";
                isRunning = true;
            }
            else
            {
                dataLog.Close();
                updateTimer?.Dispose();
                _recordStatus = false;
                SW.Stop();
                btn_Record.Text = "Record";
                isRunning = false;
                pcNumbers = 0;
                setPointCloudNum(pcNumbers);
                setClockText("00:00");
                SW.Reset();

            }
        }
    }
}
