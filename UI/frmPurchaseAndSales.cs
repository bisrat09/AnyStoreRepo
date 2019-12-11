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
        productsDAL pDAL = new productsDAL();

        DataTable transactionDT = new DataTable();

        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
            //Get the transactiontype value from user dashboard
            string type = frmUserDashBoard.transactionType;
            // set the value on lblTop
            lblTop.Text = type;

            //Specify Columns for our transactionDataTable
            transactionDT.Columns.Add("ProductName");
            transactionDT.Columns.Add("Rate");
            transactionDT.Columns.Add("Quantity");
            transactionDT.Columns.Add("Total");

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

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            // Get the keyword from productSearch textbox
            string keyword = txtSearchProduct.Text;

            // check if txtSearchProduct has value
            if(keyword == "")
            {
                txtProductName.Text = "";
                txtInventory.Text = "";
                txtRate.Text = "";
                txtQty.Text = "";
                return;
            }

            // search the product and display on respective textboxes 
           productsBLL p = pDAL.GetProductsForTransaction(keyword);

            // set the values on textboxes based on the p object
            txtProductName.Text = p.name;
            txtInventory.Text = p.qty.ToString();
            txtRate.Text = p.rate.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Get Product Name, Rate and Qty customer wants to buy
            string productName = txtProductName.Text;
            decimal Rate = decimal.Parse(txtRate.Text);
            decimal Qty = decimal.Parse(txtQty.Text);
            Decimal Total = Rate * Qty;

            //display the subTotal in text box
            //Get total value from text box
            decimal subTotal = Decimal.Parse(txtSubTotal.Text);
            subTotal = subTotal + Total; 

            // check if the product is selected or not 
            if (productName == "")
            {
                // display Error message
                MessageBox.Show("Select the product first and Try again");

            }
            else
            {
                // add pdt to data grid view 
                transactionDT.Rows.Add(productName, Rate, Qty, Total);

                // Show in Data grid view
                dgvAddedProducts.DataSource = transactionDT;

                //display the subtotal in textBox
                txtSubTotal.Text = subTotal.ToString();

                // clear text boxes 
                txtSearchProduct.Text = "";
                txtProductName.Text = "";
                txtInventory.Text = "0.00";
                txtRate.Text = "0.00";
                txtQty.Text = "0.00";
            }
        }
    }
}
