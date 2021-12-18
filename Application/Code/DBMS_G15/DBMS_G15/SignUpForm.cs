﻿using System;
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
    public partial class SignUpForm : Form
    {
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.ShowDialog();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxDriver_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxDriver.Checked == true)
            {
                tbBank.Enabled = true;
                tbBSX.Enabled = true;
                tbCMND.Enabled = true;
                cbbArea.Enabled = true;
            }
            else
            {
                tbBank.Text = tbBSX.Text = tbCMND.Text = "";
                cbbArea.SelectedItem = null;
                tbBank.Enabled = false;
                tbBSX.Enabled = false;
                tbCMND.Enabled = false;
                cbbArea.Enabled = false;
            }
        }

        private void SignUpForm_Load(object sender, EventArgs e)
        {
            tbBank.Enabled = false;
            tbBSX.Enabled = false;
            tbCMND.Enabled = false;
            cbbArea.Enabled = false;
            cbbArea.DropDownStyle = ComboBoxStyle.DropDownList;
            using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
            {
                try
                {
                    string queryStr = "SELECT * FROM KHUVUC";
                    SqlDataAdapter da = new SqlDataAdapter(queryStr, connection);
                    connection.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds, "KHUVUC");
                    cbbArea.DisplayMember = "TenKV";
                    cbbArea.ValueMember = "MaKV";
                    cbbArea.DataSource = ds.Tables["KHUVUC"];
                    cbbArea.SelectedItem = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối với cơ sở dữ liệu!");
                }
            }
        }

        private void signUpBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(local);Initial Catalog=DBMS_ThucHanh_Nhom15;Integrated Security=True"))
            {
                try
                {

                }
                catch(Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối với cơ sở dữ liệu!");
                }
            }
        }
    }
}