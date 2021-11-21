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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.AcceptButton = loginBtn;
        }

        //The very first lines of code
        //For testing close login form after login
        //Leave it here until login verification done
        private void loginBtn_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm();
            this.Hide();
            homeForm.StartPosition = FormStartPosition.CenterScreen;
            homeForm.ShowDialog();
            this.Close();
        }
    }
}
