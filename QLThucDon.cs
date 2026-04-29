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

    
    
    public partial class QLThucDon : Form
    {

        Database db = new Database();
        public QLThucDon()
        {

            InitializeComponent();
        }

        private void QLThucDon_Load(object sender, EventArgs e)
        {
            LoadDanhMuc();
            LoadSanPham();
            LoadComboDanhMuc();

        }
        void LoadDanhMuc()
        {
            dgvdanhmuc.DataSource = db.GetDataTable("SELECT * FROM DanhMuc");

        }
        void LoadSanPham()
        {
            dgvsanpham.DataSource = db.GetDataTable("SELECT * FROM SanPham");
        }
        void LoadComboDanhMuc()
        {
            cbodanhmuc.DataSource = db.GetDataTable("SELECT * FROM DanhMuc");
            cbodanhmuc.DisplayMember = "TenDanhMuc";
            cbodanhmuc.ValueMember = "MaDM";

        }

        private void btnthemdm_Click(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO DanhMuc (TenDanhMuc, MoTa) VALUES (N'{txttendm.Text}', N'{txtmota.Text}')";


            MessageBox.Show("Câu lệnh sắp chạy: " + sql);

            db.Execute(sql);
            LoadDanhMuc();
        }

        private void btnthemsp_Click(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO SanPham(TenSP, GiaBan, MaDM) " +
                     $"VALUES(N'{txttensp.Text}', {txtgiaban.Text}, {cbodanhmuc.SelectedValue})";
            db.Execute(sql);
            LoadSanPham();
        }

        //an vao dm thi loc san pham tuong ung
        private void dgvdanhmuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvdanhmuc.Rows[e.RowIndex];


                string maDM = row.Cells["MaDM"].Value.ToString();
                string tenDM = row.Cells["TenDanhMuc"].Value.ToString();
                string moTa = row.Cells["MoTa"].Value != null ? row.Cells["MoTa"].Value.ToString() : "";

                // Hien thi dl ra textbox ben trai 
                txtmadm.Text = maDM;
                txttendm.Text = tenDM;
                txtmota.Text = moTa;

                // loc sp theo danh mục bên trái vừa chọn 
                try
                {
                    string sql = "SELECT * FROM SanPham WHERE MaDM = " + maDM;
                    dgvsanpham.DataSource = db.GetDataTable(sql);
                }
                catch
                {

                    string sql = $"SELECT * FROM SanPham WHERE MaDM = '{maDM}'";
                    dgvsanpham.DataSource = db.GetDataTable(sql);
                }
            }

        }

        private void dgvsanpham_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dgvsanpham.Rows[e.RowIndex];

                txtmasp.Text = row.Cells["MaSP"].Value.ToString();
                txttensp.Text = row.Cells["TenSP"].Value.ToString();
                txtgiaban.Text = row.Cells["GiaBan"].Value.ToString();


                if (row.Cells["MoTa"].Value != null)
                {
                    txtmota.Text = row.Cells["MoTa"].Value.ToString();
                }
                else
                {
                    txtmota.Text = ""; //neu null thi xoa trang textbox 
                }

                // cap nhật combobox theo danh mục
                string MaDM = row.Cells["MaDM"].Value.ToString();
                cbodanhmuc.SelectedValue = MaDM;
            }
        }

        private void btnsuadm_Click(object sender, EventArgs e)
        {

            string sql = $"UPDATE DanhMuc SET TenDanhMuc = N'{txttendm.Text}', MoTa = N'{txtmota.Text}' WHERE MaDM = {txtmadm.Text}";
            db.Execute(sql);
            LoadDanhMuc();

        }

        private void btnxoadm_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Xóa danh mục sẽ ảnh hưởng đến sản phẩm. Bạn có chắc không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                string sql = $"DELETE FROM DanhMuc WHERE MaDM = {txtmadm.Text}";
                db.Execute(sql);
                LoadDanhMuc();
            }


        }

        private void btnsuasp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtmasp.Text))
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm trong bảng để sửa!");
                return;
            }
            string maDM = cbodanhmuc.SelectedValue.ToString();
            string sql = $"UPDATE SanPham SET TenSP = N'{txttensp.Text}', GiaBan = {txtgiaban.Text}, " +
                         $"MaDM = {maDM}, MoTa = N'{txtmota.Text}' WHERE MaSP = {txtmasp.Text}";
            db.Execute(sql);
            LoadSanPham();
        }

        private void btnxoasp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa sản phẩm này?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string sql = "DELETE FROM SanPham WHERE MaSP = " + txtmasp.Text;
                db.Execute(sql);
                LoadSanPham();
            }
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            string ten = txttensp.Text.Trim();
            string gia = txtgiaban.Text.Trim();
             string sql = "SELECT * FROM SanPham WHERE 1=1";

            
            if (!string.IsNullOrEmpty(ten))
            {
                sql += $" AND TenSP LIKE N'%{ten}%'";
            }

           
            if (!string.IsNullOrEmpty(gia))
            {
                // kiem tra xem co nhap dung gia kh
                if (double.TryParse(gia, out double giaValue))
                {
                    sql += $" AND GiaBan = {giaValue}";
                    // neu kh co gia do thi se hien thi mon co tu gia do tro len 
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập giá bán là chữ số!");
                    return;
                }
            }

           
            DataTable dt = db.GetDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                dgvsanpham.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Không tìm thấy sản phẩm nào khớp với điều kiện!");
               
            }
        }

        private void btninbcQLTD_Click(object sender, EventArgs e)
        {
            try
            {
                // lay dl tu store procude 
                DataTable dt = db.GetDataTable("EXEC sp_BaoCaoThucDon");

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Sử dụng class report đã được generate
                    BCQuanLyThucDon rpt = new BCQuanLyThucDon();
                    rpt.SetDataSource(dt);

                    // Hiển thị báo cáo
                    FormBCThucDon frm = new FormBCThucDon();
                    
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

        private void btllammoi_Click(object sender, EventArgs e)
        {
            
            txtmasp.Clear();
            txttensp.Clear();
            txtgiaban.Clear();
            txtmota.Clear(); 

            
            if (cbodanhmuc.Items.Count > 0)
                cbodanhmuc.SelectedIndex = 0;

            //tai loai all ds san pham 
            LoadSanPham();

           
            

        }

        private void txtmadm_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class Database
        {
            string strConn = global::Database.ConnectionString;
            SqlConnection conn;

            public Database()
            {
                conn = new SqlConnection(strConn);
            }


            public DataTable GetDataTable(string sql)
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
                    return null;
                }
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
                    MessageBox.Show("Lỗi thực thi SQL: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }

