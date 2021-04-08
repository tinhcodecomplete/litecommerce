
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

    public class CustomerController : Controller
    {
       
        // GET: Customer
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            var model = new Models.CustomerPaginationResult()
            {
                SearchValue = searchValue,
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                RowCount = CatalogBLL.Customer_Count(searchValue),
                Data = CatalogBLL.Customer_List(page, AppSettings.DefaultPageSize, searchValue)
            };
            return View(model);

        }
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.message = "Add new Customer";
                Customer newCustomer = new Customer();
                newCustomer.CustomerID = "";
                return View(newCustomer);
            }
            else
            {
                ViewBag.message = "Edit Customer";
                Customer editCustomer = CatalogBLL.Customer_Get(id);
                if (editCustomer == null)
                {
                    return RedirectToAction("Index");
                }
                return View(editCustomer);
            }

        }
        [HttpPost]
        public ActionResult Input(Customer model)
        {
            try
            {

                
                if (string.IsNullOrEmpty(model.Fax))
                {
                    model.Fax = "";
                }             


                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Customer customerGet = CatalogBLL.Customer_Get(model.CustomerID);

                if (customerGet == null)
                {
                    int customerID = CatalogBLL.Customer_Add(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool updateResult = CatalogBLL.Customer_Update(model);
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
        /// <param name="customerIDs"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string method = "", string[] customerIDs = null)
        {
            if (customerIDs != null)
            {
                CatalogBLL.Customer_Delete(customerIDs);
            }
            return RedirectToAction("Index");
        }
    }
}