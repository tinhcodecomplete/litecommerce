using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModel
{
    /// <summary>
    /// 
    /// </summary>
     public class UserAccount
    {
        /// <summary>
        /// is cua user (ten dang nhap)
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// ten day du cua user
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// duong dan anh cua user
        /// </summary>
        public string Photo { get; set; }
        public string GroupName { get; set; }
        public string Password { get; set; }
        public string NewPassword  { get; set; }
        
        public string ReNewPassword { get; set; }
        public string ResetPasswordCode { get; set; }

    }
}
