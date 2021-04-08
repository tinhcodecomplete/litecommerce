using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface ICustomerDAL
    {
        /// <summary>
        /// bổ sung thêm 1 customer
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của customer được bổ sung (nhỏ hơn hoặc bằng 0 nếu lỗi)</returns>
        int Add(Customer data);
        /// <summary>
        /// update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Customer data);
        /// <summary>
        /// delete  customer
        /// </summary>
        /// <param name="customerIDs"></param>
        /// <returns></returns>
        bool Delete(string[] customerIDs);
        /// <summary>
        /// get customer
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        Customer Get(string customerID);
        List<Customer> List(int page, int pagesize, string searchValue);
        /// <summary>
        /// đếm số customer tìm thấy
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);
    }
}
