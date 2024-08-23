
namespace IOPointCloud
{
    partial class IOpcForm
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
            this.panel_PC = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel_PC
            // 
            this.panel_PC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_PC.Location = new System.Drawing.Point(0, 0);
            this.panel_PC.Name = "panel_PC";
            this.panel_PC.Size = new System.Drawing.Size(915, 520);
            this.panel_PC.TabIndex = 0;
            // 
            // IOpcForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 520);
            this.Controls.Add(this.panel_PC);
            this.Name = "IOpcForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_PC;
    }
}

