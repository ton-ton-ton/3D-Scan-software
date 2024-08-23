
namespace UserControlEditor
{
    partial class SubMenu1_Setting
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
            this.components = new System.ComponentModel.Container();
            this.panelSubconnect = new System.Windows.Forms.Panel();
            this.editorConnect = new UserControlEditor.EditorConnect();
            this.panelSubCalibration = new System.Windows.Forms.Panel();
            this.iconBtnInertial = new FontAwesome.Sharp.IconButton();
            this.iconBtnADNS = new FontAwesome.Sharp.IconButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMinimizwEditor = new FontAwesome.Sharp.IconButton();
            this.panelConnect = new System.Windows.Forms.Panel();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.panelCalibration = new System.Windows.Forms.Panel();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.panelSubconnect.SuspendLayout();
            this.panelSubCalibration.SuspendLayout();
            this.panelConnect.SuspendLayout();
            this.panelCalibration.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSubconnect
            // 
            this.panelSubconnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.panelSubconnect.Controls.Add(this.editorConnect);
            this.panelSubconnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSubconnect.Location = new System.Drawing.Point(0, 48);
            this.panelSubconnect.Margin = new System.Windows.Forms.Padding(0);
            this.panelSubconnect.Name = "panelSubconnect";
            this.panelSubconnect.Size = new System.Drawing.Size(329, 175);
            this.panelSubconnect.TabIndex = 17;
            // 
            // editorConnect
            // 
            this.editorConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.editorConnect.Location = new System.Drawing.Point(17, 3);
            this.editorConnect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.editorConnect.Name = "editorConnect";
            this.editorConnect.Size = new System.Drawing.Size(335, 175);
            this.editorConnect.TabIndex = 0;
            // 
            // panelSubCalibration
            // 
            this.panelSubCalibration.Controls.Add(this.iconBtnInertial);
            this.panelSubCalibration.Controls.Add(this.iconBtnADNS);
            this.panelSubCalibration.Controls.Add(this.label2);
            this.panelSubCalibration.Controls.Add(this.label1);
            this.panelSubCalibration.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSubCalibration.Location = new System.Drawing.Point(0, 271);
            this.panelSubCalibration.Name = "panelSubCalibration";
            this.panelSubCalibration.Size = new System.Drawing.Size(329, 102);
            this.panelSubCalibration.TabIndex = 16;
            // 
            // iconBtnInertial
            // 
            this.iconBtnInertial.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.iconBtnInertial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnInertial.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.iconBtnInertial.ForeColor = System.Drawing.SystemColors.Control;
            this.iconBtnInertial.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconBtnInertial.IconColor = System.Drawing.Color.Black;
            this.iconBtnInertial.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtnInertial.Location = new System.Drawing.Point(223, 22);
            this.iconBtnInertial.Name = "iconBtnInertial";
            this.iconBtnInertial.Size = new System.Drawing.Size(75, 30);
            this.iconBtnInertial.TabIndex = 2;
            this.iconBtnInertial.Text = "Start";
            this.iconBtnInertial.UseVisualStyleBackColor = true;
            this.iconBtnInertial.Click += new System.EventHandler(this.iconBtnInertial_Click);
            // 
            // iconBtnADNS
            // 
            this.iconBtnADNS.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.iconBtnADNS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnADNS.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.iconBtnADNS.ForeColor = System.Drawing.Color.White;
            this.iconBtnADNS.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconBtnADNS.IconColor = System.Drawing.Color.Black;
            this.iconBtnADNS.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtnADNS.Location = new System.Drawing.Point(223, 64);
            this.iconBtnADNS.Name = "iconBtnADNS";
            this.iconBtnADNS.Size = new System.Drawing.Size(75, 30);
            this.iconBtnADNS.TabIndex = 3;
            this.iconBtnADNS.Text = "Start";
            this.iconBtnADNS.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(47, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "ADNS-9800";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(36, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Accel  and Gyro";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnMinimizwEditor
            // 
            this.btnMinimizwEditor.AutoSize = true;
            this.btnMinimizwEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.btnMinimizwEditor.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimizwEditor.FlatAppearance.BorderSize = 0;
            this.btnMinimizwEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizwEditor.IconChar = FontAwesome.Sharp.IconChar.Neuter;
            this.btnMinimizwEditor.IconColor = System.Drawing.Color.White;
            this.btnMinimizwEditor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMinimizwEditor.IconSize = 15;
            this.btnMinimizwEditor.Location = new System.Drawing.Point(329, 0);
            this.btnMinimizwEditor.Margin = new System.Windows.Forms.Padding(0);
            this.btnMinimizwEditor.Name = "btnMinimizwEditor";
            this.btnMinimizwEditor.Size = new System.Drawing.Size(21, 509);
            this.btnMinimizwEditor.TabIndex = 15;
            this.btnMinimizwEditor.UseMnemonic = false;
            this.btnMinimizwEditor.UseVisualStyleBackColor = false;
            this.btnMinimizwEditor.Click += new System.EventHandler(this.BtnMinimizwEditor_Click);
            // 
            // panelConnect
            // 
            this.panelConnect.Controls.Add(this.iconButton1);
            this.panelConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelConnect.Location = new System.Drawing.Point(0, 0);
            this.panelConnect.Name = "panelConnect";
            this.panelConnect.Size = new System.Drawing.Size(329, 48);
            this.panelConnect.TabIndex = 18;
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.Color.Indigo;
            this.iconButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Font = new System.Drawing.Font("微軟正黑體", 8F);
            this.iconButton1.ForeColor = System.Drawing.SystemColors.Window;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.D;
            this.iconButton1.IconColor = System.Drawing.Color.White;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.IconSize = 10;
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconButton1.Location = new System.Drawing.Point(0, 0);
            this.iconButton1.Margin = new System.Windows.Forms.Padding(0);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Size = new System.Drawing.Size(329, 48);
            this.iconButton1.TabIndex = 0;
            this.iconButton1.Text = "Connect";
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // panelCalibration
            // 
            this.panelCalibration.Controls.Add(this.iconButton2);
            this.panelCalibration.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCalibration.Location = new System.Drawing.Point(0, 223);
            this.panelCalibration.Name = "panelCalibration";
            this.panelCalibration.Size = new System.Drawing.Size(329, 48);
            this.panelCalibration.TabIndex = 12;
            // 
            // iconButton2
            // 
            this.iconButton2.BackColor = System.Drawing.Color.Indigo;
            this.iconButton2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconButton2.FlatAppearance.BorderSize = 0;
            this.iconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton2.Font = new System.Drawing.Font("微軟正黑體", 8F);
            this.iconButton2.ForeColor = System.Drawing.SystemColors.Window;
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.D;
            this.iconButton2.IconColor = System.Drawing.Color.White;
            this.iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton2.IconSize = 10;
            this.iconButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconButton2.Location = new System.Drawing.Point(0, 0);
            this.iconButton2.Margin = new System.Windows.Forms.Padding(0);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Size = new System.Drawing.Size(329, 48);
            this.iconButton2.TabIndex = 1;
            this.iconButton2.Text = "Calibration";
            this.iconButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton2.UseVisualStyleBackColor = false;
            this.iconButton2.Click += new System.EventHandler(this.iconButton2_Click);
            // 
            // SubMenu1_Setting
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.Controls.Add(this.panelSubCalibration);
            this.Controls.Add(this.panelCalibration);
            this.Controls.Add(this.panelSubconnect);
            this.Controls.Add(this.panelConnect);
            this.Controls.Add(this.btnMinimizwEditor);
            this.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "SubMenu1_Setting";
            this.Size = new System.Drawing.Size(350, 509);
            this.panelSubconnect.ResumeLayout(false);
            this.panelSubCalibration.ResumeLayout(false);
            this.panelSubCalibration.PerformLayout();
            this.panelConnect.ResumeLayout(false);
            this.panelCalibration.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelSubconnect;
        private System.Windows.Forms.Panel panelSubCalibration;
        public  FontAwesome.Sharp.IconButton btnMinimizwEditor;
        private System.Windows.Forms.Panel panelConnect;
        private FontAwesome.Sharp.IconButton iconButton1;
        private System.Windows.Forms.Panel panelCalibration;
        private FontAwesome.Sharp.IconButton iconButton2;
        public UserControlEditor.EditorConnect editorConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton iconBtnInertial;
        private FontAwesome.Sharp.IconButton iconBtnADNS;
    }
}
