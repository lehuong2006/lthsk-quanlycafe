namespace QLQuanCafe
{
    partial class frm_LapHoaDon
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
            this.btn_ThemKH = new System.Windows.Forms.Button();
            this.txt_SDT_Moi = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_GhiChu = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txt_MaHD = new System.Windows.Forms.TextBox();
            this.cbo_KhachHang = new System.Windows.Forms.ComboBox();
            this.cbo_NhanVien = new System.Windows.Forms.ComboBox();
            this.txt_KhachHang = new System.Windows.Forms.Label();
            this.txt_NhanVien = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flp_NhomMon = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_TatCa = new System.Windows.Forms.Button();
            this.btn_CaPhe = new System.Windows.Forms.Button();
            this.btn_Tra = new System.Windows.Forms.Button();
            this.btn_SinhTo = new System.Windows.Forms.Button();
            this.btn_NuocNgot = new System.Windows.Forms.Button();
            this.btn_DoAnNhe = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_LamMoi = new System.Windows.Forms.Button();
            this.btn_InHoaDon = new System.Windows.Forms.Button();
            this.btn_ThanhToan = new System.Windows.Forms.Button();
            this.btn_Xoa = new System.Windows.Forms.Button();
            this.cbo_LoaiGiam = new System.Windows.Forms.ComboBox();
            this.cbo_ThanhToan = new System.Windows.Forms.ComboBox();
            this.txt_TongTien = new System.Windows.Forms.TextBox();
            this.txt_GiamGia = new System.Windows.Forms.TextBox();
            this.txt_TamTinh = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dgv_HoaDon = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.dgv_SanPham = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.flp_NhomMon.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_HoaDon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SanPham)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ThemKH
            // 
            this.btn_ThemKH.Location = new System.Drawing.Point(707, 42);
            this.btn_ThemKH.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ThemKH.Name = "btn_ThemKH";
            this.btn_ThemKH.Size = new System.Drawing.Size(180, 28);
            this.btn_ThemKH.TabIndex = 7;
            this.btn_ThemKH.Text = "Thêm khách hàng";
            this.btn_ThemKH.UseVisualStyleBackColor = true;
            this.btn_ThemKH.Click += new System.EventHandler(this.btn_ThemKH_Click);
            // 
            // txt_SDT_Moi
            // 
            this.txt_SDT_Moi.Location = new System.Drawing.Point(708, 70);
            this.txt_SDT_Moi.Margin = new System.Windows.Forms.Padding(4);
            this.txt_SDT_Moi.Name = "txt_SDT_Moi";
            this.txt_SDT_Moi.Size = new System.Drawing.Size(179, 22);
            this.txt_SDT_Moi.TabIndex = 8;
            this.txt_SDT_Moi.TextChanged += new System.EventHandler(this.txt_SDT_Moi_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_ThemKH);
            this.panel1.Controls.Add(this.txt_SDT_Moi);
            this.panel1.Controls.Add(this.txt_GhiChu);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.txt_MaHD);
            this.panel1.Controls.Add(this.cbo_KhachHang);
            this.panel1.Controls.Add(this.cbo_NhanVien);
            this.panel1.Controls.Add(this.txt_KhachHang);
            this.panel1.Controls.Add(this.txt_NhanVien);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(907, 92);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // txt_GhiChu
            // 
            this.txt_GhiChu.Location = new System.Drawing.Point(732, 14);
            this.txt_GhiChu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_GhiChu.Name = "txt_GhiChu";
            this.txt_GhiChu.Size = new System.Drawing.Size(153, 22);
            this.txt_GhiChu.TabIndex = 6;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(349, 14);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(247, 22);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // txt_MaHD
            // 
            this.txt_MaHD.Location = new System.Drawing.Point(91, 14);
            this.txt_MaHD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_MaHD.Name = "txt_MaHD";
            this.txt_MaHD.Size = new System.Drawing.Size(153, 22);
            this.txt_MaHD.TabIndex = 4;
            // 
            // cbo_KhachHang
            // 
            this.cbo_KhachHang.FormattingEnabled = true;
            this.cbo_KhachHang.Location = new System.Drawing.Point(455, 52);
            this.cbo_KhachHang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbo_KhachHang.Name = "cbo_KhachHang";
            this.cbo_KhachHang.Size = new System.Drawing.Size(229, 24);
            this.cbo_KhachHang.TabIndex = 3;
            // 
            // cbo_NhanVien
            // 
            this.cbo_NhanVien.FormattingEnabled = true;
            this.cbo_NhanVien.Location = new System.Drawing.Point(87, 53);
            this.cbo_NhanVien.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbo_NhanVien.Name = "cbo_NhanVien";
            this.cbo_NhanVien.Size = new System.Drawing.Size(229, 24);
            this.cbo_NhanVien.TabIndex = 2;
            // 
            // txt_KhachHang
            // 
            this.txt_KhachHang.AutoSize = true;
            this.txt_KhachHang.Location = new System.Drawing.Point(347, 54);
            this.txt_KhachHang.Name = "txt_KhachHang";
            this.txt_KhachHang.Size = new System.Drawing.Size(83, 16);
            this.txt_KhachHang.TabIndex = 1;
            this.txt_KhachHang.Text = "Khách hàng: ";
            // 
            // txt_NhanVien
            // 
            this.txt_NhanVien.AutoSize = true;
            this.txt_NhanVien.Location = new System.Drawing.Point(11, 54);
            this.txt_NhanVien.Name = "txt_NhanVien";
            this.txt_NhanVien.Size = new System.Drawing.Size(70, 16);
            this.txt_NhanVien.TabIndex = 1;
            this.txt_NhanVien.Text = "Nhân viên:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(619, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Ghi chú (Nếu có):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ngày lập:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã hóa đơn:";
            // 
            // flp_NhomMon
            // 
            this.flp_NhomMon.Controls.Add(this.btn_TatCa);
            this.flp_NhomMon.Controls.Add(this.btn_CaPhe);
            this.flp_NhomMon.Controls.Add(this.btn_Tra);
            this.flp_NhomMon.Controls.Add(this.btn_SinhTo);
            this.flp_NhomMon.Controls.Add(this.btn_NuocNgot);
            this.flp_NhomMon.Controls.Add(this.btn_DoAnNhe);
            this.flp_NhomMon.Location = new System.Drawing.Point(89, 114);
            this.flp_NhomMon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flp_NhomMon.Name = "flp_NhomMon";
            this.flp_NhomMon.Size = new System.Drawing.Size(245, 121);
            this.flp_NhomMon.TabIndex = 1;
            // 
            // btn_TatCa
            // 
            this.btn_TatCa.Location = new System.Drawing.Point(3, 2);
            this.btn_TatCa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_TatCa.Name = "btn_TatCa";
            this.btn_TatCa.Size = new System.Drawing.Size(75, 34);
            this.btn_TatCa.TabIndex = 0;
            this.btn_TatCa.Text = "Tất cả";
            this.btn_TatCa.UseVisualStyleBackColor = true;
            this.btn_TatCa.Click += new System.EventHandler(this.btn_TatCa_Click);
            // 
            // btn_CaPhe
            // 
            this.btn_CaPhe.Location = new System.Drawing.Point(84, 2);
            this.btn_CaPhe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_CaPhe.Name = "btn_CaPhe";
            this.btn_CaPhe.Size = new System.Drawing.Size(75, 34);
            this.btn_CaPhe.TabIndex = 1;
            this.btn_CaPhe.Text = "Cà phê";
            this.btn_CaPhe.UseVisualStyleBackColor = true;
            this.btn_CaPhe.Click += new System.EventHandler(this.btn_CaPhe_Click);
            // 
            // btn_Tra
            // 
            this.btn_Tra.Location = new System.Drawing.Point(165, 2);
            this.btn_Tra.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Tra.Name = "btn_Tra";
            this.btn_Tra.Size = new System.Drawing.Size(75, 34);
            this.btn_Tra.TabIndex = 2;
            this.btn_Tra.Text = "Trà";
            this.btn_Tra.UseVisualStyleBackColor = true;
            this.btn_Tra.Click += new System.EventHandler(this.btn_Tra_Click);
            // 
            // btn_SinhTo
            // 
            this.btn_SinhTo.Location = new System.Drawing.Point(3, 40);
            this.btn_SinhTo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_SinhTo.Name = "btn_SinhTo";
            this.btn_SinhTo.Size = new System.Drawing.Size(75, 32);
            this.btn_SinhTo.TabIndex = 3;
            this.btn_SinhTo.Text = "Sinh tố";
            this.btn_SinhTo.UseVisualStyleBackColor = true;
            this.btn_SinhTo.Click += new System.EventHandler(this.btn_SinhTo_Click);
            // 
            // btn_NuocNgot
            // 
            this.btn_NuocNgot.Location = new System.Drawing.Point(84, 40);
            this.btn_NuocNgot.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_NuocNgot.Name = "btn_NuocNgot";
            this.btn_NuocNgot.Size = new System.Drawing.Size(93, 32);
            this.btn_NuocNgot.TabIndex = 4;
            this.btn_NuocNgot.Text = "Nước ngọt";
            this.btn_NuocNgot.UseVisualStyleBackColor = true;
            this.btn_NuocNgot.Click += new System.EventHandler(this.btn_NuocNgot_Click);
            // 
            // btn_DoAnNhe
            // 
            this.btn_DoAnNhe.Location = new System.Drawing.Point(3, 76);
            this.btn_DoAnNhe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_DoAnNhe.Name = "btn_DoAnNhe";
            this.btn_DoAnNhe.Size = new System.Drawing.Size(107, 39);
            this.btn_DoAnNhe.TabIndex = 5;
            this.btn_DoAnNhe.Text = "Đồ ăn nhẹ";
            this.btn_DoAnNhe.UseVisualStyleBackColor = true;
            this.btn_DoAnNhe.Click += new System.EventHandler(this.btn_DoAnNhe_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Sản phẩm";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_LamMoi);
            this.panel2.Controls.Add(this.btn_InHoaDon);
            this.panel2.Controls.Add(this.btn_ThanhToan);
            this.panel2.Controls.Add(this.btn_Xoa);
            this.panel2.Controls.Add(this.cbo_LoaiGiam);
            this.panel2.Controls.Add(this.cbo_ThanhToan);
            this.panel2.Controls.Add(this.txt_TongTien);
            this.panel2.Controls.Add(this.txt_GiamGia);
            this.panel2.Controls.Add(this.txt_TamTinh);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.dgv_HoaDon);
            this.panel2.Location = new System.Drawing.Point(368, 117);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(531, 410);
            this.panel2.TabIndex = 9;
            // 
            // btn_LamMoi
            // 
            this.btn_LamMoi.Location = new System.Drawing.Point(409, 359);
            this.btn_LamMoi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_LamMoi.Name = "btn_LamMoi";
            this.btn_LamMoi.Size = new System.Drawing.Size(101, 27);
            this.btn_LamMoi.TabIndex = 22;
            this.btn_LamMoi.Text = "Làm Mới";
            this.btn_LamMoi.UseVisualStyleBackColor = true;
            this.btn_LamMoi.Click += new System.EventHandler(this.btn_LamMoi_Click);
            // 
            // btn_InHoaDon
            // 
            this.btn_InHoaDon.Location = new System.Drawing.Point(280, 359);
            this.btn_InHoaDon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_InHoaDon.Name = "btn_InHoaDon";
            this.btn_InHoaDon.Size = new System.Drawing.Size(108, 27);
            this.btn_InHoaDon.TabIndex = 21;
            this.btn_InHoaDon.Text = "In hóa đơn";
            this.btn_InHoaDon.UseVisualStyleBackColor = true;
            this.btn_InHoaDon.Click += new System.EventHandler(this.btn_InHoaDon_Click);
            // 
            // btn_ThanhToan
            // 
            this.btn_ThanhToan.Location = new System.Drawing.Point(153, 359);
            this.btn_ThanhToan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ThanhToan.Name = "btn_ThanhToan";
            this.btn_ThanhToan.Size = new System.Drawing.Size(101, 27);
            this.btn_ThanhToan.TabIndex = 20;
            this.btn_ThanhToan.Text = "Thanh toán";
            this.btn_ThanhToan.UseVisualStyleBackColor = true;
            this.btn_ThanhToan.Click += new System.EventHandler(this.btn_ThanhToan_Click);
            // 
            // btn_Xoa
            // 
            this.btn_Xoa.Location = new System.Drawing.Point(19, 359);
            this.btn_Xoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Xoa.Name = "btn_Xoa";
            this.btn_Xoa.Size = new System.Drawing.Size(125, 27);
            this.btn_Xoa.TabIndex = 19;
            this.btn_Xoa.Text = "Xóa hóa đơn";
            this.btn_Xoa.UseVisualStyleBackColor = true;
            this.btn_Xoa.Click += new System.EventHandler(this.btn_Xoa_Click);
            // 
            // cbo_LoaiGiam
            // 
            this.cbo_LoaiGiam.FormattingEnabled = true;
            this.cbo_LoaiGiam.Items.AddRange(new object[] {
            "Tiền",
            "Phần trăm"});
            this.cbo_LoaiGiam.Location = new System.Drawing.Point(365, 254);
            this.cbo_LoaiGiam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbo_LoaiGiam.Name = "cbo_LoaiGiam";
            this.cbo_LoaiGiam.Size = new System.Drawing.Size(128, 24);
            this.cbo_LoaiGiam.TabIndex = 18;
            this.cbo_LoaiGiam.SelectedIndexChanged += new System.EventHandler(this.cbo_LoaiGiam_SelectedIndexChanged);
            // 
            // cbo_ThanhToan
            // 
            this.cbo_ThanhToan.FormattingEnabled = true;
            this.cbo_ThanhToan.Items.AddRange(new object[] {
            "Tiền mặt",
            "Chuyển khoản"});
            this.cbo_ThanhToan.Location = new System.Drawing.Point(173, 315);
            this.cbo_ThanhToan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbo_ThanhToan.Name = "cbo_ThanhToan";
            this.cbo_ThanhToan.Size = new System.Drawing.Size(156, 24);
            this.cbo_ThanhToan.TabIndex = 17;
            // 
            // txt_TongTien
            // 
            this.txt_TongTien.Location = new System.Drawing.Point(173, 286);
            this.txt_TongTien.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_TongTien.Name = "txt_TongTien";
            this.txt_TongTien.Size = new System.Drawing.Size(156, 22);
            this.txt_TongTien.TabIndex = 16;
            this.txt_TongTien.TextChanged += new System.EventHandler(this.txt_TongTien_TextChanged);
            // 
            // txt_GiamGia
            // 
            this.txt_GiamGia.Location = new System.Drawing.Point(173, 255);
            this.txt_GiamGia.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_GiamGia.Name = "txt_GiamGia";
            this.txt_GiamGia.Size = new System.Drawing.Size(156, 22);
            this.txt_GiamGia.TabIndex = 15;
            this.txt_GiamGia.TextChanged += new System.EventHandler(this.txt_GiamGia_TextChanged_1);
            // 
            // txt_TamTinh
            // 
            this.txt_TamTinh.Location = new System.Drawing.Point(173, 224);
            this.txt_TamTinh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_TamTinh.Name = "txt_TamTinh";
            this.txt_TamTinh.Size = new System.Drawing.Size(156, 22);
            this.txt_TamTinh.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 315);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 16);
            this.label11.TabIndex = 13;
            this.label11.Text = "Hình thức thanh toán:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 286);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 16);
            this.label10.TabIndex = 12;
            this.label10.Text = "Tổng tiền:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 256);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "Giảm giá:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 16);
            this.label8.TabIndex = 11;
            this.label8.Text = "Tạm tính:";
            // 
            // dgv_HoaDon
            // 
            this.dgv_HoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_HoaDon.Location = new System.Drawing.Point(17, 14);
            this.dgv_HoaDon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgv_HoaDon.Name = "dgv_HoaDon";
            this.dgv_HoaDon.RowHeadersWidth = 51;
            this.dgv_HoaDon.RowTemplate.Height = 24;
            this.dgv_HoaDon.Size = new System.Drawing.Size(493, 188);
            this.dgv_HoaDon.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(365, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 16);
            this.label7.TabIndex = 10;
            this.label7.Text = "Chi tiết hóa đơn";
            // 
            // dgv_SanPham
            // 
            this.dgv_SanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_SanPham.Location = new System.Drawing.Point(15, 271);
            this.dgv_SanPham.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgv_SanPham.Name = "dgv_SanPham";
            this.dgv_SanPham.RowHeadersWidth = 51;
            this.dgv_SanPham.RowTemplate.Height = 24;
            this.dgv_SanPham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_SanPham.Size = new System.Drawing.Size(320, 256);
            this.dgv_SanPham.TabIndex = 8;
            this.dgv_SanPham.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_SanPham_CellClick);
            // 
            // frm_LapHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 561);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgv_SanPham);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.flp_NhomMon);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frm_LapHoaDon";
            this.Text = "LẬP HÓA ĐƠN";
            this.Load += new System.EventHandler(this.frm_LapHoaDon_Load_1);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flp_NhomMon.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_HoaDon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SanPham)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_GhiChu;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox txt_MaHD;
        private System.Windows.Forms.ComboBox cbo_KhachHang;
        private System.Windows.Forms.ComboBox cbo_NhanVien;
        private System.Windows.Forms.Label txt_KhachHang;
        private System.Windows.Forms.Label txt_NhanVien;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flp_NhomMon;
        private System.Windows.Forms.Button btn_TatCa;
        private System.Windows.Forms.Button btn_CaPhe;
        private System.Windows.Forms.Button btn_Tra;
        private System.Windows.Forms.Button btn_SinhTo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_NuocNgot;
        private System.Windows.Forms.Button btn_DoAnNhe;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgv_HoaDon;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgv_SanPham;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btn_InHoaDon;
        private System.Windows.Forms.Button btn_ThanhToan;
        private System.Windows.Forms.Button btn_Xoa;
        private System.Windows.Forms.ComboBox cbo_LoaiGiam;
        private System.Windows.Forms.ComboBox cbo_ThanhToan;
        private System.Windows.Forms.TextBox txt_TongTien;
        private System.Windows.Forms.TextBox txt_GiamGia;
        private System.Windows.Forms.TextBox txt_TamTinh;
        private System.Windows.Forms.Button btn_ThemKH;
        private System.Windows.Forms.TextBox txt_SDT_Moi;
        private System.Windows.Forms.Button btn_LamMoi;
    }
}