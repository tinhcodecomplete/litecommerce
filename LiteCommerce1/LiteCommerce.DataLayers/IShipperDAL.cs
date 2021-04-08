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
    public interface IShipperDAL
    {
        /// <summary>
        /// bổ sung thêm 1 shipper
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của supplier được bổ sung (nhỏ hơn hoặc bằng 0 nếu lỗi)</returns>
        int Add(Shipper data);
        /// <summary>
        /// cap nhat du lieu shipper (data duoc lay tu form)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Shipper data);
        /// <summary>
        /// xoa danh sach cac shipper
        /// </summary>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>        
        bool Delete(int[] supplierIDs);
        /// <summary>
        /// get Shipper
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>        
        Shipper Get(int supplierID);
        /// <summary>
        /// get List shipper
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Shipper> List(int page, int pagesize, string searchValue);
        /// <summary>
        /// count number shipper tim thay
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>       
        int Count(string searchValue);
        /// <summary>
        /// dung trong select option
        /// </summary>
        /// <returns></returns>
        List<Shipper> List_Shipper_And_ShipperID();
    }
}
