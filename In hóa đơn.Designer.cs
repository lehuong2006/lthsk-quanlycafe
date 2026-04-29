namespace QLQuanCafe
{
    partial class frm_InHoaDon
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
            this.HoaDonView1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // HoaDonView1
            // 
            this.HoaDonView1.ActiveViewIndex = -1;
            this.HoaDonView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HoaDonView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.HoaDonView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HoaDonView1.Location = new System.Drawing.Point(0, 0);
            this.HoaDonView1.Name = "HoaDonView1";
            this.HoaDonView1.Size = new System.Drawing.Size(800, 450);
            this.HoaDonView1.TabIndex = 0;
            this.HoaDonView1.Load += new System.EventHandler(this.HoaDonView1_Load);
            // 
            // frm_InHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.HoaDonView1);
            this.Name = "frm_InHoaDon";
            this.Text = "IN HÓA ĐƠN";
            this.Load += new System.EventHandler(this.frm_InHoaDon_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer HoaDonView1;
    }
}