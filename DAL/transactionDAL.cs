using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class transactionDAL
    {
        // create a connection string varable 
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert Transaction Method
        public bool Insert_Transaction ( transactionsBLL t , out int transactionID)
        {
            //create a boolean value and set its default to false 
            bool isSuccess = false;
            // set the out transactionID value to negative one.
            transactionID = -1;
            // create sql connection 
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                // SQL query to insert transaction 
                string sql = "insert into tbl_transactions (type, dea_cust_id, grandTotal, transaction_date, tax, discount, added_by) values(@type, @dea_cust_id, @grandTotal, @transaction_date, @tax, @discount, @added_by); select @@IDENTITY;";

                //sql command to pass the value in sql query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing the value to sql query using cmd
                cmd.Parameters.AddWithValue("@type", t.type);
                cmd.Parameters.AddWithValue("@dea_cust_id", t.dea_cust_id);
                cmd.Parameters.AddWithValue("@grandTotal", t.grandTotal);
                cmd.Parameters.AddWithValue("@transaction_date", t.transaction_date);
                cmd.Parameters.AddWithValue("@tax", t.tax);
                cmd.Parameters.AddWithValue("@discount", t.discount);
                cmd.Parameters.AddWithValue("@added_by", t.added_by);

                // Open database connection
                conn.Open();

                // excute the query
                object o = cmd.ExecuteScalar();

                // if query is excuted successfully then the value will not be null , else it will be null
                if (o != null)
                {
                    // Query executed successfully
                    transactionID = int.Parse(o.ToString());
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
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
        #region Method to display all the transaction
        public DataTable DisplayAllTransactions()
        {
            //SqlConnection 
            SqlConnection conn = new SqlConnection(myconnstrng);

            // Create DataTable and return it 
            DataTable dt = new DataTable();
            try
            {
                // Write the SQL query to display database temporarily
                string sql = "select * from tbl_transactions";

                // Sql command to excute Query
                SqlCommand cmd = new SqlCommand(sql, conn);
                // sql adapter hold data temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // open db connection
                conn.Open();

                adapter.Fill(dt);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;


        }
        #endregion
        #region Method to display Transaction based on Transaction Type
        public DataTable DisplayTransactionByType(string type)
        {
            // sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            // create a dataTable 
            DataTable dt = new DataTable();
            try
            {
                // create sql query
                string sql = "select * from tbl_transactions where type='"+type+"'";
                // sql command 
                SqlCommand cmd = new SqlCommand(sql, conn);

                // adpater to hol
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // open connection
                conn.Open();

                adapter.Fill(dt);



            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            

            

            return dt;
        }
        #endregion
    }
}
