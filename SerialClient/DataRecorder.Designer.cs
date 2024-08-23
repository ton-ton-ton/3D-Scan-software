namespace SerialClient
{
    partial class DataRecorder
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BTN_RECORD = new System.Windows.Forms.Button();
            this.lbl_status = new System.Windows.Forms.Label();
            this.lbl_time = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_sampleHz = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(71, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Time";
            // 
            // BTN_RECORD
            // 
            this.BTN_RECORD.Location = new System.Drawing.Point(76, 95);
            this.BTN_RECORD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BTN_RECORD.Name = "BTN_RECORD";
            this.BTN_RECORD.Size = new System.Drawing.Size(133, 32);
            this.BTN_RECORD.TabIndex = 2;
            this.BTN_RECORD.Text = "Record";
            this.BTN_RECORD.UseVisualStyleBackColor = true;
            this.BTN_RECORD.Click += new System.EventHandler(this.BTN_RECORD_Click);
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_status.Location = new System.Drawing.Point(126, 10);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(98, 20);
            this.lbl_status.TabIndex = 3;
            this.lbl_status.Text = "Not Started.";
            // 
            // lbl_time
            // 
            this.lbl_time.AutoSize = true;
            this.lbl_time.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_time.Location = new System.Drawing.Point(126, 35);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(50, 20);
            this.lbl_time.TabIndex = 4;
            this.lbl_time.Text = "00:00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(212, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Hz";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Sample Freq";
            // 
            // cmb_sampleHz
            // 
            this.cmb_sampleHz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_sampleHz.FormattingEnabled = true;
            this.cmb_sampleHz.Items.AddRange(new object[] {
            "0.05",
            "0.1",
            "0.2",
            "0.5",
            "1",
            "2",
            "5",
            "10",
            "20",
            "50"});
            this.cmb_sampleHz.Location = new System.Drawing.Point(126, 62);
            this.cmb_sampleHz.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_sampleHz.Name = "cmb_sampleHz";
            this.cmb_sampleHz.Size = new System.Drawing.Size(80, 23);
            this.cmb_sampleHz.TabIndex = 8;
            // 
            // DataRecorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 136);
            this.Controls.Add(this.cmb_sampleHz);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_time);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.BTN_RECORD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataRecorder";
            this.Text = "DataRecorder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataRecorder_FormClosing);
            this.Load += new System.EventHandler(this.DataRecorder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BTN_RECORD;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Label lbl_time;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_sampleHz;
    }
}