namespace JordyCodeGenerator
{
    partial class CodeGeneratorFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_start = new System.Windows.Forms.Button();
            this.cbb_conStr = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(216, 178);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "生成模型";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // cbb_conStr
            // 
            this.cbb_conStr.DisplayMember = "ConnectionName";
            this.cbb_conStr.FormattingEnabled = true;
            this.cbb_conStr.Location = new System.Drawing.Point(12, 12);
            this.cbb_conStr.Name = "cbb_conStr";
            this.cbb_conStr.Size = new System.Drawing.Size(189, 20);
            this.cbb_conStr.TabIndex = 1;
            this.cbb_conStr.ValueMember = "ProviderName";
            // 
            // CodeGeneratorFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 213);
            this.Controls.Add(this.cbb_conStr);
            this.Controls.Add(this.btn_start);
            this.Name = "CodeGeneratorFrm";
            this.Text = "代码生成器";
            this.Load += new System.EventHandler(this.CodeGeneratorFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.ComboBox cbb_conStr;
    }
}

