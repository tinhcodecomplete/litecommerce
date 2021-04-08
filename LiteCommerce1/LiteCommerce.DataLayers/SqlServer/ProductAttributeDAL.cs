using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SqlServer
{
    public class ProductAttributeDAL : IProductAttributeDAL
    {
        private string connectionString;
        public ProductAttributeDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(ProductAttribute data)
        {
            int productAttribute = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO ProductAttributes
                                                       (
                                                        ProductID
                                                       ,AttributeName
                                                       ,AttributeValues
                                                       ,DisplayOrder)
                                                 VALUES
                                                       (@ProductID, 
                                                       @AttributeName, 
                                                       @AttributeValues, 
                                                       @DisplayOrder )";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                    cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                    cmd.Parameters.AddWithValue("@AttributeValues", data.AttributeValues);
                    cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                    productAttribute = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return productAttribute;
        }

        public bool Delete(int[] AttributeIDs)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"DELETE FROM ProductAttributes
                                            WHERE AttributeID = @AttributeID

                                             ";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                  
                    cmd.Parameters.Add("@AttributeID", SqlDbType.Int);
                    foreach (int AttributeID in AttributeIDs)
                    {
                        cmd.Parameters["@AttributeID"].Value = AttributeID;
                        cmd.ExecuteNonQuery();
                    }

                }
                connection.Close();
            }
            return result;
        }

        public bool DeleteByProductID(int productID)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"DELETE FROM ProductAttributes
                                            WHERE ProductID = @ProductID

                                             ";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;

                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    
                    cmd.ExecuteNonQuery();
                    

                }
                connection.Close();
            }
            return result;
        }

        public ProductAttribute Get(int AttributeID)
        {
            ProductAttribute data = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"SELECT *  FROM ProductAttributes WHERE AttributeID = @AttributeID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@AttributeID", AttributeID);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            data = new ProductAttribute
                            {
                                ProductID = Convert.ToInt32(dbReader["ProductID"]),
                                AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                                AttributeName = Convert.ToString(dbReader["AttributeName"]),
                                AttributeValues= Convert.ToString(dbReader["AttributeValues"]),
                                DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                            };

                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        public List<ProductAttribute> List(int ProductID)
        {
            List<ProductAttribute> data = new List<ProductAttribute>();
           
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"SELECT *  FROM ProductAttributes WHERE ProductID = @ProductID";
                    cmd.CommandType = System.Data.CommandType.Text;
                    //
                    cmd.Connection = connection;
                    //dua du lieu vao cau lenh sql
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new ProductAttribute()
                            {
                                ProductID = Convert.ToInt32(dbReader["ProductID"]),
                                AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                                AttributeName = Convert.ToString(dbReader["AttributeName"]),
                                AttributeValues = Convert.ToString(dbReader["AttributeValues"]),
                                DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])


                            });
                        }

                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool Update(ProductAttribute data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"UPDATE ProductAttributes SET 
                                                       ProductID = @ProductID
                                                      ,AttributeName = @AttributeName
                                                      ,AttributeValues = @AttributeValues
                                                      ,DisplayOrder = @DisplayOrder                                             
                                                WHERE  AttributeID = @AttributeID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                    cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                    cmd.Parameters.AddWithValue("@AttributeValues", data.AttributeValues);
                    cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                    cmd.Parameters.AddWithValue("@AttributeID", data.AttributeID);


                    rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                }
                connection.Close();
            }
            return rowsAffected > 0;
        }
    }
}
