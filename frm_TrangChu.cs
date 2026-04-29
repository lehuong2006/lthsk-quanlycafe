// namespace removed
using Microsoft.Data.SqlClient;
using QuanLyCaPhe;
using System;
using System.Data;
using System.Windows.Forms;
using WindowsFormsAppBTL;

namespace QLQuanCafe
{
    public partial class frm_TrangChu : Form
    {
        // Nhận từ đăng nhập
        public string TenNV;
        public string VaiTro;

        // Chuỗi kết nối SQL
        public string constr = @"Data Source=ADMIN-PC\SQLEXPRESS;Initial Catalog=BTL_LTHSK;Integrated Security=True;TrustServerCertificate=True";
        public frm_TrangChu() : this("Admin", "Quản lý") { }

        public frm_TrangChu(string tenNV, string vaiTro)
        {
            InitializeComponent();
            this.TenNV = tenNV;
            this.VaiTro = vaiTro;

            // GÁN SỰ KIỆN CLICK CHO TỪNG BUTTON
            btnSanPham.Click += btnSanPham_Click;
            btnKhachHang.Click += btnKhachHang_Click;
            //btnNhanVien.Click += btnNhanVien_Click;
            btnHoaDon.Click += btnHoaDon_Click;
            btnNguyenLieu.Click += btnNguyenLieu_Click;
            // btnPhieuNhap.Click += btnPhieuNhap_Click;
            btnThongKe.Click += btnThongKe_Click;

            // GÁN SỰ KIỆN LOAD FORM
            this.Load += frm_TrangChu_Load;
        }

        // Khi load form
        private void frm_TrangChu_Load(object sender, EventArgs e)
        {
            lblUser.Text = "Xin chào: " + TenNV;

            // Phân quyền: ẩn nút nếu không phải Quản lý
            if (VaiTro != "Quản lý")
            {
                btnThongKe.Visible = false;
                btnNhanVien.Visible = false;
                btnquanlykho.Visible = false;
            }
        }

        // ================= HÀM LOAD DỮ LIỆU =================
        void LoadData(string sql)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Hiển thị dữ liệu lên DataGridView
                    dgvMain.DataSource = dt;

                    // Tự động căn chỉnh cột cho đẹp
                    dgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dgvMain.AutoResizeColumns();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ================= SỰ KIỆN BUTTON =================
        private void btnSanPham_Click(object sender, EventArgs e)
        {
            LoadData("SELECT * FROM SanPham");
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            LoadData("SELECT * FROM KhachHang");
        }

        /*private void btnNhanVien_Click(object sender, EventArgs e)
        {
            LoadData("SELECT * FROM NhanVien");
        }*/

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            LoadData("SELECT * FROM HoaDon");
        }

        private void btnNguyenLieu_Click(object sender, EventArgs e)
        {
            LoadData("SELECT * FROM NguyenLieu");
        }

        /* private void btnPhieuNhap_Click(object sender, EventArgs e)
         {
             LoadData("SELECT * FROM PhieuNhap");
         }*/

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            LoadData(@"
                SELECT 
                    CAST(NgayLap AS DATE) AS Ngay,
                    COUNT(*) AS SoHoaDon,
                    SUM(ThanhToan) AS TongDoanhThu
                FROM HoaDon
                WHERE TrangThai = N'Đã thanh toán'
                GROUP BY CAST(NgayLap AS DATE)
                ORDER BY Ngay DESC
            ");
        }

        private void btndangxuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
       "Bạn có chắc muốn đăng xuất không?",
       "Xác nhận",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Question
   );

            if (result == DialogResult.Yes)
            {
                Dangnhap f = new Dangnhap(); // form đăng nhập
                f.Show();
                this.Hide(); // ẩn trang chủ
            }
        }


        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            frm_ThongTinNhanVien frm = new frm_ThongTinNhanVien();
            frm.Show();
        }

        private void btnquanlykho_Click(object sender, EventArgs e)
        {
            QuanLyKho f = new QuanLyKho();
            f.Show();
        }

        private void btnquanlythucdon_Click(object sender, EventArgs e)
        {
            QLThucDon qL = new QLThucDon();
            qL.Show();
        }

        private void btnKhachHang_Click_1(object sender, EventArgs e)
        {
            Form1 f1= new Form1();
            f1.Show();

        }

        private void btncongthuc_Click(object sender, EventArgs e)
        {
            frmDinhMuc dinhMuc = new frmDinhMuc();
            dinhMuc.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_LapHoaDon laphoadon = new frm_LapHoaDon();
            laphoadon.Show();
        }

        private void btnNguyenLieu_Click_1(object sender, EventArgs e)
        {

        }

        private void btnThongKe_Click_1(object sender, EventArgs e)
        {
            frm_ThongKe thongKe = new frm_ThongKe();
            thongKe.Show();
        }

        private void btnSanPham_Click_1(object sender, EventArgs e)
        {

        }
    }
}