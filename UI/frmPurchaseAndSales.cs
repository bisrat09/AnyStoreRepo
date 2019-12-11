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

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            // Get the value from discount Textbox
            string value = txtDiscount.Text;

            if (value == "")
            {
                //Display error message
                MessageBox.Show("Please Add Discount First and try Again ");
            }
            else
            {
                // get the discount in decimal 
                decimal discount = decimal.Parse(txtDiscount.Text);
                decimal subTotal = decimal.Parse(txtSubTotal.Text);
                // calculate Grandtotal based on discount 
                decimal grandTotal = ((100 - discount) / 100) * subTotal;

                //display the grand total in text box
                txtGrandTotal.Text = grandTotal.ToString();
            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            // check if grandtotal has a value , if no vaue , then calculate discount first
            string check = txtGrandTotal.Text;
            if (check == "")
            {
                //Display error message
                MessageBox.Show("Please Calculate the discount and set Grand total first");
            }
            else
            {
                //calculate VAT
                //getting the VAT percentage first
                decimal PreviousGT = decimal.Parse(txtGrandTotal.Text);
                decimal vat = decimal.Parse(txtVat.Text);
                decimal grandTotalWithVAt = ((100 + vat) / 100) * PreviousGT;

                // displaying new Grand total with VAT
                txtGrandTotal.Text = grandTotalWithVAt.ToString();
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            // get the paidAmount and Grand Total
            decimal grandTotal = decimal.Parse(txtGrandTotal.Text);
            decimal paidAmount = decimal.Parse(txtPaidAmount.Text);

            // display the return amount 

            decimal returnAmount = paidAmount - grandTotal;
            txtReturnAmount.Text = returnAmount.ToString();
        }
    }
}
