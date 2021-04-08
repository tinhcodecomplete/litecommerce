using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserAccountDAL
    {
        /// <summary>
        /// kien tra thong tin dang nhap cua user xem co hop le hay khong?
        /// - neu hop le thi tra ve thong tin cua user
        /// - neu khong hop le thi tra ve null
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserAccount Authorize(string userName, string password);
        /// <summary>
        /// thay doi password
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="newPass"></param>
        /// <returns></returns>
        bool ChangePwd(string userID, string newPass);
        /// <summary>
        /// get user by userID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        UserAccount User(string userID);
        /// <summary>
        /// check Email is Exist
        /// </summary>
        /// <param name="emailID"></param>
        /// <returns>user</returns>
        bool IsEmailExist(string emailID);
        /// <summary>
        /// get user co resetPasswordCode la resetCode
        /// </summary>
        /// <param name="resetCode">duoc gui vao mail cho user</param>
        /// <returns>user</returns>
        UserAccount GetUserByResetPasswordCode(string resetCode);
        /// <summary>
        /// add resetPasswordCode vao bang Employee cho user co Email la : email
        /// </summary>
        /// <param name="email">email of user</param>
        /// <param name="resetPasswordCode">resetPassword genarate Auto</param>
        /// <returns></returns>
        bool AddResetCode(string email, string resetPasswordCode);



    }
}
