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
    public partial class SubMenu1_Output : UserControl
    {
        PanelControl panelControl = new PanelControl();
        

        private string Output_path = "../../../Output File/";      //儲存相對路徑


        //創建委任事件
        public event EventHandler MinimezedClick;   //  頁面隱藏
        public event EventHandler PcdSaveClick;
        public event EventHandler TxtSaveClick;


        //屬性
        public bool SaveStatus { get; set; }

        public bool ShowStatus { get; set; }
        public PointCloudXYZ _pc { get; set;}

        public SubMenu1_Output()
        {
            InitializeComponent();
            SaveStatus = false;
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

        /// <summary>
        /// 子頁面1 隱藏與顯示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconButton1_Click(object sender, EventArgs e)
        {
            panelControl.ShowSubMenuPanel(panelSubconnect);
        }

        /// <summary>
        /// 子頁面2 隱藏與顯示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconButton2_Click(object sender, EventArgs e)
        {
            panelControl.ShowSubMenuPanel(panelSubCalibration);
        }

        private void iconbtn_PCD_Click(object sender, EventArgs e)
        {
            //如委任方法有被繼承，則進入條件式內執行
            if (PcdSaveClick != null)
            {
                PcdSaveClick?.Invoke(this, e);  //判斷當前是否在UI執行緒上，如果不是就用invoke，避免跨執行緒異常
            }
        }

        private void iconbtn_PLY_Click(object sender, EventArgs e)
        {
            if (SaveStatus != true)
            {
                try
                {
                    SaveStatus = true;
                    Io.savePlyFile(Output_path + "PLY", _pc.PointCloudXYZPointer, 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("儲存失敗: " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void iconbtn_TXT_Click(object sender, EventArgs e)
        {
            if (TxtSaveClick != null)
            {
                TxtSaveClick?.Invoke(this, e);  //判斷當前是否在UI執行緒上，如果不是就用invoke，避免跨執行緒異常
            }
        }
    }
}
