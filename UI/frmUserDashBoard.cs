using AnyStore.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore
{
    public partial class frmUserDashBoard : Form
    {
        public frmUserDashBoard()
        {
            InitializeComponent();
        }

        private void LblLoggedInUser_Click(object sender, EventArgs e)
        {

        }

        private void frmUserDashBoard_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
        }
    }
}
