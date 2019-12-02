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
    public partial class frmCategories : Form
    {
        public frmCategories()
        {
            InitializeComponent();
        }

        private void frmCategories_Load(object sender, EventArgs e)
        {
            // display all the categories in the data grid view
            DataTable dt = dal.Select();
            dgvCategories.DataSource = dt;

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        categoriesBLL c = new categoriesBLL();
        categoriesDAL dal = new categoriesDAL();
        userDAL udal = new userDAL();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get the values from the category form
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;

            // Getting ID in Added by field
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);
            // passing the id of loggedin user in added by field
            c.added_by = usr.id;

            //creating Boolean Method to insert data to database
            bool success = dal.Insert(c);
            // if the category is inserted successfully then the value of success will be true ,false otherwise
            if (success == true)
            {
                //newCategory inserted successfully
                MessageBox.Show("New category inserted successfully.");
                clear();
                // refresh data grid view
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;

            }
            else
            {
                // failed to insert new category
                MessageBox.Show("Failed to Insert new category");
            }
         }

        public void clear()
        {
            txtCategoryID.Text = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";


        }

        private void dgvCategories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // find the row index value i.e id of the row clicked
            int RowIndex = e.RowIndex;
            txtCategoryID.Text = dgvCategories.Rows[RowIndex].Cells[0].Value.ToString();
            txtTitle.Text = dgvCategories.Rows[RowIndex].Cells[1].Value.ToString();
            txtDescription.Text = dgvCategories.Rows[RowIndex].Cells[2].Value.ToString();


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get the values from the categories form 
            c.id = int.Parse(txtCategoryID.Text);
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);
            // passing the id of loggedin user in added by field
            c.added_by = usr.id;

            // boolean variable to update categories and check
            bool success = dal.Update(c);
            //if category is updated successfully then the value of success will be true, false otherwise
            if (success == true)
            {
                //category updated sucessfully 
                MessageBox.Show("Category Updated Successfully");
                clear();
                // refresh data grid view 
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;

            }
            else
            {
                //Failed to Update category
                MessageBox.Show("Failed to Update Category");
            }



        }
        private void btnDelete_Click(object sender, EventArgs e)
        {

            //Get the ID of the Category Which we want to Delete
            c.id = int.Parse(txtCategoryID.Text);

            //Creating Boolean Variable to Delete The CAtegory
            bool success = dal.Delete(c);

            //If the Category id Deleted Successfully then the vaue of success will be true else it will be false
            if (success == true)
            {
                //Category Deleted Successfully
                MessageBox.Show("Category Deleted Successfully");
                clear();
                //REfreshing DAta Grid View
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                //FAiled to Delete CAtegory 
                MessageBox.Show("Failed to Delete CAtegory");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // get the keywords first
            string keywords = txtSearch.Text;
            // filter the categories based on keywords
            if (keywords!= null)
            {
                //Use Search method to display Categories 
                DataTable dt = dal.Search(keywords);
                dgvCategories.DataSource = dt;
                
            }
            else
            {
                // use select methods to display all categories present
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
        }
    }
}
