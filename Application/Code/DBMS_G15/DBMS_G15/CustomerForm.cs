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
namespace DBMS_G15
{
    public partial class CustomerForm : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        string str = @"Data Source = (local); Initial Catalog = DBMS_ThucHanh_Nhom15; Integrated Security = True";

        private int offset;
        const int maxRowsPerPage = 15;
        public CustomerForm()
        {
            InitializeComponent();
            connection = new SqlConnection(str);
            offset = 0;
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            try
            {
                autoLoadData();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi khi kết nối với cơ sở dữ liệu!");
            }
        }
        void autoLoadData()
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "select kh.MaKH, tt.HoTen, tt.SoDienThoai, tt.DiaChi, tt.Email from THONGTINCANHAN tt, KHACHHANG kh where tt.ID = kh.ID order by MaKH offset @offset rows fetch next @rows rows only";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@rows", maxRowsPerPage);
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                customerDGV.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kết nối với cơ sở dữ liệu!");
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            btnPrevious.Enabled = true;
            offset += 10;
            try
            {
                autoLoadData();
                btnPrevious.Enabled = true;
            }
            catch (Exception ex)
            {
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            offset -= 10;
            if (offset <= 0)
            {
                offset = 0;
                btnPrevious.Enabled = false;
            }
            autoLoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void customerDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
