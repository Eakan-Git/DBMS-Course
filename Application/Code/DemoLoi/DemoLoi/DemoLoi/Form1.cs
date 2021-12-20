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
    public partial class Form1 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        SqlDataAdapter adapter2 = new SqlDataAdapter();
        DataTable table = new DataTable();
        DataTable table2 = new DataTable();
        string str = @"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            this.Hide();
            form.ShowDialog();
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
        }
        private void LoadNV()
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "select * from CUNGCAP_SP cc, SANPHAM sp where sp.MaSP = cc.MaSP and cc.MaCN = @MaCN";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@MaCN", tbChiNhanhNV.Text);
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadKH()
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "select * from CUNGCAP_SP cc, SANPHAM sp where sp.MaSP = cc.MaSP and cc.MaCN = @MaCN";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@MaCN", tbChiNhanhKH.Text);
                adapter2.SelectCommand = command;
                table2.Clear();
                adapter2.Fill(table2);
                dataGridView2.DataSource = table2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void NVChayLoi_Click(object sender, EventArgs e)
        {
            using(SqlConnection con = new SqlConnection(str))
            {
                using(SqlCommand cmd = new SqlCommand("NV_giamgia_SP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaCN", tbChiNhanhNV.Text);
                    cmd.Parameters.AddWithValue("@GiaGiam", tbGiaGiam.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Chạy thành công.");
                }
            }
        }

        private void NVSuaLoi_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("NV_giamgia_SP2", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaCN", tbChiNhanhNV.Text);
                    cmd.Parameters.AddWithValue("@GiaGiam", tbGiaGiam.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Chạy thành công.");
                }
            }
        }

        private void KHChayLoi_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("XemSP_CuaCN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaCN", tbChiNhanhKH.Text);
                    con.Open();
                    adapter2 = new SqlDataAdapter(cmd);
                    adapter2.Fill(table2);
                    dataGridView2.DataSource = table2;
                    MessageBox.Show("Chạy thành công.");
                }
            }
        }

        private void KHSuaLoi_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("XemSP_CuaCN2", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaCN", tbChiNhanhKH.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Chạy thành công.");
                }
            }
        }

        private void btnLoadNV_Click(object sender, EventArgs e)
        {
            LoadNV();
        }

        private void btnLoadKH_Click(object sender, EventArgs e)
        {
            LoadKH();
        }
    }
}
