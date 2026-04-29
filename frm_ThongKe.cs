using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using QuanLyCaPhe;

namespace QLQuanCafe
{
    public partial class frm_ThongKe : Form
    {
        public string strConn = @"Data Source=ADMIN-PC\SQLEXPRESS;Initial Catalog=BTL_LTHSK;Integrated Security=True;TrustServerCertificate=True";
        public frm_ThongKe()
        {
            InitializeComponent();
        }

        private void frm_ThongKe_Load(object sender, EventArgs e)
        {
            // Danh sách loại thống kê
            cboLoai.Items.AddRange(new string[] { "Ngày", "Tuần", "Tháng", "Năm" });
            cboLoai.SelectedIndex = 0;

            dtpTu.Value = new DateTime(DateTime.Today.Year, 1, 1);
            dtpDen.Value = DateTime.Today;

            // Style 2 grid
            FormatGrid(dgvDoanhThu);
            FormatGrid(dgvSanPham);
            FormatGrid(dgvNguyenLieu);
        }

        void FormatGrid(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (cboLoai.SelectedItem == null) return;

            string loai = cboLoai.SelectedItem.ToString();
            DateTime tuNgay = dtpTu.Value.Date;
            DateTime denNgay = dtpDen.Value.Date;

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            XemDoanhThu(loai, tuNgay, denNgay);
            XemSanPham(tuNgay, denNgay);
            XemNguyenLieu(tuNgay, denNgay);
        }

        void XemDoanhThu(string loai, DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ThongKeDoanhThu", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LoaiTK", loai);
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvDoanhThu.DataSource = dt;

                    // Tính tổng
                    decimal tong = 0;
                    foreach (DataRow row in dt.Rows)
                        tong += Convert.ToDecimal(row["Doanh thu (VNĐ)"]);

                    lblTong.Text = $"Tổng doanh thu: {tong:N0} VNĐ";
                    lblTong.ForeColor = Color.DarkGreen;
                    lblTong.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
        void XemNguyenLieu(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ThongKeNguyenLieu", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvNguyenLieu.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi nguyên liệu: " + ex.Message); }
        }
        void XemSanPham(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ThongKeSanPham", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvSanPham.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("In thống kê Doanh thu", null, (s, ev) => InThongKe("DoanhThu"));
            menu.Items.Add("In thống kê Sản phẩm", null, (s, ev) => InThongKe("SanPham"));
            menu.Items.Add("In thống kê Nguyên liệu", null, (s, ev) => InThongKe("NguyenLieu"));
            menu.Show(btnIn, new Point(0, btnIn.Height));
        }

        void InThongKe(string loaiTK)
        {
            try
            {
                DataTable dt = new DataTable();
                string procName = "";
                string title = "";

                if (loaiTK == "DoanhThu") { procName = "sp_ThongKeDoanhThu"; title = "BÁO CÁO DOANH THU"; }
                else if (loaiTK == "SanPham") { procName = "sp_ThongKeSanPham"; title = "BÁO CÁO SẢN PHẨM BÁN CHẠY"; }
                else { procName = "sp_ThongKeNguyenLieu"; title = "BÁO CÁO NGUYÊN LIỆU ĐÃ DÙNG"; }

                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    SqlCommand cmd = new SqlCommand(procName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (loaiTK == "DoanhThu") cmd.Parameters.AddWithValue("@LoaiTK", cboLoai.Text);
                    cmd.Parameters.AddWithValue("@TuNgay", dtpTu.Value.Date);
                    cmd.Parameters.AddWithValue("@DenNgay", dtpDen.Value.Date);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dt.TableName = procName; // Quan trọng: Đặt tên bảng trùng với tên Proc để CR dễ nhận diện
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Gọi báo cáo tương ứng
                ReportClass rpt;
                if (loaiTK == "DoanhThu") rpt = new BCDoanhThu();
                else if (loaiTK == "SanPham") rpt = new BCSanPham();
                else rpt = new BCNguyenLieu();

                rpt.SetDataSource(dt);

                // Gán tham số ngày tháng để hiện lên tiêu đề báo cáo (nếu bạn có đặt tham số trong rpt)
                try { rpt.SetParameterValue("TuNgay", dtpTu.Value.ToString("dd/MM/yyyy")); } catch { }
                try { rpt.SetParameterValue("DenNgay", dtpDen.Value.ToString("dd/MM/yyyy")); } catch { }
                try { rpt.SetParameterValue("@TuNgay", dtpTu.Value.ToString("dd/MM/yyyy")); } catch { }
                try { rpt.SetParameterValue("@DenNgay", dtpDen.Value.ToString("dd/MM/yyyy")); } catch { }

                // Hiển thị báo cáo
                frmHienThiBaoCao f = new frmHienThiBaoCao();
                f.crystalReportViewer2.ReportSource = rpt;
                f.Text = title;
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in thống kê: " + ex.Message + "\n\nChi tiết: " + (ex.InnerException != null ? ex.InnerException.Message : ""));
            }
        }

        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}