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
    public partial class frmTransactions : Form
    {
        public frmTransactions()
        {
            InitializeComponent();
        }
        // make atransaction DAL obj
        transactionDAL tDAL = new transactionDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmTransactions_Load(object sender, EventArgs e)
        {
            // Display all the transactions 
            DataTable dt = tDAL.DisplayAllTransactions();
            // pass it to datagrid view 
            dgvTransactions.DataSource = dt;
           
        }

        private void cmbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the value from comboBox
            string type = cmbTransactionType.Text;

            DataTable dt = tDAL.DisplayTransactionByType(type);
            dgvTransactions.DataSource = dt;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            // Display all the transactions 
            DataTable dt = tDAL.DisplayAllTransactions();
            // pass it to datagrid view 
            dgvTransactions.DataSource = dt;


        }
    }
}
