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

        private void frmProducts_Load(object sender, EventArgs e)
        {
            // create datatable to hold the categories from Db 
            DataTable categoriesDT = cDal.Select();

            // specifify data source for category Combobox
            cmbCategory.DataSource = categoriesDT;

            //specify display  member and value member for combobox
            cmbCategory.DisplayMember = "title";
            cmbCategory.ValueMember = "title";
        }
    }
}
