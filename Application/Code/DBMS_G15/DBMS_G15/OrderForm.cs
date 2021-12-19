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
    public partial class OrderForm : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable tableProduct = new DataTable();
        public OrderForm()
        {
            InitializeComponent();
            loadProduct();
            loadIDCus();
            loadArea();
        }

        private void loadProduct()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
            {
                try
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = "select MaSP,TenSP from SANPHAM";
                    command.CommandType = CommandType.Text;
                    adapter.SelectCommand = command;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    cbbProduct.DataSource = dt;
                    cbbProduct.DisplayMember = "TenSP";
                    cbbProduct.ValueMember = "MaSP";
                    cbbProduct.SelectedItem = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối với cơ sở dữ liệu!");
                }
            }
        }

        private void loadIDCus()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
            {
                try
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = "select MaKH from KHACHHANG";
                    command.CommandType = CommandType.Text;
                    adapter.SelectCommand = command;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    cbbIDCustomer.DataSource = dt;
                    cbbIDCustomer.DisplayMember = "MAKH";
                    cbbIDCustomer.ValueMember = "MAKH";
                    cbbIDCustomer.SelectedItem = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối với cơ sở dữ liệu!");
                }
            }
        }

        private void loadArea()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
            {
                try
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = "select MaKV ,TenKV from KHUVUC";
                    command.CommandType = CommandType.Text;
                    adapter.SelectCommand = command;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    cbbArea.DataSource = dt;
                    cbbArea.DisplayMember = "TenKV";
                    cbbArea.ValueMember = "MaKV";
                    cbbArea.SelectedItem = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối với cơ sở dữ liệu!");
                }
            }
        }

        //void LoadOrder()
        //{
        //    if (cbbIDCustomer.Text != "" && cbbArea.Text != "")
        //    {
        //        command = connection.CreateCommand();
        //        command.CommandText = "select * from DONHANG where MaKH=@MaKH'";
        //        command.CommandType = CommandType.Text;
        //        command.Parameters.AddWithValue("@MaKH", cbbIDCustomer.SelectedValue.ToString());
        //        adapter.SelectCommand = command;
        //        tableProduct.Clear();
        //        adapter.Fill(tableProduct);
        //        OrderDGV.DataSource = tableProduct;
        //    }
        //}

        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    if (cbbIDCustomer.Text == "" || cbbArea.Text == "")
        //    {     
        //        MessageBox.Show("Vui lòng chọn đủ thông tin.");
        //    }
        //    else
        //    {
        //        DialogResult confirm = MessageBox.Show("Xác nhận thêm đơn hàng này?", "Thêm đơn hàng", MessageBoxButtons.YesNo);
        //        if (confirm == DialogResult.Yes)
        //        {
        //            try
        //            {
        //                connection.Open();
        //                    SqlCommand updateCommand = new SqlCommand("Insert into DONHANG(MaKH,PhiVanChuyen,MaKV) values (@MaKH,@PhiVanChuyen,@MaKV)", connection);
        //                    updateCommand.Parameters.AddWithValue("@MaKH", cbbIDCustomer.SelectedValue.ToString());
        //                    //updateCommand.Parameters.AddWithValue("@TinhTrang", "Đang xử lý");
        //                    updateCommand.Parameters.AddWithValue("@MaKV", cbbArea.SelectedValue.ToString());
        //                    updateCommand.ExecuteNonQuery();
        //                    LoadOrder();
        //                    MessageBox.Show("Thêm Đơn hàng thành công");
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message);
        //            }
        //        }
        //    }
        //}

        private void bttADD_CT_Click(object sender, EventArgs e)
        {

        }
    }
}