using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnyStore.BLL;

namespace AnyStore.DAL
{
    class categoriesDAL
    {
        // static string method for a database connection String
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        #region Select method 
        public DataTable Select()
        {
            //connection to the database
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // sql query to get all the data from the database 
                string sql = "select * from tbl_categories";
                SqlCommand cmd = new SqlCommand(sql,conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // open database coonection
                conn.Open();
                // adding value from adapter to datatable dt
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
        #region Insert New Category
        public bool Insert(categoriesBLL c)
        {
            //creating a boolean variable and setting its default value to false 
            bool isSuccess = false;
            //connecting to Database 
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                // query to add new category
                string sql = "insert into tbl_categories (title,description,added_date,added_by) values (@title,@description,@added_date,@added_by)";
                //creating sql command to pass values in our query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //passing values through parameter
                cmd.Parameters.AddWithValue("@title",c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);

                // open Database connection 
                conn.Open();

                // Creating the int variable to excute query
                int rows = cmd.ExecuteNonQuery();
                //If query is excuted successfully then its value will be greater than 0,else it will be less than 0
                if (rows > 0)
                {
                    //Query excuted Successfully 
                    isSuccess = true;

                }
                else
                {
                    // Query Failed
                    isSuccess = false;
                }


            }
            catch(Exception ex)
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
        #region Update Method
        public bool Update (categoriesBLL c)
        {
            //creating a boolean varaible and set it to false 
            bool isSuccess = false;
            // Creating SQL Connection 
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //Query to update category
                string sql = "update tbl_categories set title =@title,description=@description,added_date=@added_date,added_by=@added_by where id=@id";
                // Sql command to pass the value of the query 
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);
                cmd.Parameters.AddWithValue("@id", c.id);

                // open the db connection
                conn.Open();
                // create Int variable to excute query
                int rows = cmd.ExecuteNonQuery();
                // if query is successfully excuted then value will be greater than 0.
                if (rows> 0)
                {
                    isSuccess = true;
                }
                else
                {
                    // Query excution Failed 
                    isSuccess = false;
                }

            }catch( Exception ex)
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
        #region Delete Category Method
        public bool Delete( categoriesBLL c)
        {
            //Create a boolean vartiable and set its value to false 
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                // SQL query to Delete from Database
                string sql = "delete from tbl_categories where id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Passing the value using cmd
                cmd.Parameters.AddWithValue("@id", c.id);

                //Open SqlConnection 
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                
                if (rows > 0)
                {
                    //query excuted successfully 
                    isSuccess = true;
                }
                else
                {
                    // query failed to excute
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
        #region Method for Search Functionality
        public DataTable Search(string keywords)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            // create datatable to hold the data from database temporarily
            DataTable dt = new DataTable();
            try
            {
                //SQL Query to search categories from database 
                string sql = "select * from tbl_categories where id like '%" + keywords + "%' or title like '%" + keywords + "%' or description like '%" + keywords + "%'";
                //excute the query 
                SqlCommand cmd = new SqlCommand(sql, conn);
                // get data rom database 
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // open database connection 
                conn.Open();
                // passing values from datatable dt
                adapter.Fill(dt);
            }
            catch( Exception ex)
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
