
using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Roles = WebUserRoles.STAFF)]

    public class SupplierController : Controller
    {
        /// <summary>
        /// Trang hiển thị: danh sách các supplier, các liên kết đến các chức năng liên quan
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page= 1, string searchValue ="")
        {
            var model = new Models.SupplierPaginationResult()
            {
                
                SearchValue = searchValue,
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                RowCount = CatalogBLL.Supplier_Count(searchValue),
                Data = CatalogBLL.Supplier_List(page, AppSettings.DefaultPageSize, searchValue)
            };
            return View(model);

            
        }
        /// <summary>
        /// Add or Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Input(string id="")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.message = "Add new supplier";
                Supplier newSuppier = new Supplier();
                newSuppier.SupplierID = 0;
                return View(newSuppier);
            }
            else
            {
                ViewBag.message = "Edit supplier";
                Supplier editSupplier = CatalogBLL.Supplier_Get(Convert.ToInt32(id));
                if(editSupplier == null)
                {
                    return RedirectToAction("Index");
                }
                return View(editSupplier);
            }
           
        }
        /// <summary>
        /// add or update suplier
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Input(Supplier model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Fax)) model.Fax = "";
                if (string.IsNullOrEmpty(model.HomePage)) model.HomePage = "";
                if (!ModelState.IsValid)
                {
                    return View(model);
                }


                if (model.SupplierID == 0)
                {
                    int supplierID = CatalogBLL.Supplier_Add(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool updateResult = CatalogBLL.Supplier_Update(model);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model); 
            }

            
        }
        /// <summary>
        /// delete supplier
        /// </summary>
        /// <param name="method"></param>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string method ="", int[] supplierIDs = null)
        {
            if(supplierIDs != null)
            {
                CatalogBLL.Supplier_Delete(supplierIDs);
            }
            return RedirectToAction("Index");
        }
    }
}