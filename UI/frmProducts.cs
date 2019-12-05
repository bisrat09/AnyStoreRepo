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
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        categoriesDAL cDal = new categoriesDAL();
        productsBLL p = new productsBLL();
        productsDAL pdal = new productsDAL();
        userDAL udal = new userDAL();


        private void frmProducts_Load(object sender, EventArgs e)
        {
            // create datatable to hold the categories from Db 
            DataTable categoriesDT = cDal.Select();

            // specifify data source for category Combobox
            cmbCategory.DataSource = categoriesDT;

            //specify display  member and value member for combobox
            cmbCategory.DisplayMember = "title";
            cmbCategory.ValueMember = "title";

            //Load all the products in the data grid view 
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // get all the values from product form
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.qty = 0;
            p.added_date = DateTime.Now;

            // get username of the loggedin user 
            string loggedUsr = frmLogin.loggedIn;

            // get the id of th euloggedin user
            userBLL usr = udal.GetIDFromUsername(loggedUsr);
            p.added_by = usr.id;

            // boolean varaible if product is added successfully or not 
            bool success = pdal.Insert(p);
            // if product is added successfully then the value of success is will be true , false otherwise 
            if (success == true)
            {
                MessageBox.Show("Product Added Successfully");
                // clear the text in each field after inserting 
                Clear();
                // refreshing the data grid view
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;

                
            }
            else
            {
                MessageBox.Show("Failed to add new Product");
            }

            
        }
        public void Clear()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";

        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // create int variable to know which product is clicked 
            int rowIndex = e.RowIndex;
            //Display the value of respective text boxes
            txtID.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategory.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
            txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // get the values from UI or product form 
            p.id = int.Parse(txtID.Text);
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.added_date = DateTime.Now;
            // getting username of the loggedin user for added by 
            string loggedUsr = frmLogin.loggedIn;

            // get the id of the loggedin user
            userBLL usr = udal.GetIDFromUsername(loggedUsr);
            p.added_by = usr.id;

            // create boolean variable to check if product is updated or not 
            bool success = pdal.Update(p);
            if (success == true)
            {
                // product updated successfully 
                MessageBox.Show("Product updated successfully");
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;

            } else
            {
                // update failed 
                MessageBox.Show("Failed to Update Product");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the id of the product to be deleted 
            p.id = int.Parse(txtID.Text);

            //create boolean variable to check if data is deleted or not 
            bool success = pdal.Delete(p);

            //if product is deleted successfully then the value of success will be true else it will be false 
            if (success == true)
            {
                // product successfully deleted 
                MessageBox.Show("Product Deleted Successfully");
                // clear txt box 
                Clear();
                // refresh data grid view 
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;

            }else
            {
                MessageBox.Show("Failed to Delete Product");
            }
        }
    }
}
