
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

    public class ShipperController : Controller
    {
        // GET: Shipper
       
        public ActionResult Index(int page = 1, string searchValue = "")
        {

            var model = new Models.Shipper_Result()
            {
                SearchValue = searchValue,
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                RowCount = CatalogBLL.Shipper_Count(searchValue),
                Data = CatalogBLL.Shipper_List(page, AppSettings.DefaultPageSize, searchValue)
            };
            return View(model);

            
        }
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.message = "Add new Shipper";
                Shipper newSuppier = new Shipper();
                newSuppier.ShipperID = 0;
                return View(newSuppier);
            }
            else
            {
                ViewBag.message = "Edit Shipper";
                Shipper editShipper = CatalogBLL.Shipper_Get(Convert.ToInt32(id));
                Console.WriteLine(editShipper.ShipperID);
                if (editShipper == null)
                {
                    return RedirectToAction("Index");
                }
                return View(editShipper);
            }

        }
        [HttpPost]
        public ActionResult Input(Shipper model)
        {
            try
            {              

                if (model.ShipperID == 0)
                {
                    int shipperID = CatalogBLL.Shipper_Add(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool updateResult = CatalogBLL.Shipper_Update(model);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string method = "", int[] shipperIDs = null)
        {
            if (shipperIDs != null)
            {
                CatalogBLL.Shipper_Delete(shipperIDs);
            }
            return RedirectToAction("Index");
        }
    }
}