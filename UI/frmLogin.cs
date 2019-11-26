using AnyStore.BLL;
using AnyStore.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();
        public static string loggedIn;
        private void pboxClose_Click(object sender, EventArgs e)
        {
            // code to close this form 
            this.Close();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            l.username = txtUsername.Text.Trim();
            l.password = txtUsername.Text.Trim();
            l.user_type = cmbUserType.Text.Trim();

            //checking the login credentials

            bool success = dal.loginCheck(l);
            if(success == true)
            {
                //login success
                MessageBox.Show("Login Successful");
                loggedIn = l.username;
                // open respective forms based on user type 
                switch (l.user_type)
                {
                    case "Admin":
                        {
                            //display Admin Dashboard
                            frmAdminDashboard admin = new frmAdminDashboard();
                            admin.Show();
                            this.Hide();

                        }
                        break;
                    case "User":
                        {
                            //display User dashboard
                            frmUserDashBoard user = new frmUserDashBoard();
                            user.Show();
                            this.Hide();
                        }
                        break;
                    default:
                        {
                            //display error message
                            MessageBox.Show("Invalid User Type.");
                        }
                        break;
                }

            }
            else
            {
                // login failed 
                MessageBox.Show("Login Failed. Try Again");

            }

        }
    }
}
