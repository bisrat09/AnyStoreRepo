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
    class DeaCustDAL
    {
        //create a static string method for Database connection 
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region SELECT method for Dealer and Customer
        public DataTable Select()
        {
            // SQL connection for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            // DataTable to hold value
            DataTable dt = new DataTable();


            try
            {
                // write sql Query to Insert Details of Dealer and Customer 
                string sql = "select * from tbl_dea_cust";

                // sql Command to pass the values of the query and excute 
                SqlCommand cmd = new SqlCommand(sql, conn);

                // creating sql command to excute query 
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // open Database connection
                conn.Open();
                //Passing the value from Data Adapter to data table 
                adapter.Fill(dt);
             
            }
            catch (Exception ex)
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
        #region INSERT method to Add details for Dealer and Customer 
        public bool Insert(DeaCustBLL dc)
        {
            // create sql connection 
            SqlConnection conn = new SqlConnection(myconnstrng);

            // Create a boolean value and set its default
            bool isSuccess = false;

            try
            {
                // sql query to insert details of Dealer or Customer   
                string sql = "insert into tbl_dea_cust(type, name, email, contact, address, added_date, added_by) values (@type, @name, @email, @contact, @address, @added_date, @added_by)";

                // sql command to pass the values to query and excute 
                SqlCommand cmd = new SqlCommand(sql, conn);

                // passing the values using Parameters
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);

                // Open db connection 
                conn.Open();

                // integer varaiable to check weather the query excuted or not 
                int rows = cmd.ExecuteNonQuery();
                // if query excuted successfully then the value of rows will be greater than 0 else it will be less than 0.

                if (rows >0)
                {
                    // query excuted successfully
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }catch (Exception ex)
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
        #region UPDATE method for Dealer and Customer Module
        public bool Update(DeaCustBLL dc)
        {
            // sql connection for db connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //boolean variable and set it to false 
            bool isSuccess = false;
            try
            {
                //write sql query to update the database
                string sql = "Update tbl_dea_cust set type=@type, name=@name, email=@email, contact=@contact, address=@address, added_date=@added_date, added_by=@added_by where id=@id";

                // create sql command to pass value in sql
                SqlCommand cmd = new SqlCommand(sql, conn);
                //passing the values through parameters 
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);
                cmd.Parameters.AddWithValue("@id", dc.id);

                //open the database connection
                conn.Open();

                // int variable to check if the query is excuted fully or not 
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                   {
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
        #region DELETE method for Dealer and customer
        public bool Delete(DeaCustBLL dc)
        {
            // SQL connection for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            // create a boolean variable and set its default value to false 
            bool isSuccess = false;
            try
            {
                // SQl Query to delete data from database
                string sql = "delete from tbl_dea_cust where id=@id";
                // sql command to pass value 
                SqlCommand cmd = new SqlCommand(sql, conn);
                // pass the value 
                cmd.Parameters.AddWithValue("@id", dc.id);
                // open DB connection
                conn.Open();
                // int varable to check delete is successful
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }catch(Exception ex)
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
