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

namespace DemoLoi
{
    public partial class Form2 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        SqlDataAdapter adapter2 = new SqlDataAdapter();
        DataTable table = new DataTable();
        string str = @"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True";
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            this.Hide();
            form.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.ShowDialog();
            this.Close();
        }

        private void DHKH_Click(object sender, EventArgs e)
        {
            
        }

        private void SPKH_Click(object sender, EventArgs e)
        {

        }

        private void loadDH()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
            {
                try
                {
                    string queryStr = "select MaDH from DONHANG order by MaDH offset 0 rows fetch next 20 rows only";
                    SqlDataAdapter da = new SqlDataAdapter(queryStr, connection);
                    connection.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DONHANG");
                    DHKH.DisplayMember = "MaDH";
                    DHKH.ValueMember = "MaDH";
                    DHKH.DataSource = ds.Tables["DONHANG"];
                    DHKH.SelectedItem = null;

                    SqlDataAdapter da2 = new SqlDataAdapter(queryStr, connection);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2, "DONHANG");
                    DHTX.DisplayMember = "MaDH";
                    DHTX.ValueMember = "MaDH";
                    DHTX.DataSource = ds2.Tables["DONHANG"];
                    DHTX.SelectedItem = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối với cơ sở dữ liệu!");
                }
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            loadDH();
        }
        private void capNhat_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("KH_capnhat_SL", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaDH", DHKH.SelectedValue);
                    cmd.Parameters.AddWithValue("@MaSP", SPKH.SelectedValue);
                    cmd.Parameters.AddWithValue("@SoLuong", soLuong.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thành công.");
                }
            }
            updateSoLuong();
        }

        private void chayLoi_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("TX_xem_CTDH", con))
                {
                    DataSet ds = new DataSet();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaDH", DHTX.SelectedValue);
                    cmd.Parameters.AddWithValue("@MaSP", SPTX.SelectedValue);
                    con.Open();
                    ds.Clear();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    dataGridView2.DataSource = ds.Tables[0];
                    dataGridView1.DataSource = ds.Tables[1];
                    MessageBox.Show("Chạy thành công.");
                }
            }
        }
        private void loadCTDH_KH()
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                try
                {
                    using(SqlCommand cmd = new SqlCommand("select MaSP from CHITIETDONHANG where MaDH = @MaDH", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@MaDH", DHKH.SelectedValue);
                        con.Open();
                        DataSet ds = new DataSet();
                        adapter.SelectCommand = cmd;
                        adapter.Fill(ds, "CHITIETDONHANG");
                        SPKH.DisplayMember = "MaSP";
                        SPKH.ValueMember = "MaSP";
                        SPKH.DataSource = ds.Tables["CHITIETDONHANG"];
                        SPKH.SelectedItem = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void loadCTDH_TX()
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("select MaSP from CHITIETDONHANG where MaDH = @MaDH", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@MaDH", DHTX.SelectedValue);
                        con.Open();
                        DataSet ds = new DataSet();
                        adapter.SelectCommand = cmd;
                        adapter.Fill(ds, "CHITIETDONHANG");
                        SPTX.DisplayMember = "MaSP";
                        SPTX.ValueMember = "MaSP";
                        SPTX.DataSource = ds.Tables["CHITIETDONHANG"];
                        SPTX.SelectedItem = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void suaLoi_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("TX_xem_CTDH2", con))
                {
                    DataSet ds = new DataSet();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaDH", DHTX.SelectedValue);
                    cmd.Parameters.AddWithValue("@MaSP", SPTX.SelectedValue);
                    con.Open();
                    table.Clear();
                    adapter2 = new SqlDataAdapter(cmd);
                    adapter2.Fill(ds);
                    dataGridView2.DataSource = ds.Tables[0];
                    dataGridView1.DataSource = ds.Tables[1];
                    MessageBox.Show("Chạy thành công.");
                }
            }
        }

        private void DHKH_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loadCTDH_KH();
        }

        private void DHTX_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loadCTDH_TX();
        }

        private void updateSoLuong()
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("select SoLuong from CHITIETDONHANG where MaDH = @MaDH and MaSP = @MaSP", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@MaDH", DHKH.SelectedValue);
                        cmd.Parameters.AddWithValue("@MaSP", SPKH.SelectedValue);
                        con.Open();
                        soLuong.Value = (Int32)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void SPKH_SelectionChangeCommitted(object sender, EventArgs e)
        {
            updateSoLuong();
        }

        private void suaLoiKH_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("KH_capnhat_SL2", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaDH", DHKH.SelectedValue);
                    cmd.Parameters.AddWithValue("@MaSP", SPKH.SelectedValue);
                    cmd.Parameters.AddWithValue("@SoLuong", soLuong.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thành công.");
                }
            }
            updateSoLuong();
        }
    }
}
