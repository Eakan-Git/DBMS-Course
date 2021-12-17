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
        const int maxRowsPerPage = 10;
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi khi kết nối với cơ sở dữ liệu!");
            }
        }
        void loadData()
        {
           
        }
    }
}
