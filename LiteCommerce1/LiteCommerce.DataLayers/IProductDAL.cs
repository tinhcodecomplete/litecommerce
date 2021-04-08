using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IProductDAL
    {
        /// <summary>
        /// bổ sung thêm 1 product
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của product được bổ sung (nhỏ hơn hoặc bằng 0 nếu lỗi)</returns>
        int Add(Product data);
        /// <summary>
        /// update product
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Product data);
        /// <summary>
        /// delete one or list product by productID
        /// </summary>
        /// <param name="productIDs"></param>
        /// <returns></returns>
        bool Delete(int[] productIDs);
        /// <summary>
        /// get Product
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        Product Get(int productID);
        /// <summary>
        /// get list product
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <param name="CategoryID"></param>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        List<Product> List(int page, int pagesize, string searchValue, int CategoryID, int SupplierID);
        /// <summary>
        /// count product timthay
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="CategoryID"></param>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        int Count(string searchValue, int CategoryID, int SupplierID);

    }
}
