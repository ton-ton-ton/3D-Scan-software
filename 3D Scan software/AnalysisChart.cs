using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp; 

namespace _3D_Scan_software
{
    public partial class AnalysisChart : Form
    {
        private string SavePath = "..\\..\\..\\AnalysisData/";
        //int Storagednum = 0;

        public int Storagednum { get; set; } = 0;

        public AnalysisChart()
        {
            InitializeComponent();
        }

        private void AnalysisChart_Load(object sender, EventArgs e)
        {
     
        }


        public void SaveforAnalysis(object sender, EventArgs e)
        {
            // 取得當前按鈕的父控制項，即所在的 GroupBox
            GroupBox groupBox = (sender as Button).Parent as GroupBox;
            string groupName = groupBox.Text;   // 取得 GroupBox 的名稱
            String completePath = SavePath + groupName + ".csv";
            // 檢查檔案是否存在，若不存在則創建
            if (!File.Exists(completePath) && groupBox != null)
            {
                using (StreamWriter writer = File.CreateText(completePath)) { }
            }

            String alldata = "";
            foreach (Control control in groupBox.Controls)
            {
                if (control is TextBox textbox)
                {
                    string textboxText = textbox.Text; // 取得 Label 的文字
                    alldata = textboxText + "," + alldata;

                }
            }
            WriteToFile(alldata, completePath);
            Storagednum++;
            lbl_StoragedNum.Text = Storagednum.ToString();
        }


        private void WriteToFile(string  value, String filename)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(filename))
                {
                    writer.WriteLine(value);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("儲存失敗： " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbl_StoragedNum_Click(object sender, EventArgs e)
        {
            Storagednum = 0;
            lbl_StoragedNum.Text = Storagednum.ToString();
        }
    }
}
