using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class transactionDetailDAL
    {
        // create a connection string 
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert Method for Transaction Detail
        public bool InsertTransactionDetail(transactionDetailBLL td)
        {
            // create a boolean value and set it to false 
            bool isSuccess = false;

            //create Sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                // Sql to insert transaction details 
                string sql = "insert into tbl_transaction_detail(product_id, rate, qty, total, dea_cust_id, added_date, added_by) values (@product_id, @rate, @qty, @total, @dea_cust_id, @added_date, @added_by)";

                // pass the value to the sql query 
                SqlCommand cmd = new SqlCommand(sql, conn);

                // passing the values using cmd
                cmd.Parameters.AddWithValue("@product_id", td.product_id);
                cmd.Parameters.AddWithValue("@rate", td.rate);
                cmd.Parameters.AddWithValue("@qty", td.qty);
                cmd.Parameters.AddWithValue("@total", td.total);
                cmd.Parameters.AddWithValue("@dea_cust_id", td.dea_cust_id);
                cmd.Parameters.AddWithValue("@added_date", td.added_date);
                cmd.Parameters.AddWithValue("added_by", td.added_by);

                // open db connection
                conn.Open();

                // decalre int variable and excute the query
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // query excuted successfully
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch( Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
    }
}
