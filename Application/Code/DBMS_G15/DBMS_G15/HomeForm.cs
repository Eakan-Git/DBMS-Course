using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMS_G15
{
    public partial class HomeForm : Form
    {
        private Form currentChildForm;
        public HomeForm()
        {
            InitializeComponent();
            //Test for hidding from difference role
            //btnContract.Hide();
            //btnDriver.Hide();
            //btnPartner.Hide();
            //btnCustomer.Hide();
            //btnDriver.Hide();
            //btnStaff.Hide();
            
        }
        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childFormPanel.Controls.Add(childForm);
            childFormPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void btnProduct_Click(object sender, EventArgs e)
        {
            navMenu.BringToFront();
            navMenu.Height = btnProduct.Height;
            navMenu.Top = btnProduct.Top;
            OpenChildForm(new productForm());
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            navMenu.BringToFront();
            navMenu.Height = btnOrder.Height;
            navMenu.Top = btnOrder.Top;
            OpenChildForm(new OrderForm());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            navMenu.BringToFront();
            navMenu.Height = btnCustomer.Height;
            navMenu.Top = btnCustomer.Top;
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            navMenu.BringToFront();
            navMenu.Height = btnStaff.Height;
            navMenu.Top = btnStaff.Top;
            OpenChildForm(new StaffForm());
        }

        private void btnDriver_Click(object sender, EventArgs e)
        {
            navMenu.BringToFront();
            navMenu.Height = btnDriver.Height;
            navMenu.Top = btnDriver.Top;
            OpenChildForm(new DriverForm());
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            navMenu.BringToFront();
            navMenu.Height = btnPartner.Height;
            navMenu.Top = btnPartner.Top;
        }

        private void btnContract_Click(object sender, EventArgs e)
        {
            navMenu.BringToFront();
            navMenu.Height = btnContract.Height;
            navMenu.Top = btnContract.Top;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(OpenLoginForm));
            t.Start();
            this.Close();
        }
        public static void OpenLoginForm()
        {
            Application.Run(new LoginForm());
        }
    }
}
