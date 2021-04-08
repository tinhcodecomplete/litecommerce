
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
    [Authorize(Roles = WebUserRoles.SALEMAN)]

    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index(int page = 1, string ShipCountry = "", string CustomerID = "",int ShipperID=0, int EmployeeID=0 )
        {
            var model = new Models.Order_Result()
            {
                ShipCountry = ShipCountry,
                customerID = CustomerID,
                employeeID = EmployeeID,
                shipperID = ShipperID,
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                RowCount = SaleManagementBLL.Order_Count(ShipCountry,CustomerID,EmployeeID, ShipperID),
                Data = SaleManagementBLL.Order_List(page, AppSettings.DefaultPageSize, ShipCountry,CustomerID,EmployeeID,ShipperID),
               
            };
            return View(model);
        }
        public ActionResult Detail(string id = "")
        {
            if (!String.IsNullOrEmpty(id))
            {
                Order model = SaleManagementBLL.Order_Get(Convert.ToInt32(id));
                if (model == null)
                {
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    ViewData["Attribute"] = SaleManagementBLL.OrderDetails_GetList(model.OrderID);
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Index", "Order");
            }
        }
    }
}