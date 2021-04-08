
using LiteCommerce.Admin;
using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{

   
    
    [Authorize(Roles = WebUserRoles.ADMINISTRATOR)]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index(int page = 1, string searchValue = "", string Country="")
        {
            var model = new Models.EmployeePaginatonResult()
            {
                Page = page,
                Country = Country,
                PageSize = AppSettings.DefaultEmployeePageSize,
                RowCount = EmployeeBLL.CountOfSearchValue(searchValue, Country),
                Data = EmployeeBLL.Employee_List(page, AppSettings.DefaultEmployeePageSize, searchValue, Country),
                SearchValue = searchValue
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.message = "Add new Employee";
                Employee newEmployee = new Employee();
                newEmployee.EmployeeID = 0;
                return View(newEmployee);
            }
            else
            {
                ViewBag.message = "Edit Employee";
                Employee editEmployee = EmployeeBLL.Employee_Get(Convert.ToInt32(id));
                if (editEmployee == null)
                    return RedirectToAction("Index");
                return View(editEmployee);
            }
        }
        

        [HttpPost]
        public ActionResult Input(Employee model)
        {

            if (model.EmployeeID == 0)
            {

                bool isHaveEmail = EmployeeBLL.Employee_CheckEmail(model.Email, model.EmployeeID);

                
                if(isHaveEmail == true)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại trong hệ thống vui lòng nhập Emai khác");
                    return View(model);
                }
                else
                {
                    int ID = EmployeeBLL.Employee_Add(model);
                    return RedirectToAction("Index");
                }
               
            }
            else
            {
                bool isHaveEmail = EmployeeBLL.Employee_CheckEmail(model.Email, model.EmployeeID);
                if (isHaveEmail == true)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại trong hệ thống vui lòng nhập Emai khác");
                    return View(model);
                }
                else
                {
                    Employee dataGet = EmployeeBLL.Employee_Get(model.EmployeeID);
                    string passTemp = Md5HashCode.GetMd5Hash(model.Password);
                    if (dataGet.Password == model.Password)
                    {
                        model.Password = model.Password;
                    }
                    else
                    {
                        model.Password = passTemp;
                    }
                    bool updateChecked = EmployeeBLL.Employee_Update(model);
                }
                
            }
            return RedirectToAction("Index");


        }
        [HttpPost]
        public ActionResult Delete(string method = "", int[] employeeIDs = null)
        {
            if (employeeIDs != null)
            {
                EmployeeBLL.Employee_Delete(employeeIDs);
            }
            return RedirectToAction("Index");
        }


        
        public string ProcessUpload(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("~/Images/" + file.FileName));
            return "/Images/" + file.FileName;
        }
    }
}