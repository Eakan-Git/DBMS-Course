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
    public partial class productForm : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable tableProduct = new DataTable();
        string str = @"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True";
        string placeholder = "Nhập mã sản phâm...";

        private int offset;
        const int maxRowsPerPage = 15;
        public productForm()
        {
            InitializeComponent();
            offset = 0;
        }

        private void productForm_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            autoLoadProductData();
            btnPrevious.Enabled = false;
            searchBox.Text = placeholder;
            btnAdd.Enabled = false;
        }
        private void autoLoadProductData()
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "select * from SANPHAM order by MaSP offset @offset rows fetch next @rows rows only";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@rows", maxRowsPerPage);
                adapter.SelectCommand = command;
                tableProduct.Clear();
                adapter.Fill(tableProduct);
                productDGV.DataSource = tableProduct;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.");
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            btnPrevious.Enabled = true;
            offset += 10;
            try
            {
                autoLoadProductData();
                btnPrevious.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            autoLoadProductData();
        }

        private void productDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = productDGV.CurrentCell.RowIndex;
            tbID.Text = productDGV.Rows[rowIndex].Cells[0].Value.ToString();
            tbName.Text = productDGV.Rows[rowIndex].Cells[1].Value.ToString();
            tbPrice.Text = productDGV.Rows[rowIndex].Cells[2].Value.ToString();
            tbDescription.Text = productDGV.Rows[rowIndex].Cells[3].Value.ToString();
            loadDepartment();
        }

        private void searchBox_Enter(object sender, EventArgs e)
        {
            if (searchBox.Text == placeholder)
                searchBox.Text = "";
        }

        private void searchBox_Leave(object sender, EventArgs e)
        {
            if (searchBox.Text == "")
                searchBox.Text = placeholder;
        }
        private void seacrhBtn_Click(object sender, EventArgs e)
        {
            if (searchBox.Text != placeholder && searchBox.Text != "")
            {
                command = connection.CreateCommand();
                command.CommandText = "select * from SANPHAM where MaSp = @MaSP";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@MaSP", searchBox.Text);
                adapter.SelectCommand = command;
                tableProduct.Clear();
                adapter.Fill(tableProduct);
                productDGV.DataSource = tableProduct;
            }
            else
            {
                MessageBox.Show("Hãy nhập mã sản phẩm.");
            }
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            if(searchBox.Text == "")
            {
                autoLoadProductData();
            }
        }
        private void loadDepartment()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
            {
                try
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = "select cc.MaCN from CUNGCAP_SP cc, SANPHAM sp where cc.MaSP = @MaSP and sp.MaSP = cc.MaSP";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@MaSP", tbID.Text);
                    adapter.SelectCommand = command;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    cbbDepartment.DataSource = dt;
                    cbbDepartment.DisplayMember = "MaCN";
                    cbbDepartment.ValueMember = "MaCN";
                    cbbDepartment.SelectedItem = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối với cơ sở dữ liệu!");
                }
            }
        }
        private void searchBox_KeyPress(object sender, KeyPressEventArgs key)
        {
            if(key.KeyChar == (char)13)
            {
                if(searchBox.Text != placeholder && searchBox.Text != "")
                {
                    command = connection.CreateCommand();
                    command.CommandText = "select * from SANPHAM where MaSp = @MaSP";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@MaSP", searchBox.Text);
                    adapter.SelectCommand = command;
                    tableProduct.Clear();
                    adapter.Fill(tableProduct);
                    productDGV.DataSource = tableProduct;
                }
                else
                {
                    MessageBox.Show("Hãy nhập mã sản phẩm.");
                }
            }
        }
        private void loadAfterSave()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from SANPHAM where MaSp = @MaSP";
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@MaSP", tbID.Text);
            adapter.SelectCommand = command;
            tableProduct.Clear();
            adapter.Fill(tableProduct);
            productDGV.DataSource = tableProduct;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
            {
                try
                {
                    connection.Open();
                    SqlCommand checkProductExisted = new SqlCommand("select * from SANPHAM where MaSP = @MaSP", connection);
                    checkProductExisted.Parameters.AddWithValue("@MaSP", tbID.Text);
                    SqlDataReader reader = checkProductExisted.ExecuteReader();
                    if(reader.HasRows)
                    {
                        reader.Close();
                        DialogResult confirm = MessageBox.Show("Xác nhận xóa sản phẩm này?", "Xóa Sản Phẩm", MessageBoxButtons.YesNo);
                        if(confirm == DialogResult.Yes)
                        {
                            SqlCommand deleteCommand = new SqlCommand("delete from SANPHAM where MaSP = @MaSP", connection);
                            deleteCommand.Parameters.AddWithValue("@MaSP", tbID.Text);
                            deleteCommand.ExecuteNonQuery();
                            autoLoadProductData();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sản phẩm không tồn tại.");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
            {
                try
                {
                    connection.Open();
                    SqlCommand checkProductExisted = new SqlCommand("select * from SANPHAM where MaSP = @MaSP", connection);
                    checkProductExisted.Parameters.AddWithValue("@MaSP", tbID.Text);
                    SqlDataReader reader = checkProductExisted.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Close();
                        DialogResult confirm = MessageBox.Show("Xác nhận cập nhật sản phẩm này?", "Cập Nhật Sản Phẩm", MessageBoxButtons.YesNo);
                        if (confirm == DialogResult.Yes)
                        {
                            SqlCommand updateCommand = new SqlCommand("update SANPHAM set TenSP = @TenSP, Gia = @Gia, MoTa = @MoTa where MaSP = @MaSP", connection);
                            updateCommand.Parameters.AddWithValue("@MaSP", tbID.Text);
                            updateCommand.Parameters.AddWithValue("@TenSP", tbName.Text);
                            updateCommand.Parameters.AddWithValue("@Gia", tbPrice.Text);
                            updateCommand.Parameters.AddWithValue("@MoTa", tbDescription.Text);
                            updateCommand.ExecuteNonQuery();
                            autoLoadProductData();
                            MessageBox.Show("Cập nhật sản phẩm thành công");
                            //loadAfterSave();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sản phẩm không tồn tại.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void checkBoxAdd_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxAdd.Checked == true)
            {
                tbID.ReadOnly = false;
                tbID.Text = "";
                tbName.Text = "";
                tbPrice.Text = "";
                tbDescription.Text = "";
                btnAdd.Enabled = true;
            }
            else
            {
                tbID.ReadOnly = true;
                btnAdd.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(tbID.Text == "" || tbName.Text == "" || tbPrice.Text == "" || tbDescription.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                DialogResult confirm = MessageBox.Show("Xác nhận thêm sản phẩm này?", "Thêm Sản Phẩm", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        SqlCommand updateCommand = new SqlCommand("insert into SANPHAM values (@MaSP, @TenSP, @Gia, @MoTa)", connection);
                        updateCommand.Parameters.AddWithValue("@MaSP", tbID.Text);
                        updateCommand.Parameters.AddWithValue("@TenSP", tbName.Text);
                        updateCommand.Parameters.AddWithValue("@Gia", tbPrice.Text);
                        updateCommand.Parameters.AddWithValue("@MoTa", tbDescription.Text);
                        updateCommand.ExecuteNonQuery();
                        autoLoadProductData();
                        MessageBox.Show("Thêm sản phẩm thành công");
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi kết nối.");
                    }
                }
            }
        }
    }
}
