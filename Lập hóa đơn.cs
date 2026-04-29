using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLQuanCafe
{
    public partial class frm_LapHoaDon : Form
    {
        public string strConn = @"Data Source=ADMIN-PC\SQLEXPRESS;Initial Catalog=BTL_LTHSK;Integrated Security=True;TrustServerCertificate=True";
        private int maHD_HienTai = -1; // Lưu mã hóa đơn đang phục vụ

        public frm_LapHoaDon()
        {
            InitializeComponent();
        }

        private void frm_LapHoaDon_Load(object sender, EventArgs e)
        {
            txt_KhachHang.Text = "Số điện thoại:";
            // Khởi tạo hóa đơn mới ngay khi mở form
            TaoHoaDonMoi();

            // Load danh sách sản phẩm ban đầu (Tất cả)
            LocMonTheoNhom(0);
            LoadComboData();    // Hiện tên nhân viên/khách hàng
            ;
            if (cbo_LoaiGiam.Items.Count > 0) cbo_LoaiGiam.SelectedIndex = 0;
        }

        public DataTable GetDataTable(string sql)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void ExecuteNonQuery(string sql)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void TaoHoaDonMoi()
        {
            try
            {
                // Lấy MaNV đầu tiên có trong bảng NhanVien để làm người lập mặc định
                DataTable dtNV = GetDataTable("SELECT TOP 1 MaNV FROM NhanVien");
                int maNV_MacDinh = 1; // Giá trị dự phòng
                if (dtNV.Rows.Count > 0)
                {
                    maNV_MacDinh = Convert.ToInt32(dtNV.Rows[0]["MaNV"]);
                }

                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    SqlCommand cmd = new SqlCommand("sp_TaoHoaDon", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaNV", maNV_MacDinh);

                    SqlParameter outParam = new SqlParameter("@MaHD", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(outParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    maHD_HienTai = (int)outParam.Value;
                    txt_MaHD.Text = maHD_HienTai.ToString(); 
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khởi tạo hóa đơn: " + ex.Message); }
        }

        private void LoadDataHoaDon()
        {
            // Load lại danh sách các món khách đã gọi trong hóa đơn này
            string sql = $@"SELECT ct.MaSP, sp.TenSP, ct.SoLuong, ct.DonGia, ct.ThanhTien 
                            FROM ChiTietHoaDon ct 
                            JOIN SanPham sp ON ct.MaSP = sp.MaSP 
                            WHERE ct.MaHD = {maHD_HienTai}";
            dgv_HoaDon.DataSource = GetDataTable(sql);
        }
        private void TinhToanSoTien()
        {
            try
            {
                decimal tamTinh = 0;
                foreach (DataGridViewRow row in dgv_HoaDon.Rows)
                {
                    if (row.Cells["ThanhTien"].Value != null)
                        tamTinh += Convert.ToDecimal(row.Cells["ThanhTien"].Value);
                }
                txt_TamTinh.Text = tamTinh.ToString("N0");

                decimal giamNhap = string.IsNullOrEmpty(txt_GiamGia.Text) ? 0 : Convert.ToDecimal(txt_GiamGia.Text);
                decimal tienGiam = (cbo_LoaiGiam.Text == "%") ? (tamTinh * giamNhap / 100) : giamNhap;

                decimal tongTien = tamTinh - tienGiam;
                txt_TongTien.Text = (tongTien < 0 ? 0 : tongTien).ToString("N0");
            }
            catch { }
        }

        private void dgv_SanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int maSP = Convert.ToInt32(dgv_SanPham.Rows[e.RowIndex].Cells["MaSP"].Value);
                string tenSP = dgv_SanPham.Rows[e.RowIndex].Cells["TenSP"].Value.ToString();

                // Hiển thị hộp thoại nhập số lượng
                string input = Microsoft.VisualBasic.Interaction.InputBox($"Nhập số lượng cho món [{tenSP}]:", "Số lượng", "1");

                if (int.TryParse(input, out int soLuong) && soLuong > 0)
                {
                    ExecuteNonQuery($"EXEC sp_ThemMon @MaHD = {maHD_HienTai}, @MaSP = {maSP}, @SoLuong = {soLuong}");
                    LoadDataHoaDon();
                    TinhToanSoTien();
                }
            }
        }
        private void txt_GiamGia_TextChanged(object sender, EventArgs e)
        {
            TinhToanSoTien(); // Tự động tính lại tiền khi gõ giảm giá
        }
        private void btn_ThanhToan_Click(object sender, EventArgs e)
        {
            if (dgv_HoaDon.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có món nào để thanh toán!", "Thông báo");
                return;
            }

            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn thanh toán hóa đơn này không?",
                                              "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    decimal giamGia = string.IsNullOrEmpty(txt_GiamGia.Text) ? 0 : Convert.ToDecimal(txt_GiamGia.Text);
                    string hinhThuc = cbo_ThanhToan.Text == "" ? "Tiền mặt" : cbo_ThanhToan.Text;

                    ExecuteNonQuery($"EXEC sp_ThanhToan @MaHD = {maHD_HienTai}, @GiamGia = {giamGia}, @HinhThucTT = N'{hinhThuc}'");

                    MessageBox.Show("Thanh toán thành công!", "Thông báo");

                    // Tự động gọi hàm tính tiền và In hóa đơn sau khi Thanh toán
                    btn_InHoaDon_Click(null, null);

                    // Làm mới Form để tạo hóa đơn tiếp theo phục vụ khách mới
                    LamMoiForm();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi thanh toán: " + ex.Message); }
            }
        }
        private void LamMoiForm()
        {
            txt_GiamGia.Clear();
            // txt_NhanVien.Clear();
            // txt_KhachHang.Clear();
            txt_TamTinh.Text = "0";
            txt_TongTien.Text = "0";
            dgv_HoaDon.DataSource = null;
            TaoHoaDonMoi(); // Tạo mã HD mới
        }

        private void btn_XoaHoaDon_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Hủy toàn bộ hóa đơn này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ExecuteNonQuery($"DELETE FROM HoaDon WHERE MaHD = {maHD_HienTai}");
                LamMoiForm();
            }
        }
        void LocMonTheoNhom(int maDM)
        {
            string sql = (maDM == 0) ? "SELECT MaSP, TenSP, GiaBan FROM SanPham WHERE TrangThai = 1"
                                     : $"SELECT MaSP, TenSP, GiaBan FROM SanPham WHERE TrangThai = 1 AND MaDM = {maDM}";
            dgv_SanPham.DataSource = GetDataTable(sql);
        }

        private void btn_CaPhe_Click(object sender, EventArgs e)
        {
            LocMonTheoNhom(1);
        }
        private void btn_Tra_Click(object sender, EventArgs e)
        {
            LocMonTheoNhom(2);
        }

        private void btn_SinhTo_Click(object sender, EventArgs e)
        {
            LocMonTheoNhom(3);
        }
        private void btn_NuocNgot_Click(object sender, EventArgs e)
        {
            LocMonTheoNhom(5);
        }

        private void btn_DoAnNhe_Click(object sender, EventArgs e)
        {
            LocMonTheoNhom(4);
        }

        private void btn_TatCa_Click(object sender, EventArgs e)
        {
            LocMonTheoNhom(0);
        }
        private void LoadComboData()
        {
            try
            {
                // 1. Nạp danh sách Nhân viên
                string sqlNV = "SELECT MaNV, TenNV FROM NhanVien";
                DataTable dtNV = GetDataTable(sqlNV); // Sử dụng hàm GetDataTable đã viết ở trên
                cbo_NhanVien.DataSource = dtNV;
                cbo_NhanVien.DisplayMember = "TenNV"; // Hiển thị tên
                cbo_NhanVien.ValueMember = "MaNV";    // Giá trị ẩn bên dưới là Mã

                // 2. Nạp danh sách Khách hàng
                string sqlKH = "SELECT MaKH, TenKH FROM KhachHang";
                DataTable dtKH = GetDataTable(sqlKH);
                cbo_KhachHang.DataSource = dtKH;
                cbo_KhachHang.DisplayMember = "TenKH";
                cbo_KhachHang.ValueMember = "MaKH";

                // Để mặc định là "Khách vãng lai" hoặc không chọn gì
                cbo_KhachHang.SelectedIndex = -1;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi nạp danh sách: " + ex.Message); }
        }
        private void LoadComboNhanVien()
        {
            // 1. Lấy dữ liệu từ bảng NhanVien trong SQL
            string sql = "SELECT MaNV, HoTen FROM NhanVien WHERE TrangThai = 1";
            DataTable dt = GetDataTable(sql); // Hàm GetDataTable đã viết ở trên

            // 2. Đổ vào ComboBox
            cbo_NhanVien.DataSource = dt;

            // Hiển thị tên cho người dùng chọn
            cbo_NhanVien.DisplayMember = "HoTen";

            // Lưu mã ẩn bên dưới để khi cần thì lấy ra dùng
            cbo_NhanVien.ValueMember = "MaNV";
        }
        private void LoadComboKhachHang()
        {
            try
            {
                // Lấy mã, tên và số điện thoại từ bảng KhachHang
                string sql = "SELECT MaKH, HoTen, SoDienThoai FROM KhachHang";
                DataTable dt = GetDataTable(sql);

                // Đổ dữ liệu vào ComboBox
                cbo_KhachHang.DataSource = dt;
                cbo_KhachHang.DisplayMember = "SoDienThoai"; // Hiện số điện thoại
                cbo_KhachHang.ValueMember = "MaKH";    

                // Cấu hình để có thể gõ số điện thoại mới
                cbo_KhachHang.DropDownStyle = ComboBoxStyle.DropDown;
                cbo_KhachHang.Text = ""; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách khách hàng: " + ex.Message);
            }
        }

        private void frm_LapHoaDon_Load_1(object sender, EventArgs e)
        {

            LoadComboNhanVien();
            LoadComboKhachHang();

            TaoHoaDonMoi();       // Khởi tạo hóa đơn mới
            LocMonTheoNhom(0);    // Hiển thị tất cả sản phẩm
        }
        private void CapNhatTongTien()
        {
            try
            {
                // 1. Lấy Tạm tính (loại bỏ dấu phẩy/chấm phân cách nếu có)
                decimal tamTinh = 0;
                if (!string.IsNullOrEmpty(txt_TamTinh.Text))
                    decimal.TryParse(txt_TamTinh.Text, out tamTinh);

                // 2. Lấy số nhập vào ô Giảm giá
                decimal giaTriNhap = 0;
                decimal.TryParse(txt_GiamGia.Text, out giaTriNhap);

                decimal tongTien = 0;

                // 3. Kiểm tra ComboBox đơn vị giảm giá (Giả sử tên là cbo_DonViGiam)
                if (cbo_LoaiGiam.Text == "Phần trăm")
                {
                    // Công thức tính phần trăm: 55,000 - (55,000 * 10 / 100) = 49,500
                    tongTien = tamTinh - (tamTinh * giaTriNhap / 100);
                }
                else
                {
                    // Trừ thẳng số tiền
                    tongTien = tamTinh - giaTriNhap;
                }

                // 4. Hiển thị kết quả (N0 để định dạng 49,500)
                txt_TongTien.Text = tongTien.ToString("N0");
            }
            catch
            {
                txt_TongTien.Text = txt_TamTinh.Text;
            }
        }

        private void txt_GiamGia_TextChanged_1(object sender, EventArgs e)
        {
            CapNhatTongTien();
        }

        private void txt_TongTien_TextChanged(object sender, EventArgs e)
        {
            CapNhatTongTien();
        }

        private void cbo_LoaiGiam_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatTongTien();
        }
        // Hàm xóa hóa đơn (Hủy toàn bộ, không chỉ xóa món)
        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (maHD_HienTai == -1) return;

            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn HỦY hóa đơn số {maHD_HienTai} không?",
                "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    // Gọi Proc hoặc dùng lệnh Delete
                    ExecuteNonQuery($"EXEC sp_HuyHoaDon @MaHD = {maHD_HienTai}");

                    MessageBox.Show("Đã hủy hóa đơn thành công!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        private void btn_InHoaDon_Click(object sender, EventArgs e)
        {
            if (maHD_HienTai == -1) return;

            try
            {
                // 1. Lấy mã Nhân viên
                int maNV = Convert.ToInt32(cbo_NhanVien.SelectedValue);

                // 2. Xử lý Khách hàng qua Số điện thoại
                string sdt = cbo_KhachHang.Text.Trim();
                string maKH = "NULL";

                if (!string.IsNullOrEmpty(sdt))
                {
                    // Kiểm tra xem số điện thoại đã tồn tại chưa
                    DataTable dtKH = GetDataTable($"SELECT MaKH FROM KhachHang WHERE SoDienThoai = '{sdt}'");
                    if (dtKH.Rows.Count > 0)
                    {
                        maKH = dtKH.Rows[0]["MaKH"].ToString();
                    }
                    else
                    {
                        // Thêm khách hàng mới nếu chưa có
                        ExecuteNonQuery($"INSERT INTO KhachHang (HoTen, SoDienThoai) VALUES (N'Khách {sdt}', '{sdt}')");
                        DataTable dtMoi = GetDataTable($"SELECT MaKH FROM KhachHang WHERE SoDienThoai = '{sdt}'");
                        if (dtMoi.Rows.Count > 0)
                            maKH = dtMoi.Rows[0]["MaKH"].ToString();
                    }
                }

                // 3. Lấy số tiền và giảm giá
                decimal giamGia = 0;
                decimal.TryParse(txt_GiamGia.Text, out giamGia);

                string strTongTien = txt_TongTien.Text.Replace(",", "").Replace(".", "");
                decimal tongTien = 0;
                decimal.TryParse(strTongTien, out tongTien);

                // 4. Cập nhật tất cả thông tin vào Database trước khi in
                string sqlUpdate = $@"UPDATE HoaDon 
                            SET MaNV = {maNV}, 
                                MaKH = {maKH}, 
                                GiamGia = {giamGia}, 
                                TongTien = {tongTien} 
                            WHERE MaHD = {maHD_HienTai}";

                ExecuteNonQuery(sqlUpdate);

                // 5. Mở form in
                frm_InHoaDon fIn = new frm_InHoaDon();
                fIn.MaHD = maHD_HienTai;
                fIn.ShowDialog();
            }
            catch (Exception ex)
            {
                // Hiện toàn bộ lỗi để debug (bao gồm StackTrace)
                MessageBox.Show("!!! PHÁT HIỆN LỖI IN HÓA ĐƠN !!!\n\n" + ex.ToString(), "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_LamMoi_Click(object sender, EventArgs e)
        {
            LamMoiForm();
        }

        private void btn_ThemKH_Click(object sender, EventArgs e)
        {
            string sdt = txt_SDT_Moi.Text.Trim();
            if (string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại khách hàng!");
                return;
            }

            try
            {
                // Kiểm tra xem số đã tồn tại chưa
                DataTable dt = GetDataTable($"SELECT * FROM KhachHang WHERE SoDienThoai = '{sdt}'");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Số điện thoại này đã tồn tại trong danh sách! Vui lòng nhập số điện thoại khác.", "Thông báo trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_SDT_Moi.Text = "";
                    txt_SDT_Moi.Focus();
                }
                else
                {
                    ExecuteNonQuery($"INSERT INTO KhachHang (HoTen, SoDienThoai) VALUES (N'Khách {sdt}', '{sdt}')");
                    MessageBox.Show("Đã thêm khách hàng mới thành công!");
                    LoadComboKhachHang(); 
                    cbo_KhachHang.Text = sdt;
                    txt_SDT_Moi.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm khách hàng: " + ex.Message);
            }
        }

        private void txt_SDT_Moi_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
