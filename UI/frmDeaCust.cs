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
    public partial class frmDeaCust : Form
    {
        public frmDeaCust()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            // write code to close this form 
            this.Hide();
        }

        DeaCustBLL dc = new DeaCustBLL();
        DeaCustDAL dcDal = new DeaCustDAL();
        userDAL uDal = new userDAL();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // get the values from form 
            dc.type = cmbDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;
            // getting the loggedin user id and passing its value to deal or customer module 
            string loggedUsr = frmLogin.loggedIn;
            userBLL usr = uDal.GetIDFromUsername(loggedUsr);
            dc.added_by = usr.id;

            // boolean variable to check if the dealer/customer is added or not 
            bool success = dcDal.Insert(dc);

            if(success == true)
            {
                //dealer/customer is inserted successfully
                MessageBox.Show("Dealer or customer added Successfully");
                Clear();
                // refresh data grid view 
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;

            }
            else
            {
                //Failed to insert dealer /customer
                MessageBox.Show("");
            }

        }
        public void Clear()
        {
            txtDeaCustID.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = ""; 
            txtAddress.Text = "";
            txtSearch.Text = "";
        }

        private void frmDeaCust_Load(object sender, EventArgs e)
        {
            // refresh data grid view 
            DataTable dt =  dcDal.Select();
            dgvDeaCust.DataSource = dt;
        }

        private void dgvDeaCust_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // int variable to get the identity of row clicked 
            int rowIndex = e.RowIndex;
            // 
            txtDeaCustID.Text = dgvDeaCust.Rows[rowIndex].Cells[0].Value.ToString();
            cmbDeaCust.Text = dgvDeaCust.Rows[rowIndex].Cells[1].Value.ToString();
            txtName.Text = dgvDeaCust.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDeaCust.Rows[rowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDeaCust.Rows[rowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dgvDeaCust.Rows[rowIndex].Cells[5].Value.ToString();


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // get the values from form 
            dc.id = int.Parse(txtDeaCustID.Text);
            dc.type = cmbDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;
            // getting the loggedin user id and passing its value to deal or customer module 
            string loggedUsr = frmLogin.loggedIn;
            userBLL usr = uDal.GetIDFromUsername(loggedUsr);
            dc.added_by = usr.id;
            // boolean variaable to check if update of dealer/customer is successful or not 
            bool success = dcDal.Update(dc);
            if (success == true)
            {
                // updated dealer/customer successfully
                MessageBox.Show("Dealer or Customer updated successfully");
                Clear();
                // refresh the data grid view 
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;

            }
            else
            {
                // failed to update customer/ dealer 
                MessageBox.Show("Failed to update dealer or Customer");

            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // get the id of the user to be deleted 
            dc.id = int.Parse(txtDeaCustID.Text);

            // boolean var to check if the data dealer/customer is deleted or not 
            bool success = dcDal.Delete(dc);
            if (success == true)
            {
                // dealer/cutomer is deleted successfully
                MessageBox.Show("Dealer or Customer deleted Successfully");
                Clear();
                // refresh the data grid view
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to delete Dealer/Customer");
            }
        }
    }
}
