namespace QLQuanCafe
{
    partial class Form2
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
            this.BaoCaoNhanVienView1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // BaoCaoNhanVienView1
            // 
            this.BaoCaoNhanVienView1.ActiveViewIndex = -1;
            this.BaoCaoNhanVienView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BaoCaoNhanVienView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.BaoCaoNhanVienView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaoCaoNhanVienView1.Location = new System.Drawing.Point(0, 0);
            this.BaoCaoNhanVienView1.Name = "BaoCaoNhanVienView1";
            this.BaoCaoNhanVienView1.Size = new System.Drawing.Size(800, 450);
            this.BaoCaoNhanVienView1.TabIndex = 0;
            this.BaoCaoNhanVienView1.Load += new System.EventHandler(this.BaoCaoNhanVienView1_Load);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BaoCaoNhanVienView1);
            this.Name = "Form2";
            this.Text = "BÁO CÁO NHÂN VIÊN";
            this.ResumeLayout(false);

        }

        #endregion

    }
}