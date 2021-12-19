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
    public partial class StaffForm : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable tableStaff = new DataTable();
        string str = @"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True";
        string placeholder = "Nhập mã nhân viên...";
        private int offset;
        const int maxRowsPerPage = 15;
        public StaffForm()
        {
            InitializeComponent();
            offset = 0;
        }

        private void StaffForm_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            autoLoadStaffData();
            btnPrevious.Enabled = false;
            searchBox.Text = placeholder;
        }

        private void autoLoadStaffData()
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "select nv.MaNV,tt.HoTen,tt.SoDienThoai,tt.DiaChi,tt.Email from NHANVIEN nv, THONGTINCANHAN tt where nv.ID=tt.ID Order by nv.MaNV,tt.HoTen,tt.SoDienThoai,tt.DiaChi,tt.Email offset @offset rows fetch next @rows rows only  ";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@rows", maxRowsPerPage);
                adapter.SelectCommand = command;
                tableStaff.Clear();
                adapter.Fill(tableStaff);
                StaffDGV.DataSource = tableStaff;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.");
            }
        }

        private void StaffDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = StaffDGV.CurrentCell.RowIndex;
            tbID.Text = StaffDGV.Rows[rowIndex].Cells[0].Value.ToString();
            tbName.Text = StaffDGV.Rows[rowIndex].Cells[1].Value.ToString();
            tbPhone.Text = StaffDGV.Rows[rowIndex].Cells[2].Value.ToString();
            tbAddress.Text = StaffDGV.Rows[rowIndex].Cells[3].Value.ToString();
            tbEmail.Text = StaffDGV.Rows[rowIndex].Cells[4].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
            {
                try
                {
                    connection.Open();
                    SqlCommand DeleteNVCommand = new SqlCommand("Delete NHANVIEN where MANV=@MANV", connection);
                    DeleteNVCommand.Parameters.AddWithValue("@MANV", tbID.Text);
                    DeleteNVCommand.ExecuteNonQuery();
                    SqlCommand DeleteTTCNCommand = new SqlCommand("Delete THONGTINCANHAN where SoDienThoai = @SDT", connection);
                    DeleteTTCNCommand.Parameters.AddWithValue("@SDT", tbPhone.Text);
                    DeleteTTCNCommand.ExecuteNonQuery();
                    autoLoadStaffData();
                    MessageBox.Show("Đã xóa Nhân viên!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
            {
                try
                {
                    connection.Open();
                    SqlCommand InsertCommand = new SqlCommand("EXEC InsertNV @Hoten, @SDT,@DiaChi,@Email", connection);
                    InsertCommand.Parameters.AddWithValue("@HoTen", tbName.Text);
                    InsertCommand.Parameters.AddWithValue("@SDT", tbPhone.Text);
                    InsertCommand.Parameters.AddWithValue("@DiaChi", tbAddress.Text);
                    InsertCommand.Parameters.AddWithValue("@Email", tbEmail.Text);
                    InsertCommand.ExecuteNonQuery();
                    autoLoadStaffData();  
                    MessageBox.Show("Nhân viên được thêm thành công.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "select NV.MANV, TT.HOTEN,TT.DIACHI,TT.EMAIL from NHANVIEN NV, THONGTINCANHAN TT where NV.MANV = @MANV and NV.ID = TT.ID";
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@MANV", searchBox.Text);
            adapter.SelectCommand = command;
            tableStaff.Clear();
            adapter.Fill(tableStaff);
            StaffDGV.DataSource = tableStaff;
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            if (searchBox.Text == "")
            {
                autoLoadStaffData();
            }
        }

        private void searchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                command = connection.CreateCommand();
                command.CommandText = "select NV.MANV, TT.HOTEN,TT.DIACHI,TT.EMAIL from NHANVIEN NV, THONGTINCANHAN TT where NV.MANV = @MANV and NV.ID = TT.ID";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@MANV", searchBox.Text);
                adapter.SelectCommand = command;
                tableStaff.Clear();
                adapter.Fill(tableStaff);
                StaffDGV.DataSource = tableStaff;
            }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
            {
                try
                {
                    connection.Open();
                    SqlCommand SaveCommand = new SqlCommand("UPDATE THONGTINCANHAN set HoTen=@HoTen, SoDienThoai=@SDT, DiaChi=@DiaChi, Email=@Email where ID=(select NV.ID from NHANVIEN NV where NV.MaNV=@MaNV)", connection);
                    SaveCommand.Parameters.AddWithValue("@MaNV", tbID.Text);
                    SaveCommand.Parameters.AddWithValue("@HoTen", tbName.Text);
                    SaveCommand.Parameters.AddWithValue("@SDT", tbPhone.Text);
                    SaveCommand.Parameters.AddWithValue("@DiaChi", tbAddress.Text);
                    SaveCommand.Parameters.AddWithValue("@Email", tbEmail.Text);
                    SaveCommand.ExecuteNonQuery();
                    autoLoadStaffData();
                    MessageBox.Show("Chỉnh sửa thông tin thành công.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            btnPrevious.Enabled = true;
            offset += 10;
            try
            {
                autoLoadStaffData();
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
            autoLoadStaffData();
        }
    }
}
