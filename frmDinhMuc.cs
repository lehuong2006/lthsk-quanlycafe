using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace QuanLyCaPhe
{
    public partial class frmDinhMuc : Form
    {
        // ⚠️ QUAN TRỌNG: Sửa lại chuỗi kết nối này cho đúng với máy tính của bạn
        // (Ví dụ: "Data Source=DESKTOP-ABC123\\SQLEXPRESS;Initial Catalog=BTL_LTHSK;Integrated Security=True")
        public string connectionString = @"Data Source=ADMIN-PC\SQLEXPRESS;Initial Catalog=BTL_LTHSK;Integrated Security=True;TrustServerCertificate=True";
        // Biến lưu mã công thức đang được chọn trên DataGridView để Sửa/Xóa
        int maCTPH_Selected = -1;

        public frmDinhMuc()
        {
            InitializeComponent();
        }
        // Tải danh sách Sản Phẩm vào Bảng Sidebar bên trái (có hỗ trợ lọc)
        private void LoadDanhSachSanPham(string filter = "")
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT sp.MaSP, sp.TenSP, dm.TenDanhMuc,
                                 (
                                    SELECT STUFF((
                                        SELECT ', ' + nl.TenNguyenLieu + ' ' + CAST(CAST(ct.LuongDung AS FLOAT) AS NVARCHAR(20)) + ct.DonVi
                                        FROM CongThucPhaChe ct
                                        JOIN NguyenLieu nl ON ct.MaNL = nl.MaNL
                                        WHERE ct.MaSP = sp.MaSP
                                        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '')
                                 ) AS ThanhPhan
                                 FROM SanPham sp 
                                 JOIN DanhMuc dm ON sp.MaDM = dm.MaDM 
                                 WHERE sp.TrangThai = 1";
                if (!string.IsNullOrEmpty(filter))
                {
                    query += " AND sp.TenSP LIKE @filter";
                }

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                if (!string.IsNullOrEmpty(filter)) da.SelectCommand.Parameters.AddWithValue("@filter", "%" + filter + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvDanhSachSP.DataSource = dt;

                if (dgvDanhSachSP.Columns.Count > 0)
                {
                    dgvDanhSachSP.Columns["MaSP"].Visible = false;
                    dgvDanhSachSP.Columns["TenSP"].HeaderText = "Sản phẩm";
                    dgvDanhSachSP.Columns["TenSP"].Width = 180;
                    dgvDanhSachSP.Columns["TenDanhMuc"].HeaderText = "Loại";
                    dgvDanhSachSP.Columns["TenDanhMuc"].Width = 120;
                    if (dgvDanhSachSP.Columns.Contains("ThanhPhan"))
                    {
                        dgvDanhSachSP.Columns["ThanhPhan"].HeaderText = "Công thức / Thành phần";
                        dgvDanhSachSP.Columns["ThanhPhan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }

                // Đồng bộ với ComboBox cũ (vẫn giữ để dùng ID nếu cần)
                cbSanPham.DataSource = dt;
                cbSanPham.DisplayMember = "TenSP";
                cbSanPham.ValueMember = "MaSP";
            }
        }

        private void dgvDanhSachSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachSP.Rows[e.RowIndex];
                int maSP = Convert.ToInt32(row.Cells["MaSP"].Value);
                string tenSP = row.Cells["TenSP"].Value.ToString();

                lblTenSPHienTai.Text = tenSP;
                cbSanPham.SelectedValue = maSP; // Sync ComboBox ngầm

                LoadDataGridView(maSP);
                ClearInputs();
            }
        }

        // Tải danh sách Nguyên Liệu vào ComboBox
        private void LoadComboBoxNguyenLieu()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT MaNL, TenNguyenLieu FROM NguyenLieu";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbNguyenLieu.DataSource = dt;
                cbNguyenLieu.DisplayMember = "TenNguyenLieu";
                cbNguyenLieu.ValueMember = "MaNL";
                cbNguyenLieu.SelectedIndex = -1;
            }
        }

        // Tải danh sách định mức và tính toán giá vốn
        private void LoadDataGridView(int maSP)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT ct.MaCTPH, sp.TenSP, nl.TenNguyenLieu, ct.LuongDung, ct.DonVi, ct.GhiChu, ct.MaNL 
                                 FROM CongThucPhaChe ct
                                 JOIN SanPham sp ON ct.MaSP = sp.MaSP
                                 JOIN NguyenLieu nl ON ct.MaNL = nl.MaNL
                                 WHERE ct.MaSP = @MaSP";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvDinhMuc.DataSource = dt;

                    if (dgvDinhMuc.Columns.Count > 0)
                    {
                        dgvDinhMuc.Columns["MaCTPH"].Visible = false;
                        dgvDinhMuc.Columns["MaNL"].Visible = false;
                        dgvDinhMuc.Columns["TenSP"].HeaderText = "Sản Phẩm";
                        dgvDinhMuc.Columns["TenNguyenLieu"].HeaderText = "Nguyên Liệu";
                        dgvDinhMuc.Columns["LuongDung"].HeaderText = "Lượng Dùng";
                        dgvDinhMuc.Columns["DonVi"].HeaderText = "Đơn Vị Tính";
                        dgvDinhMuc.Columns["GhiChu"].HeaderText = "Ghi Chú";
                    }
                }
            }
            UpdateRecipeCost(maSP);
        }

        // Hàm tính toán giá vốn dựa trên giá nhập mới nhất
        private void UpdateRecipeCost(int maSP)
        {
            decimal totalCost = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Truy vấn lấy lượng dùng và giá nhập gần nhất của từng nguyên liệu trong công thức
                string query = @"
                    SELECT ct.LuongDung, 
                           ISNULL((SELECT TOP 1 GiaNhap FROM ChiTietNhap ctn 
                                   WHERE ctn.MaNL = ct.MaNL 
                                   ORDER BY MaCTN DESC), 0) as LastPrice
                    FROM CongThucPhaChe ct
                    WHERE ct.MaSP = @MaSP";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            totalCost += dr.GetDecimal(0) * dr.GetDecimal(1);
                        }
                    }
                }
            }
            lblGiaVon.Text = $"Giá vốn ước tính: {totalCost:N0} VNĐ";
        }

        private void txtTimKiemSanPham_TextChanged(object sender, EventArgs e)
        {
            LoadDanhSachSanPham(txtTimKiemSanPham.Text);
        }

        private void cbNguyenLieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNguyenLieu.SelectedValue != null && int.TryParse(cbNguyenLieu.SelectedValue.ToString(), out int maNL))
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT DonViTinh, SoLuongTon FROM NguyenLieu WHERE MaNL = @maNL";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@maNL", maNL);
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtDonVi.Text = dr["DonViTinh"].ToString();
                                decimal tonKho = Convert.ToDecimal(dr["SoLuongTon"]);
                                lblTonKho.Text = $"Tồn kho: {tonKho:N2}";
                                lblTonKho.ForeColor = tonKho < 10 ? System.Drawing.Color.Red : System.Drawing.Color.Blue;
                            }
                        }
                    }
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputs();
            if (cbSanPham.SelectedValue != null)
                LoadDataGridView(Convert.ToInt32(cbSanPham.SelectedValue));
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (cbSanPham.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn món nguồn cần sao chép công thức!");
                return;
            }

            int maSPNguon = Convert.ToInt32(cbSanPham.SelectedValue);

            // Mockup: Trong thực tế bạn có thể hiện 1 hộp thoại chọn món đích. 
            // Ở đây tôi sẽ hướng dẫn người dùng chọn món đích trên ComboBox rồi nhấn xác nhận qua InputBox hoặc nút khác.
            // Để đơn giản, tôi sẽ hiện thông báo hướng dẫn.

            string input = Microsoft.VisualBasic.Interaction.InputBox("Nhập Mã Sản Phẩm Đích (MaSP) để sao chép vào:", "Sao chép công thức", "");
            if (int.TryParse(input, out int maSPDich))
            {
                if (maSPDich == maSPNguon) { MessageBox.Show("Món đích trùng với món nguồn!"); return; }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();
                    try
                    {
                        // 1. Xóa công thức cũ của món đích (Tùy chọn)
                        string deleteQuery = "DELETE FROM CongThucPhaChe WHERE MaSP = @maDich";
                        using (SqlCommand delCmd = new SqlCommand(deleteQuery, con, trans))
                        {
                            delCmd.Parameters.AddWithValue("@maDich", maSPDich);
                            delCmd.ExecuteNonQuery();
                        }

                        // 2. Chèn công thức mới từ món nguồn
                        string copyQuery = @"INSERT INTO CongThucPhaChe (MaSP, MaNL, LuongDung, DonVi, GhiChu)
                                             SELECT @maDich, MaNL, LuongDung, DonVi, GhiChu 
                                             FROM CongThucPhaChe WHERE MaSP = @maNguon";
                        using (SqlCommand copyCmd = new SqlCommand(copyQuery, con, trans))
                        {
                            copyCmd.Parameters.AddWithValue("@maDich", maSPDich);
                            copyCmd.Parameters.AddWithValue("@maNguon", maSPNguon);
                            copyCmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        MessageBox.Show("Sao chép công thức thành công!");
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Lỗi khi sao chép: " + ex.Message);
                    }
                }
            }
        }

        private void ClearInputs()
        {
            cbNguyenLieu.SelectedIndex = -1;
            txtLuongDung.Clear();
            txtDonVi.Clear();
            txtGhiChu.Clear();
            lblTonKho.Text = "Tồn kho: ???";
            maCTPH_Selected = -1;
        }

        private bool ValidateInputs()
        {
            if (cbSanPham.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbNguyenLieu.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!decimal.TryParse(txtLuongDung.Text, out decimal luong) || luong <= 0)
            {
                MessageBox.Show("Lượng dùng phải là số > 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void frmDinhMuc_Load(object sender, EventArgs e)
        {
            LoadDanhSachSanPham();
            LoadComboBoxNguyenLieu();
            ClearInputs();
        }

        private void cbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSanPham.SelectedValue != null && int.TryParse(cbSanPham.SelectedValue.ToString(), out int maSP))
            {
                LoadDataGridView(maSP);
                ClearInputs();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string checkQuery = "SELECT COUNT(*) FROM CongThucPhaChe WHERE MaSP = @MaSP AND MaNL = @MaNL";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                {
                    checkCmd.Parameters.AddWithValue("@MaSP", cbSanPham.SelectedValue);
                    checkCmd.Parameters.AddWithValue("@MaNL", cbNguyenLieu.SelectedValue);
                    con.Open();
                    if ((int)checkCmd.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Nguyên liệu này đã tồn tại!");
                        return;
                    }
                }
                string query = "INSERT INTO CongThucPhaChe (MaSP, MaNL, LuongDung, DonVi, GhiChu) VALUES (@MaSP, @MaNL, @LuongDung, @DonVi, @GhiChu)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MaSP", cbSanPham.SelectedValue);
                    cmd.Parameters.AddWithValue("@MaNL", cbNguyenLieu.SelectedValue);
                    cmd.Parameters.AddWithValue("@LuongDung", decimal.Parse(txtLuongDung.Text));
                    cmd.Parameters.AddWithValue("@DonVi", txtDonVi.Text);
                    cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công!");
                    LoadDataGridView(Convert.ToInt32(cbSanPham.SelectedValue));
                    ClearInputs();
                }
            }
        }

        private void btnThemNhieu_Click(object sender, EventArgs e)
        {
            if (cbSanPham.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn món trước khi thêm nhiều nguyên liệu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int maSP = Convert.ToInt32(cbSanPham.SelectedValue);

            // Mở cửa sổ Pop-up
            Form popup = new Form();
            popup.Text = $"Thêm Nhanh Nguyên Liệu cho: {lblTenSPHienTai.Text}";
            popup.Size = new System.Drawing.Size(700, 500);
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.FormBorderStyle = FormBorderStyle.FixedDialog;
            popup.MaximizeBox = false;
            popup.MinimizeBox = false;

            // Header panel (Search)
            Panel panelTop = new Panel() { Dock = DockStyle.Top, Height = 50, Padding = new Padding(10) };
            Label lblSearch = new Label() { Text = "Tìm nguyên liệu:", Top = 15, Left = 10, Width = 100 };
            TextBox txtSearch = new TextBox() { Top = 12, Left = 110, Width = 300 };
            panelTop.Controls.Add(lblSearch);
            panelTop.Controls.Add(txtSearch);

            // Grid
            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoGenerateColumns = false;

            // Add grid columns
            DataGridViewCheckBoxColumn colChk = new DataGridViewCheckBoxColumn() { Name = "Chon", HeaderText = "Chọn", Width = 50 };
            DataGridViewTextBoxColumn colMa = new DataGridViewTextBoxColumn() { Name = "MaNL", DataPropertyName = "MaNL", Visible = false };
            DataGridViewTextBoxColumn colTen = new DataGridViewTextBoxColumn() { Name = "TenNguyenLieu", DataPropertyName = "TenNguyenLieu", HeaderText = "Tên Nguyên Liệu", Width = 200, ReadOnly = true };
            DataGridViewTextBoxColumn colDonVi = new DataGridViewTextBoxColumn() { Name = "DonViTinh", DataPropertyName = "DonViTinh", HeaderText = "Đơn Vị Tính", Width = 100, ReadOnly = true };
            DataGridViewTextBoxColumn colGhiChu = new DataGridViewTextBoxColumn() { Name = "GhiChu", HeaderText = "Ghi Chú", Width = 150 };
            DataGridViewTextBoxColumn colLuong = new DataGridViewTextBoxColumn() { Name = "LuongDung", HeaderText = "Lượng Dùng (Nhập số)", Width = 150 };
            colLuong.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow; // Highlight input col

            dgv.Columns.AddRange(colChk, colMa, colTen, colDonVi, colGhiChu, colLuong);

            // Fetch and bind data
            DataTable dtNL = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaNL, TenNguyenLieu, DonViTinh FROM NguyenLieu", con);
                da.Fill(dtNL);
            }
            dgv.DataSource = dtNL;

            // Search filter event
            txtSearch.TextChanged += (s, ev) => {
                dtNL.DefaultView.RowFilter = string.Format("TenNguyenLieu LIKE '%{0}%'", txtSearch.Text);
            };

            // Bottom panel (Buttons)
            Panel panelBot = new Panel() { Dock = DockStyle.Bottom, Height = 50 };
            Button btnSave = new Button() { Text = "Xác Nhận Lưu", Width = 120, Height = 30, Top = 10, Left = popup.Width - 150 - 20 };
            btnSave.BackColor = System.Drawing.Color.PaleGreen;
            panelBot.Controls.Add(btnSave);

            // Form Layout
            popup.Controls.Add(dgv);
            popup.Controls.Add(panelTop);
            popup.Controls.Add(panelBot);

            // Save logic
            btnSave.Click += (s, ev) =>
            {
                int countAdded = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.Cells["Chon"].Value != null && (bool)row.Cells["Chon"].Value == true)
                        {
                            int maNL = Convert.ToInt32(row.Cells["MaNL"].Value);
                            string donVi = row.Cells["DonViTinh"].Value.ToString();
                            string ghiChu = row.Cells["GhiChu"].Value?.ToString() ?? "";
                            string luongStr = row.Cells["LuongDung"].Value?.ToString();

                            if (string.IsNullOrEmpty(luongStr) || !decimal.TryParse(luongStr, out decimal luong) || luong <= 0)
                            {
                                MessageBox.Show($"Dòng nguyên liệu '{row.Cells["TenNguyenLieu"].Value}' chưa nhập lượng dùng hợp lệ. Lượng dùng phải > 0.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return; // Stop if invalid
                            }

                            // Optional: Check if already exists in CongThucPhaChe
                            string checkQuery = "SELECT COUNT(*) FROM CongThucPhaChe WHERE MaSP = @MaSP AND MaNL = @MaNL";
                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                            {
                                checkCmd.Parameters.AddWithValue("@MaSP", maSP);
                                checkCmd.Parameters.AddWithValue("@MaNL", maNL);
                                if ((int)checkCmd.ExecuteScalar() > 0) continue; // Skip existing
                            }

                            // Insert
                            string insertQ = "INSERT INTO CongThucPhaChe (MaSP, MaNL, LuongDung, DonVi, GhiChu) VALUES (@MaSP, @MaNL, @LuongDung, @DonVi, @GhiChu)";
                            using (SqlCommand cmd = new SqlCommand(insertQ, con))
                            {
                                cmd.Parameters.AddWithValue("@MaSP", maSP);
                                cmd.Parameters.AddWithValue("@MaNL", maNL);
                                cmd.Parameters.AddWithValue("@LuongDung", luong);
                                cmd.Parameters.AddWithValue("@DonVi", donVi);
                                cmd.Parameters.AddWithValue("@GhiChu", ghiChu);
                                cmd.ExecuteNonQuery();
                                countAdded++;
                            }
                        }
                    }
                }

                if (countAdded > 0)
                {
                    MessageBox.Show($"Đã thêm thành công {countAdded} nguyên liệu!");
                    LoadDataGridView(maSP);
                    popup.Close();
                }
                else
                {
                    MessageBox.Show("Có vẻ bạn chưa chọn nguyên liệu nào, hoặc nguyên liệu đã có sẵn trong công thức.");
                }
            };

            popup.ShowDialog();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maCTPH_Selected == -1 || !ValidateInputs()) return;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE CongThucPhaChe SET MaNL = @MaNL, LuongDung = @LuongDung, DonVi = @DonVi, GhiChu = @GhiChu WHERE MaCTPH = @MaCTPH";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MaCTPH", maCTPH_Selected);
                    cmd.Parameters.AddWithValue("@MaNL", cbNguyenLieu.SelectedValue);
                    cmd.Parameters.AddWithValue("@LuongDung", decimal.Parse(txtLuongDung.Text));
                    cmd.Parameters.AddWithValue("@DonVi", txtDonVi.Text);
                    cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa thành công!");
                    LoadDataGridView(Convert.ToInt32(cbSanPham.SelectedValue));
                    ClearInputs();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maCTPH_Selected == -1) return;
            if (MessageBox.Show("Xác nhận xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM CongThucPhaChe WHERE MaCTPH = @MaCTPH";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@MaCTPH", maCTPH_Selected);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa thành công!");
                        LoadDataGridView(Convert.ToInt32(cbSanPham.SelectedValue));
                        ClearInputs();
                    }
                }
            }
        }

        private void dgvDinhMuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDinhMuc.Rows[e.RowIndex];
                maCTPH_Selected = Convert.ToInt32(row.Cells["MaCTPH"].Value);
                cbNguyenLieu.SelectedValue = row.Cells["MaNL"].Value;
                txtLuongDung.Text = row.Cells["LuongDung"].Value.ToString();
                txtDonVi.Text = row.Cells["DonVi"].Value.ToString();
                txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbSanPham.SelectedValue == null) return;
                int maSP = Convert.ToInt32(cbSanPham.SelectedValue);

                // Sử dụng class báo cáo đã được generate
                ReportDocument rpt = new ReportDocument();
                rpt.Load(Application.StartupPath + @"\InCTPC.rpt");
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InCongThucPhaChe", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    rpt.SetDataSource(dt);
                }

                // Gán parameter cho report
                rpt.SetParameterValue("@MaSP", maSP);

                frmHienThiBaoCao f = new frmHienThiBaoCao();
                f.crystalReportViewer2.ReportSource = rpt;
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if (ex.InnerException != null) errorMsg += "\n\nChi tiết: " + ex.InnerException.Message;
                MessageBox.Show("Lỗi in công thức: " + errorMsg);
            }
        }
    }
}