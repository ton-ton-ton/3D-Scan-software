
namespace SerialClient
{
    partial class DataRecord2txt
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
            this.Time_lbl = new System.Windows.Forms.Label();
            this.TimeClock_lbl = new System.Windows.Forms.Label();
            this.Nuber_lbl = new System.Windows.Forms.Label();
            this.PCNumber_lbl = new System.Windows.Forms.Label();
            this.btn_Record = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Time_lbl
            // 
            this.Time_lbl.AutoSize = true;
            this.Time_lbl.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.Time_lbl.ForeColor = System.Drawing.Color.White;
            this.Time_lbl.Location = new System.Drawing.Point(78, 22);
            this.Time_lbl.Name = "Time_lbl";
            this.Time_lbl.Size = new System.Drawing.Size(50, 22);
            this.Time_lbl.TabIndex = 0;
            this.Time_lbl.Text = "Time";
            // 
            // TimeClock_lbl
            // 
            this.TimeClock_lbl.AutoSize = true;
            this.TimeClock_lbl.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.TimeClock_lbl.ForeColor = System.Drawing.Color.White;
            this.TimeClock_lbl.Location = new System.Drawing.Point(246, 22);
            this.TimeClock_lbl.Name = "TimeClock_lbl";
            this.TimeClock_lbl.Size = new System.Drawing.Size(54, 22);
            this.TimeClock_lbl.TabIndex = 1;
            this.TimeClock_lbl.Text = "00:00";
            // 
            // Nuber_lbl
            // 
            this.Nuber_lbl.AutoSize = true;
            this.Nuber_lbl.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.Nuber_lbl.ForeColor = System.Drawing.Color.White;
            this.Nuber_lbl.Location = new System.Drawing.Point(12, 63);
            this.Nuber_lbl.Name = "Nuber_lbl";
            this.Nuber_lbl.Size = new System.Drawing.Size(181, 22);
            this.Nuber_lbl.TabIndex = 2;
            this.Nuber_lbl.Text = "Point Cloud Numbers";
            // 
            // PCNumber_lbl
            // 
            this.PCNumber_lbl.AutoSize = true;
            this.PCNumber_lbl.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.PCNumber_lbl.ForeColor = System.Drawing.Color.White;
            this.PCNumber_lbl.Location = new System.Drawing.Point(258, 63);
            this.PCNumber_lbl.Name = "PCNumber_lbl";
            this.PCNumber_lbl.Size = new System.Drawing.Size(20, 22);
            this.PCNumber_lbl.TabIndex = 3;
            this.PCNumber_lbl.Text = "0";
            // 
            // btn_Record
            // 
            this.btn_Record.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(45)))), ((int)(((byte)(112)))));
            this.btn_Record.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Record.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.btn_Record.ForeColor = System.Drawing.Color.White;
            this.btn_Record.Location = new System.Drawing.Point(42, 105);
            this.btn_Record.Name = "btn_Record";
            this.btn_Record.Size = new System.Drawing.Size(269, 30);
            this.btn_Record.TabIndex = 4;
            this.btn_Record.Text = "Record";
            this.btn_Record.UseVisualStyleBackColor = false;
            this.btn_Record.Click += new System.EventHandler(this.btn_Record_Click);
            // 
            // DataRecord2txt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(45)))), ((int)(((byte)(112)))));
            this.ClientSize = new System.Drawing.Size(346, 156);
            this.Controls.Add(this.btn_Record);
            this.Controls.Add(this.PCNumber_lbl);
            this.Controls.Add(this.Nuber_lbl);
            this.Controls.Add(this.TimeClock_lbl);
            this.Controls.Add(this.Time_lbl);
            this.Name = "DataRecord2txt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DataRecord2txt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Time_lbl;
        private System.Windows.Forms.Label TimeClock_lbl;
        private System.Windows.Forms.Label Nuber_lbl;
        private System.Windows.Forms.Label PCNumber_lbl;
        private System.Windows.Forms.Button btn_Record;
    }
}