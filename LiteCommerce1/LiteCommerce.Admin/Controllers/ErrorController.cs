using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [HandleError]
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BadRequest()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Forbidden()
        {
            return View();
        }

        public ActionResult InternalServerError()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult NotImplemented()
        {
            return View();
        }

        public ActionResult ServerBusyOrDown()
        {
            return View();
        }

        public ActionResult ServerUnavailable()
        {
            return View();
        }

        public ActionResult Timeout()
        {
            return View();
        }

        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}