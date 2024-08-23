using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserControlEditor
{
    public class PanelControl
    {

        private bool editorStatus = true; // editorStatus = 1 代表狀態為開，editorStatus = 0 代表為關

        public void HideSubMenuPanel(Panel subMenu)
        {
            if (subMenu.Visible == true)
                subMenu.Visible = false;
        }

        public void ShowSubMenuPanel(Panel subMenu)
        {
            if (subMenu.Visible == false)
                subMenu.Visible = true;
            else subMenu.Visible = false;
        }

        public void ShowSubMenuControl(UserControl userconrtol)
        {
           if(!(editorStatus == true))
           {
                foreach (Control control in userconrtol.Controls)
                {
                    control.Visible = true;
                    editorStatus = true;
                }
           }
           else
           {
                foreach (Control control in userconrtol.Controls)
                {
                    control.Visible = false;
                    editorStatus = false;
                }
           }
        }

    }

    
}
