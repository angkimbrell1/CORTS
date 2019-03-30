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
            ViewBag.Email = email;

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
                        Value = list[i].CarID
                    });
                }
                ViewBag.UsersCars = usersCarsList;
            }

            return View();
        }
        [HttpPost]
        public ActionResult Add(SettingsViewModel addForm)
        {
            string usersEmail = addForm.Email;

            //Get the type of car selected by the CarID
            string CarSelected = GetSelectedCarType(addForm.CarType);

            UsersCars newCar = new UsersCars();
            newCar.CarID = Guid.NewGuid().ToString();
            newCar.Type = CarSelected;
            newCar.mileage = Int32.Parse(addForm.Mileage);
            newCar.monthsOwned = Int32.Parse(addForm.MonthsOwned);
            newCar.totalSpent = Int32.Parse(addForm.TotalSpent);
            newCar.InspectionDue = addForm.InspectionDate;

            if (dal.AddCar(usersEmail, newCar))
            {
                Session["email"] = usersEmail;
                var email = (string)Session["email"];
                return RedirectToAction("Settings", "Manage", new { email });
            }
            else
            {
                throw new Exception("Error");
            }
        }
        [HttpPost]
        public ActionResult Remove(SettingsViewModel remove)
        {
            string usersEmail = remove.Email;
            Session["email"] = usersEmail;
            var email = (string)Session["email"];
            string carToBeRemoved = remove.CarType;
            dal.RemoveCar(carToBeRemoved, usersEmail);
            return RedirectToAction("Settings", "Manage", new { email });
        }

        private string GetSelectedCarType(string CarSelected)
        {
            return dal.GetSelectedCar(CarSelected);
        }

        private List<UsersCars> getCurrentUserCars(string email)
        {
            return dal.getCurrentUsersCars(email);
        }
        private List<UsersCars> getUsersCars(string email)
        {
            return dal.getCurrentUsersCars(email);
        }
        public List<Cars> getCurrentCarList()
        {
            return dal.getCurrentCarList();
        }
    }
}