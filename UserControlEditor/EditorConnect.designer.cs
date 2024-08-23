
namespace UserControlEditor
{
    partial class EditorConnect
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelCOM = new System.Windows.Forms.Label();
            this.labelBaud = new System.Windows.Forms.Label();
            this.iconBtnConnectStatus = new FontAwesome.Sharp.IconButton();
            this.iconBtnConnect = new FontAwesome.Sharp.IconButton();
            this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.comboBoxCOM = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelCOM
            // 
            this.labelCOM.AutoSize = true;
            this.labelCOM.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.labelCOM.ForeColor = System.Drawing.SystemColors.Window;
            this.labelCOM.Location = new System.Drawing.Point(40, 27);
            this.labelCOM.Margin = new System.Windows.Forms.Padding(0);
            this.labelCOM.Name = "labelCOM";
            this.labelCOM.Size = new System.Drawing.Size(42, 18);
            this.labelCOM.TabIndex = 1;
            this.labelCOM.Text = "COM";
            // 
            // labelBaud
            // 
            this.labelBaud.AutoSize = true;
            this.labelBaud.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.labelBaud.ForeColor = System.Drawing.SystemColors.Window;
            this.labelBaud.Location = new System.Drawing.Point(32, 82);
            this.labelBaud.Margin = new System.Windows.Forms.Padding(0);
            this.labelBaud.Name = "labelBaud";
            this.labelBaud.Size = new System.Drawing.Size(73, 18);
            this.labelBaud.TabIndex = 2;
            this.labelBaud.Text = "BandRate";
            // 
            // iconBtnConnectStatus
            // 
            this.iconBtnConnectStatus.Cursor = System.Windows.Forms.Cursors.No;
            this.iconBtnConnectStatus.FlatAppearance.BorderSize = 0;
            this.iconBtnConnectStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnConnectStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconBtnConnectStatus.IconChar = FontAwesome.Sharp.IconChar.NetworkWired;
            this.iconBtnConnectStatus.IconColor = System.Drawing.Color.DimGray;
            this.iconBtnConnectStatus.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtnConnectStatus.Location = new System.Drawing.Point(38, 119);
            this.iconBtnConnectStatus.Name = "iconBtnConnectStatus";
            this.iconBtnConnectStatus.Size = new System.Drawing.Size(47, 40);
            this.iconBtnConnectStatus.TabIndex = 9;
            this.iconBtnConnectStatus.Text = " ";
            this.iconBtnConnectStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconBtnConnectStatus.UseVisualStyleBackColor = true;
            // 
            // iconBtnConnect
            // 
            this.iconBtnConnect.FlatAppearance.BorderSize = 0;
            this.iconBtnConnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(105)))));
            this.iconBtnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnConnect.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.iconBtnConnect.ForeColor = System.Drawing.SystemColors.Window;
            this.iconBtnConnect.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconBtnConnect.IconColor = System.Drawing.Color.Black;
            this.iconBtnConnect.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtnConnect.Location = new System.Drawing.Point(189, 124);
            this.iconBtnConnect.Name = "iconBtnConnect";
            this.iconBtnConnect.Size = new System.Drawing.Size(109, 26);
            this.iconBtnConnect.TabIndex = 8;
            this.iconBtnConnect.Text = "Connect";
            this.iconBtnConnect.UseVisualStyleBackColor = true;
            this.iconBtnConnect.Click += new System.EventHandler(this.iconBtnConnect_Click);
            // 
            // comboBoxBaudRate
            // 
            this.comboBoxBaudRate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.comboBoxBaudRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxBaudRate.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.comboBoxBaudRate.ForeColor = System.Drawing.Color.White;
            this.comboBoxBaudRate.FormattingEnabled = true;
            this.comboBoxBaudRate.Location = new System.Drawing.Point(188, 75);
            this.comboBoxBaudRate.Name = "comboBoxBaudRate";
            this.comboBoxBaudRate.Size = new System.Drawing.Size(111, 25);
            this.comboBoxBaudRate.TabIndex = 7;
            this.comboBoxBaudRate.SelectedIndexChanged += new System.EventHandler(this.comboBoxBaudRate_SelectedIndexChanged);
            // 
            // comboBoxCOM
            // 
            this.comboBoxCOM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.comboBoxCOM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxCOM.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.comboBoxCOM.ForeColor = System.Drawing.Color.White;
            this.comboBoxCOM.FormattingEnabled = true;
            this.comboBoxCOM.Location = new System.Drawing.Point(188, 24);
            this.comboBoxCOM.MaxDropDownItems = 12;
            this.comboBoxCOM.Name = "comboBoxCOM";
            this.comboBoxCOM.Size = new System.Drawing.Size(111, 25);
            this.comboBoxCOM.TabIndex = 6;
            this.comboBoxCOM.DropDown += new System.EventHandler(this.comboBoxCOM_DropDown);
            this.comboBoxCOM.SelectedIndexChanged += new System.EventHandler(this.comboBoxCOM_SelectedIndexChanged);
            // 
            // EditorConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.Controls.Add(this.iconBtnConnectStatus);
            this.Controls.Add(this.iconBtnConnect);
            this.Controls.Add(this.comboBoxBaudRate);
            this.Controls.Add(this.comboBoxCOM);
            this.Controls.Add(this.labelBaud);
            this.Controls.Add(this.labelCOM);
            this.Name = "EditorConnect";
            this.Size = new System.Drawing.Size(335, 175);
            this.Load += new System.EventHandler(this.EditorConnect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCOM;
        private System.Windows.Forms.Label labelBaud;
        private FontAwesome.Sharp.IconButton iconBtnConnect;
        private System.Windows.Forms.ComboBox comboBoxBaudRate;
        private System.Windows.Forms.ComboBox comboBoxCOM;
        private FontAwesome.Sharp.IconButton iconBtnConnectStatus;
    }
}
