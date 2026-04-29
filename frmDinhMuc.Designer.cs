namespace QuanLyCaPhe
{
    partial class frmDinhMuc
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblTenSPHienTai = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSanPham = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblTonKho = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnThemNhieu = new System.Windows.Forms.Button();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.txtDonVi = new System.Windows.Forms.TextBox();
            this.txtLuongDung = new System.Windows.Forms.TextBox();
            this.cbNguyenLieu = new System.Windows.Forms.ComboBox();
            this.dgvDinhMuc = new System.Windows.Forms.DataGridView();
            this.lblGiaVon = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvDanhSachSP = new System.Windows.Forms.DataGridView();
            this.txtTimKiemSanPham = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDinhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachSP)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblTenSPHienTai);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cbSanPham);
            this.groupBox3.Location = new System.Drawing.Point(650, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(830, 68);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông tin món đang chọn";
            // 
            // lblTenSPHienTai
            // 
            this.lblTenSPHienTai.AutoSize = true;
            this.lblTenSPHienTai.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenSPHienTai.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTenSPHienTai.Location = new System.Drawing.Point(136, 31);
            this.lblTenSPHienTai.Name = "lblTenSPHienTai";
            this.lblTenSPHienTai.Size = new System.Drawing.Size(130, 20);
            this.lblTenSPHienTai.TabIndex = 2;
            this.lblTenSPHienTai.Text = "Chưa chọn món";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Sản phẩm :";
            // 
            // cbSanPham
            // 
            this.cbSanPham.FormattingEnabled = true;
            this.cbSanPham.Location = new System.Drawing.Point(300, 28);
            this.cbSanPham.Name = "cbSanPham";
            this.cbSanPham.Size = new System.Drawing.Size(100, 24);
            this.cbSanPham.TabIndex = 0;
            this.cbSanPham.Visible = false;
            this.cbSanPham.SelectedIndexChanged += new System.EventHandler(this.cbSanPham_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblTonKho);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.panel2);
            this.groupBox4.Controls.Add(this.txtGhiChu);
            this.groupBox4.Controls.Add(this.txtDonVi);
            this.groupBox4.Controls.Add(this.txtLuongDung);
            this.groupBox4.Controls.Add(this.cbNguyenLieu);
            this.groupBox4.Location = new System.Drawing.Point(650, 86);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(830, 238);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Chi tiết định mức (Nguyên liệu)";
            // 
            // lblTonKho
            // 
            this.lblTonKho.AutoSize = true;
            this.lblTonKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTonKho.ForeColor = System.Drawing.Color.Blue;
            this.lblTonKho.Location = new System.Drawing.Point(408, 24);
            this.lblTonKho.Name = "lblTonKho";
            this.lblTonKho.Size = new System.Drawing.Size(86, 16);
            this.lblTonKho.TabIndex = 9;
            this.lblTonKho.Text = "Tồn kho: ???";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 16);
            this.label10.TabIndex = 8;
            this.label10.Text = "Ghi chú";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 16);
            this.label9.TabIndex = 7;
            this.label9.Text = "Đơn vị tính";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 16);
            this.label8.TabIndex = 6;
            this.label8.Text = "Lượng dùng";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Nguyên liệu";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnLamMoi);
            this.panel2.Controls.Add(this.btnXoa);
            this.panel2.Controls.Add(this.btnSua);
            this.panel2.Controls.Add(this.btnThemNhieu);
            this.panel2.Controls.Add(this.btnThem);
            this.panel2.Location = new System.Drawing.Point(19, 185);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(420, 31);
            this.panel2.TabIndex = 5;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(325, 5);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(80, 23);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(250, 5);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(70, 23);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(175, 5);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(70, 23);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThemNhieu
            // 
            this.btnThemNhieu.Location = new System.Drawing.Point(80, 5);
            this.btnThemNhieu.Name = "btnThemNhieu";
            this.btnThemNhieu.Size = new System.Drawing.Size(90, 23);
            this.btnThemNhieu.TabIndex = 4;
            this.btnThemNhieu.Text = "Thêm Nhiều";
            this.btnThemNhieu.UseVisualStyleBackColor = true;
            this.btnThemNhieu.Click += new System.EventHandler(this.btnThemNhieu_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(5, 5);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(70, 23);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(140, 127);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(645, 48);
            this.txtGhiChu.TabIndex = 4;
            // 
            // txtDonVi
            // 
            this.txtDonVi.Location = new System.Drawing.Point(140, 92);
            this.txtDonVi.Name = "txtDonVi";
            this.txtDonVi.Size = new System.Drawing.Size(262, 22);
            this.txtDonVi.TabIndex = 3;
            // 
            // txtLuongDung
            // 
            this.txtLuongDung.Location = new System.Drawing.Point(140, 57);
            this.txtLuongDung.Name = "txtLuongDung";
            this.txtLuongDung.Size = new System.Drawing.Size(262, 22);
            this.txtLuongDung.TabIndex = 2;
            // 
            // cbNguyenLieu
            // 
            this.cbNguyenLieu.FormattingEnabled = true;
            this.cbNguyenLieu.Location = new System.Drawing.Point(140, 21);
            this.cbNguyenLieu.Name = "cbNguyenLieu";
            this.cbNguyenLieu.Size = new System.Drawing.Size(262, 24);
            this.cbNguyenLieu.TabIndex = 1;
            this.cbNguyenLieu.SelectedIndexChanged += new System.EventHandler(this.cbNguyenLieu_SelectedIndexChanged);
            // 
            // dgvDinhMuc
            // 
            this.dgvDinhMuc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDinhMuc.Location = new System.Drawing.Point(650, 370);
            this.dgvDinhMuc.Name = "dgvDinhMuc";
            this.dgvDinhMuc.RowHeadersWidth = 51;
            this.dgvDinhMuc.RowTemplate.Height = 24;
            this.dgvDinhMuc.Size = new System.Drawing.Size(830, 220);
            this.dgvDinhMuc.TabIndex = 2;
            this.dgvDinhMuc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDinhMuc_CellClick);
            // 
            // lblGiaVon
            // 
            this.lblGiaVon.AutoSize = true;
            this.lblGiaVon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaVon.ForeColor = System.Drawing.Color.Red;
            this.lblGiaVon.Location = new System.Drawing.Point(650, 337);
            this.lblGiaVon.Name = "lblGiaVon";
            this.lblGiaVon.Size = new System.Drawing.Size(188, 18);
            this.lblGiaVon.TabIndex = 3;
            this.lblGiaVon.Text = "Giá vốn ước tính: 0 VNĐ";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(1323, 332);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(157, 30);
            this.btnCopy.TabIndex = 4;
            this.btnCopy.Text = "Sao chép công thức";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1347, 600);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 30);
            this.button1.TabIndex = 5;
            this.button1.Text = "In báo cáo công thức";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvDanhSachSP
            // 
            this.dgvDanhSachSP.AllowUserToAddRows = false;
            this.dgvDanhSachSP.AllowUserToDeleteRows = false;
            this.dgvDanhSachSP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachSP.Location = new System.Drawing.Point(12, 68);
            this.dgvDanhSachSP.Name = "dgvDanhSachSP";
            this.dgvDanhSachSP.ReadOnly = true;
            this.dgvDanhSachSP.RowHeadersWidth = 20;
            this.dgvDanhSachSP.RowTemplate.Height = 24;
            this.dgvDanhSachSP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachSP.Size = new System.Drawing.Size(620, 522);
            this.dgvDanhSachSP.TabIndex = 6;
            this.dgvDanhSachSP.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachSP_CellClick);
            // 
            // txtTimKiemSanPham
            // 
            this.txtTimKiemSanPham.Location = new System.Drawing.Point(85, 31);
            this.txtTimKiemSanPham.Name = "txtTimKiemSanPham";
            this.txtTimKiemSanPham.Size = new System.Drawing.Size(277, 22);
            this.txtTimKiemSanPham.TabIndex = 7;
            this.txtTimKiemSanPham.TextChanged += new System.EventHandler(this.txtTimKiemSanPham_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 34);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 16);
            this.label11.TabIndex = 8;
            this.label11.Text = "Tìm món:";
            // 
            // frmDinhMuc
            // 
            this.ClientSize = new System.Drawing.Size(1500, 642);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtTimKiemSanPham);
            this.Controls.Add(this.dgvDanhSachSP);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.lblGiaVon);
            this.Controls.Add(this.dgvDinhMuc);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Name = "frmDinhMuc";
            this.Text = "Hệ thống quản lý Định mức Pha chế";
            this.Load += new System.EventHandler(this.frmDinhMuc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDinhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachSP)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbSanPham;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnThemNhieu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.TextBox txtDonVi;
        private System.Windows.Forms.TextBox txtLuongDung;
        private System.Windows.Forms.ComboBox cbNguyenLieu;
        private System.Windows.Forms.DataGridView dgvDinhMuc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtTimKiemSanPham;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblTonKho;
        private System.Windows.Forms.Label lblGiaVon;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DataGridView dgvDanhSachSP;
        private System.Windows.Forms.Label lblTenSPHienTai;
    }
}