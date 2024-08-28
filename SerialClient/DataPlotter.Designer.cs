using System.Windows.Forms;

namespace SerialClient
{
    partial class DataPlotter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataPlotter));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_PauseRun = new System.Windows.Forms.ToolStripButton();
            this.BTN_ShowLegend = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtn_FPS = new System.Windows.Forms.ToolStripButton();
            this.btn_ShowSpectrum = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.TB_XAxisTime = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel_windows = new System.Windows.Forms.ToolStripLabel();
            this.btn_ShowNames = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBox_WindowItems = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripbtn_SpectrumWindowed = new System.Windows.Forms.ToolStripButton();
            this.toolStripbtn_YAisScale = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtn_SavingData = new System.Windows.Forms.ToolStripButton();
            this.Splitter = new System.Windows.Forms.SplitContainer();
            this.SubSplitContainer = new System.Windows.Forms.SplitContainer();
            this.panel_DataPlot = new System.Windows.Forms.Panel();
            this.label_FPS = new System.Windows.Forms.Label();
            this.panel_Spectrum = new System.Windows.Forms.Panel();
            this.btn_RemoveAllLines = new System.Windows.Forms.Button();
            this.dgvplots = new System.Windows.Forms.DataGridView();
            this.VisibleSereis = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Signal = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter)).BeginInit();
            this.Splitter.Panel1.SuspendLayout();
            this.Splitter.Panel2.SuspendLayout();
            this.Splitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SubSplitContainer)).BeginInit();
            this.SubSplitContainer.Panel1.SuspendLayout();
            this.SubSplitContainer.Panel2.SuspendLayout();
            this.SubSplitContainer.SuspendLayout();
            this.panel_DataPlot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvplots)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_PauseRun,
            this.BTN_ShowLegend,
            this.toolStripBtn_FPS,
            this.btn_ShowSpectrum,
            this.toolStripLabel1,
            this.TB_XAxisTime,
            this.toolStripLabel_windows,
            this.btn_ShowNames,
            this.toolStripComboBox_WindowItems,
            this.toolStripbtn_SpectrumWindowed,
            this.toolStripbtn_YAisScale,
            this.toolStripBtn_SavingData});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(922, 31);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_PauseRun
            // 
            this.btn_PauseRun.CheckOnClick = true;
            this.btn_PauseRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_PauseRun.ForeColor = System.Drawing.Color.White;
            this.btn_PauseRun.Image = ((System.Drawing.Image)(resources.GetObject("btn_PauseRun.Image")));
            this.btn_PauseRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_PauseRun.Name = "btn_PauseRun";
            this.btn_PauseRun.Size = new System.Drawing.Size(44, 28);
            this.btn_PauseRun.Text = "Pause";
            this.btn_PauseRun.Click += new System.EventHandler(this.btn_PauseRun_Click);
            // 
            // BTN_ShowLegend
            // 
            this.BTN_ShowLegend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BTN_ShowLegend.CheckOnClick = true;
            this.BTN_ShowLegend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BTN_ShowLegend.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_ShowLegend.ForeColor = System.Drawing.Color.White;
            this.BTN_ShowLegend.Image = ((System.Drawing.Image)(resources.GetObject("BTN_ShowLegend.Image")));
            this.BTN_ShowLegend.ImageTransparentColor = System.Drawing.Color.White;
            this.BTN_ShowLegend.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.BTN_ShowLegend.Name = "BTN_ShowLegend";
            this.BTN_ShowLegend.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.BTN_ShowLegend.Size = new System.Drawing.Size(28, 28);
            this.BTN_ShowLegend.Text = " Show Legend";
            this.BTN_ShowLegend.Click += new System.EventHandler(this.btn_ShowLegend_Click);
            // 
            // toolStripBtn_FPS
            // 
            this.toolStripBtn_FPS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtn_FPS.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripBtn_FPS.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtn_FPS.Image")));
            this.toolStripBtn_FPS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtn_FPS.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.toolStripBtn_FPS.Name = "toolStripBtn_FPS";
            this.toolStripBtn_FPS.Size = new System.Drawing.Size(28, 28);
            this.toolStripBtn_FPS.ToolTipText = "Frame Rate";
            this.toolStripBtn_FPS.Click += new System.EventHandler(this.toolStripBtn_FPS_Click);
            // 
            // btn_ShowSpectrum
            // 
            this.btn_ShowSpectrum.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_ShowSpectrum.ForeColor = System.Drawing.Color.White;
            this.btn_ShowSpectrum.Image = ((System.Drawing.Image)(resources.GetObject("btn_ShowSpectrum.Image")));
            this.btn_ShowSpectrum.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_ShowSpectrum.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.btn_ShowSpectrum.Name = "btn_ShowSpectrum";
            this.btn_ShowSpectrum.Size = new System.Drawing.Size(28, 28);
            this.btn_ShowSpectrum.Text = " Spectrum Plot";
            this.btn_ShowSpectrum.Click += new System.EventHandler(this.btn_ShowSpectrum_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(80, 28);
            this.toolStripLabel1.Text = "Time-Length :";
            // 
            // TB_XAxisTime
            // 
            this.TB_XAxisTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.TB_XAxisTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_XAxisTime.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.TB_XAxisTime.ForeColor = System.Drawing.Color.White;
            this.TB_XAxisTime.Margin = new System.Windows.Forms.Padding(1, 0, 1, 1);
            this.TB_XAxisTime.MergeIndex = 0;
            this.TB_XAxisTime.Name = "TB_XAxisTime";
            this.TB_XAxisTime.Size = new System.Drawing.Size(49, 30);
            this.TB_XAxisTime.Text = "*";
            this.TB_XAxisTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_XAxisTime_KeyPress);
            this.TB_XAxisTime.Validating += new System.ComponentModel.CancelEventHandler(this.TB_XAxisTime_Validating);
            // 
            // toolStripLabel_windows
            // 
            this.toolStripLabel_windows.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel_windows.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel_windows.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel_windows.Image")));
            this.toolStripLabel_windows.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.toolStripLabel_windows.Name = "toolStripLabel_windows";
            this.toolStripLabel_windows.Size = new System.Drawing.Size(67, 28);
            this.toolStripLabel_windows.Text = "Windows : ";
            // 
            // btn_ShowNames
            // 
            this.btn_ShowNames.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btn_ShowNames.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_ShowNames.ForeColor = System.Drawing.Color.White;
            this.btn_ShowNames.Image = ((System.Drawing.Image)(resources.GetObject("btn_ShowNames.Image")));
            this.btn_ShowNames.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_ShowNames.Name = "btn_ShowNames";
            this.btn_ShowNames.Size = new System.Drawing.Size(23, 28);
            this.btn_ShowNames.Text = "▶";
            this.btn_ShowNames.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripComboBox_WindowItems
            // 
            this.toolStripComboBox_WindowItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.toolStripComboBox_WindowItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toolStripComboBox_WindowItems.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.toolStripComboBox_WindowItems.ForeColor = System.Drawing.Color.White;
            this.toolStripComboBox_WindowItems.Items.AddRange(new object[] {
            "Null"});
            this.toolStripComboBox_WindowItems.MaxDropDownItems = 10;
            this.toolStripComboBox_WindowItems.Name = "toolStripComboBox_WindowItems";
            this.toolStripComboBox_WindowItems.Size = new System.Drawing.Size(92, 31);
            this.toolStripComboBox_WindowItems.Text = "Null";
            this.toolStripComboBox_WindowItems.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox_WindowItems_SelectedIndexChanged);
            // 
            // toolStripbtn_SpectrumWindowed
            // 
            this.toolStripbtn_SpectrumWindowed.BackColor = System.Drawing.Color.MidnightBlue;
            this.toolStripbtn_SpectrumWindowed.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripbtn_SpectrumWindowed.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripbtn_SpectrumWindowed.ForeColor = System.Drawing.Color.White;
            this.toolStripbtn_SpectrumWindowed.Image = ((System.Drawing.Image)(resources.GetObject("toolStripbtn_SpectrumWindowed.Image")));
            this.toolStripbtn_SpectrumWindowed.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtn_SpectrumWindowed.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.toolStripbtn_SpectrumWindowed.Name = "toolStripbtn_SpectrumWindowed";
            this.toolStripbtn_SpectrumWindowed.Size = new System.Drawing.Size(104, 28);
            this.toolStripbtn_SpectrumWindowed.Text = "Apply on Spectrum";
            this.toolStripbtn_SpectrumWindowed.Click += new System.EventHandler(this.toolStripbtn_SpectrumWindowed_Click);
            // 
            // toolStripbtn_YAisScale
            // 
            this.toolStripbtn_YAisScale.BackColor = System.Drawing.Color.MidnightBlue;
            this.toolStripbtn_YAisScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripbtn_YAisScale.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripbtn_YAisScale.ForeColor = System.Drawing.Color.White;
            this.toolStripbtn_YAisScale.Image = ((System.Drawing.Image)(resources.GetObject("toolStripbtn_YAisScale.Image")));
            this.toolStripbtn_YAisScale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtn_YAisScale.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.toolStripbtn_YAisScale.Name = "toolStripbtn_YAisScale";
            this.toolStripbtn_YAisScale.Size = new System.Drawing.Size(56, 28);
            this.toolStripbtn_YAisScale.Text = "Log scale";
            this.toolStripbtn_YAisScale.Click += new System.EventHandler(this.toolStripbtn_YAisScale_Click);
            // 
            // toolStripBtn_SavingData
            // 
            this.toolStripBtn_SavingData.BackColor = System.Drawing.Color.MidnightBlue;
            this.toolStripBtn_SavingData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBtn_SavingData.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripBtn_SavingData.ForeColor = System.Drawing.Color.White;
            this.toolStripBtn_SavingData.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtn_SavingData.Image")));
            this.toolStripBtn_SavingData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtn_SavingData.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.toolStripBtn_SavingData.Name = "toolStripBtn_SavingData";
            this.toolStripBtn_SavingData.Size = new System.Drawing.Size(78, 28);
            this.toolStripBtn_SavingData.Text = "Save All Data";
            this.toolStripBtn_SavingData.Click += new System.EventHandler(this.toolStripBtn_SavingData_Click);
            // 
            // Splitter
            // 
            this.Splitter.BackColor = System.Drawing.Color.Black;
            this.Splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splitter.Location = new System.Drawing.Point(0, 31);
            this.Splitter.Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.Splitter.Name = "Splitter";
            // 
            // Splitter.Panel1
            // 
            this.Splitter.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.Splitter.Panel1.Controls.Add(this.SubSplitContainer);
            this.Splitter.Panel1.Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            // 
            // Splitter.Panel2
            // 
            this.Splitter.Panel2.BackColor = System.Drawing.Color.Black;
            this.Splitter.Panel2.Controls.Add(this.btn_RemoveAllLines);
            this.Splitter.Panel2.Controls.Add(this.dgvplots);
            this.Splitter.Panel2.Margin = new System.Windows.Forms.Padding(11, 12, 0, 12);
            this.Splitter.Panel2.Padding = new System.Windows.Forms.Padding(0, 6, 6, 6);
            this.Splitter.Size = new System.Drawing.Size(922, 298);
            this.Splitter.SplitterDistance = 771;
            this.Splitter.TabIndex = 3;
            // 
            // SubSplitContainer
            // 
            this.SubSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SubSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SubSplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.SubSplitContainer.Name = "SubSplitContainer";
            // 
            // SubSplitContainer.Panel1
            // 
            this.SubSplitContainer.Panel1.BackColor = System.Drawing.Color.Black;
            this.SubSplitContainer.Panel1.Controls.Add(this.panel_DataPlot);
            this.SubSplitContainer.Panel1.ForeColor = System.Drawing.Color.White;
            this.SubSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            // 
            // SubSplitContainer.Panel2
            // 
            this.SubSplitContainer.Panel2.BackColor = System.Drawing.Color.Black;
            this.SubSplitContainer.Panel2.Controls.Add(this.panel_Spectrum);
            this.SubSplitContainer.Panel2.ForeColor = System.Drawing.Color.White;
            this.SubSplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.SubSplitContainer.Size = new System.Drawing.Size(771, 298);
            this.SubSplitContainer.SplitterDistance = 382;
            this.SubSplitContainer.SplitterWidth = 11;
            this.SubSplitContainer.TabIndex = 0;
            // 
            // panel_DataPlot
            // 
            this.panel_DataPlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(112)))));
            this.panel_DataPlot.Controls.Add(this.label_FPS);
            this.panel_DataPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_DataPlot.Location = new System.Drawing.Point(6, 6);
            this.panel_DataPlot.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel_DataPlot.Name = "panel_DataPlot";
            this.panel_DataPlot.Size = new System.Drawing.Size(370, 286);
            this.panel_DataPlot.TabIndex = 1;
            // 
            // label_FPS
            // 
            this.label_FPS.AutoSize = true;
            this.label_FPS.BackColor = System.Drawing.Color.White;
            this.label_FPS.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_FPS.ForeColor = System.Drawing.Color.Black;
            this.label_FPS.Location = new System.Drawing.Point(2, 2);
            this.label_FPS.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_FPS.Name = "label_FPS";
            this.label_FPS.Size = new System.Drawing.Size(15, 15);
            this.label_FPS.TabIndex = 0;
            this.label_FPS.Text = "0";
            this.label_FPS.Visible = false;
            // 
            // panel_Spectrum
            // 
            this.panel_Spectrum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(112)))));
            this.panel_Spectrum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Spectrum.ForeColor = System.Drawing.Color.White;
            this.panel_Spectrum.Location = new System.Drawing.Point(6, 6);
            this.panel_Spectrum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel_Spectrum.Name = "panel_Spectrum";
            this.panel_Spectrum.Size = new System.Drawing.Size(366, 286);
            this.panel_Spectrum.TabIndex = 0;
            this.panel_Spectrum.SizeChanged += new System.EventHandler(this.panel_Spectrum_SizeChanged);
            this.panel_Spectrum.Resize += new System.EventHandler(this.FrmResize);
            // 
            // btn_RemoveAllLines
            // 
            this.btn_RemoveAllLines.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btn_RemoveAllLines.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_RemoveAllLines.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_RemoveAllLines.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RemoveAllLines.Location = new System.Drawing.Point(0, 272);
            this.btn_RemoveAllLines.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_RemoveAllLines.Name = "btn_RemoveAllLines";
            this.btn_RemoveAllLines.Size = new System.Drawing.Size(141, 20);
            this.btn_RemoveAllLines.TabIndex = 1;
            this.btn_RemoveAllLines.Text = "Remove all lines";
            this.btn_RemoveAllLines.UseVisualStyleBackColor = false;
            this.btn_RemoveAllLines.Click += new System.EventHandler(this.btn_RemoveAllLines_Click);
            // 
            // dgvplots
            // 
            this.dgvplots.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvplots.AllowUserToResizeColumns = false;
            this.dgvplots.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Indigo;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 7.8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvplots.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvplots.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(112)))));
            this.dgvplots.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvplots.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvplots.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvplots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvplots.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VisibleSereis,
            this.Signal});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(112)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvplots.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvplots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvplots.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvplots.EnableHeadersVisualStyles = false;
            this.dgvplots.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.dgvplots.Location = new System.Drawing.Point(0, 6);
            this.dgvplots.Margin = new System.Windows.Forms.Padding(0);
            this.dgvplots.Name = "dgvplots";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(112)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvplots.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvplots.RowHeadersVisible = false;
            this.dgvplots.RowHeadersWidth = 51;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(112)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvplots.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvplots.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.dgvplots.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dgvplots.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvplots.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvplots.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvplots.Size = new System.Drawing.Size(141, 286);
            this.dgvplots.TabIndex = 0;
            this.dgvplots.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvplots_CellValueChanged);
            // 
            // VisibleSereis
            // 
            this.VisibleSereis.FalseValue = "F";
            this.VisibleSereis.HeaderText = "Visible";
            this.VisibleSereis.MinimumWidth = 6;
            this.VisibleSereis.Name = "VisibleSereis";
            this.VisibleSereis.TrueValue = "T";
            this.VisibleSereis.Width = 80;
            // 
            // Signal
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.Signal.DefaultCellStyle = dataGridViewCellStyle3;
            this.Signal.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Signal.HeaderText = "Name";
            this.Signal.MinimumWidth = 6;
            this.Signal.Name = "Signal";
            this.Signal.Width = 155;
            // 
            // DataPlotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 329);
            this.Controls.Add(this.Splitter);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "DataPlotter";
            this.Text = "Serial Plotter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataPlotter_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.Splitter.Panel1.ResumeLayout(false);
            this.Splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Splitter)).EndInit();
            this.Splitter.ResumeLayout(false);
            this.SubSplitContainer.Panel1.ResumeLayout(false);
            this.SubSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SubSplitContainer)).EndInit();
            this.SubSplitContainer.ResumeLayout(false);
            this.panel_DataPlot.ResumeLayout(false);
            this.panel_DataPlot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvplots)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox TB_XAxisTime;
        private System.Windows.Forms.ToolStripButton btn_ShowNames;
        private System.Windows.Forms.SplitContainer Splitter;
        private System.Windows.Forms.DataGridView dgvplots;
        private System.Windows.Forms.ToolStripButton btn_PauseRun;
        private System.Windows.Forms.ToolStripButton BTN_ShowLegend;
        private SplitContainer SubSplitContainer;
        private ToolStripButton btn_ShowSpectrum;
        private DataGridViewCheckBoxColumn VisibleSereis;
        private DataGridViewComboBoxColumn Signal;
        private Panel panel_DataPlot;
        private Panel panel_Spectrum;
        private ToolStripLabel toolStripLabel_windows;
        private ToolStripComboBox toolStripComboBox_WindowItems;
        private ToolStripButton toolStripBtn_FPS;
        private Label label_FPS;
        private ToolStripButton toolStripbtn_SpectrumWindowed;
        private Button btn_RemoveAllLines;
        private ToolStripButton toolStripbtn_YAisScale;
        private ToolStripButton toolStripBtn_SavingData;
    }
}