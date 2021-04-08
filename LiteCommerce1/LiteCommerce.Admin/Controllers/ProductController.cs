
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


    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int page = 1, string searchValue = "", int Category = 0, int Supplier =0 )
        {
            var model = new Models.ProductPaginationResult()
            {

                SearchValue = searchValue,
                Page = page,
                Category = Category,
                Supplier = Supplier,
                PageSize = AppSettings.DefaultPageSize,
                RowCount = CatalogBLL.Product_Count(searchValue, Category, Supplier),
                Data = CatalogBLL.Product_List(page, AppSettings.DefaultPageSize, searchValue, Category, Supplier)
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.message = "Add new Product";
                Product newProduct = new Product();
                newProduct.ProductID = 0;
                return View(newProduct);
            }
            else
            {
                ViewBag.message = "Edit Product";
                Product editProduct = CatalogBLL.Product_Get(Convert.ToInt32(id));
              
                if (editProduct == null)
                {
                    return RedirectToAction("Index");
                }
                ViewData["ProducAttribute"] = CatalogBLL.ProductAttribute_List(editProduct.ProductID);
                return View(editProduct);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Input(Product model)
        {
            try
            {              

                if (model.ProductID == 0)
                {
                    int productID = CatalogBLL.Product_Add(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool updateResult = CatalogBLL.Product_Update(model);
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
        public ActionResult Delete(string method = "", int[] productIDs = null)
        {
            if (productIDs != null)
            {
                foreach(int productID in productIDs)
                {
                    CatalogBLL.ProductAttribute_DeleteByProductID(productID);
                }
               
                CatalogBLL.Product_Delete(productIDs);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string ProcessUpload(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("~/Images/Product/" + file.FileName));
            return "/Images/Product/" + file.FileName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(string id = "")
        {
            if (!String.IsNullOrEmpty(id))
            {
                Product model = CatalogBLL.Product_Get(Convert.ToInt32(id));
                if (model == null)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ViewData["Attribute"] = CatalogBLL.ProductAttribute_List(model.ProductID);
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
        }
      
        public ActionResult AddProductAttribute(string id = "")
        {
            return View();
        }
        [HttpPost]
        public ActionResult InputProductAttribute(ProductAttribute model)
        {
            if (model.AttributeID == 0)
            {
                var addAttr = CatalogBLL.ProductAttribute_Add(model);
                return RedirectToAction("Input", "Product", new { id = model.ProductID });
            }
            else
            {
                var editAttr = CatalogBLL.ProductAttribute_Update(model);
                return RedirectToAction("Input", "Product", new { id = model.ProductID });
            }
        }
        [HttpPost]
        public ActionResult DeleteProductAttribute(int[] attributesIDs, string productID)
        {
            if (attributesIDs != null)
            {
                var deleteAttr = CatalogBLL.ProductAttribute_Delete(attributesIDs);
            }
            return RedirectToAction("Input", "Product", new { @id = Convert.ToInt32(productID) });
        }
    }
}