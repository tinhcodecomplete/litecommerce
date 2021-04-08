using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Bổ sung thêm 1 Employee
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của Employee (<=0 nếu lỗi)</returns>
        int Add(Employee data);
        /// <summary>
        /// update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Employee data);
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="EmployeeIDs"></param>
        /// <returns></returns>
        bool Delete(int[] EmployeeIDs);
        /// <summary>
        /// get
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        Employee Get(int EmployeeID);
        /// <summary>
        /// get List
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Employee> List(int page, int pageSize, string searchValue, string Country);
        /// <summary>
        /// count
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="Country"></param>
        /// <returns></returns>
        int CountOfSearchValue(string searchValue, string Country);
        /// <summary>
        /// check duplicate
        /// </summary>
        /// <param name="email"></param>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>       
        bool CheckEmail(string email, int EmployeeID);
        /// <summary>
        /// get by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Employee GetByEmail(string email);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Employee> List_FullName_And_EmployeeID();

    }
}
