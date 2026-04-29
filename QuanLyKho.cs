using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace WindowsFormsAppBTL
{
    public partial class QuanLyKho : Form
    {
        public string strConn = @"Data Source=ADMIN-PC\SQLEXPRESS;Initial Catalog=BTL_LTHSK;Integrated Security=True;TrustServerCertificate=True"; SqlConnection conn;

        public QuanLyKho()
        {
            InitializeComponent();
            conn = new SqlConnection(strConn);
        }

        
        public DataTable GetDataTable(string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void Execute(string sql)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void QuanLyKho_Load(object sender, EventArgs e)
        {
            // //lay ncc vao combobox 
            DataTable dtNCC = GetDataTable("SELECT MaNCC, TenNCC FROM NhaCungCap");
            cboncc.DataSource = dtNCC;
            cboncc.DisplayMember = "TenNCC";
            cboncc.ValueMember = "MaNCC";

            LoadData(); // Load danh sách nguyên liệu
        }
        void LoadData()
        {
            string sql = @"SELECT nl.MaNL, nl.TenNguyenLieu, nl.DonViTinh, 
                                  nl.SoLuongTon, ncc.TenNCC, nl.GhiChu 
                           FROM NguyenLieu nl 
                           LEFT JOIN NhaCungCap ncc ON nl.MaNCC = ncc.MaNCC";
            dgvkho.DataSource = GetDataTable(sql);
        }

        private void dgvkho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow r = dgvkho.Rows[e.RowIndex];
                txtmanl.Text = r.Cells["MaNL"].Value.ToString();
                txttennl.Text = r.Cells["TenNguyenLieu"].Value.ToString();
                txtdvt.Text = r.Cells["DonViTinh"].Value.ToString();
                txtsoluongton.Text = r.Cells["SoLuongTon"].Value.ToString();
                txtghichu.Text = r.Cells["GhiChu"].Value.ToString();
                cboncc.Text = r.Cells["TenNCC"].Value.ToString();
            }
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttennl.Text)) { MessageBox.Show("Vui lòng nhập tên nguyên liệu!"); return; }
            if (cboncc.SelectedValue == null) { MessageBox.Show("Vui lòng chọn nhà cung cấp!"); return; }
            if (!decimal.TryParse(txtsoluongton.Text, out decimal sl)) sl = 0;

            string sql = $@"INSERT INTO NguyenLieu (TenNguyenLieu, DonViTinh, SoLuongTon, MaNCC, GhiChu) 
                            VALUES (N'{txttennl.Text}', N'{txtdvt.Text}', {sl}, {cboncc.SelectedValue}, N'{txtghichu.Text}')";
            Execute(sql);
            LoadData();
            MessageBox.Show("Đã thêm nguyên liệu!");
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtmanl.Text)) return;
            if (cboncc.SelectedValue == null) { MessageBox.Show("Vui lòng chọn nhà cung cấp!"); return; }
            if (!decimal.TryParse(txtsoluongton.Text, out decimal sl)) sl = 0;

            string sql = $@"UPDATE NguyenLieu SET TenNguyenLieu = N'{txttennl.Text}', DonViTinh = N'{txtdvt.Text}', 
                            SoLuongTon = {sl}, MaNCC = {cboncc.SelectedValue}, GhiChu = N'{txtghichu.Text}' 
                            WHERE MaNL = {txtmanl.Text}";
            Execute(sql);
            LoadData();
            MessageBox.Show("Đã cập nhật!");
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtmanl.Text)) return;
            if (MessageBox.Show("Xóa nguyên liệu này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Execute("DELETE FROM NguyenLieu WHERE MaNL = " + txtmanl.Text);
                LoadData();
                btnlammoi_Click(null, null);
            }
        }

        private void btnlammoi_Click(object sender, EventArgs e)
        {
            txtmanl.Clear();
            txttennl.Clear();
            txtdvt.Clear();
            txtsoluongton.Text = "0";
            txtghichu.Clear();
            LoadData();
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txttimkiem.Text.Trim(); 
                if (string.IsNullOrEmpty(tuKhoa))
                {
                    LoadData(); // trống thì hiện lại all 
                    return;
                }

                // sd sql để lọc tên theo ncc 
                string sql = @"SELECT nl.MaNL, nl.TenNguyenLieu, nl.DonViTinh, nl.SoLuongTon, ncc.TenNCC, nl.GhiChu 
                       FROM NguyenLieu nl 
                       LEFT JOIN NhaCungCap ncc ON nl.MaNCC = ncc.MaNCC 
                       WHERE ";

                // kiem tra xem ng dung nhap so hay ten 
                if (int.TryParse(tuKhoa, out int maResult))
                {
                    // neu la so : tim dung ma 
                    sql += $"(nl.MaNL = {maResult} OR nl.TenNguyenLieu LIKE N'%{tuKhoa}%')";
                }
                else
                {
                    // neu la chu tim the ten nl hoac chu 
                    sql += $"(nl.TenNguyenLieu LIKE N'%{tuKhoa}%' OR ncc.TenNCC LIKE N'%{tuKhoa}%')";
                }

                DataTable dt = GetDataTable(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgvkho.DataSource = dt;
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy nguyên liệu nào khớp với: '{tuKhoa}'", "Thông báo");
                    LoadData(); //kh co thi hien thi lai all 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void btninbcQLK_Click(object sender, EventArgs e)
        {
            try
            {
                // lay dl tu sql 
                DataTable dt = GetDataTable("EXEC sp_BaoCaoTonKho");

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Sử dụng class report đã được generate (an toàn hơn load file path)
                    BCQuanLyKho rpt = new BCQuanLyKho();
                    rpt.SetDataSource(dt);

                    // Hiển thị báo cáo
                    FormBCKho frm = new FormBCKho();
                    
                    // Tìm CrystalReportViewer trong FormBCKho (giả sử tên là crystalReportViewer1 hoặc tìm theo kiểu)
                    // Ở đây tôi sẽ gán trực tiếp nếu biết tên, hoặc tìm trong Controls
                    bool foundViewer = false;
                    foreach (Control ctrl in frm.Controls)
                    {
                        if (ctrl is CrystalDecisions.Windows.Forms.CrystalReportViewer viewer)
                        {
                            viewer.ReportSource = rpt;
                            viewer.Refresh();
                            foundViewer = true;
                            break;
                        }
                    }

                    if (!foundViewer)
                    {
                        // Nếu không tìm thấy viewer, ta thêm mới một cái
                        CrystalDecisions.Windows.Forms.CrystalReportViewer newViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                        newViewer.Dock = DockStyle.Fill;
                        newViewer.ReportSource = rpt;
                        frm.Controls.Add(newViewer);
                    }

                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để in báo cáo!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in báo cáo: " + ex.Message);
            }
        }

        private void cboncc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
