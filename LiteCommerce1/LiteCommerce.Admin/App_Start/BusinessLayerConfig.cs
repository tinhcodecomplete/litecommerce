using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.App_Start
{
    /// <summary>
    /// Khởi tạo các chức năng tác nghiệp cho ứng dụng
    /// </summary>
    public static class BusinessLayerConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Initialize()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LiteCommerce"].ConnectionString;

            CatalogBLL.Initialize(connectionString);
            //TODO: Bổ sung việc khởi tạo các BLL khác khi cần sử dụng
            EmployeeBLL.Initialize(connectionString);
            CountryBLL.Initialize(connectionString);
            UserAccountBLL.Initialize(connectionString);
            SaleManagementBLL.Initialize(connectionString);
        }
    }
}