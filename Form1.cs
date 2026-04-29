using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting;
using System.Windows.Forms;


namespace QuanLyCaPhe
{
    public partial class Form1 : Form
    {
        public string constr = @"Data Source=ADMIN-PC\SQLEXPRESS;Initial Catalog=BTL_LTHSK;Integrated Security=True;TrustServerCertificate=True"; public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                try
                {
                    string query = "SELECT * FROM KhachHang";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKhachHang.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi load dữ liệu: " + ex.Message, "Thông báo");
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO KhachHang (HoTen, SoDienThoai, Email, NgaySinh, DiemTichLuy) " +
                                 "VALUES (@name, @phone, @email, @dob, 0)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@phone", txtSDT.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@dob", dtpNgaySinh.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm khách hàng thành công!");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm: " + ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn một khách hàng từ danh sách!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(constr))
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE KhachHang SET HoTen=@name, SoDienThoai=@phone, Email=@email, NgaySinh=@dob WHERE MaKH=@id";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", txtMaKH.Text);
                    cmd.Parameters.AddWithValue("@name", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@phone", txtSDT.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@dob", dtpNgaySinh.Value);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo");
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text)) return;

            DialogResult dg = MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo);
            if (dg == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    try
                    {
                        conn.Open();
                        string sql = "DELETE FROM KhachHang WHERE MaKH=@id";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@id", txtMaKH.Text);

                        cmd.ExecuteNonQuery();
                        LoadData();
                        MessageBox.Show("Đã xóa khách hàng.");
                    }
                    catch (Exception)
                    {
                        // Lỗi này thường xảy ra do ràng buộc khóa ngoại (khách đã có hóa đơn)
                        MessageBox.Show("Không thể xóa khách hàng này vì đã có lịch sử giao dịch!");
                    }
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaKH.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtDiemTichLuy.Text = "0";
            dtpNgaySinh.Value = DateTime.Now;
            LoadData();
            txtHoTen.Focus();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM KhachHang WHERE 1=1";

                    if (!string.IsNullOrEmpty(txtMaKH.Text))
                        sql += " AND MaKH = @id";
                    if (!string.IsNullOrEmpty(txtHoTen.Text))
                        sql += " AND HoTen LIKE N'%" + txtHoTen.Text + "%'";
                    if (!string.IsNullOrEmpty(txtSDT.Text))
                        sql += " AND SoDienThoai LIKE '%" + txtSDT.Text + "%'";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (!string.IsNullOrEmpty(txtMaKH.Text))
                        cmd.Parameters.AddWithValue("@id", txtMaKH.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvKhachHang.DataSource = dt;
                    if (dt.Rows.Count == 0) MessageBox.Show("Không tìm thấy kết quả!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
                }
            }
        }

        private void btnInDanhSach_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy dữ liệu
                string str = Database.ConnectionString;
                using (SqlConnection cnn = new SqlConnection(str))
                {
                    SqlCommand cmd = new SqlCommand("sp_InDSKhachHang", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // 2. Sử dụng class báo cáo đã generate (Tránh lỗi TypeInitializationException)
                    Indanhsach rpt = new Indanhsach();
                    rpt.SetDataSource(dt);

                    // 3. Hiển thị lên Form
                    frmHienThiBaoCao f = new frmHienThiBaoCao();
                    f.crystalReportViewer2.ReportSource = rpt;
                    f.crystalReportViewer2.Refresh();
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMsg += "\n\nChi tiết lỗi hệ thống: " + ex.InnerException.Message;
                }
                MessageBox.Show("Lỗi khởi tạo báo cáo: " + errorMsg, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMaKH_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
