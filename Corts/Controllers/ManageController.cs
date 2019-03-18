using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Corts.Controllers
{
    public class ManageController : Controller
    {
        public ActionResult Settings()
        {
            ViewBag.Message = "Settings";
            return View();
        }
    }
}