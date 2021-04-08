using LiteCommerce.DataLayers;
using LiteCommerce.DataLayers.SqlServer;
using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    public static class UserAccountBLL
    {
        private static string connectionString;
        public static void Initialize(string connectionString)
        {
            UserAccountBLL.connectionString = connectionString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public static UserAccount Authorize(string userName, string password, UserAccountTypes userType)
        {
            IUserAccountDAL userAccountDB =null;
            switch (userType) 
            {
                case UserAccountTypes.Employee:
                    userAccountDB = new EmployeeUserAccountDAL(connectionString);
                    break;
                //case UserAccountTypes.Customer:
                //    userAccountDB = new CustomerUserAccountDAL(connectionString);
                //    break;
                default:
                    throw new Exception("UserType is not valid");
            }
            return userAccountDB.Authorize(userName, password);
        }
        public static bool UserAccount_ChangePassword(string userID, string newPass)
        {
            IUserAccountDAL userAccountDB = new EmployeeUserAccountDAL(connectionString);
            return userAccountDB.ChangePwd(userID, newPass);
        }
        public static UserAccount UserAccount_GetUser(string email)
        {
            IUserAccountDAL userAccountDB = new EmployeeUserAccountDAL(connectionString);
            return userAccountDB.User(email);
        }
        
        public static  bool UserAccount_IsEmailExist(string emailID)
        {
            IUserAccountDAL userAccountDB = new EmployeeUserAccountDAL(connectionString);
            return userAccountDB.IsEmailExist(emailID);
        }
        public static UserAccount UserAccount_GetUserByResetPasswordCode(string id)
        {
            IUserAccountDAL userAccountDB = new EmployeeUserAccountDAL(connectionString);
            return userAccountDB.GetUserByResetPasswordCode(id);
        }
        public static bool UserAccount_AddResetCode(string email, string ResetPasswordCode)
        {
            IUserAccountDAL userAccountDB = new EmployeeUserAccountDAL(connectionString);
            return userAccountDB.AddResetCode(email,ResetPasswordCode);
        }
    }
}
