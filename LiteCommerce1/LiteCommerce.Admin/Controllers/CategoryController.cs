
using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize(Roles = WebUserRoles.STAFF)]
    public class CategoryController : Controller
    {

        

        // GET: Category
        public ActionResult Index(int page = 1, string searchValue = "")
        {

            var model = new Models.Category_Result()
            {
                SearchValue= searchValue,
                Page = page,
                RowCount = CatalogBLL.Category_Count(searchValue),
                Data = CatalogBLL.Category_List(page, AppSettings.DefaultPageSize, searchValue)
            };
            return View(model);

            //var listOfSuppliers = CatalogBLL.Supplier_List(page, 10, searchValue);
            //int rowCount = CatalogBLL.Supplier_Count(searchValue);
            //ViewBag.row_Count = rowCount;
            //return View(listOfSuppliers);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.message = "Add new  Category";
                Category newCategory = new Category();
                newCategory.CategoryID = 0;
                return View(newCategory);
            }
            else
            {
                ViewBag.message = "Edit Category";
                Category editCategory = CatalogBLL.Category_Get(Convert.ToInt32(id));
                if (editCategory == null)
                {
                    return RedirectToAction("Index");
                }
                return View(editCategory);
            }

        }
        [HttpPost]
        public ActionResult Input(Category model)
        {
            try
            {
                if(model.CategoryID == 0)
                {
                    int categoryID = CatalogBLL.Category_Add(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool editCategory = CatalogBLL.Category_Update(model);
                    return RedirectToAction("Index");

                }
            }catch(Exception ex)
            {
                ModelState.AddModelError("addAndUpdate", ex.Message);
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Delete(string method ="", int[] categoryIDs = null)
        {
            if(categoryIDs != null)
            {
                bool deleteCategory = CatalogBLL.Category_Delete(categoryIDs);                   
            }
            return RedirectToAction("Index");
            
        }
    }
}