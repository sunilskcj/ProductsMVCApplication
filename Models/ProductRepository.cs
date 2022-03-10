
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace ProductsMVCApplication.Models
{
    public class ProductRepository
    {
        SqlDataReader reader;

        private string ConnectionString;
       

        public ProductRepository()
            {
            this.ConnectionString = "server=SKCJPC;database=ProductList;Integrated Security=true";
            }
        

        public bool AddProduct(Product product)
        {  
            SqlConnection connection = new SqlConnection(ConnectionString); 
            SqlCommand command;
           
        bool status = false;

            try
            {
                command = new SqlCommand();
                
                command.Connection = connection;   
                command.CommandText = "AddProduct";
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("@productId", product.ProductId)
                    .SqlDbType = SqlDbType.Int;
                   
                command.Parameters.AddWithValue("@productname", product.ProductName)
                    .SqlDbType = SqlDbType.VarChar;
                command.Parameters.AddWithValue("@price", product.Price)
                    .SqlDbType = SqlDbType.Decimal;
                command.Parameters.AddWithValue("@description", product.Description)
                    .SqlDbType = SqlDbType.VarChar;

                connection.Open();
                int result = command.ExecuteNonQuery(); //ExecuteReader Executescalar 
                if (result > 0) status = true;
               
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return status;
        }
        public List<Product> GetAllProducts()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command;
            

            List<Product> products = null;
            try
            {
               
                command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "GetAllProducts";
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    products = new List<Product>();
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductId = (int)reader["productid"],
                            ProductName = (string)reader["productname"],
                            Price = (decimal)reader["price"],
                            Description = (string)reader["description"],
                           
                        };
                        products.Add(product);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return products;
        }

        public Product GetARecordById(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command;
           
            Product product = null;
            try
            {
              
                command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "GetProduct";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@productId", id).SqlDbType = SqlDbType.Int;
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        product = new Product
                        {
                            ProductId = (int)reader["productid"],
                            ProductName = (string)reader["productname"],
                            Price = (decimal)reader["price"],
                            Description = (string)reader["description"],
                           
                        };
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return product;
        }
        public bool DeleteProduct(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command;

            bool status = false;

            try
            {
                command = new SqlCommand();

                command.Connection = connection;
                command.CommandText = "DeleteProduct";
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("@productId", id)
                    .SqlDbType = SqlDbType.Int;
                

                connection.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0) status = true;

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return status;
        }
    }
}
