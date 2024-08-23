
namespace UserControlEditor
{
    partial class SubMenu1_Scan
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
            this.panelSubconnect = new System.Windows.Forms.Panel();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.ScalarBar = new System.Windows.Forms.CheckBox();
            this.iconBtn_Reset = new FontAwesome.Sharp.IconButton();
            this.iconbtn_Pause = new FontAwesome.Sharp.IconButton();
            this.iconbtn_Scan = new FontAwesome.Sharp.IconButton();
            this.panelSubCalibration = new System.Windows.Forms.Panel();
            this.label_lightness = new System.Windows.Forms.Label();
            this.trackBar_LEDIntensity = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.iconBtn_MinBoundDown = new FontAwesome.Sharp.IconButton();
            this.iconBtn_MinBoundUP = new FontAwesome.Sharp.IconButton();
            this.label2 = new System.Windows.Forms.Label();
            this.iconBtn_ShutterDown = new FontAwesome.Sharp.IconButton();
            this.iconBtn_ShutterUP = new FontAwesome.Sharp.IconButton();
            this.label_Shutter = new System.Windows.Forms.Label();
            this.iconBtn_liftDetecDown = new FontAwesome.Sharp.IconButton();
            this.label_LiftDetec = new System.Windows.Forms.Label();
            this.iconBtn_liftDetecUP = new FontAwesome.Sharp.IconButton();
            this.iconBtn_RestartSensor = new FontAwesome.Sharp.IconButton();
            this.iconBtn_SensorMidBtn = new FontAwesome.Sharp.IconButton();
            this.btnMinimizwEditor = new FontAwesome.Sharp.IconButton();
            this.panelConnect = new System.Windows.Forms.Panel();
            this.comboBox_ScanMode = new System.Windows.Forms.ComboBox();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.panelCalibration = new System.Windows.Forms.Panel();
            this.iconBtn_SensorCtr = new FontAwesome.Sharp.IconButton();
            this.panelSubconnect.SuspendLayout();
            this.panelSubCalibration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_LEDIntensity)).BeginInit();
            this.panelConnect.SuspendLayout();
            this.panelCalibration.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSubconnect
            // 
            this.panelSubconnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.panelSubconnect.Controls.Add(this.iconButton2);
            this.panelSubconnect.Controls.Add(this.ScalarBar);
            this.panelSubconnect.Controls.Add(this.iconBtn_Reset);
            this.panelSubconnect.Controls.Add(this.iconbtn_Pause);
            this.panelSubconnect.Controls.Add(this.iconbtn_Scan);
            this.panelSubconnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSubconnect.Location = new System.Drawing.Point(0, 50);
            this.panelSubconnect.Margin = new System.Windows.Forms.Padding(0);
            this.panelSubconnect.Name = "panelSubconnect";
            this.panelSubconnect.Size = new System.Drawing.Size(347, 122);
            this.panelSubconnect.TabIndex = 17;
            // 
            // iconButton2
            // 
            this.iconButton2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.iconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton2.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.iconButton2.ForeColor = System.Drawing.SystemColors.Control;
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconButton2.IconColor = System.Drawing.Color.Black;
            this.iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton2.Location = new System.Drawing.Point(16, 59);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Size = new System.Drawing.Size(166, 60);
            this.iconButton2.TabIndex = 8;
            this.iconButton2.Text = "Save and Reset";
            this.iconButton2.UseVisualStyleBackColor = true;
            this.iconButton2.Click += new System.EventHandler(this.iconButton2_Click_1);
            // 
            // ScalarBar
            // 
            this.ScalarBar.AutoSize = true;
            this.ScalarBar.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.ScalarBar.ForeColor = System.Drawing.Color.White;
            this.ScalarBar.Location = new System.Drawing.Point(209, 79);
            this.ScalarBar.Name = "ScalarBar";
            this.ScalarBar.Size = new System.Drawing.Size(96, 22);
            this.ScalarBar.TabIndex = 7;
            this.ScalarBar.Text = "Scalar Bar";
            this.ScalarBar.UseVisualStyleBackColor = true;
            this.ScalarBar.CheckedChanged += new System.EventHandler(this.ScalarBar_CheckedChanged);
            // 
            // iconBtn_Reset
            // 
            this.iconBtn_Reset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.iconBtn_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtn_Reset.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.iconBtn_Reset.ForeColor = System.Drawing.SystemColors.Control;
            this.iconBtn_Reset.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconBtn_Reset.IconColor = System.Drawing.Color.Black;
            this.iconBtn_Reset.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtn_Reset.Location = new System.Drawing.Point(230, 20);
            this.iconBtn_Reset.Name = "iconBtn_Reset";
            this.iconBtn_Reset.Size = new System.Drawing.Size(75, 30);
            this.iconBtn_Reset.TabIndex = 6;
            this.iconBtn_Reset.Text = "Reset";
            this.iconBtn_Reset.UseVisualStyleBackColor = true;
            this.iconBtn_Reset.Click += new System.EventHandler(this.iconBtn_Reset_Click);
            // 
            // iconbtn_Pause
            // 
            this.iconbtn_Pause.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.iconbtn_Pause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconbtn_Pause.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.iconbtn_Pause.ForeColor = System.Drawing.SystemColors.Control;
            this.iconbtn_Pause.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconbtn_Pause.IconColor = System.Drawing.Color.Black;
            this.iconbtn_Pause.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconbtn_Pause.Location = new System.Drawing.Point(139, 20);
            this.iconbtn_Pause.Name = "iconbtn_Pause";
            this.iconbtn_Pause.Size = new System.Drawing.Size(75, 30);
            this.iconbtn_Pause.TabIndex = 5;
            this.iconbtn_Pause.Text = "Pause";
            this.iconbtn_Pause.UseVisualStyleBackColor = true;
            this.iconbtn_Pause.Click += new System.EventHandler(this.iconbtn_Pause_Click);
            // 
            // iconbtn_Scan
            // 
            this.iconbtn_Scan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconbtn_Scan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.iconbtn_Scan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconbtn_Scan.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.iconbtn_Scan.ForeColor = System.Drawing.SystemColors.Control;
            this.iconbtn_Scan.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconbtn_Scan.IconColor = System.Drawing.Color.Black;
            this.iconbtn_Scan.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconbtn_Scan.Location = new System.Drawing.Point(16, 20);
            this.iconbtn_Scan.Name = "iconbtn_Scan";
            this.iconbtn_Scan.Size = new System.Drawing.Size(108, 30);
            this.iconbtn_Scan.TabIndex = 4;
            this.iconbtn_Scan.Text = "Start to Scan";
            this.iconbtn_Scan.UseVisualStyleBackColor = false;
            this.iconbtn_Scan.Click += new System.EventHandler(this.BtnScanStart_Click);
            // 
            // panelSubCalibration
            // 
            this.panelSubCalibration.Controls.Add(this.label_lightness);
            this.panelSubCalibration.Controls.Add(this.trackBar_LEDIntensity);
            this.panelSubCalibration.Controls.Add(this.label1);
            this.panelSubCalibration.Controls.Add(this.iconBtn_MinBoundDown);
            this.panelSubCalibration.Controls.Add(this.iconBtn_MinBoundUP);
            this.panelSubCalibration.Controls.Add(this.label2);
            this.panelSubCalibration.Controls.Add(this.iconBtn_ShutterDown);
            this.panelSubCalibration.Controls.Add(this.iconBtn_ShutterUP);
            this.panelSubCalibration.Controls.Add(this.label_Shutter);
            this.panelSubCalibration.Controls.Add(this.iconBtn_liftDetecDown);
            this.panelSubCalibration.Controls.Add(this.label_LiftDetec);
            this.panelSubCalibration.Controls.Add(this.iconBtn_liftDetecUP);
            this.panelSubCalibration.Controls.Add(this.iconBtn_RestartSensor);
            this.panelSubCalibration.Controls.Add(this.iconBtn_SensorMidBtn);
            this.panelSubCalibration.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSubCalibration.Location = new System.Drawing.Point(0, 222);
            this.panelSubCalibration.Name = "panelSubCalibration";
            this.panelSubCalibration.Size = new System.Drawing.Size(347, 276);
            this.panelSubCalibration.TabIndex = 16;
            // 
            // label_lightness
            // 
            this.label_lightness.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_lightness.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_lightness.ForeColor = System.Drawing.Color.White;
            this.label_lightness.Location = new System.Drawing.Point(307, 186);
            this.label_lightness.Name = "label_lightness";
            this.label_lightness.Size = new System.Drawing.Size(53, 20);
            this.label_lightness.TabIndex = 18;
            this.label_lightness.Text = "0";
            this.label_lightness.Visible = false;
            // 
            // trackBar_LEDIntensity
            // 
            this.trackBar_LEDIntensity.LargeChange = 1;
            this.trackBar_LEDIntensity.Location = new System.Drawing.Point(164, 182);
            this.trackBar_LEDIntensity.Maximum = 41;
            this.trackBar_LEDIntensity.Name = "trackBar_LEDIntensity";
            this.trackBar_LEDIntensity.Size = new System.Drawing.Size(141, 56);
            this.trackBar_LEDIntensity.TabIndex = 17;
            this.trackBar_LEDIntensity.TickFrequency = 5;
            this.trackBar_LEDIntensity.Value = 40;
            this.trackBar_LEDIntensity.ValueChanged += new System.EventHandler(this.trackBar_LASERIntensity_ValueChanged);
            this.trackBar_LEDIntensity.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_LASERIntensity_MouseUp);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(31, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 18);
            this.label1.TabIndex = 16;
            this.label1.Text = "CPI";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // iconBtn_MinBoundDown
            // 
            this.iconBtn_MinBoundDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.iconBtn_MinBoundDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconBtn_MinBoundDown.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.iconBtn_MinBoundDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtn_MinBoundDown.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.iconBtn_MinBoundDown.ForeColor = System.Drawing.SystemColors.Control;
            this.iconBtn_MinBoundDown.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconBtn_MinBoundDown.IconColor = System.Drawing.Color.Black;
            this.iconBtn_MinBoundDown.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtn_MinBoundDown.Location = new System.Drawing.Point(243, 138);
            this.iconBtn_MinBoundDown.Name = "iconBtn_MinBoundDown";
            this.iconBtn_MinBoundDown.Size = new System.Drawing.Size(62, 24);
            this.iconBtn_MinBoundDown.TabIndex = 15;
            this.iconBtn_MinBoundDown.Text = "▼";
            this.iconBtn_MinBoundDown.UseVisualStyleBackColor = false;
            this.iconBtn_MinBoundDown.Click += new System.EventHandler(this.iconBtn_MinBoundDown_Click);
            // 
            // iconBtn_MinBoundUP
            // 
            this.iconBtn_MinBoundUP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.iconBtn_MinBoundUP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconBtn_MinBoundUP.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.iconBtn_MinBoundUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtn_MinBoundUP.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.iconBtn_MinBoundUP.ForeColor = System.Drawing.SystemColors.Control;
            this.iconBtn_MinBoundUP.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconBtn_MinBoundUP.IconColor = System.Drawing.Color.Black;
            this.iconBtn_MinBoundUP.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtn_MinBoundUP.Location = new System.Drawing.Point(164, 138);
            this.iconBtn_MinBoundUP.Name = "iconBtn_MinBoundUP";
            this.iconBtn_MinBoundUP.Size = new System.Drawing.Size(62, 24);
            this.iconBtn_MinBoundUP.TabIndex = 14;
            this.iconBtn_MinBoundUP.Text = "▲";
            this.iconBtn_MinBoundUP.UseVisualStyleBackColor = false;
            this.iconBtn_MinBoundUP.Click += new System.EventHandler(this.iconBtn_MinBoundUP_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(37, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 36);
            this.label2.TabIndex = 13;
            this.label2.Text = "Frame Period \r\n  Min Bound";
            // 
            // iconBtn_ShutterDown
            // 
            this.iconBtn_ShutterDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.iconBtn_ShutterDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconBtn_ShutterDown.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.iconBtn_ShutterDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtn_ShutterDown.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.iconBtn_ShutterDown.ForeColor = System.Drawing.SystemColors.Control;
            this.iconBtn_ShutterDown.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconBtn_ShutterDown.IconColor = System.Drawing.Color.Black;
            this.iconBtn_ShutterDown.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtn_ShutterDown.Location = new System.Drawing.Point(243, 99);
            this.iconBtn_ShutterDown.Name = "iconBtn_ShutterDown";
            this.iconBtn_ShutterDown.Size = new System.Drawing.Size(62, 25);
            this.iconBtn_ShutterDown.TabIndex = 12;
            this.iconBtn_ShutterDown.Text = "▼";
            this.iconBtn_ShutterDown.UseVisualStyleBackColor = false;
            this.iconBtn_ShutterDown.Click += new System.EventHandler(this.iconBtn_ShutterDown_Click);
            // 
            // iconBtn_ShutterUP
            // 
            this.iconBtn_ShutterUP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.iconBtn_ShutterUP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconBtn_ShutterUP.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.iconBtn_ShutterUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtn_ShutterUP.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.iconBtn_ShutterUP.ForeColor = System.Drawing.SystemColors.Control;
            this.iconBtn_ShutterUP.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconBtn_ShutterUP.IconColor = System.Drawing.Color.Black;
            this.iconBtn_ShutterUP.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtn_ShutterUP.Location = new System.Drawing.Point(164, 100);
            this.iconBtn_ShutterUP.Name = "iconBtn_ShutterUP";
            this.iconBtn_ShutterUP.Size = new System.Drawing.Size(62, 24);
            this.iconBtn_ShutterUP.TabIndex = 11;
            this.iconBtn_ShutterUP.Text = "▲";
            this.iconBtn_ShutterUP.UseVisualStyleBackColor = false;
            this.iconBtn_ShutterUP.Click += new System.EventHandler(this.iconBtn_ShutterUP_Click);
            // 
            // label_Shutter
            // 
            this.label_Shutter.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.label_Shutter.ForeColor = System.Drawing.Color.White;
            this.label_Shutter.Location = new System.Drawing.Point(38, 104);
            this.label_Shutter.Name = "label_Shutter";
            this.label_Shutter.Size = new System.Drawing.Size(106, 18);
            this.label_Shutter.TabIndex = 10;
            this.label_Shutter.Text = "Shutter Bound";
            // 
            // iconBtn_liftDetecDown
            // 
            this.iconBtn_liftDetecDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.iconBtn_liftDetecDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconBtn_liftDetecDown.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.iconBtn_liftDetecDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtn_liftDetecDown.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.iconBtn_liftDetecDown.ForeColor = System.Drawing.SystemColors.Control;
            this.iconBtn_liftDetecDown.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconBtn_liftDetecDown.IconColor = System.Drawing.Color.Black;
            this.iconBtn_liftDetecDown.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtn_liftDetecDown.Location = new System.Drawing.Point(243, 58);
            this.iconBtn_liftDetecDown.Name = "iconBtn_liftDetecDown";
            this.iconBtn_liftDetecDown.Size = new System.Drawing.Size(62, 24);
            this.iconBtn_liftDetecDown.TabIndex = 9;
            this.iconBtn_liftDetecDown.Text = "▼";
            this.iconBtn_liftDetecDown.UseVisualStyleBackColor = false;
            this.iconBtn_liftDetecDown.Click += new System.EventHandler(this.iconBtn_liftDetecDown_Click);
            // 
            // label_LiftDetec
            // 
            this.label_LiftDetec.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.label_LiftDetec.ForeColor = System.Drawing.Color.White;
            this.label_LiftDetec.Location = new System.Drawing.Point(38, 64);
            this.label_LiftDetec.Name = "label_LiftDetec";
            this.label_LiftDetec.Size = new System.Drawing.Size(99, 18);
            this.label_LiftDetec.TabIndex = 8;
            this.label_LiftDetec.Text = "Lift Detection";
            // 
            // iconBtn_liftDetecUP
            // 
            this.iconBtn_liftDetecUP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.iconBtn_liftDetecUP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconBtn_liftDetecUP.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.iconBtn_liftDetecUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtn_liftDetecUP.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.iconBtn_liftDetecUP.ForeColor = System.Drawing.SystemColors.Control;
            this.iconBtn_liftDetecUP.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconBtn_liftDetecUP.IconColor = System.Drawing.Color.Black;
            this.iconBtn_liftDetecUP.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtn_liftDetecUP.Location = new System.Drawing.Point(164, 58);
            this.iconBtn_liftDetecUP.Name = "iconBtn_liftDetecUP";
            this.iconBtn_liftDetecUP.Size = new System.Drawing.Size(62, 24);
            this.iconBtn_liftDetecUP.TabIndex = 7;
            this.iconBtn_liftDetecUP.Text = "▲";
            this.iconBtn_liftDetecUP.UseVisualStyleBackColor = false;
            this.iconBtn_liftDetecUP.Click += new System.EventHandler(this.iconBtn_liftDetecUP_Click);
            // 
            // iconBtn_RestartSensor
            // 
            this.iconBtn_RestartSensor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.iconBtn_RestartSensor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconBtn_RestartSensor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.iconBtn_RestartSensor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtn_RestartSensor.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.iconBtn_RestartSensor.ForeColor = System.Drawing.SystemColors.Control;
            this.iconBtn_RestartSensor.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconBtn_RestartSensor.IconColor = System.Drawing.Color.Black;
            this.iconBtn_RestartSensor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtn_RestartSensor.Location = new System.Drawing.Point(195, 18);
            this.iconBtn_RestartSensor.Name = "iconBtn_RestartSensor";
            this.iconBtn_RestartSensor.Size = new System.Drawing.Size(110, 24);
            this.iconBtn_RestartSensor.TabIndex = 6;
            this.iconBtn_RestartSensor.Text = "Restart";
            this.iconBtn_RestartSensor.UseVisualStyleBackColor = false;
            this.iconBtn_RestartSensor.Click += new System.EventHandler(this.iconBtn_RestartSensor_Click);
            // 
            // iconBtn_SensorMidBtn
            // 
            this.iconBtn_SensorMidBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.iconBtn_SensorMidBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.iconBtn_SensorMidBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.iconBtn_SensorMidBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtn_SensorMidBtn.Font = new System.Drawing.Font("微軟正黑體", 7.8F);
            this.iconBtn_SensorMidBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.iconBtn_SensorMidBtn.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconBtn_SensorMidBtn.IconColor = System.Drawing.Color.Black;
            this.iconBtn_SensorMidBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtn_SensorMidBtn.Location = new System.Drawing.Point(34, 18);
            this.iconBtn_SensorMidBtn.Name = "iconBtn_SensorMidBtn";
            this.iconBtn_SensorMidBtn.Size = new System.Drawing.Size(119, 24);
            this.iconBtn_SensorMidBtn.TabIndex = 5;
            this.iconBtn_SensorMidBtn.Text = "Turn On ";
            this.iconBtn_SensorMidBtn.UseVisualStyleBackColor = false;
            this.iconBtn_SensorMidBtn.Click += new System.EventHandler(this.iconBtn_SensorMidBtn_Click);
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
            this.btnMinimizwEditor.Location = new System.Drawing.Point(347, 0);
            this.btnMinimizwEditor.Margin = new System.Windows.Forms.Padding(0);
            this.btnMinimizwEditor.Name = "btnMinimizwEditor";
            this.btnMinimizwEditor.Size = new System.Drawing.Size(21, 551);
            this.btnMinimizwEditor.TabIndex = 15;
            this.btnMinimizwEditor.UseMnemonic = false;
            this.btnMinimizwEditor.UseVisualStyleBackColor = false;
            this.btnMinimizwEditor.Click += new System.EventHandler(this.BtnMinimizwEditor_Click);
            // 
            // panelConnect
            // 
            this.panelConnect.Controls.Add(this.comboBox_ScanMode);
            this.panelConnect.Controls.Add(this.iconButton1);
            this.panelConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelConnect.Location = new System.Drawing.Point(0, 0);
            this.panelConnect.Name = "panelConnect";
            this.panelConnect.Size = new System.Drawing.Size(347, 50);
            this.panelConnect.TabIndex = 18;
            // 
            // comboBox_ScanMode
            // 
            this.comboBox_ScanMode.AutoCompleteCustomSource.AddRange(new string[] {
            "2-D",
            "3-D",
            "IMU-Tracking"});
            this.comboBox_ScanMode.BackColor = System.Drawing.Color.Indigo;
            this.comboBox_ScanMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_ScanMode.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_ScanMode.ForeColor = System.Drawing.Color.White;
            this.comboBox_ScanMode.FormattingEnabled = true;
            this.comboBox_ScanMode.Location = new System.Drawing.Point(121, 14);
            this.comboBox_ScanMode.MaxDropDownItems = 3;
            this.comboBox_ScanMode.Name = "comboBox_ScanMode";
            this.comboBox_ScanMode.Size = new System.Drawing.Size(74, 23);
            this.comboBox_ScanMode.TabIndex = 8;
            this.comboBox_ScanMode.SelectedIndexChanged += new System.EventHandler(this.comboBox_ScanMode_SelectedIndexChanged);
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
            this.iconButton1.Size = new System.Drawing.Size(347, 50);
            this.iconButton1.TabIndex = 0;
            this.iconButton1.Text = "Scannig Mode";
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // panelCalibration
            // 
            this.panelCalibration.Controls.Add(this.iconBtn_SensorCtr);
            this.panelCalibration.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCalibration.Location = new System.Drawing.Point(0, 172);
            this.panelCalibration.Name = "panelCalibration";
            this.panelCalibration.Size = new System.Drawing.Size(347, 50);
            this.panelCalibration.TabIndex = 12;
            // 
            // iconBtn_SensorCtr
            // 
            this.iconBtn_SensorCtr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.iconBtn_SensorCtr.BackColor = System.Drawing.Color.Indigo;
            this.iconBtn_SensorCtr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconBtn_SensorCtr.FlatAppearance.BorderSize = 0;
            this.iconBtn_SensorCtr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtn_SensorCtr.Font = new System.Drawing.Font("微軟正黑體", 8F);
            this.iconBtn_SensorCtr.ForeColor = System.Drawing.SystemColors.Window;
            this.iconBtn_SensorCtr.IconChar = FontAwesome.Sharp.IconChar.D;
            this.iconBtn_SensorCtr.IconColor = System.Drawing.Color.White;
            this.iconBtn_SensorCtr.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtn_SensorCtr.IconSize = 10;
            this.iconBtn_SensorCtr.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconBtn_SensorCtr.Location = new System.Drawing.Point(0, 0);
            this.iconBtn_SensorCtr.Margin = new System.Windows.Forms.Padding(0);
            this.iconBtn_SensorCtr.Name = "iconBtn_SensorCtr";
            this.iconBtn_SensorCtr.Size = new System.Drawing.Size(347, 50);
            this.iconBtn_SensorCtr.TabIndex = 1;
            this.iconBtn_SensorCtr.Text = "Sensor Control";
            this.iconBtn_SensorCtr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconBtn_SensorCtr.UseVisualStyleBackColor = false;
            this.iconBtn_SensorCtr.Click += new System.EventHandler(this.iconButton2_Click);
            // 
            // SubMenu1_Scan
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(86)))));
            this.Controls.Add(this.panelSubCalibration);
            this.Controls.Add(this.panelCalibration);
            this.Controls.Add(this.panelSubconnect);
            this.Controls.Add(this.panelConnect);
            this.Controls.Add(this.btnMinimizwEditor);
            this.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "SubMenu1_Scan";
            this.Size = new System.Drawing.Size(368, 551);
            this.panelSubconnect.ResumeLayout(false);
            this.panelSubconnect.PerformLayout();
            this.panelSubCalibration.ResumeLayout(false);
            this.panelSubCalibration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_LEDIntensity)).EndInit();
            this.panelConnect.ResumeLayout(false);
            this.panelCalibration.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Panel panelSubconnect;
        private System.Windows.Forms.Panel panelSubCalibration;
        public FontAwesome.Sharp.IconButton btnMinimizwEditor;
        private System.Windows.Forms.Panel panelConnect;
        private FontAwesome.Sharp.IconButton iconButton1;
        private System.Windows.Forms.Panel panelCalibration;
        private FontAwesome.Sharp.IconButton iconBtn_SensorCtr;
        public FontAwesome.Sharp.IconButton iconbtn_Scan;
        public FontAwesome.Sharp.IconButton iconbtn_Pause;
        public FontAwesome.Sharp.IconButton iconBtn_Reset;
        public System.Windows.Forms.CheckBox ScalarBar;
        public FontAwesome.Sharp.IconButton iconBtn_SensorMidBtn;
        public FontAwesome.Sharp.IconButton iconBtn_RestartSensor;
        public FontAwesome.Sharp.IconButton iconBtn_liftDetecDown;
        private System.Windows.Forms.Label label_LiftDetec;
        public FontAwesome.Sharp.IconButton iconBtn_liftDetecUP;
        public FontAwesome.Sharp.IconButton iconBtn_ShutterDown;
        public FontAwesome.Sharp.IconButton iconBtn_ShutterUP;
        private System.Windows.Forms.Label label_Shutter;
        public FontAwesome.Sharp.IconButton iconBtn_MinBoundDown;
        public FontAwesome.Sharp.IconButton iconBtn_MinBoundUP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TrackBar trackBar_LEDIntensity;
        public System.Windows.Forms.Label label_lightness;
        public System.Windows.Forms.ComboBox comboBox_ScanMode;
        public FontAwesome.Sharp.IconButton iconButton2;
    }
}
