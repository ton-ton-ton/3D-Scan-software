namespace SerialClient
{
    partial class Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_reset = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newPlotterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fFTPloatterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataRecord2CsvtoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataRecord2TxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tb_send = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.rtb_serialdata = new System.Windows.Forms.RichTextBox();
            this.cmb_eol = new System.Windows.Forms.ComboBox();
            this.lbl_eol = new System.Windows.Forms.Label();
            this.cb_autoscroll = new System.Windows.Forms.CheckBox();
            this.cbk_FilterASI = new System.Windows.Forms.CheckBox();
            this.btn_Pause = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_reset
            // 
            this.btn_reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_reset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.btn_reset.FlatAppearance.BorderSize = 0;
            this.btn_reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_reset.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.btn_reset.ForeColor = System.Drawing.Color.White;
            this.btn_reset.Location = new System.Drawing.Point(603, 2);
            this.btn_reset.Margin = new System.Windows.Forms.Padding(4);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(117, 25);
            this.btn_reset.TabIndex = 1;
            this.btn_reset.Text = "Reset Text Box";
            this.btn_reset.UseVisualStyleBackColor = false;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(726, 28);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPlotterToolStripMenuItem,
            this.fFTPloatterToolStripMenuItem,
            this.dataRecord2CsvtoolStripMenuItem1,
            this.dataRecord2TxtToolStripMenuItem,
            this.restartDBToolStripMenuItem});
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // newPlotterToolStripMenuItem
            // 
            this.newPlotterToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.newPlotterToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.newPlotterToolStripMenuItem.Name = "newPlotterToolStripMenuItem";
            this.newPlotterToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.newPlotterToolStripMenuItem.Text = "New Plotter";
            this.newPlotterToolStripMenuItem.Click += new System.EventHandler(this.newPlotterToolStripMenuItem_Click);
            // 
            // fFTPloatterToolStripMenuItem
            // 
            this.fFTPloatterToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.fFTPloatterToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.fFTPloatterToolStripMenuItem.Name = "fFTPloatterToolStripMenuItem";
            this.fFTPloatterToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.fFTPloatterToolStripMenuItem.Text = "FFT Ploatter";
            this.fFTPloatterToolStripMenuItem.Visible = false;
            // 
            // dataRecord2CsvtoolStripMenuItem1
            // 
            this.dataRecord2CsvtoolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.dataRecord2CsvtoolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.dataRecord2CsvtoolStripMenuItem1.Name = "dataRecord2CsvtoolStripMenuItem1";
            this.dataRecord2CsvtoolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.dataRecord2CsvtoolStripMenuItem1.Text = "DataRecorder(.csv)";
            this.dataRecord2CsvtoolStripMenuItem1.Click += new System.EventHandler(this.dataRecord2CsvtoolStripMenuItem1_Click);
            // 
            // dataRecord2TxtToolStripMenuItem
            // 
            this.dataRecord2TxtToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.dataRecord2TxtToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.dataRecord2TxtToolStripMenuItem.Name = "dataRecord2TxtToolStripMenuItem";
            this.dataRecord2TxtToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.dataRecord2TxtToolStripMenuItem.Text = "DataRecorder(.txt)";
            this.dataRecord2TxtToolStripMenuItem.Click += new System.EventHandler(this.dataRecord2TxtToolStripMenuItem_Click);
            // 
            // restartDBToolStripMenuItem
            // 
            this.restartDBToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.restartDBToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.restartDBToolStripMenuItem.Name = "restartDBToolStripMenuItem";
            this.restartDBToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.restartDBToolStripMenuItem.Text = "RestartDB";
            this.restartDBToolStripMenuItem.Visible = true;
            this.restartDBToolStripMenuItem.Click += new System.EventHandler(this.restartDBToolStripMenuItem_Click);
            // 
            // tb_send
            // 
            this.tb_send.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_send.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(112)))));
            this.tb_send.ForeColor = System.Drawing.Color.White;
            this.tb_send.Location = new System.Drawing.Point(4, 35);
            this.tb_send.Margin = new System.Windows.Forms.Padding(4);
            this.tb_send.Name = "tb_send";
            this.tb_send.Size = new System.Drawing.Size(599, 25);
            this.tb_send.TabIndex = 10;
            // 
            // btn_send
            // 
            this.btn_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_send.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.btn_send.FlatAppearance.BorderSize = 0;
            this.btn_send.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_send.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.btn_send.ForeColor = System.Drawing.Color.White;
            this.btn_send.Location = new System.Drawing.Point(611, 35);
            this.btn_send.Margin = new System.Windows.Forms.Padding(4);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(108, 25);
            this.btn_send.TabIndex = 11;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = false;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // rtb_serialdata
            // 
            this.rtb_serialdata.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_serialdata.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(112)))));
            this.rtb_serialdata.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_serialdata.ForeColor = System.Drawing.Color.White;
            this.rtb_serialdata.Location = new System.Drawing.Point(4, 68);
            this.rtb_serialdata.Margin = new System.Windows.Forms.Padding(4);
            this.rtb_serialdata.MinimumSize = new System.Drawing.Size(679, 223);
            this.rtb_serialdata.Name = "rtb_serialdata";
            this.rtb_serialdata.ReadOnly = true;
            this.rtb_serialdata.Size = new System.Drawing.Size(715, 257);
            this.rtb_serialdata.TabIndex = 12;
            this.rtb_serialdata.Text = "";
            // 
            // cmb_eol
            // 
            this.cmb_eol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(112)))));
            this.cmb_eol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_eol.ForeColor = System.Drawing.Color.White;
            this.cmb_eol.FormattingEnabled = true;
            this.cmb_eol.Location = new System.Drawing.Point(575, 328);
            this.cmb_eol.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_eol.Name = "cmb_eol";
            this.cmb_eol.Size = new System.Drawing.Size(143, 23);
            this.cmb_eol.TabIndex = 5;
            // 
            // lbl_eol
            // 
            this.lbl_eol.AutoSize = true;
            this.lbl_eol.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.lbl_eol.ForeColor = System.Drawing.Color.White;
            this.lbl_eol.Location = new System.Drawing.Point(392, 329);
            this.lbl_eol.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_eol.Name = "lbl_eol";
            this.lbl_eol.Size = new System.Drawing.Size(175, 19);
            this.lbl_eol.TabIndex = 6;
            this.lbl_eol.Text = "End of Line Characcters:";
            // 
            // cb_autoscroll
            // 
            this.cb_autoscroll.AutoSize = true;
            this.cb_autoscroll.Checked = true;
            this.cb_autoscroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_autoscroll.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.cb_autoscroll.ForeColor = System.Drawing.Color.White;
            this.cb_autoscroll.Location = new System.Drawing.Point(7, 328);
            this.cb_autoscroll.Margin = new System.Windows.Forms.Padding(4);
            this.cb_autoscroll.Name = "cb_autoscroll";
            this.cb_autoscroll.Size = new System.Drawing.Size(101, 23);
            this.cb_autoscroll.TabIndex = 13;
            this.cb_autoscroll.Text = "Autoscroll";
            this.cb_autoscroll.UseVisualStyleBackColor = true;
            this.cb_autoscroll.CheckedChanged += new System.EventHandler(this.cb_autoscroll_CheckedChanged);
            // 
            // cbk_FilterASI
            // 
            this.cbk_FilterASI.AutoSize = true;
            this.cbk_FilterASI.Checked = true;
            this.cbk_FilterASI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbk_FilterASI.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.cbk_FilterASI.ForeColor = System.Drawing.Color.White;
            this.cbk_FilterASI.Location = new System.Drawing.Point(110, 328);
            this.cbk_FilterASI.Margin = new System.Windows.Forms.Padding(4);
            this.cbk_FilterASI.Name = "cbk_FilterASI";
            this.cbk_FilterASI.Size = new System.Drawing.Size(252, 23);
            this.cbk_FilterASI.TabIndex = 14;
            this.cbk_FilterASI.Text = "Filter AdvancedSerial Messages";
            this.cbk_FilterASI.UseVisualStyleBackColor = true;
            // 
            // btn_Pause
            // 
            this.btn_Pause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Pause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.btn_Pause.FlatAppearance.BorderSize = 0;
            this.btn_Pause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Pause.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.btn_Pause.ForeColor = System.Drawing.Color.White;
            this.btn_Pause.Location = new System.Drawing.Point(517, 2);
            this.btn_Pause.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Pause.Name = "btn_Pause";
            this.btn_Pause.Size = new System.Drawing.Size(86, 25);
            this.btn_Pause.TabIndex = 15;
            this.btn_Pause.Text = "Pause";
            this.btn_Pause.UseVisualStyleBackColor = false;
            this.btn_Pause.Click += new System.EventHandler(this.btn_Pause_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(726, 357);
            this.Controls.Add(this.btn_Pause);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.cbk_FilterASI);
            this.Controls.Add(this.cb_autoscroll);
            this.Controls.Add(this.lbl_eol);
            this.Controls.Add(this.rtb_serialdata);
            this.Controls.Add(this.cmb_eol);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.tb_send);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Client";
            this.Text = "NoCom";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Client_FormClosing);
            this.Load += new System.EventHandler(this.Client_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newPlotterToolStripMenuItem;
        private System.Windows.Forms.TextBox tb_send;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.RichTextBox rtb_serialdata;
        private System.Windows.Forms.ComboBox cmb_eol;
        private System.Windows.Forms.Label lbl_eol;
        private System.Windows.Forms.CheckBox cb_autoscroll;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.CheckBox cbk_FilterASI;
        private System.Windows.Forms.ToolStripMenuItem restartDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataRecord2TxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataRecord2CsvtoolStripMenuItem1;
        private System.Windows.Forms.Button btn_Pause;
        private System.Windows.Forms.ToolStripMenuItem fFTPloatterToolStripMenuItem;
    }
}

