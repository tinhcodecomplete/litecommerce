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
    public interface ISupplierDAL
    {
        /// <summary>
        /// bổ sung thêm 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của supplier được bổ sung (nhỏ hơn hoặc bằng 0 nếu lỗi)</returns>
        int Add(Supplier data);
        /// <summary>
        /// Update du lieu cho cupplier
        /// </summary>
        /// <param name="data">duoc lay tu form </param>
        /// <returns></returns>
        bool Update(Supplier data);
        /// <summary>
        /// xoa danh sach cac supplier 
        /// </summary>
        /// <param name="supplierIDs">mang cac supplierID Chon tu checkbox</param>
        /// <returns></returns>
        bool Delete(int[] supplierIDs);
        /// <summary>
        /// get supplier by supplierID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        Supplier Get(int supplierID);
        /// <summary>
        /// lay ra danh sach cac supplier da duoc phan trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Supplier> List(int page, int pagesize, string searchValue );
        /// <summary>
        /// dem so supplier tim thay khi search or run first Time
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// dung de dua vao select Option
        /// </summary>
        /// <returns></returns>
        List<Supplier> List_CompanyName_And_SupplierID();
    }
}
