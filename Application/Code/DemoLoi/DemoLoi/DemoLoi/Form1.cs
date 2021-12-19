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
        DataTable tableProduct = new DataTable();
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
            connection.Open();
        }
        private void LoadNV()
        {

        }
        private void LoadKH()
        {

        }
        private void NVChayLoi_Click(object sender, EventArgs e)
        {
            LoadNV();
        }

        private void NVSuaLoi_Click(object sender, EventArgs e)
        {
            LoadNV();
        }

        private void KHChayLoi_Click(object sender, EventArgs e)
        {
            LoadKH();
        }

        private void KHSuaLoi_Click(object sender, EventArgs e)
        {
            LoadKH();
        }
    }
}
