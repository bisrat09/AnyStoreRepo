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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }


        userBLL u = new userBLL();
        userDAL dal = new userDAL();
        private void PictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click_1(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click_2(object sender, EventArgs e)
        {

        }

        private void FrmUsers_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            //getting data from UI
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;
            // change 1. when login form is done 
            u.added_by = 1;

            //inserting data into database
            bool success = dal.Insert(u);
            //if data is successfully inserted,then value will be true
            if (success == true)
            {
                //data inserted 
                MessageBox.Show("User successfully created.");
                clear();

            }
            else
            {
                //data insertion failed
                MessageBox.Show("Failed to add new user");
            }
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;

        }
        private void clear()
        {
            txtUserID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtUsername.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
            cmbUserType.Text = "";

        }

        private void DgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // get the index of particular row 
            int rowIndex = e.RowIndex;
            txtUserID.Text = dgvUsers.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvUsers.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvUsers.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[rowIndex].Cells[3].Value.ToString();
            txtUsername.Text = dgvUsers.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[rowIndex].Cells[5].Value.ToString();
            txtContact.Text = dgvUsers.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text = dgvUsers.Rows[rowIndex].Cells[7].Value.ToString();
            cmbGender.Text = dgvUsers.Rows[rowIndex].Cells[8].Value.ToString();
            cmbUserType.Text = dgvUsers.Rows[rowIndex].Cells[9].Value.ToString();


        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            //getting data from UI
            u.id = Convert.ToInt32(txtUserID.Text);
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;
            // change 1. when login form is done 
            u.added_by = 1;
            // updating data in to database 
            bool success = dal.Update(u);
            // if data is updated successfully then success will be true,false otherwise
            if ( success == true)
            {
                //data updated sucessfully
                MessageBox.Show("User updated successfully");
                clear();
            }else
            {
                // failed to update DB
                MessageBox.Show("Failed to updtae User");

            }
            //refreshing data grid view
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;


        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // get userID from form 
            u.id = Convert.ToInt32(txtUserID.Text);
            bool success = dal.Delete(u);
            // if data is deleted then success will be true,false otherwise 
            if( success == true)
            {
                //User deleted successfully
                MessageBox.Show("User deleted successfully");
            }
            else
            {
                //Failed to delete user
                MessageBox.Show("Failed to delete user.");
            }
            //refreshing data grid view
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
            

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //get keyword from text box
            string keywords = txtSearch.Text;
            //check if keywords has value 
            if (keywords!= null)
            {
                //show user based on keywords
                DataTable dt = dal.Search(keywords);
                dgvUsers.DataSource = dt;
            }
            else
            {
                //show all users from Database
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;
            }
        }
    }
}
