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
    public class CategoryDAL: ICategoryDAL
    {
        /// <summary>
        /// 
        /// </summary>
        private string connectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CategoryDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Category data)
        {
            int shipperID = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO Categories 
                                    (
                                    CategoryName,
                                    Description
                                    )
                                    VALUES
                                    (
                                    @CategoryName, 
                                    @Description
                                    )";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                    cmd.Parameters.AddWithValue("@Description", data.Description);
                    shipperID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return shipperID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            int rowCount = 0;
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }
            // tao doi tuong ket noi csdl
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // mo ket noi
                connection.Open();
                // cau lenh thuc thi yeu cau truy van du lieu
                using (SqlCommand cmd = new SqlCommand())
                {
                    // chuoi chua cau lenh can thuc thi
                    cmd.CommandText = @"select count(*) 
                                        from Categories 
                                                where (@searchValue = N'') 
                                                or (CategoryName like @searchValue)";
                    //cho biet lenh su dung de thuc thi ở dạng nào
                    cmd.CommandType = System.Data.CommandType.Text;
                    //
                    cmd.Connection = connection;
                    //dua du lieu vao cau lenh sql
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);

                    //Thuc thi cau lenh (cmd.ExecuteReader)
                    //SqlDataReader dbReader tao doi tuong luu tru du lieu 
                    rowCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
                //dong ket noi
                connection.Close();
            }
            return rowCount;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryIDs"></param>
        /// <returns></returns>
        public bool Delete(int[] categoryIDs)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"DELETE FROM Categories
                                            WHERE(CategoryID = @categoryID)
                                             AND(CategoryID NOT IN(SELECT CategoryID FROM Products)) ";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@categoryID", SqlDbType.Int);
                    foreach (int categoryID in categoryIDs)
                    {
                        cmd.Parameters["@CategoryID"].Value = categoryID;
                        cmd.ExecuteNonQuery();
                    }

                }
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public Category Get(int categoryID)
        {
            Category data = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM Categories WHERE CategoryID = @categoryID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@categoryID", categoryID);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            data = new Category
                            {
                                CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                                CategoryName = Convert.ToString(dbReader["CategoryName"]),
                                Description = Convert.ToString(dbReader["Description"])
                            };

                        }
                    }
                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Country> GetAllCountry()
        {
            List<Country> data = new List<Country>();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select * from Countries";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            data.Add(new Country()
                            {
                                CountryID = Convert.ToInt32(dataReader["id"]),
                                CountryCode = Convert.ToString(dataReader["country_code"]),
                                CountryName = Convert.ToString(dataReader["country_name"])
                            });
                        }
                    }
                }
                connection.Close();

            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Category> List(int page, int pagesize, string searchValue)
        {
            List<Category> data = new List<Category>();
           
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }
            // tao doi tuong ket noi csdl
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // mo ket noi
                connection.Open();
                // cau lenh thuc thi yeu cau truy van du lieu
                using (SqlCommand cmd = new SqlCommand())
                {
                    // chuoi chua cau lenh can thuc thi
                    cmd.CommandText = @"select * 
                                        from
                                        (
                                        select * , ROW_NUMBER() over(order by CategoryID) as RowNumber
                                        from Categories
                                        where (@searchValue = N'') 
                                                or (CategoryName like @searchValue)
                                        ) as t
                                        
                                        order by t.RowNumber";
                    //cho biet lenh su dung de thuc thi ở dạng nào
                    cmd.CommandType = System.Data.CommandType.Text;
                    //
                    cmd.Connection = connection;
                    //dua du lieu vao cau lenh sql
                   
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);

                    //Thuc thi cau lenh (cmd.ExecuteReader)
                    //SqlDataReader dbReader tao doi tuong luu tru du lieu 
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        // lay du lieu dua vao list<supplier>
                        while (dbReader.Read())
                        {
                            data.Add(new Category()
                            {
                                CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                                CategoryName = Convert.ToString(dbReader["CategoryName"]),
                                Description = Convert.ToString(dbReader["Description"])


                            });
                        }

                    }
                }
                //dong ket noi
                connection.Close();
            }
            return data;
           
        }
        /// <summary>
        /// dung de dua vao select option
        /// </summary>
        /// <returns></returns>
        public List<Category> List_CategoryName_And_CategoryID()
        {
            List<Category> data = new List<Category>();            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {               
                connection.Open();               
                using (SqlCommand cmd = new SqlCommand())
                {                   
                    cmd.CommandText = @"select * from Categories";                    
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = connection;                    
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {                       
                        while (dbReader.Read())
                        {
                            data.Add(new Category()
                            {
                                CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                                CategoryName = Convert.ToString(dbReader["CategoryName"])
                            });
                        }
                    }
                }
                //dong ket noi
                connection.Close();
            }
            return data;

        }

        public bool Update(Category data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"UPDATE Categories SET 
                                                CategoryName = @CategoryName,
                                                Description = @Description                                               
                                                WHERE CategoryID = @categoryID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                    cmd.Parameters.AddWithValue("@Description", data.Description);
                    cmd.Parameters.AddWithValue("@categoryID", data.CategoryID);


                    rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                }
                connection.Close();
            }
            return rowsAffected > 0;
        }
    }
}
