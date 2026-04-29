using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace QLQuanCafe
{
    public partial class frm_ThongTinNhanVien : Form
    {
        public frm_ThongTinNhanVien()
        {
            InitializeComponent();
        }
        public string str = @"Data Source=ADMIN-PC\SQLEXPRESS;Initial Catalog=BTL_LTHSK;Integrated Security=True;TrustServerCertificate=True"; SqlConnection sql = null;
        private void frm_ThongTinNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            if (sql == null) sql = new SqlConnection(str);
            string query = "SELECT * FROM NhanVien";
            SqlDataAdapter adapter = new SqlDataAdapter(query, str);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvNhanVien.DataSource = dt;
        }
        //Sự kiện click của nút "Thêm"
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (sql == null) sql = new SqlConnection(str);
                if (sql.State == ConnectionState.Closed) sql.Open();

                // Nếu người dùng chọn "Đang làm" thì là true (1), ngược lại là false (0)
                bool statusValue = (cboTrangThai.Text == "Đang làm");

                string query = "INSERT INTO NhanVien (HoTen, SoDienThoai, Email, VaiTro, NgayVaoLam, TrangThai) " +
                               "VALUES (@Ten, @SDT, @Email, @Role, @Date, @Status)";

                SqlCommand cmd = new SqlCommand(query, sql);

                // Gán các tham số
                cmd.Parameters.AddWithValue("@Ten", txtHoTen.Text);
                cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Role", cboVaiTro.Text);
                cmd.Parameters.AddWithValue("@Date", dtpNgayVaoLam.Value);

                //Truyền statusValue (kiểu bool)
                cmd.Parameters.AddWithValue("@Status", statusValue);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm nhân viên thành công!");
                LoadData(); // Tải lại lưới
                // Xóa trắng các ô nhập liệu sau khi thêm thành công
                LamMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                if (sql.State == ConnectionState.Open) sql.Close();
            }
        }
        // Hàm bổ trợ để xóa sạch nội dung sau khi nhập 
        private void LamMoi()
        {
            txtHoTen.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            cboVaiTro.SelectedIndex = -1;
            cboTrangThai.SelectedIndex = -1;
            dtpNgayVaoLam.Value = DateTime.Now;


        }
        //sự kiện click của nút "Sửa"
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (sql.State == ConnectionState.Closed) sql.Open();
                string query = "UPDATE NhanVien SET HoTen=@Ten, SoDienThoai=@SDT, Email=@Email, VaiTro=@Role, NgayVaoLam=@Date, TrangThai=@Status WHERE MaNV=@Ma";
                SqlCommand cmd = new SqlCommand(query, sql);

                cmd.Parameters.AddWithValue("@Ma", txtMaNV.Text);
                cmd.Parameters.AddWithValue("@Ten", txtHoTen.Text);
                cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Role", cboVaiTro.Text);
                cmd.Parameters.AddWithValue("@Date", dtpNgayVaoLam.Value);
                cmd.Parameters.AddWithValue("@Status", (cboTrangThai.Text == "Đang làm"));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            finally { sql.Close(); }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            // Kiểm tra nếu người dùng click vào dòng dữ liệu (không phải tiêu đề)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                cboVaiTro.Text = row.Cells["VaiTro"].Value.ToString();
                dtpNgayVaoLam.Value = Convert.ToDateTime(row.Cells["NgayVaoLam"].Value);

                // Xử lý hiển thị ComboBox trạng thái (nếu DB lưu 1/0)
                bool status = Convert.ToBoolean(row.Cells["TrangThai"].Value);
                cboTrangThai.Text = status ? "Đang làm" : "Đã nghỉ";
            }
        }
        //sự kiện click của nút "Tìm kiếm"
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (sql.State == ConnectionState.Closed) sql.Open();
                string query = "SELECT * FROM NhanVien WHERE 1=1";

                // 2. Tạo đối tượng Command
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sql;

                // 3. Kiểm tra từng ô, nếu có dữ liệu thì cộng dồn vào Query
                if (!string.IsNullOrEmpty(txtHoTen.Text))
                {
                    query += " AND HoTen LIKE @Ten";
                    cmd.Parameters.AddWithValue("@Ten", "%" + txtHoTen.Text.Trim() + "%");
                }

                if (cboVaiTro.SelectedIndex != -1 && !string.IsNullOrEmpty(cboVaiTro.Text))
                {
                    query += " AND VaiTro = @Role";
                    cmd.Parameters.AddWithValue("@Role", cboVaiTro.Text);
                }

                if (cboTrangThai.SelectedIndex != -1 && !string.IsNullOrEmpty(cboTrangThai.Text))
                {
                    // Chuyển đổi chữ sang bit để khớp Database
                    query += " AND TrangThai = @Status";
                    cmd.Parameters.AddWithValue("@Status", (cboTrangThai.Text == "Đang làm") ? 1 : 0);
                }

                if (!string.IsNullOrEmpty(txtSDT.Text))
                {
                    query += " AND SoDienThoai LIKE @SDT";
                    cmd.Parameters.AddWithValue("@SDT", "%" + txtSDT.Text.Trim() + "%");
                }

                // 4. Thực thi
                cmd.CommandText = query;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvNhanVien.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả phù hợp với các tiêu chí đã chọn!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lọc dữ liệu: " + ex.Message);
            }
            finally
            {
                sql.Close();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
            LoadData();
        }
        //sự kiện click của nút "In báo cáo"
        private void btnDanhSach_Click(object sender, EventArgs e)
        {
            try
            {
                if (sql.State == ConnectionState.Closed) sql.Open();

                // 1. Gọi Stored Procedure từ Database
                SqlCommand cmd = new SqlCommand("sp_BaoCaoNhanVien", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt); // Đổ dữ liệu từ Proc vào DataTable

                // 2. Gán DataTable vào Crystal Report
                BCNhanVien rpt = new BCNhanVien();
                rpt.SetDataSource(dt);

                // Gán Parameter (Thử cả 2 cách đặt tên để đảm bảo thành công)
                string vt = (cboVaiTro.SelectedIndex != -1) ? cboVaiTro.Text : "";
                string tt = (cboTrangThai.SelectedIndex != -1) ? cboTrangThai.Text : "";

                try { rpt.SetParameterValue("VaiTro", vt); } catch { }
                try { rpt.SetParameterValue("@VaiTro", vt); } catch { }
                try { rpt.SetParameterValue("TrangThai", tt); } catch { }
                try { rpt.SetParameterValue("@TrangThai", tt); } catch { }

                // 3. Hiển thị lên Form mới
                Form2 fIn = new Form2();
                fIn.BaoCaoNhanVienView1.ReportSource = rpt;
                fIn.BaoCaoNhanVienView1.Refresh();
                fIn.ShowDialog();
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMsg += "\n\nLỗi chi tiết hệ thống: " + ex.InnerException.Message;
                }
                MessageBox.Show("!!! LỖI IN NHÂN VIÊN !!!\n\n" + errorMsg + "\n\nStackTrace: " + ex.StackTrace, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { sql.Close(); }
        }
        //sự kiện click của nút "Xóa"
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (sql.State == ConnectionState.Closed) sql.Open();
                string query = "DELETE FROM NhanVien WHERE MaNV=@Ma";
                SqlCommand cmd = new SqlCommand(query, sql);
                cmd.Parameters.AddWithValue("@Ma", txtMaNV.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa nhân viên thành công!");
                LoadData();
                LamMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                sql.Close();
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
