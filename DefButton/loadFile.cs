using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace DefButton
{
    public  class loadFile
    {
        public void loadTxt(ref string url)
        {
            //打開PCD檔的點雲資料
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "選擇點雲檔案";
            ofd.InitialDirectory = @"\";
            ofd.Filter = "TXT 檔 (*.txt)|*.txt|所有檔案 (*.*)|*.*";
            ofd.ShowDialog();
            url = ofd.FileName;
        }


        public  void loadPcd(ref string url)
        {
            //打開PCD檔的點雲資料
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "選擇點雲檔案";
            ofd.InitialDirectory = @"\";
            ofd.Filter = "PCD 檔 (*.pcd)|*.pcd|所有檔案 (*.*)|*.*";
            ofd.ShowDialog();
            url = ofd.FileName;
        }

        public  void loadStl(ref string url)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "請選擇stl檔案";
            ofd.InitialDirectory = @"\";
            ofd.Filter = "*.stl";
            ofd.ShowDialog();
            url = ofd.FileName;
        }

        public  void loadPly(ref string url)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "請選擇PLY檔案";
            ofd.InitialDirectory = @"\";
            ofd.Filter = "PLY 檔 (*.ply)|*.ply|所有檔案 (*.*)|*.*";
            ofd.ShowDialog();
            url = ofd.FileName;
        }

        public  void loadObj(ref string url)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "請選擇OBJ檔案";
            ofd.InitialDirectory = @"\";
            ofd.Filter = "OBJ 檔 (*.obj)|*.obj|所有檔案 (*.*)|*.*";
            ofd.ShowDialog();
            url = ofd.FileName;

        }

    }
}
