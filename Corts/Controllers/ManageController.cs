using Corts.DAL;
using Corts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Corts.Models.Classes;

namespace Corts.Controllers
{
    public class ManageController : Controller
    {
        private Dal dal = new Dal();
        public ActionResult Settings(string email)
        {

            ViewBag.Message = "Settings";
            ViewBag.user = email;

            ViewBag.Message = "Settings";

            //Make a function
            //Get list of current cars in the database
            List<Cars> currentCarList = new List<Cars>();
            currentCarList = getCurrentCarList();
            List<SelectListItem> currentCarsAvailable = new List<SelectListItem>();
            for (int i = 0; i < currentCarList.Count; i++)
            {
                currentCarsAvailable.Add(new SelectListItem
                {
                    Text = currentCarList[i].type,
                    Value = currentCarList[i].id.ToString()
                });
            }
            ViewBag.CurrentCarList = currentCarsAvailable;

            //Make a function
            //Get the users current list of vehicles and return it to the view
            List<UsersCars> list = new List<UsersCars>();
            list = getUsersCars(email);
            List<SelectListItem> usersCarsList = new List<SelectListItem>();
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    usersCarsList.Add(new SelectListItem
                    {
                        Text = list[i].Type,
                        Value = list[i].ID.ToString()
                    });
                }
                ViewBag.UsersCars = usersCarsList;
            }

            return View();
        }
        [HttpPost]
        public ActionResult Add()
        {
            return View("~/Views/Home/Index.cshtml");
        }
        private List<UsersCars> getCurrentUserCars(string email)
        {
            return dal.getCurrentUsersCars(email);
        }
        public List<UsersCars> getUsersCars(string email)
        {
            return dal.getCurrentUsersCars(email);
        }
        public List<Cars> getCurrentCarList()
        {
            return dal.getCurrentCarList();
        }
    }
}