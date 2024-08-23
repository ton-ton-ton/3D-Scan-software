using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PclCSharp;
using PointCloudSharp;

namespace UserControlEditor
{
    public partial class SubMenu1_Scan : UserControl
    {

        PanelControl panelControl;

        //創建委任事件
        public event EventHandler MinimezedClick;
        public event EventHandler ScanStartClick;
        public event EventHandler ScanPauseClick;
        public event EventHandler ScanResetClick;
        public event EventHandler ScalarBarCheck;
        public event EventHandler SensorCtrClick;
        public event EventHandler RestartSensorClick;
        public event EventHandler liftDetecUPClick;
        public event EventHandler liftDetecDownClick;
        public event EventHandler ShutterUPClick;
        public event EventHandler ShutterDownClick;
        public event EventHandler MinBoundUPClick;
        public event EventHandler MinBoundDownClick;
        public event EventHandler CPIChanged;
        public event EventHandler LASERIntensityChanged;
        public event EventHandler DimentionChanged;
        public event EventHandler SaveandResetClick;



        //屬性
        public bool ShowStatus { get; set; }

        public bool SensorOperatingStatus { get; set; }

        public SubMenu1_Scan()
        {
            InitializeComponent();
            SensorOperatingStatus = false;

            comboBox_ScanMode.DropDownStyle = ComboBoxStyle.DropDownList;
            string[] ScanmodeItems = new string[] { "3D", "2D", "Tracking" };   //  Tracking、2D、3D，
            comboBox_ScanMode.Items.AddRange(ScanmodeItems);
            this.comboBox_ScanMode.Text = "2D";

        }

        /// <summary>
        /// 頁面隱藏與顯示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected  void BtnMinimizwEditor_Click(object sender, EventArgs e)
        {
            //如委任方法有被繼承，則進入條件式內執行
            if (MinimezedClick != null)
            {
                MinimezedClick?.Invoke(this, e);  //判斷當前是否在UI執行緒上，如果不是就用invoke，避免跨執行緒異常
            }
        }

        protected void BtnScanStart_Click(object sender, EventArgs e)
        {
            if(ScanStartClick != null)
            {
                ScanStartClick?.Invoke(this, e);
            }
        }

        /// <summary>
        /// 子頁面1 隱藏與顯示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconButton1_Click(object sender, EventArgs e)
        {
            panelControl = new PanelControl();
            panelControl.ShowSubMenuPanel(panelSubconnect);
        }

        /// <summary>
        /// 子頁面2 隱藏與顯示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconButton2_Click(object sender, EventArgs e)
        {
            
            panelControl = new PanelControl();
            panelControl.ShowSubMenuPanel(panelSubCalibration);
        }

        private void iconbtn_Pause_Click(object sender, EventArgs e)
        {
            if (ScanPauseClick != null)
            {
                ScanPauseClick?.Invoke(this, e);
            }
        }

        private void iconBtn_Reset_Click(object sender, EventArgs e)
        {
            if (ScanResetClick != null)
            {
                ScanResetClick?.Invoke(this, e);
            }
        }

        private void ScalarBar_CheckedChanged(object sender, EventArgs e)
        {
            if (ScalarBarCheck != null)
            {
                ScalarBarCheck?.Invoke(this, e);
            }
        }

        private void iconBtn_SensorMidBtn_Click(object sender, EventArgs e)
        {
            if (SensorCtrClick != null)
            {
                SensorCtrClick?.Invoke(this, e);
            }
            if(SensorOperatingStatus != true)
            {
                this.iconBtn_SensorMidBtn.Text = "Turn OFF";
                this.iconBtn_SensorMidBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
                SensorOperatingStatus = true;
            }
            else
            {
                this.iconBtn_SensorMidBtn.Text = "Turn On";
                this.iconBtn_SensorMidBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
                SensorOperatingStatus = false;
            }


        }
        private void iconBtn_RestartSensor_Click(object sender, EventArgs e)
        {
            if (RestartSensorClick != null)
            {
                RestartSensorClick?.Invoke(this, e);
            }
        }

        private void iconBtn_liftDetecUP_Click(object sender, EventArgs e)
        {
            if (liftDetecUPClick != null)
            {
                liftDetecUPClick?.Invoke(this, e);
            }
        }

        private void iconBtn_liftDetecDown_Click(object sender, EventArgs e)
        {
            if (liftDetecDownClick != null)
            {
                liftDetecDownClick?.Invoke(this, e);
            }
        }

        private void iconBtn_ShutterUP_Click(object sender, EventArgs e)
        {
            if (ShutterUPClick != null)
            {
                ShutterUPClick?.Invoke(this, e);
            }
        }

        private void iconBtn_ShutterDown_Click(object sender, EventArgs e)
        {
            if (ShutterDownClick != null)
            {
                ShutterDownClick?.Invoke(this, e);
            }
        }

        private void iconBtn_MinBoundUP_Click(object sender, EventArgs e)
        {
            if (MinBoundUPClick != null)
            {
                MinBoundUPClick?.Invoke(this, e);
            }
        }

        private void iconBtn_MinBoundDown_Click(object sender, EventArgs e)
        {
            if (MinBoundDownClick != null)
            {
                MinBoundDownClick?.Invoke(this, e);
            }
        }

        private void trackBar_LASERIntensity_ValueChanged(object sender, EventArgs e)
        {
            if (LASERIntensityChanged != null)
            {
                LASERIntensityChanged?.Invoke(this, e);
            }
        }

        private void trackBar_LASERIntensity_MouseUp(object sender, MouseEventArgs e)
        {
            if (CPIChanged != null)
            {
                CPIChanged?.Invoke(this, e);
            }
        }

        private void comboBox_ScanMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DimentionChanged != null)
            {
                DimentionChanged?.Invoke(this, e);
            }
        }

        private void iconButton2_Click_1(object sender, EventArgs e)
        {
            if (SaveandResetClick != null)
            {
                SaveandResetClick?.Invoke(this, e);
            }
        }
    }
}
