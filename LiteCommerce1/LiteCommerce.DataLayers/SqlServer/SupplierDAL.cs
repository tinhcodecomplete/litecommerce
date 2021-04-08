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
    /// <summary>
    /// 
    /// </summary>
    public class SupplierDAL : ISupplierDAL
    {
        private string connectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public SupplierDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(Supplier data)
        {
            int supplierID = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO Suppliers
                                          (
	                                          CompanyName,
	                                          ContactName,
	                                          ContactTitle,
	                                          Address,
	                                          City,
	                                          Country,
	                                          Phone,
	                                          Fax,
	                                          HomePage
                                          )
                                          VALUES
                                          (
	                                          @CompanyName,
	                                          @ContactName,
	                                          @ContactTitle,
	                                          @Address,
	                                          @City,
	                                          @Country,
	                                          @Phone,
	                                          @Fax,
	                                          @HomePage
                                          );
                                          SELECT @@IDENTITY;";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@CompanyName", data.CompanyName);
                    cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                    cmd.Parameters.AddWithValue("@ContactTitle", data.ContactTitle);
                    cmd.Parameters.AddWithValue("@Address", data.Address);
                    cmd.Parameters.AddWithValue("@City", data.City);
                    cmd.Parameters.AddWithValue("@Country", data.Country);
                    cmd.Parameters.AddWithValue("@Phone", data.Phone);
                    cmd.Parameters.AddWithValue("@Fax", data.Fax);
                    cmd.Parameters.AddWithValue("@HomePage", data.HomePage);
                    supplierID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return supplierID;
        }

        public bool Delete(int[] supplierIDs)
        {
            bool result = true;
            using(SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"DELETE FROM Suppliers
                                            WHERE(SupplierID = @supplierId)
                                              AND(SupplierID NOT IN(SELECT SupplierID FROM Products))";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@supplierID", SqlDbType.Int);
                    foreach( int supplierID in supplierIDs)
                    {
                        cmd.Parameters["@supplierID"].Value = supplierID;
                        cmd.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
            

            return result ;
        }

        public Supplier Get(int supplierID)
        {
            Supplier data = null;
            using(SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Suppliers WHERE SupplierID = @supplierID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@supplierID", supplierID);
                    using(SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            data = new Supplier()
                            {
                                SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                                CompanyName = Convert.ToString(dbReader["CompanyName"]),
                                ContactName = Convert.ToString(dbReader["ContactName"]),
                                ContactTitle = Convert.ToString(dbReader["ContactTitle"]),
                                Address = Convert.ToString(dbReader["Address"]),
                                City = Convert.ToString(dbReader["City"]),
                                Country = Convert.ToString(dbReader["Country"]),
                                Phone = Convert.ToString(dbReader["Phone"]),
                                Fax = Convert.ToString(dbReader["Fax"]),
                                HomePage = Convert.ToString(dbReader["HomePage"])
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
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Supplier> List(int page, int pagesize, string searchValue)
        {
            List<Supplier> data = new List<Supplier>();
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
                                        select * , ROW_NUMBER() over(order by SupplierID) as RowNumber
                                        from Suppliers
                                        where (@searchValue = N'') 
                                                or (CompanyName like @searchValue)
                                        ) as t
                                        where t.RowNumber between (@page -1) * @pageSize +1  and @page* @pageSize
                                        order by t.RowNumber";
                    //cho biet lenh su dung de thuc thi ở dạng nào
                    cmd.CommandType = System.Data.CommandType.Text;
                    //
                    cmd.Connection = connection;
                    //dua du lieu vao cau lenh sql
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pagesize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);

                    //Thuc thi cau lenh (cmd.ExecuteReader)
                    //SqlDataReader dbReader tao doi tuong luu tru du lieu 
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        // lay du lieu dua vao list<supplier>
                        while (dbReader.Read())
                        {
                            data.Add(new Supplier()
                            {
                                SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                                CompanyName = Convert.ToString(dbReader["CompanyName"]),
                                ContactName = Convert.ToString(dbReader["ContactName"]),
                                ContactTitle = Convert.ToString(dbReader["ContactTitle"]),
                                Address = Convert.ToString(dbReader["Address"]),
                                City = Convert.ToString(dbReader["City"]),
                                Country = Convert.ToString(dbReader["Country"]),
                                Phone = Convert.ToString(dbReader["Phone"]),
                                Fax = Convert.ToString(dbReader["Fax"]),
                                HomePage = Convert.ToString(dbReader["HomePage"])

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
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Supplier data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"UPDATE Suppliers
                                                SET 
                                                CompanyName = @CompanyName,
                                                ContactName = @ContactName,
                                                ContactTitle =@ContactTitle,
                                                Address= @Address,
                                                City= @City,
                                                Country= @Country,
                                                Phone = @Phone,
                                                Fax = @Fax,
                                                HomePage = @HomePage
                                                WHERE SupplierID = @SupplierID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@CompanyName", data.CompanyName);
                    cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                    cmd.Parameters.AddWithValue("@ContactTitle", data.ContactTitle);
                    cmd.Parameters.AddWithValue("@Address", data.Address);
                    cmd.Parameters.AddWithValue("@City", data.City);
                    cmd.Parameters.AddWithValue("@Country", data.Country);
                    cmd.Parameters.AddWithValue("@Phone", data.Phone);
                    cmd.Parameters.AddWithValue("@Fax", data.Fax);
                    cmd.Parameters.AddWithValue("@HomePage", data.HomePage);
                    cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);


                    rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                }
                connection.Close();
            }
            return rowsAffected > 0;
        }
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
                                        from Suppliers 
                                                where (@searchValue = N'') 
                                                or (CompanyName like @searchValue)";
                    //cho biet lenh su dung de thuc thi ở dạng nào
                    cmd.CommandType = System.Data.CommandType.Text;
                    //
                    cmd.Connection = connection;
                    //dua du lieu vao cau lenh sql
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);

                    //Thuc thi cau lenh (cmd.ExecuteReader)
                   
                    rowCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
                //dong ket noi
                connection.Close();
            }
            return rowCount;
        }
        /// <summary>
        /// dung de dua vao select option (selectListHelper)
        /// </summary>
        /// <returns></returns>
        public List<Supplier> List_CompanyName_And_SupplierID()
        {
            List<Supplier> data = new List<Supplier>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"select * from Suppliers";

                    cmd.CommandType = System.Data.CommandType.Text;
                    //
                    cmd.Connection = connection;

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (dbReader.Read())
                        {
                            data.Add(new Supplier()
                            {
                                SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                                CompanyName = Convert.ToString(dbReader["CompanyName"]),
                                ContactName = Convert.ToString(dbReader["ContactName"])
                            });
                        }

                    }
                }
                //dong ket noi
                connection.Close();
            }
            return data;
        }
    }
}
