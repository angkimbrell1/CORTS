using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Corts.Controllers
{
    public class MaintenanceController : Controller
    {
        public ActionResult Maintenance(string email)
        {
            ViewBag.Message = "Maintenance";
            if (email != null)
            {
                ViewBag.Email = email;
            }
            else
            {
                ViewBag.UsersEmail = null;
            }
            ViewBag.Email = email;
            return View();
        }
    }
}