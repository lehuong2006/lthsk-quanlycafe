using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace QLQuanCafe
{
    public partial class Dangnhap : Form
    {
        public Dangnhap()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void chkHienMK_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHienMK.Checked)
                txtPass.PasswordChar = '\0';
            else
                txtPass.PasswordChar = '*';
        }

        public string constr = @"Data Source=ADMIN-PC\SQLEXPRESS;Initial Catalog=BTL_LTHSK;Integrated Security=True;TrustServerCertificate=True";
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open();

                    string sql = @"SELECT nv.HoTen, nv.VaiTro 
                                   FROM TaiKhoan tk 
                                   JOIN NhanVien nv ON tk.MaNV = nv.MaNV 
                                   WHERE tk.TenDangNhap = @tk AND tk.MatKhau = @mk";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@tk", txtUser.Text.Trim());
                    cmd.Parameters.AddWithValue("@mk", txtPass.Text);

                    SqlDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                    {
                        string tenNV = rd["HoTen"].ToString();
                        string vaiTro = rd["VaiTro"].ToString();
                        rd.Close();

                        // ✅ Truyền đủ dữ liệu qua constructor
                        frm_TrangChu f = new frm_TrangChu(tenNV, vaiTro);
                        f.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Đăng nhập thất bại",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPass.Clear();
                        txtPass.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }
    }
}