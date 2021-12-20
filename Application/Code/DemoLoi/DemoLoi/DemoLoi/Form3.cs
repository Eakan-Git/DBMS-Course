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
    public partial class Form3 : Form
    {
        string defaultText = "Số sản phẩm tìm được: ";
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        string str = @"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True";
        public Form3()
        {
            InitializeComponent();
            connection = new SqlConnection(str);
            this.dataGridView2.Columns[0].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void LoadSP()
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "select * from SANPHAM where Gia=@Gia";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Gia", GiaKH.Text);
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
               dataGridView2.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            this.Hide();
            form.ShowDialog();
            this.Close();
        }

        private void capNhat_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("NV_themSP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenSP", tenSP.Text);
                    cmd.Parameters.AddWithValue("@Gia", GiaNV.Text);
                    cmd.Parameters.AddWithValue("@MoTa", moTa.Text);
                    cmd.Parameters.AddWithValue("@GiaTieuChuan", GiaTC.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void chayLoi_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("Xem_SP_voigia", con))
                {
                    DataSet ds = new DataSet();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Gia", GiaKH.Text);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    labelQuery.Text = defaultText + ds.Tables[0].Rows[0][0].ToString();
                    dataGridView2.DataSource = ds.Tables[1].DefaultView;
                    MessageBox.Show("Chạy thành công.");
                }
            }
        }

        private void suaLoi_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("Xem_SP_voigia2", con))
                {
                    DataSet ds = new DataSet();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Gia", GiaKH.Text);
                    con.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    labelQuery.Text = defaultText + ds.Tables[0].Rows[0][0].ToString();
                    dataGridView2.DataSource = ds.Tables[1].DefaultView;
                    MessageBox.Show("Chạy thành công.");
                }
            }
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView2.Columns[0].Visible = true;
            this.dataGridView2.Rows[e.RowIndex].Cells["STT"].Value = (e.RowIndex + 1).ToString();
        }
    }
}
