using LiteCommerce.DataLayers.SqlServer;
using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    public static class EmployeeBLL
    {
        /// <summary>
        /// 
        /// </summary>
        private static EmployeeDAL EmployeeDB { get; set; }
        /// <summary>
        /// Hàm này phải được gọi để khởi tạo các chức năng của hệ thống
        /// </summary>
        public static void Initialize(string connectionString)
        {
            EmployeeDB = new DataLayers.SqlServer.EmployeeDAL(connectionString);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        /// //----------------Employee--------------------------
        public static List<Employee> Employee_List(int page, int pageSize, string searchValue, string Country)
        {
            if (page < 1) page = 1;
            if (pageSize <= 0) pageSize = 5;
            return EmployeeDB.List(page, pageSize, searchValue, Country);
        }
        public static int CountOfSearchValue(string searchValue,string Country)
        {
            return EmployeeDB.CountOfSearchValue(searchValue, Country);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerIDs"></param>
        /// <returns></returns>
        public static bool Employee_Delete(int[] employeeIDs)
        {
            return EmployeeDB.Delete(employeeIDs);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Employee_Add(Employee data)
        {
            return EmployeeDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Employee_Update(Employee data)
        {
            return EmployeeDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Employee Employee_Get(int employeeID)
        {
            return EmployeeDB.Get(employeeID);
        }

        public static Employee Employee_GetByEmail(string email)
        {
            return EmployeeDB.GetByEmail(email);
        }


        public static bool Employee_CheckEmail(string email, int EmployeeID)
        {
            return EmployeeDB.CheckEmail(email, EmployeeID);
        }

        public static List<Employee> Employee_List_FullName_And_EmployeeID()
        {
            return EmployeeDB.List_FullName_And_EmployeeID();
        }
    }
}
