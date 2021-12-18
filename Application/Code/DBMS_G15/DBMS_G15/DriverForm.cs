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
    public partial class DriverForm : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable tableProduct = new DataTable();
        string str = @"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True";
        string placeholder = "Nhập mã sản phâm...";

        private int offset;
        const int maxRowsPerPage = 15;
        public DriverForm()
        {
            InitializeComponent();
            offset = 0;
        }

        private void DriverForm_Load(object sender, EventArgs e)
        {

        }
    }
}
