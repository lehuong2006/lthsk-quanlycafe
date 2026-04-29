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

namespace QLQuanCafe
{
    public partial class frm_InHoaDon : Form
    {
        public int MaHD { get; set; }

        // Các biến kết nối SQL của bạn...
        string strConn = global::Database.ConnectionString;
        public frm_InHoaDon()
        {
            InitializeComponent();
        }

        private void HoaDonView1_Load(object sender, EventArgs e)
        {

        }

        private void frm_InHoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    SqlCommand cmd = new SqlCommand("sp_InHoaDon", conn); // Proc đã tạo ở câu trước
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaHD", MaHD);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    BaoCaoInHoaDon rpt = new BaoCaoInHoaDon();
                    rpt.SetDataSource(dt);

                    // Gán tham số (thử cả 2 cách đặt tên phổ biến)
                    try { rpt.SetParameterValue("MaHD", MaHD); } catch { }
                    try { rpt.SetParameterValue("@MaHD", MaHD); } catch { }

                    HoaDonView1.ReportSource = rpt;
                    HoaDonView1.Refresh();
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show("!!! LỖI TẠI FORM IN !!!\n\n" + ex.ToString()); 
            }
        }
    

    }
    
}
