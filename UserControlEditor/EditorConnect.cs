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
using FontAwesome.Sharp;

namespace UserControlEditor
{
    public partial class EditorConnect : UserControl
    {
        //================連線宣告================

        SerialPort ComPort = new SerialPort();

        //  連線狀態
        public enum EnumComPortStatus { NotConnected, Connected, Disconnected, ConnectionFailed }
        public EnumComPortStatus ComPortStatus;

        //  鮑率選擇
        object[] BaudRate = new object[10]
        { 9600,28800,38400,57600,115200,128000,250000,500000,1000000, 2000000};

        //  屬性
        public SerialPort COM { get; set; }


        //  委派事件
        //public event EventHandler ConnectEvent;
        public event EventHandler ComPortStatusChanged;


        public EditorConnect()
        {
            InitializeComponent();
            COM = ComPort;


            //===============連線功能定義=================
            iconBtnConnect.Enabled = false; //連線按鈕失效
            ComPortStatus = EnumComPortStatus.NotConnected;
            comboBoxBaudRate.Items.AddRange(BaudRate);

            comboBoxCOM.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxBaudRate.DropDownStyle = ComboBoxStyle.DropDownList;

            // 預設串口連線對象
            comboBoxCOM.Items.Clear();
            string[] port = SerialPort.GetPortNames();
            comboBoxCOM.Items.AddRange(port);
            if (port.Contains("COM10"))
            {
                comboBoxCOM.Text = "COM10";  // 設定PortName
                comboBoxBaudRate.Text = "2000000";
            }
        }

        private void comboBoxCOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBoxBaudRate.SelectedIndex >= 0) && (comboBoxCOM.SelectedIndex >= 0))
            {
                iconBtnConnect.Enabled = true;
            }
            else
            {
                iconBtnConnect.Enabled = false;
            }

        }

        private void comboBoxBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBoxBaudRate.SelectedIndex >= 0) && (comboBoxCOM.SelectedIndex >= 0))
            {
                iconBtnConnect.Enabled = true;
            }
            else
            {
                iconBtnConnect.Enabled = false;
            }

        }

        private void iconBtnConnect_Click(object sender, EventArgs e)
        {
            //如委任方法有被繼承，則進入條件式內執行
            if (ComPortStatusChanged != null)
            {

                if (ComPortStatus == EnumComPortStatus.Connected)
                {
                    ComPort.Close();
                    ComPort.Dispose();
                    comboBoxCOM.Enabled = true;
                    comboBoxBaudRate.Enabled = true;
                    iconBtnConnect.Text = "Connect";
                    ComPortStatus = EnumComPortStatus.Disconnected;
                    iconBtnConnectStatus.IconColor = Color.DimGray;

                }

                else
                {
                    try
                    {
                        ComPort.PortName = comboBoxCOM.Text;
                        ComPort.BaudRate = Convert.ToInt32(comboBoxBaudRate.Text);
                        ComPort.DataBits = 8;
                        ComPort.StopBits = StopBits.One;
                        COM = ComPort;

                        ComPort.Open();
                        ComPortStatus = EnumComPortStatus.Connected;
                        comboBoxCOM.Enabled = false;
                        comboBoxBaudRate.Enabled = false;
                        iconBtnConnect.Text = "Disconnect";
                        this.iconBtnConnectStatus.IconColor = Color.Lime;
                    }
                    catch(Exception ex)
                    {
                        this.iconBtnConnectStatus.IconColor = Color.Red;
                        ComPortStatus = EnumComPortStatus.ConnectionFailed;
                        MessageBox.Show("鮑率選擇錯誤，請重新選擇！" + '\n'+ ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
                ComPortStatusChanged?.Invoke(this, e);  //判斷當前是否在UI執行緒上，如果不是就用invoke，避免跨執行緒異常
            }



        }


        private void comboBoxCOM_DropDown(object sender, EventArgs e)
        {
                comboBoxCOM.Items.Clear();
                string[] port = SerialPort.GetPortNames();
                comboBoxCOM.Items.AddRange(port);

        }

        private void EditorConnect_Load(object sender, EventArgs e)
        {
            ////如委任方法有被繼承，則進入條件式內執行
            //if (ComPortStatusChanged != null)
            //{
            //    ComPortStatusChanged?.Invoke(this, e);  //判斷當前是否在UI執行緒上，如果不是就用invoke，避免跨執行緒異常
            //}
        }
    }
}
