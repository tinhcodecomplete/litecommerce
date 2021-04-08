using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin
{
      /// <summary>
      /// Định nghĩa danh sách các Role của user
      /// </summary>
      public class WebUserRoles
      {
            /// <summary>
            /// Không xác định
            /// </summary>
            public const string ANONYMOUS = "anonymous";
            /// <summary>
            /// Nhân viên
            /// </summary>
            public const string STAFF = "dataManagementStaff";
            /// <summary>
            /// Quản trị hệ thống
            /// </summary>
            public const string ADMINISTRATOR = "Administrator";
            /// <summary>
            /// nhân viên bán hàng
            /// </summary>
            public const string SALEMAN = "SaleMan";
            /// <summary>
            /// nhân viên quản lý dữ liệu
            /// </summary>

            public const string DATA_MANAGER_STAFF = "dataManagementStaff";
        /// <summary>
        /// 
        /// </summary>
            public const string DATA_AD = "dataManagementStaff,Administrator";
        /// <summary>
        /// 
        /// </summary>
            public const string FULL = "SaleMan,dataManagementStaff,Administrator";
    }
}