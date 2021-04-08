using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface ICategoryDAL
    {
        /// <summary>
        /// bổ sung thêm 1 category
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của category được bổ sung (nhỏ hơn hoặc bằng 0 nếu lỗi)</returns>
        int Add(Category data);
        /// <summary>
        /// update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Category data);
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="categoryIDs"></param>
        /// <returns></returns>
        bool Delete(int[] categoryIDs);
        /// <summary>
        /// get category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        Category Get(int categoryID);
        /// <summary>
        /// get list category
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Category> List(int page, int pagesize, string searchValue);
        /// <summary>
        /// count category
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// dung de dua vao select option
        /// </summary>
        /// <returns></returns>
        List<Category> List_CategoryName_And_CategoryID();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Country> GetAllCountry();
    }
}
