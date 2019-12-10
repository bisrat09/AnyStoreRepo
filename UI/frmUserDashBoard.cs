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
        // set a public static method to specify if the form is purchase or sales
        public static string transactionType;
        private void LblLoggedInUser_Click(object sender, EventArgs e)
        {

        }

        private void frmUserDashBoard_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
        }

        private void frmUserDashBoard_Load(object sender, EventArgs e)
        {
            lblLoggedInUser.Text = frmLogin.loggedIn;
        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeaCust DeaCust = new frmDeaCust();
            DeaCust.Show();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // set value on transaction type static 
            transactionType = "Purchase";
            frmPurchaseAndSales purchase = new frmPurchaseAndSales();
            purchase.Show();
            
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // set the cvalue of transaction type method to sales
            transactionType = "Sales";
            frmPurchaseAndSales sales = new frmPurchaseAndSales();
            sales.Show();
        
        }
    }
}
