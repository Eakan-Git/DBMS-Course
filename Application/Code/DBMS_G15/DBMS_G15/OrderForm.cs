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
        
        public OrderForm()
        {
            InitializeComponent();
            
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

        void LoadOrder()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
            {
                try
                {
                    connection.Open();
                    DataTable tbOrder = new DataTable();
                    command = connection.CreateCommand();
                    command.CommandText = "select MaDH, NgayDat, NgayGiao, TinhTrang, PhiVanChuyen, TongTien, MaKV from DONHANG where MaKH=@MaKH";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@MaKH", cbbIDCustomer.SelectedValue);
                    adapter.SelectCommand = command;
                    tbOrder.Clear();
                    adapter.Fill(tbOrder);
                    OrderDGV.DataSource = tbOrder;
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void bttADD_CT_Click(object sender, EventArgs e)
        {
            if (cbbProduct.SelectedValue.ToString() == "" || numericUpDown1.Value==0)
            {
                MessageBox.Show("Vui lòng chọn đủ thông tin.");
            }
            else
            {
                DialogResult confirm = MessageBox.Show("Xác nhận thêm sản phẩm này?", "Thêm sản phẩm", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
                    {
                        try
                        {
                            connection.Open();
                            SqlCommand updateCommand = new SqlCommand("Insert into CHITIETDONHANG(MaDH,MaSP,SoLuong) values (@MaDH,@MaSP,@SL)", connection);
                            updateCommand.Parameters.AddWithValue("@MaDH", OrderDGV.CurrentRow.Cells[0].Value);
                            updateCommand.Parameters.AddWithValue("@MaSP", cbbProduct.SelectedValue);
                            updateCommand.Parameters.AddWithValue("@SL", numericUpDown1.Value);
                            updateCommand.ExecuteNonQuery();
                            LoadDetailOrder(); 
                            MessageBox.Show("Thêm Sản phẩm vào giỏ hàng thành công");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void cbbIDCustomer_Click(object sender, EventArgs e)
        {
            loadIDCus();
        }

        private void cbbProduct_Click(object sender, EventArgs e)
        {
            loadProduct();
        }

        private void cbbArea_Click(object sender, EventArgs e)
        {
            loadArea();
        }

        private void cbbIDCustomer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
            {
                try
                {
                    connection.Open();
                    DataTable tbOrder = new DataTable();
                    command = connection.CreateCommand();
                    command.CommandText = "select MaDH, NgayDat, NgayGiao, TinhTrang, PhiVanChuyen, TongTien, MaKV from DONHANG where MaKH=@MaKH";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@MaKH", cbbIDCustomer.SelectedValue);
                    adapter.SelectCommand = command;
                    tbOrder.Clear();
                    adapter.Fill(tbOrder);
                    OrderDGV.DataSource = tbOrder;
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cbbProduct_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cbbArea_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            if (cbbIDCustomer.SelectedValue.ToString() == "" || cbbArea.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Vui lòng chọn đủ thông tin.");
            }
            else
            {
                DialogResult confirm = MessageBox.Show("Xác nhận thêm đơn hàng này?", "Thêm đơn hàng", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
                    {
                        try
                        {
                            connection.Open();
                            SqlCommand updateCommand = new SqlCommand("Insert into DONHANG(MaKH,MaKV) values (@MaKH,@MaKV)", connection);
                            updateCommand.Parameters.AddWithValue("@MaKH", cbbIDCustomer.SelectedValue);
                            //updateCommand.Parameters.AddWithValue("@TinhTrang", "Đang xử lý");
                            updateCommand.Parameters.AddWithValue("@MaKV", cbbArea.SelectedValue);
                            updateCommand.ExecuteNonQuery();
                            LoadOrder();
                            MessageBox.Show("Thêm Đơn hàng thành công");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        void LoadDetailOrder()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
            {
                try
                {
                    connection.Open();
                    DataTable tbDetailOrder = new DataTable();
                    command = connection.CreateCommand();
                    command.CommandText = "select CT.MaDH,SP.MaSP,SP.TenSP,CT.SoLuong,CT.ThanhTien from CHITIETDONHANG CT,SANPHAM SP where CT.MaDH=@MaDH and CT.MaSP=SP.MaSP";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@MaDH", OrderDGV.CurrentRow.Cells[0].Value);
                    adapter.SelectCommand = command;
                    tbDetailOrder.Clear();
                    adapter.Fill(tbDetailOrder);
                    DetailOrderDGV.DataSource = tbDetailOrder;
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void OrderDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadDetailOrder();
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Xác nhận xóa sản phẩm khỏi đơn hàng?", "Xóa Sản phẩm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand DeleteCommand = new SqlCommand("delete from CHITIETDONHANG WHERE MaDH=@MaDH and MaSP=@MaSP", connection);
                        DeleteCommand.Parameters.AddWithValue("@MaDH", DetailOrderDGV.CurrentRow.Cells[0].Value);
                        DeleteCommand.Parameters.AddWithValue("@MaSP", DetailOrderDGV.CurrentRow.Cells[1].Value);
                        DeleteCommand.ExecuteNonQuery();
                        LoadDetailOrder();
                        MessageBox.Show("Xóa sản phẩm khỏi đơn hàng thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Xác nhận xóa đơn hàng?", "Xóa Đơn hàng", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand DeleteCommand = new SqlCommand("delete from CHITIETDONHANG WHERE MaDH=@MaDH delete from DONHANG WHERE MaDH=@MaDH", connection);
                        DeleteCommand.Parameters.AddWithValue("@MaDH", DetailOrderDGV.CurrentRow.Cells[0].Value);
                        DeleteCommand.ExecuteNonQuery();
                        LoadDetailOrder();
                        LoadOrder();
                        MessageBox.Show("Xóa đơn hàng thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }


    }
}