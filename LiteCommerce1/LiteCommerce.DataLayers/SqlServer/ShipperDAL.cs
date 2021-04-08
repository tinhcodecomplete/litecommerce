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
    public class ShipperDAL : IShipperDAL
    {
        private string connectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public ShipperDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(Shipper data)
        {
            int shipperID = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO Shippers 
                                    (
                                    CompanyName,
                                    Phone
                                    )
                                    VALUES
                                    (
                                    @CompanyName, 
                                    @Phone
                                    )";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@CompanyName", data.CompanyName);
                    cmd.Parameters.AddWithValue("@Phone", data.Phone);
                    shipperID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return shipperID;
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
                                        from Shippers 
                                                where (@searchValue = N'') 
                                                or (CompanyName like @searchValue)";
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

        public bool Delete(int[] shipperIDs)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"DELETE FROM Shippers
                                            WHERE(ShipperID = @shipperID)

                                             ";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.Add("@shipperID", SqlDbType.Int);
                    foreach (int shipperID in shipperIDs)
                    {
                        cmd.Parameters["@shipperID"].Value = shipperID;
                        cmd.ExecuteNonQuery();
                    }

                }
                connection.Close();
            }
            return result;
        }

        public Shipper Get(int shipperID)
        {
            Shipper data = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"SELECT *  FROM Shippers WHERE ShipperID = @shipperID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@shipperID", shipperID);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            data = new Shipper
                            {
                                ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                                CompanyName = Convert.ToString(dbReader["CompanyName"]),
                                Phone = Convert.ToString(dbReader["Phone"])
                            };

                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        public List<Shipper> List(int page, int pagesize, string searchValue)
        {
            List<Shipper> data = new List<Shipper>();
            // tranh truong hop search loi
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
                                        select * , ROW_NUMBER() over(order by ShipperID) as RowNumber
                                        from Shippers
                                        where (@searchValue = N'') 
                                                or (CompanyName like @searchValue)
                                        ) as t
                                       
                                        order by t.RowNumber";
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
                        while (dbReader.Read())
                        {
                            data.Add(new Shipper()
                            {
                                ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                                CompanyName = Convert.ToString(dbReader["CompanyName"]),
                                Phone = Convert.ToString(dbReader["Phone"]),


                            });
                        }

                    }
                }
                connection.Close();
            }
            return data;
        }

        public List<Shipper> List_Shipper_And_ShipperID()
        {
            List<Shipper> data = new List<Shipper>();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select CompanyName, ShipperID from Shippers ";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            data.Add(new Shipper()
                            {
                                CompanyName = Convert.ToString(dataReader["CompanyName"]),
                                ShipperID = Convert.ToInt32(dataReader["ShipperID"])
                            });
                        }
                    }
                }
                connection.Close();

            }
            return data;
        }

        public bool Update(Shipper data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"UPDATE Shippers SET 
                                                CompanyName = @CompanyName,
                                                Phone = @Phone                                               
                                                WHERE ShipperID = @shipperID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@CompanyName", data.CompanyName);
                    cmd.Parameters.AddWithValue("@Phone", data.Phone);
                    cmd.Parameters.AddWithValue("@shipperID", data.ShipperID);


                    rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                }
                connection.Close();
            }
            return rowsAffected > 0;
        }
    }
}
