﻿using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SqlServer
{
    public class ProductDAL : IProductDAL
    {
        private string connectionString;
        public ProductDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(Product data)
        {
            int productID = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO Products
                                                       (ProductName
                                                       ,SupplierID
                                                       ,CategoryID
                                                       ,QuantityPerUnit
                                                       ,UnitPrice
                                                       ,Descriptions
                                                       ,PhotoPath)
                                                 VALUES
                                                       (@ProductName,
                                                       @SupplierID, 
                                                       @CategoryID,
                                                       @QuantityPerUnit,
                                                       @UnitPrice, 
                                                       @Descriptions, 
                                                       @PhotoPath )";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                    cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                    cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                    cmd.Parameters.AddWithValue("@QuantityPerUnit", data.QuantityPerUnit);
                    cmd.Parameters.AddWithValue("@UnitPrice", data.UnitPrice);
                    cmd.Parameters.AddWithValue("@Descriptions", data.Descriptions);
                    cmd.Parameters.AddWithValue("@PhotoPath", data.PhotoPath);
                    productID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return productID;
        }

        public int Count(string searchValue, int CategoryID, int SupplierID)
        {
            int rowCount = 0;
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                connection.Open();
               
                using (SqlCommand cmd = new SqlCommand())
                {



                    cmd.CommandText = @"select count(*) 
                                        from Products 
                                                 where((@searchValue = N'') or(ProductName like @searchValue)) and
			                                        ((@category = N'') or (CategoryID = @category)) and
			                                        ((@supplier = N'') or (SupplierID = @supplier))";

                    cmd.CommandType = System.Data.CommandType.Text;
                    
                    cmd.Connection = connection;
                    
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@category", CategoryID);
                    cmd.Parameters.AddWithValue("@supplier", SupplierID);

                    rowCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
               
                connection.Close();
            }
            return rowCount;
        }

        public bool Delete(int[] productIDs)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    
                    cmd.CommandText = @"DELETE FROM Products
                                                WHERE(productID = @productID)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@productID", SqlDbType.Int);
                    foreach (int productID in productIDs)
                    {
                        cmd.Parameters["@productID"].Value = productID;
                        cmd.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }


            return result;
        }

        public Product Get(int productID)
        {
            Product data = null;
            using(SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM Products WHERE ProductID = @ProductID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dataReader.Read())
                        {
                            data = new Product()
                            {
                                ProductID = Convert.ToInt32(dataReader["ProductID"]),
                                CategoryID = Convert.ToInt32(dataReader["CategoryID"]),
                                Descriptions = Convert.ToString(dataReader["Descriptions"]),
                                PhotoPath = Convert.ToString(dataReader["PhotoPath"]),
                                SupplierID = Convert.ToInt32(dataReader["SupplierID"]),
                                ProductName = Convert.ToString(dataReader["ProductName"]),
                                QuantityPerUnit = Convert.ToString(dataReader["QuantityPerUnit"]),
                                UnitPrice = Convert.ToDecimal(dataReader["UnitPrice"])
                            };
                        }
                            

                    }
                }
                connection.Close();
            }
            return data;

        }

        public List<Product> List(int page, int pagesize, string searchValue, int CategoryID, int SupplierID)
        {
            List<Product> data = new List<Product>();
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select *
                                        from
                                        (
	                                        select *,
	                                        ROW_NUMBER() over(order by ProductID) as RowNumber
	                                        from Products
	                                        where ((@searchValue = N'') or(ProductName like @searchValue)) and
			                                        ((@category = N'') or (CategoryID = @category)) and
			                                        ((@supplier = N'') or (SupplierID = @supplier))
                                        ) as t
                                        where t.RowNumber between (@page-1)*@pageSize + 1 and @page*@pageSize
                                        order by t.RowNumber";
                   
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pagesize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@supplier", SupplierID);
                    cmd.Parameters.AddWithValue("@category", CategoryID);
                    using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        // lay du lieu dua vao list<supplier>
                        while (dataReader.Read())
                        {
                            data.Add(new Product()
                            {
                                ProductID = Convert.ToInt32(dataReader["ProductID"]),
                                CategoryID = Convert.ToInt32(dataReader["CategoryID"]),
                                Descriptions = Convert.ToString(dataReader["Descriptions"]),
                                PhotoPath = Convert.ToString(dataReader["PhotoPath"]),
                                SupplierID = Convert.ToInt32(dataReader["SupplierID"]),
                                ProductName = Convert.ToString(dataReader["ProductName"]),
                                QuantityPerUnit = Convert.ToString(dataReader["QuantityPerUnit"]),
                                UnitPrice = Convert.ToDecimal(dataReader["UnitPrice"])

                            });
                        }

                    }
                }
                //dong ket noi
                connection.Close();
            }
            return data;
        }

        public bool Update(Product data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"UPDATE Products
                                                   SET ProductName = @ProductName,
                                                       SupplierID = @SupplierID, 
                                                       CategoryID = @CategoryID, 
                                                       QuantityPerUnit = @QuantityPerUnit,
                                                       UnitPrice = @UnitPrice, 
                                                       Descriptions = @Descriptions, 
                                                       PhotoPath = @PhotoPath 
                                                 WHERE ProductID = @ProductID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                    cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                    cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                    cmd.Parameters.AddWithValue("@QuantityPerUnit", data.QuantityPerUnit);
                    cmd.Parameters.AddWithValue("@UnitPrice", data.UnitPrice);
                    cmd.Parameters.AddWithValue("@Descriptions", data.Descriptions);
                    cmd.Parameters.AddWithValue("@PhotoPath", data.PhotoPath);
                    cmd.Parameters.AddWithValue("@ProductID", data.ProductID);


                    rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                }
                connection.Close();
            }
            return rowsAffected > 0;
        }
    }
}
