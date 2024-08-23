using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UserControlEditor
{
    public partial class SubMenu1_Setting : UserControl
    {
        PanelControl panelControl = new PanelControl();
        public bool ShowStatus { get; set; }
        //創建委任事件
        public event EventHandler MinimezedClick;

        

        public SubMenu1_Setting()
        {
            InitializeComponent();
            //ShowStatus = true;

    }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            panelControl.ShowSubMenuPanel(panelSubconnect);
            panelControl.ShowSubMenuControl(editorConnect); // 此處"EditorConnect"需依照SubMenu1_Setting.Designer.cs內已定義的實體化參考名稱

        }

        protected  void BtnMinimizwEditor_Click(object sender, EventArgs e)
        {
            
            //如委任方法有被繼承，則進入條件式內執行
            if (MinimezedClick != null)
            {
                MinimezedClick?.Invoke(this,e);  //判斷當前是否在UI執行緒上，如果不是就用invoke，避免跨執行緒異常
            }
              
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void iconBtnInertial_Click(object sender, EventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            panelControl.ShowSubMenuPanel(panelSubCalibration);
        }
    }
}

