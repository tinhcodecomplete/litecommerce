using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IProductAttributeDAL
    {
        /// <summary>
        /// bổ sung thêm 1 ProductAttribute
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của ProductAttribute được bổ sung (nhỏ hơn hoặc bằng 0 nếu lỗi)</returns>
        int Add(ProductAttribute data);
        /// <summary>
        /// cap nhat ProductAttribute
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(ProductAttribute data);
        /// <summary>
        /// delete ProductAttribute by attributeIDs
        /// </summary>
        /// <param name="AttributeIDs"></param>
        /// <returns></returns>
        bool Delete(int[] AttributeIDs);
        /// <summary>
        /// delete ProductAttribute by productID
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        bool DeleteByProductID(int productID);
        /// <summary>
        /// get productAttribue
        /// </summary>
        /// <param name="AttributeID"></param>
        /// <returns></returns>
        ProductAttribute Get(int AttributeID);
        /// <summary>
        /// get listProductAttribute
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        List<ProductAttribute> List(int ProductID);


    }
}
