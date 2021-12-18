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
    public partial class CustomerProfile : Form
    {
        public CustomerProfile()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            nameTb.Enabled = true;
            addressTb.Enabled = true;
            phoneNumTb.Enabled = true;
            emailTb.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            nameTb.Enabled = false;
            addressTb.Enabled = false;
            phoneNumTb.Enabled = false;
            emailTb.Enabled = false;
        }

        private void CustomerProfile_Load(object sender, EventArgs e)
        {
            nameTb.Enabled = false;
            addressTb.Enabled = false;
            phoneNumTb.Enabled = false;
            emailTb.Enabled = false;
        }
    }
}
