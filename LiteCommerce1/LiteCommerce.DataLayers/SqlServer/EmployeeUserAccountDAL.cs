using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SqlServer
{
    /// <summary>
    /// kiem tra thong tin dang nhap cua nhan vien
    /// </summary>
    public class EmployeeUserAccountDAL : IUserAccountDAL
    {
        private string connectionString;

        public EmployeeUserAccountDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// Authorize nhan ven 
        /// </summary>
        /// <param name="userName">dia chi email cua nhan vien</param>
        /// <param name="password">mat khau (da ma hoa md5)</param>
        /// <returns></returns>
        public UserAccount Authorize(string userName, string password)
        {
            UserAccount data = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"Select Email,FirstName,LastName,PhotoPath,GroupNames from Employees 
                                            where Email =@email and Password=@pass";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@email", userName);
                    cmd.Parameters.AddWithValue("@pass", GetMd5Hash(password));
                    using(SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            string fName = Convert.ToString(dataReader["FirstName"]);
                            string lName = Convert.ToString(dataReader["LastName"]);
                            data = new UserAccount()
                            {
                                UserID = Convert.ToString(dataReader["Email"]),
                                FullName = fName + lName,
                                Photo = Convert.ToString(dataReader["PhotoPath"]),
                                GroupName = Convert.ToString(dataReader["GroupNames"])

                                
                            };
                        }
                    }
                }
                connection.Close();
            }
            return data;
        

        }
        

        public bool ChangePwd(string userID, string newPass)
        {
            int isSuccess = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"Update Employees 
                                                set 
                                                Password = @NewPassword
                                                Where Email = @Email";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@NewPassword",GetMd5Hash(newPass));
                    cmd.Parameters.AddWithValue("@Email", userID);
                    isSuccess = Convert.ToInt32(cmd.ExecuteNonQuery());
                }
                connection.Close();
            }
            return isSuccess > 0;
        }

        public bool IsEmailExist(string emailID)
        {
            int isSuccess = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"Select Email from Employees 
                                                Where Email = @Email";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                   
                    cmd.Parameters.AddWithValue("@Email", emailID);
                    isSuccess = Convert.ToInt32(cmd.ExecuteNonQuery());
                }
                connection.Close();
            }
            return isSuccess > 0;
        }

        public UserAccount GetUserByResetPasswordCode(string resetCode)
        {
            UserAccount data = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"Select Email,ResetPasswordCode from Employees 
                                           Where ResetPasswordCode = @id";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@id", resetCode);
                    
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                           
                            data = new UserAccount()
                            {
                                UserID = Convert.ToString(dataReader["Email"]),
                                ResetPasswordCode = Convert.ToString(dataReader["ResetPasswordCode"]),




                            };
                        }
                    }
                }
                connection.Close();
            }
            return data;
            
        }

        public UserAccount User(string email)
        {
            UserAccount user = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select Password, Email,ResetPasswordCode from Employees Where Email = @CompanyName";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@CompanyName", email);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    if (dbReader.Read())
                    {
                        user = new UserAccount()
                        {
                            Password = Convert.ToString(dbReader["Password"]),
                            UserID = Convert.ToString(dbReader["Email"]),
                            ResetPasswordCode = Convert.ToString(dbReader["ResetPasswordCode"])

                        };
                    }
                }
                connection.Close();
            }
            return user;
        }
        

        private string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));                
                StringBuilder sBuilder = new StringBuilder();                
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }               
                return sBuilder.ToString();

            }
        }

        public bool AddResetCode(string email, string ResetPasswordCode)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"UPDATE Employees
                                                SET 
                                                ResetPasswordCode = @ResetPasswordCode
                                                WHERE Email = @Email";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@ResetPasswordCode", ResetPasswordCode);
                    cmd.Parameters.AddWithValue("@Email", email);
                    


                    rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                }
                connection.Close();
            }
            return rowsAffected > 0;
        }
    }
}
