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
    class productsDAL
    {
        //creating a static string method for DB connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        #region Select Method for product module
        public DataTable Select()
        {
            //create sql connection to connect to database 
            SqlConnection conn = new SqlConnection(myconnstrng);

            //datatable to hold data from database
            DataTable dt = new DataTable();
            try
            {
                //Wrting the query to select all the products from database
                String sql = "Select * from tbl_products";

                //Creating SQL Command to evaluate Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Sql Data adapter to hold the value of the db temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open database connection 
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
        #region Method to Insert Product in database
        public bool Insert (productsBLL p)
        {
            bool isSuccess = false;
            //Sql connection for database 
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                // SQL Query to insert product in to database 
           
                string sql = "insert into tbl_products(name, category, description, rate, qty, added_date, added_by ) values (@name, @category, @description, @rate, @qty, @added_date, @added_by)";
                //creating SQL command to pass the values 
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing the values through parameters
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);

                //opening dB connection 
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // ifthe query is excuted then the value of rows will be > 0 else <0
                if (rows>0)
                {
                    // Query excuted successfully
                    isSuccess = true;
                }
                else
                {
                    // Query failed 
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
        #region Method to UPDATE product in database 
        public bool Update (productsBLL p)
        {
            //create boolean variable and set it to false 
            bool isSuccess = false;
            // create sql connection to db 
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                // sql query to update data to db
                String sql = "Update tbl_products set name =@name, category=@category, description=@description, rate=@rate, added_date=@added_date, added_by=@added_by where id =@id";

                // create sql command the value to query
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description",p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.rate);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);
                cmd.Parameters.AddWithValue("@id", p.id);

                //open db connection
                conn.Open();

                // create integer variable to check if the query is excuted successfully or not 
                int rows = cmd.ExecuteNonQuery();
                // if query is excuted successfully then value pf rows will be > 0, else < 0
                if (rows > 0)
                {
                    //query excuted successfully
                    isSuccess = true;
                }
                else
                {
                    //query failed to excute successfully
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
        #region Method to delete product from database 
        public bool Delete(productsBLL p)
        {
            // create a boolean variable tand set it to false 
            bool isSuccess = false;
            //SQl connection for DB connection 
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                // Write query to delete product from database
                string sql = "delete from tbl_products where id=@id";

                // sql command to pass the value 
                SqlCommand cmd = new SqlCommand(sql, conn);
                //passing the value using cmd
                cmd.Parameters.AddWithValue("@id", p.id);
                // open connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if query is excuted successfully then the value of rows will be > 0,else < 0
                if (rows> 0)
                {
                    // q=uery excuted successfully
                    isSuccess = true;
                }
                else
                {
                    // query failed to excute.
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
        #region Search Method for Product module
        public DataTable Search (string keywords)
        {
            
            // SQL connection to connect to Database 
            SqlConnection conn = new SqlConnection(myconnstrng);
            // Datatable to hold value from database 
            DataTable dt = new DataTable();
            try
            {
                // sql query to search for product 
                string sql = "select * from tbl_products where id like '%"+keywords+"%' or name like '%"+keywords+"%' or category like '%"+keywords+"%'";
                //sql command to excute query
                SqlCommand cmd = new SqlCommand(sql, conn);
                // sql data adapter to hold the data temporarily 
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // open database connection
                conn.Open();
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
        #region Method to search product in Transaction Module
        public productsBLL GetProductsForTransaction(string keyword)
        {
            // create an object of productsBLL and return it
            productsBLL p = new productsBLL();
            // sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            // DataTable to store data temporarily
            DataTable dt = new DataTable();
            try
            {
                //Write the query to get the details
                string sql = "select name, rate, qty from tbl_products where id like '%"+keyword+"%' or name like '%"+keyword+"%'";

                // crete sql data adapter to excute query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                //open the db connection
                conn.Open();
                // pass the value from adapter to dt
                adapter.Fill(dt);

                //if data has any values ,then set the values to productBLL
                if (dt.Rows.Count > 0)
                {
                    p.name = dt.Rows[0]["name"].ToString();
                    p.rate = decimal.Parse(dt.Rows[0]["rate"].ToString());
                    p.qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
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
            return p;
        }

        #endregion
        #region Method to get Product ID based on Product Name 
        public productsBLL GetProductIDFromName(string ProductName)
        {
            // first create an obj of Deacust BLL and return it 
            productsBLL p = new productsBLL();
            // Sql Connection here 
            SqlConnection conn = new SqlConnection(myconnstrng);

            // Data table to hold data temporarily
            DataTable dt = new DataTable();

            try
            {
                // sql query to get ID based oon name 
                string sql = "select id from tbl_products where name='" + ProductName + "'";
                // sql Data Adapter to excute query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();

                // passing the value from adapter to data table 
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //Pass the value from dt to DeaCustBLL dc
                    p.id = int.Parse(dt.Rows[0]["id"].ToString());
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

            return p;
        }
        #endregion
        #region Method to get current quantity from Database based on Product ID
        public decimal GetProductQty(int ProductID)
        {
            // sql connection 
            SqlConnection conn = new SqlConnection(myconnstrng);

            //create A decimal variable and set its default value to 0
            decimal qty = 0;

            // create a dataTable to save the data from database temporarily
            DataTable dt = new DataTable();
            try
            {
                // sql query to get Qty from database 
                string sql = "select qty from tbl_products where id = " +ProductID;

                // create sql command 
                SqlCommand cmd = new SqlCommand();

                // create a sql data Adapter to excute the query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                //open database connection
                conn.Open();

                // pass the value from data adapter to Datatable
                adapter.Fill(dt);
                // Lets check if the datatable has value or not
                if (dt.Rows.Count > 0)
                {
                    qty = decimal.Parse(dt.Rows[0]["qty"].ToString());               
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // close database connection
                conn.Close();
            }
            return qty;
        }
        #endregion
        #region Method to Update Quantity
        public bool UpdateQuantity(int ProductID, decimal Qty)
        {
            // cretae a boolean variable and set it to false;
            bool success = false;
            // sql connection to connect to database 
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                // Write sql Query to update Qty
                string sql = "Update tbl_products set qty=@qty where id=@id";
                //Create SQL Command to pass the value in to the query
                SqlCommand cmd = new SqlCommand(sql, conn);
                // passing the value through parameters
                cmd.Parameters.AddWithValue("@qty", Qty);
                cmd.Parameters.AddWithValue("@id", ProductID);

                //oprn database connection
                conn.Open();

                // check if the query is excuted successfully
                int rows = cmd.ExecuteNonQuery();
                if( rows> 0)
                {
                    //Query excuted successfully
                    success = true;
                }
                else
                {
                    //Failed to excute query
                    success = false;
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
            return success;
        }
        #endregion
        #region Method to Increase Product qty
        public bool IncreaseProduct( int ProductID, decimal IncreaseQty)
        {
            //create a boolean variable and set it false 
            bool success = false;
            // create sql connection to connect to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //Get current quantity from database based on id
                decimal currentQty = GetProductQty(ProductID);

                // increase the current qty by qty purchased from dealer
                decimal NewQty = currentQty + IncreaseQty;

                // Update the product qty now
                success = UpdateQuantity(ProductID, NewQty);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return success;
        }
        #endregion
        #region Method to Decrease product 
        public bool DecreaseProduct(int ProductID, decimal Qty)
        {
            // create a boolean and set it to false 
            bool success = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                // get the current product qty
                Decimal currentQty = GetProductQty(ProductID);

                //Decrease the product Quantity based on product sales
                decimal NewQty = currentQty - Qty;

                //update the product in database
                success = UpdateQuantity(ProductID, NewQty);



            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }
        #endregion
    }
}
