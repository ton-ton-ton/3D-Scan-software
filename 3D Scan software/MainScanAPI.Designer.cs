
namespace _3D_Scan_software
{
    partial class MainScanAPI
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnMaximum = new FontAwesome.Sharp.IconButton();
            this.btnExit = new FontAwesome.Sharp.IconButton();
            this.BtnMinimize = new FontAwesome.Sharp.IconButton();
            this.panelToolBar = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.PCDFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PLYFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.STLFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TxtFileStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.OBJFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analysisChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frameCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fFTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMode = new System.Windows.Forms.Panel();
            this.panelSubmode = new System.Windows.Forms.Panel();
            this.iconBtnSetting = new FontAwesome.Sharp.IconButton();
            this.iconBtnScan = new FontAwesome.Sharp.IconButton();
            this.iconBtnCurFil = new FontAwesome.Sharp.IconButton();
            this.iconBtnOutput = new FontAwesome.Sharp.IconButton();
            this.panelMidle = new System.Windows.Forms.Panel();
            this.SplitterEditorVisua = new System.Windows.Forms.SplitContainer();
            this.rtb_MainStatus = new System.Windows.Forms.RichTextBox();
            this.subMenu1_Setting = new UserControlEditor.SubMenu1_Setting();
            this.SubMenu1_Scan = new UserControlEditor.SubMenu1_Scan();
            this.SubMenu1_CurFil = new UserControlEditor.SubMenu1_CurFil();
            this.SubMenu1_Output = new UserControlEditor.SubMenu1_Output();
            this.panel_Pos = new System.Windows.Forms.Panel();
            this.lb_Z = new System.Windows.Forms.Label();
            this.lb_zPos = new System.Windows.Forms.Label();
            this.lb_yPos = new System.Windows.Forms.Label();
            this.lb_Y = new System.Windows.Forms.Label();
            this.lb_xPos = new System.Windows.Forms.Label();
            this.lb_X = new System.Windows.Forms.Label();
            this.panelVirsualixtion = new System.Windows.Forms.Panel();
            this.iconDropDownButton1 = new FontAwesome.Sharp.IconDropDownButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panelTitleBar.SuspendLayout();
            this.panelToolBar.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panelMode.SuspendLayout();
            this.panelSubmode.SuspendLayout();
            this.panelMidle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitterEditorVisua)).BeginInit();
            this.SplitterEditorVisua.Panel1.SuspendLayout();
            this.SplitterEditorVisua.Panel2.SuspendLayout();
            this.SplitterEditorVisua.SuspendLayout();
            this.panel_Pos.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            toolStripSeparator1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(127, 6);
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(105)))));
            this.panelTitleBar.Controls.Add(this.label1);
            this.panelTitleBar.Controls.Add(this.BtnMaximum);
            this.panelTitleBar.Controls.Add(this.btnExit);
            this.panelTitleBar.Controls.Add(this.BtnMinimize);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1412, 39);
            this.panelTitleBar.TabIndex = 0;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainScanAPI_MouseDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "3D Scan ";
            // 
            // BtnMaximum
            // 
            this.BtnMaximum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMaximum.FlatAppearance.BorderSize = 0;
            this.BtnMaximum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMaximum.IconChar = FontAwesome.Sharp.IconChar.WindowRestore;
            this.BtnMaximum.IconColor = System.Drawing.Color.White;
            this.BtnMaximum.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtnMaximum.IconSize = 20;
            this.BtnMaximum.Location = new System.Drawing.Point(1343, 0);
            this.BtnMaximum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnMaximum.Name = "BtnMaximum";
            this.BtnMaximum.Size = new System.Drawing.Size(32, 22);
            this.BtnMaximum.TabIndex = 2;
            this.BtnMaximum.UseVisualStyleBackColor = true;
            this.BtnMaximum.Click += new System.EventHandler(this.BtnMaximum_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this.btnExit.IconColor = System.Drawing.Color.White;
            this.btnExit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExit.IconSize = 20;
            this.btnExit.Location = new System.Drawing.Point(1380, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(29, 22);
            this.btnExit.TabIndex = 1;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // BtnMinimize
            // 
            this.BtnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMinimize.FlatAppearance.BorderSize = 0;
            this.BtnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMinimize.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            this.BtnMinimize.IconColor = System.Drawing.Color.White;
            this.BtnMinimize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtnMinimize.IconSize = 20;
            this.BtnMinimize.Location = new System.Drawing.Point(1304, 0);
            this.BtnMinimize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnMinimize.Name = "BtnMinimize";
            this.BtnMinimize.Size = new System.Drawing.Size(32, 22);
            this.BtnMinimize.TabIndex = 0;
            this.BtnMinimize.UseVisualStyleBackColor = true;
            this.BtnMinimize.Click += new System.EventHandler(this.BtnMinimize_Click);
            // 
            // panelToolBar
            // 
            this.panelToolBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.panelToolBar.Controls.Add(this.menuStrip1);
            this.panelToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelToolBar.Location = new System.Drawing.Point(0, 39);
            this.panelToolBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelToolBar.Name = "panelToolBar";
            this.panelToolBar.Size = new System.Drawing.Size(1412, 42);
            this.panelToolBar.TabIndex = 1;
            this.panelToolBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainScanAPI_MouseDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.viewToolStripMenuItem,
            this.toolStripMenuItem3,
            this.ToolToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1412, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "Toolstrip";
            this.menuStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainScanAPI_MouseDown);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.toolStripMenuItem1.Font = new System.Drawing.Font("新細明體", 9F);
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(43, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PCDFileToolStripMenuItem,
            this.PLYFileToolStripMenuItem,
            this.STLFileToolStripMenuItem,
            this.TxtFileStripMenuItem4,
            this.OBJFileToolStripMenuItem});
            this.toolStripMenuItem2.Font = new System.Drawing.Font("新細明體", 9F);
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(130, 26);
            this.toolStripMenuItem2.Text = "Open";
            this.toolStripMenuItem2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PCDFileToolStripMenuItem
            // 
            this.PCDFileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.PCDFileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.PCDFileToolStripMenuItem.Name = "PCDFileToolStripMenuItem";
            this.PCDFileToolStripMenuItem.Size = new System.Drawing.Size(139, 26);
            this.PCDFileToolStripMenuItem.Text = "PCD file";
            this.PCDFileToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PCDFileToolStripMenuItem.Click += new System.EventHandler(this.PCDFileToolStripMenuItem_Click);
            // 
            // PLYFileToolStripMenuItem
            // 
            this.PLYFileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.PLYFileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.PLYFileToolStripMenuItem.Name = "PLYFileToolStripMenuItem";
            this.PLYFileToolStripMenuItem.Size = new System.Drawing.Size(139, 26);
            this.PLYFileToolStripMenuItem.Text = "PLY file";
            this.PLYFileToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PLYFileToolStripMenuItem.Click += new System.EventHandler(this.PLYFileToolStripMenuItem_Click);
            // 
            // STLFileToolStripMenuItem
            // 
            this.STLFileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.STLFileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.STLFileToolStripMenuItem.Name = "STLFileToolStripMenuItem";
            this.STLFileToolStripMenuItem.Size = new System.Drawing.Size(139, 26);
            this.STLFileToolStripMenuItem.Text = "STL file";
            this.STLFileToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtFileStripMenuItem4
            // 
            this.TxtFileStripMenuItem4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.TxtFileStripMenuItem4.ForeColor = System.Drawing.SystemColors.Window;
            this.TxtFileStripMenuItem4.Name = "TxtFileStripMenuItem4";
            this.TxtFileStripMenuItem4.Size = new System.Drawing.Size(139, 26);
            this.TxtFileStripMenuItem4.Text = "TXT file";
            this.TxtFileStripMenuItem4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TxtFileStripMenuItem4.Click += new System.EventHandler(this.TxtFileStripMenuItem4_Click);
            // 
            // OBJFileToolStripMenuItem
            // 
            this.OBJFileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.OBJFileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.OBJFileToolStripMenuItem.Name = "OBJFileToolStripMenuItem";
            this.OBJFileToolStripMenuItem.Size = new System.Drawing.Size(139, 26);
            this.OBJFileToolStripMenuItem.Text = "OBJ file";
            this.OBJFileToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OBJFileToolStripMenuItem.Click += new System.EventHandler(this.OBJFileToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.saveToolStripMenuItem.Font = new System.Drawing.Font("新細明體", 9F);
            this.saveToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(130, 26);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.saveAsToolStripMenuItem.Font = new System.Drawing.Font("新細明體", 9F);
            this.saveAsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(130, 26);
            this.saveAsToolStripMenuItem.Text = "Save as";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Font = new System.Drawing.Font("新細明體", 9F);
            this.viewToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Font = new System.Drawing.Font("新細明體", 9F);
            this.toolStripMenuItem3.ForeColor = System.Drawing.SystemColors.Window;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(48, 20);
            this.toolStripMenuItem3.Text = "Help";
            // 
            // ToolToolStripMenuItem
            // 
            this.ToolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serialClientToolStripMenuItem,
            this.analysisChartToolStripMenuItem,
            this.frameCaptureToolStripMenuItem,
            this.fFTToolStripMenuItem});
            this.ToolToolStripMenuItem.Font = new System.Drawing.Font("新細明體", 9F);
            this.ToolToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.ToolToolStripMenuItem.Name = "ToolToolStripMenuItem";
            this.ToolToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.ToolToolStripMenuItem.Text = "Tool";
            // 
            // serialClientToolStripMenuItem
            // 
            this.serialClientToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.serialClientToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.serialClientToolStripMenuItem.Name = "serialClientToolStripMenuItem";
            this.serialClientToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.serialClientToolStripMenuItem.Text = "SerialClient";
            this.serialClientToolStripMenuItem.Click += new System.EventHandler(this.serialClientToolStripMenuItem_Click);
            // 
            // analysisChartToolStripMenuItem
            // 
            this.analysisChartToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.analysisChartToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.analysisChartToolStripMenuItem.Name = "analysisChartToolStripMenuItem";
            this.analysisChartToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.analysisChartToolStripMenuItem.Text = "Analysis Chart";
            this.analysisChartToolStripMenuItem.Click += new System.EventHandler(this.analysisChartToolStripMenuItem_Click);
            // 
            // frameCaptureToolStripMenuItem
            // 
            this.frameCaptureToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.frameCaptureToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.frameCaptureToolStripMenuItem.Name = "frameCaptureToolStripMenuItem";
            this.frameCaptureToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.frameCaptureToolStripMenuItem.Text = "Frame Capture";
            this.frameCaptureToolStripMenuItem.Click += new System.EventHandler(this.frameCaptureToolStripMenuItem_Click);
            // 
            // fFTToolStripMenuItem
            // 
            this.fFTToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.fFTToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.fFTToolStripMenuItem.Name = "fFTToolStripMenuItem";
            this.fFTToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.fFTToolStripMenuItem.Text = "FFT";
            // 
            // panelMode
            // 
            this.panelMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.panelMode.Controls.Add(this.panelSubmode);
            this.panelMode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelMode.Location = new System.Drawing.Point(0, 768);
            this.panelMode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelMode.Name = "panelMode";
            this.panelMode.Padding = new System.Windows.Forms.Padding(15, 2, 15, 2);
            this.panelMode.Size = new System.Drawing.Size(1412, 111);
            this.panelMode.TabIndex = 2;
            // 
            // panelSubmode
            // 
            this.panelSubmode.AutoScroll = true;
            this.panelSubmode.Controls.Add(this.iconBtnSetting);
            this.panelSubmode.Controls.Add(this.iconBtnScan);
            this.panelSubmode.Controls.Add(this.iconBtnCurFil);
            this.panelSubmode.Controls.Add(this.iconBtnOutput);
            this.panelSubmode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSubmode.Location = new System.Drawing.Point(15, 2);
            this.panelSubmode.Margin = new System.Windows.Forms.Padding(0);
            this.panelSubmode.Name = "panelSubmode";
            this.panelSubmode.Size = new System.Drawing.Size(1382, 107);
            this.panelSubmode.TabIndex = 1;
            // 
            // iconBtnSetting
            // 
            this.iconBtnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.iconBtnSetting.BackColor = System.Drawing.Color.Transparent;
            this.iconBtnSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.iconBtnSetting.FlatAppearance.BorderSize = 0;
            this.iconBtnSetting.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.iconBtnSetting.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.iconBtnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnSetting.Font = new System.Drawing.Font("新細明體", 11F, System.Drawing.FontStyle.Bold);
            this.iconBtnSetting.ForeColor = System.Drawing.SystemColors.Window;
            this.iconBtnSetting.IconChar = FontAwesome.Sharp.IconChar.Circle;
            this.iconBtnSetting.IconColor = System.Drawing.Color.White;
            this.iconBtnSetting.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.iconBtnSetting.IconSize = 60;
            this.iconBtnSetting.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.iconBtnSetting.Location = new System.Drawing.Point(59, 4);
            this.iconBtnSetting.Margin = new System.Windows.Forms.Padding(0);
            this.iconBtnSetting.Name = "iconBtnSetting";
            this.iconBtnSetting.Size = new System.Drawing.Size(96, 107);
            this.iconBtnSetting.TabIndex = 2;
            this.iconBtnSetting.Text = "Setting";
            this.iconBtnSetting.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.iconBtnSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconBtnSetting.UseVisualStyleBackColor = false;
            this.iconBtnSetting.Click += new System.EventHandler(this.iconBtnSetting_Click);
            this.iconBtnSetting.MouseLeave += new System.EventHandler(this.DeactivateChangeModeColor);
            this.iconBtnSetting.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ActivateChangeModeColor);
            // 
            // iconBtnScan
            // 
            this.iconBtnScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.iconBtnScan.BackColor = System.Drawing.Color.Transparent;
            this.iconBtnScan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.iconBtnScan.FlatAppearance.BorderSize = 0;
            this.iconBtnScan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.iconBtnScan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.iconBtnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnScan.Font = new System.Drawing.Font("新細明體", 11F, System.Drawing.FontStyle.Bold);
            this.iconBtnScan.ForeColor = System.Drawing.SystemColors.Window;
            this.iconBtnScan.IconChar = FontAwesome.Sharp.IconChar.Circle;
            this.iconBtnScan.IconColor = System.Drawing.Color.White;
            this.iconBtnScan.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.iconBtnScan.IconSize = 60;
            this.iconBtnScan.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.iconBtnScan.Location = new System.Drawing.Point(433, 4);
            this.iconBtnScan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconBtnScan.Name = "iconBtnScan";
            this.iconBtnScan.Size = new System.Drawing.Size(96, 107);
            this.iconBtnScan.TabIndex = 3;
            this.iconBtnScan.Text = "Scan";
            this.iconBtnScan.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.iconBtnScan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconBtnScan.UseVisualStyleBackColor = false;
            this.iconBtnScan.Click += new System.EventHandler(this.iconBtnScan_Click);
            this.iconBtnScan.MouseLeave += new System.EventHandler(this.DeactivateChangeModeColor);
            this.iconBtnScan.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ActivateChangeModeColor);
            // 
            // iconBtnCurFil
            // 
            this.iconBtnCurFil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.iconBtnCurFil.BackColor = System.Drawing.Color.Transparent;
            this.iconBtnCurFil.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.iconBtnCurFil.FlatAppearance.BorderSize = 0;
            this.iconBtnCurFil.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.iconBtnCurFil.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.iconBtnCurFil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnCurFil.Font = new System.Drawing.Font("新細明體", 11F, System.Drawing.FontStyle.Bold);
            this.iconBtnCurFil.ForeColor = System.Drawing.SystemColors.Window;
            this.iconBtnCurFil.IconChar = FontAwesome.Sharp.IconChar.Circle;
            this.iconBtnCurFil.IconColor = System.Drawing.Color.White;
            this.iconBtnCurFil.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.iconBtnCurFil.IconSize = 60;
            this.iconBtnCurFil.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.iconBtnCurFil.Location = new System.Drawing.Point(803, 4);
            this.iconBtnCurFil.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconBtnCurFil.Name = "iconBtnCurFil";
            this.iconBtnCurFil.Size = new System.Drawing.Size(155, 107);
            this.iconBtnCurFil.TabIndex = 4;
            this.iconBtnCurFil.Text = "Curve / Filting";
            this.iconBtnCurFil.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.iconBtnCurFil.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconBtnCurFil.UseVisualStyleBackColor = false;
            this.iconBtnCurFil.Click += new System.EventHandler(this.iconBtnCurFil_Click);
            this.iconBtnCurFil.MouseLeave += new System.EventHandler(this.DeactivateChangeModeColor);
            this.iconBtnCurFil.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ActivateChangeModeColor);
            // 
            // iconBtnOutput
            // 
            this.iconBtnOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.iconBtnOutput.BackColor = System.Drawing.Color.Transparent;
            this.iconBtnOutput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.iconBtnOutput.FlatAppearance.BorderSize = 0;
            this.iconBtnOutput.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.iconBtnOutput.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.iconBtnOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnOutput.Font = new System.Drawing.Font("新細明體", 11F, System.Drawing.FontStyle.Bold);
            this.iconBtnOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.iconBtnOutput.IconChar = FontAwesome.Sharp.IconChar.Circle;
            this.iconBtnOutput.IconColor = System.Drawing.Color.White;
            this.iconBtnOutput.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.iconBtnOutput.IconSize = 60;
            this.iconBtnOutput.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.iconBtnOutput.Location = new System.Drawing.Point(1229, 4);
            this.iconBtnOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iconBtnOutput.Name = "iconBtnOutput";
            this.iconBtnOutput.Size = new System.Drawing.Size(96, 107);
            this.iconBtnOutput.TabIndex = 5;
            this.iconBtnOutput.Text = "Output";
            this.iconBtnOutput.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.iconBtnOutput.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconBtnOutput.UseVisualStyleBackColor = false;
            this.iconBtnOutput.Click += new System.EventHandler(this.iconBtnOutput_Click);
            this.iconBtnOutput.MouseLeave += new System.EventHandler(this.DeactivateChangeModeColor);
            this.iconBtnOutput.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ActivateChangeModeColor);
            // 
            // panelMidle
            // 
            this.panelMidle.BackColor = System.Drawing.Color.Black;
            this.panelMidle.Controls.Add(this.SplitterEditorVisua);
            this.panelMidle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMidle.Location = new System.Drawing.Point(0, 81);
            this.panelMidle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelMidle.MinimumSize = new System.Drawing.Size(1113, 539);
            this.panelMidle.Name = "panelMidle";
            this.panelMidle.Padding = new System.Windows.Forms.Padding(0, 15, 15, 15);
            this.panelMidle.Size = new System.Drawing.Size(1412, 687);
            this.panelMidle.TabIndex = 3;
            // 
            // SplitterEditorVisua
            // 
            this.SplitterEditorVisua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitterEditorVisua.Location = new System.Drawing.Point(0, 15);
            this.SplitterEditorVisua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SplitterEditorVisua.Name = "SplitterEditorVisua";
            // 
            // SplitterEditorVisua.Panel1
            // 
            this.SplitterEditorVisua.Panel1.Controls.Add(this.rtb_MainStatus);
            this.SplitterEditorVisua.Panel1.Controls.Add(this.subMenu1_Setting);
            this.SplitterEditorVisua.Panel1.Controls.Add(this.SubMenu1_Scan);
            this.SplitterEditorVisua.Panel1.Controls.Add(this.SubMenu1_CurFil);
            this.SplitterEditorVisua.Panel1.Controls.Add(this.SubMenu1_Output);
            this.SplitterEditorVisua.Panel1MinSize = 15;
            // 
            // SplitterEditorVisua.Panel2
            // 
            this.SplitterEditorVisua.Panel2.Controls.Add(this.panel_Pos);
            this.SplitterEditorVisua.Panel2.Controls.Add(this.panelVirsualixtion);
            this.SplitterEditorVisua.Size = new System.Drawing.Size(1397, 657);
            this.SplitterEditorVisua.SplitterDistance = 355;
            this.SplitterEditorVisua.SplitterWidth = 15;
            this.SplitterEditorVisua.TabIndex = 5;
            // 
            // rtb_MainStatus
            // 
            this.rtb_MainStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(112)))));
            this.rtb_MainStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_MainStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtb_MainStatus.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.rtb_MainStatus.ForeColor = System.Drawing.Color.White;
            this.rtb_MainStatus.Location = new System.Drawing.Point(0, 557);
            this.rtb_MainStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtb_MainStatus.Name = "rtb_MainStatus";
            this.rtb_MainStatus.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtb_MainStatus.Size = new System.Drawing.Size(355, 100);
            this.rtb_MainStatus.TabIndex = 3;
            this.rtb_MainStatus.TabStop = false;
            this.rtb_MainStatus.Text = "";
            // 
            // subMenu1_Setting
            // 
            this.subMenu1_Setting.AllowDrop = true;
            this.subMenu1_Setting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.subMenu1_Setting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subMenu1_Setting.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.subMenu1_Setting.Location = new System.Drawing.Point(0, 0);
            this.subMenu1_Setting.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.subMenu1_Setting.Name = "subMenu1_Setting";
            this.subMenu1_Setting.ShowStatus = true;
            this.subMenu1_Setting.Size = new System.Drawing.Size(355, 657);
            this.subMenu1_Setting.TabIndex = 1;
            this.subMenu1_Setting.DoubleClick += new System.EventHandler(this.subMenu11_DoubleClick);
            // 
            // SubMenu1_Scan
            // 
            this.SubMenu1_Scan.AllowDrop = true;
            this.SubMenu1_Scan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.SubMenu1_Scan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SubMenu1_Scan.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SubMenu1_Scan.Location = new System.Drawing.Point(0, 0);
            this.SubMenu1_Scan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SubMenu1_Scan.Name = "SubMenu1_Scan";
            this.SubMenu1_Scan.SensorOperatingStatus = false;
            this.SubMenu1_Scan.ShowStatus = false;
            this.SubMenu1_Scan.Size = new System.Drawing.Size(355, 657);
            this.SubMenu1_Scan.TabIndex = 1;
            this.SubMenu1_Scan.Visible = false;
            this.SubMenu1_Scan.DoubleClick += new System.EventHandler(this.subMenu11_DoubleClick);
            // 
            // SubMenu1_CurFil
            // 
            this.SubMenu1_CurFil.AllowDrop = true;
            this.SubMenu1_CurFil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.SubMenu1_CurFil.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SubMenu1_CurFil.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SubMenu1_CurFil.Location = new System.Drawing.Point(0, 0);
            this.SubMenu1_CurFil.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SubMenu1_CurFil.Name = "SubMenu1_CurFil";
            this.SubMenu1_CurFil.ShowStatus = false;
            this.SubMenu1_CurFil.Size = new System.Drawing.Size(355, 657);
            this.SubMenu1_CurFil.TabIndex = 1;
            this.SubMenu1_CurFil.Visible = false;
            this.SubMenu1_CurFil.DoubleClick += new System.EventHandler(this.subMenu11_DoubleClick);
            // 
            // SubMenu1_Output
            // 
            this.SubMenu1_Output._pc = null;
            this.SubMenu1_Output.AllowDrop = true;
            this.SubMenu1_Output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.SubMenu1_Output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SubMenu1_Output.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SubMenu1_Output.Location = new System.Drawing.Point(0, 0);
            this.SubMenu1_Output.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SubMenu1_Output.Name = "SubMenu1_Output";
            this.SubMenu1_Output.SaveStatus = false;
            this.SubMenu1_Output.ShowStatus = false;
            this.SubMenu1_Output.Size = new System.Drawing.Size(355, 657);
            this.SubMenu1_Output.TabIndex = 1;
            this.SubMenu1_Output.Visible = false;
            this.SubMenu1_Output.DoubleClick += new System.EventHandler(this.subMenu11_DoubleClick);
            // 
            // panel_Pos
            // 
            this.panel_Pos.BackColor = System.Drawing.Color.Transparent;
            this.panel_Pos.Controls.Add(this.lb_Z);
            this.panel_Pos.Controls.Add(this.lb_zPos);
            this.panel_Pos.Controls.Add(this.lb_yPos);
            this.panel_Pos.Controls.Add(this.lb_Y);
            this.panel_Pos.Controls.Add(this.lb_xPos);
            this.panel_Pos.Controls.Add(this.lb_X);
            this.panel_Pos.Location = new System.Drawing.Point(0, 0);
            this.panel_Pos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Pos.Name = "panel_Pos";
            this.panel_Pos.Size = new System.Drawing.Size(253, 24);
            this.panel_Pos.TabIndex = 0;
            // 
            // lb_Z
            // 
            this.lb_Z.AutoSize = true;
            this.lb_Z.ForeColor = System.Drawing.Color.White;
            this.lb_Z.Location = new System.Drawing.Point(168, 0);
            this.lb_Z.Name = "lb_Z";
            this.lb_Z.Size = new System.Drawing.Size(24, 15);
            this.lb_Z.TabIndex = 4;
            this.lb_Z.Text = " Z:";
            // 
            // lb_zPos
            // 
            this.lb_zPos.AutoSize = true;
            this.lb_zPos.ForeColor = System.Drawing.Color.White;
            this.lb_zPos.Location = new System.Drawing.Point(189, 0);
            this.lb_zPos.Name = "lb_zPos";
            this.lb_zPos.Size = new System.Drawing.Size(25, 15);
            this.lb_zPos.TabIndex = 5;
            this.lb_zPos.Text = "0.0";
            // 
            // lb_yPos
            // 
            this.lb_yPos.AutoSize = true;
            this.lb_yPos.ForeColor = System.Drawing.Color.White;
            this.lb_yPos.Location = new System.Drawing.Point(105, 0);
            this.lb_yPos.Name = "lb_yPos";
            this.lb_yPos.Size = new System.Drawing.Size(25, 15);
            this.lb_yPos.TabIndex = 3;
            this.lb_yPos.Text = "0.0";
            // 
            // lb_Y
            // 
            this.lb_Y.AutoSize = true;
            this.lb_Y.ForeColor = System.Drawing.Color.White;
            this.lb_Y.Location = new System.Drawing.Point(84, 0);
            this.lb_Y.Name = "lb_Y";
            this.lb_Y.Size = new System.Drawing.Size(25, 15);
            this.lb_Y.TabIndex = 2;
            this.lb_Y.Text = " Y:";
            // 
            // lb_xPos
            // 
            this.lb_xPos.AutoSize = true;
            this.lb_xPos.Dock = System.Windows.Forms.DockStyle.Left;
            this.lb_xPos.ForeColor = System.Drawing.Color.White;
            this.lb_xPos.Location = new System.Drawing.Point(21, 0);
            this.lb_xPos.Name = "lb_xPos";
            this.lb_xPos.Size = new System.Drawing.Size(25, 15);
            this.lb_xPos.TabIndex = 1;
            this.lb_xPos.Text = "0.0";
            // 
            // lb_X
            // 
            this.lb_X.AutoSize = true;
            this.lb_X.Dock = System.Windows.Forms.DockStyle.Left;
            this.lb_X.ForeColor = System.Drawing.Color.White;
            this.lb_X.Location = new System.Drawing.Point(0, 0);
            this.lb_X.Name = "lb_X";
            this.lb_X.Size = new System.Drawing.Size(21, 15);
            this.lb_X.TabIndex = 0;
            this.lb_X.Text = "X:";
            // 
            // panelVirsualixtion
            // 
            this.panelVirsualixtion.AutoSize = true;
            this.panelVirsualixtion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVirsualixtion.Location = new System.Drawing.Point(0, 0);
            this.panelVirsualixtion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelVirsualixtion.Name = "panelVirsualixtion";
            this.panelVirsualixtion.Size = new System.Drawing.Size(1027, 657);
            this.panelVirsualixtion.TabIndex = 1;
            // 
            // iconDropDownButton1
            // 
            this.iconDropDownButton1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconDropDownButton1.IconColor = System.Drawing.Color.Black;
            this.iconDropDownButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconDropDownButton1.Name = "iconDropDownButton1";
            this.iconDropDownButton1.Size = new System.Drawing.Size(23, 23);
            this.iconDropDownButton1.Text = "iconDropDownButton1";
            // 
            // MainScanAPI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(105)))));
            this.ClientSize = new System.Drawing.Size(1412, 879);
            this.ControlBox = false;
            this.Controls.Add(this.panelMidle);
            this.Controls.Add(this.panelMode);
            this.Controls.Add(this.panelToolBar);
            this.Controls.Add(this.panelTitleBar);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1130, 862);
            this.Name = "MainScanAPI";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainScanAPI_Load);
            this.Resize += new System.EventHandler(this.MainScanAPI_Resize);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.panelToolBar.ResumeLayout(false);
            this.panelToolBar.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelMode.ResumeLayout(false);
            this.panelSubmode.ResumeLayout(false);
            this.panelMidle.ResumeLayout(false);
            this.SplitterEditorVisua.Panel1.ResumeLayout(false);
            this.SplitterEditorVisua.Panel2.ResumeLayout(false);
            this.SplitterEditorVisua.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitterEditorVisua)).EndInit();
            this.SplitterEditorVisua.ResumeLayout(false);
            this.panel_Pos.ResumeLayout(false);
            this.panel_Pos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Panel panelToolBar;
        private System.Windows.Forms.Panel panelMode;
        private System.Windows.Forms.Panel panelMidle;
        private FontAwesome.Sharp.IconButton BtnMaximum;
        private FontAwesome.Sharp.IconButton btnExit;
        private FontAwesome.Sharp.IconButton BtnMinimize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem PCDFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PLYFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem STLFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OBJFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private FontAwesome.Sharp.IconDropDownButton iconDropDownButton1;
        private FontAwesome.Sharp.IconButton iconBtnSetting;
        private FontAwesome.Sharp.IconButton iconBtnScan;
        private FontAwesome.Sharp.IconButton iconBtnOutput;
        private FontAwesome.Sharp.IconButton iconBtnCurFil;
        private System.Windows.Forms.Panel panelSubmode;
        private System.Windows.Forms.SplitContainer SplitterEditorVisua;
        private System.Windows.Forms.Panel panelVirsualixtion;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem serialClientToolStripMenuItem;
        private UserControlEditor.SubMenu1_Setting subMenu1_Setting;
        private UserControlEditor.SubMenu1_Output SubMenu1_Output;
        private UserControlEditor.SubMenu1_Scan SubMenu1_Scan;
        private UserControlEditor.SubMenu1_CurFil SubMenu1_CurFil;
        private System.Windows.Forms.RichTextBox rtb_MainStatus;
        private System.Windows.Forms.Panel panel_Pos;
        private System.Windows.Forms.Label lb_zPos;
        private System.Windows.Forms.Label lb_Z;
        private System.Windows.Forms.Label lb_yPos;
        private System.Windows.Forms.Label lb_Y;
        private System.Windows.Forms.Label lb_xPos;
        private System.Windows.Forms.Label lb_X;
        private System.Windows.Forms.ToolStripMenuItem analysisChartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frameCaptureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fFTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TxtFileStripMenuItem4;
    }
}

