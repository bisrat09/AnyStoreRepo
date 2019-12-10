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
    public partial class frmPurchaseAndSales : Form
    {
        public frmPurchaseAndSales()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void lblBillDate_Click(object sender, EventArgs e)
        {

        }
        DeaCustDAL dcDAL = new DeaCustDAL();

        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
            //Get the transactiontype value from user dashboard
            string type = frmUserDashBoard.transactionType;
            // set the value on lblTop
            lblTop.Text = type;

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Get the keyword from text box
            string keyword = txtSearch.Text;

            if (keyword == "")
            {
                //clear all the text boxes 
                txtName.Text = "";
                txtEmail.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";
                return;
            }
            //  get the details and set to trext boxes 
            DeaCustBLL dc = dcDAL.SearchDealerCustomerForTransaction(keyword);

            // transfer or set the value from DeaCustBLL to text boxes 
            txtName.Text = dc.name;
            txtEmail.Text = dc.email;
            txtContact.Text = dc.contact;
            txtAddress.Text = dc.address;

        }
    }
}
