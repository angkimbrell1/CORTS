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
            //Get username from the database to display to the user on the settings page
            string username = dal.GetUsername(email);

            //So long as username != null, create a viewbag for it
            if(username != null)
            {
                ViewBag.Username = username;
            }

            //Get the users car list
            ViewBag.UsersCars = GetUsersCarsList(email);

            //Settings page
            ViewBag.Message = "Settings";

            //Set ViewBag.user = email
            ViewBag.user = email;

            //Create an email viewbag as well
            ViewBag.Email = email;

            //Get the list of current cars available to add from the databaser
            ViewBag.CurrentCarList = CurrentCarsAvailable();


            return View();
        }

        //Creates a select list of current cars available from the DB
        private List<SelectListItem> CurrentCarsAvailable()
        {
            //Get list of current cars in the database
            List<Cars> currentCarList = new List<Cars>();

            //this line sets the List of type Cars to the DB call
            currentCarList = getCurrentCarList();

            //Create a new select list 
            List<SelectListItem> currentCarsAvailable = new List<SelectListItem>();

            //Add information from DB to the select list
            for (int i = 0; i < currentCarList.Count; i++)
            {
                currentCarsAvailable.Add(new SelectListItem
                {
                    Text = currentCarList[i].type,
                    Value = currentCarList[i].id.ToString()
                });
            }
            return currentCarsAvailable;
        }

        //Creates a select list of current users cars from the DB
        private List<SelectListItem> GetUsersCarsList(string email)
        {
            // Creates new list of type UsersCars
            List<UsersCars> list = new List<UsersCars>();
            //DB Call
            list = getUsersCars(email);
            //Creates new select list 
            List<SelectListItem> usersCarsList = new List<SelectListItem>();

            //So long as the User has cars, add that information to the select list
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    usersCarsList.Add(new SelectListItem
                    {
                        Text = list[i].CarNickname,
                        Value = list[i].CarID
                    });
                }
                return usersCarsList;
            }
            //If user does not have any cars, return null
            else
            {
                return null;
            }

        }
        [HttpPost]
        public ActionResult Add(SettingsViewModel addForm)
        {
            //Grab users email to add to correct collection document
            string usersEmail = addForm.Email;

            //Get the type of car selected by the CarID
            string CarSelected = GetSelectedCarType(addForm.CarType);


            
            //Create new UsersCar object to add into database -> Gets information from the Form
            UsersCars newCar = new UsersCars();
            newCar.CarID = Guid.NewGuid().ToString();
            newCar.Type = CarSelected;
            newCar.mileage = Int32.Parse(addForm.Mileage);
            newCar.monthsOwned = Int32.Parse(addForm.MonthsOwned);
            newCar.totalSpent = Int32.Parse(addForm.TotalSpent);
            newCar.InspectionDue = addForm.InspectionDate;
            newCar.CarNickname = addForm.CarNickname;
            //Create a statement that initializes a personal maintenance object
            newCar.PersonalMaintenance = new List<PersonalMaintenance>();

            //If dal.AddCar is succesful -> redirects to users setting page
            if (dal.AddCar(usersEmail, newCar))
            {
                Session["email"] = usersEmail;
                var email = (string)Session["email"];
                return RedirectToAction("Settings", "Manage", new { email });
            } //Not successful throw error
            else
            {
                throw new Exception("Error");
            }
        }
        //Removes a car
        [HttpPost]
        public ActionResult Remove(SettingsViewModel remove)
        {
            //Grab email to redirect to page with users email still intact
            string usersEmail = remove.Email;
            Session["email"] = usersEmail;
            var email = (string)Session["email"];

            //Grab the car selected to be removed
            string carToBeRemoved = remove.CarType;

            //Calls db function in dal.cs file
            dal.RemoveCar(carToBeRemoved, usersEmail);

            //Return to users setting page
            return RedirectToAction("Settings", "Manage", new { email });
        }

        //Update the profile information
        [HttpPost]
        public ActionResult UpdateInfo(SettingsViewModel UpdateInfo)
        {

            //Get the users current email
            string usersEmail = UpdateInfo.CurrEmail;

            //Check and make sure the password is correct -> If not return error

            if (dal.CheckPassword(usersEmail, UpdateInfo.CurrPassword) == false)
            {
                Session["email"] = usersEmail;
                var email = (string)Session["email"];
                TempData["Invalid"] = "Invalid Credentials";
                return RedirectToAction("Settings", "Manage", new { email });
            }


            //We need to check if all things are null: 
            //if((UpdateInfo.Username != null) && (UpdateInfo.NewEmail != null) && (UpdateInfo.NewPassword != null)) first
            //else if((UpdateInfo.Username != null) && (UpdateInfo.NewPassword != null)) second
            //else if((UpdateInfo.NewEmail !=null) && (UpdateInfo.NewPassword != null)) third
            if ((UpdateInfo.Username != null) && (UpdateInfo.NewEmail != null))
            {
                if(dal.UpdateUsername(usersEmail, UpdateInfo.Username) && dal.UpdateEmail(usersEmail, UpdateInfo.NewEmail))
                {
                    //Get new email variable to send as a session to new view
                    string newEmail = UpdateInfo.NewEmail;
                    Session["email"] = newEmail;
                    var email = (string)Session["email"];

                    // Return to users setting page
                    return RedirectToAction("Settings", "Manage", new { email });
                }
                else
                {
                    throw new Exception("Something broke!");
                }
            }
            else if(UpdateInfo.NewEmail != null)
            {
                if(dal.UpdateEmail(usersEmail, UpdateInfo.NewEmail))
                {
                    //Get new email variable to send as a session to the new view
                    string newEmail = UpdateInfo.NewEmail;
                    Session["email"] = newEmail;
                    var email = (string)Session["email"];

                    // Return to users setting page
                    return RedirectToAction("Settings", "Manage", new { email });
                }
                else
                {
                    throw new Exception("Something broke!");
                }
            }
            else if(UpdateInfo.Username != null)
            {
                if(dal.UpdateUsername(usersEmail, UpdateInfo.Username))
                {
                    Session["email"] = usersEmail;
                    var email = (string)Session["email"];

                    // Return to users setting page
                    return RedirectToAction("Settings", "Manage", new { email });
                }
                else
                {
                    throw new Exception("Something Broke!");
                }
            }
            else
            {
                Session["email"] = usersEmail;
                var email = (string)Session["email"];
                return RedirectToAction("Settings", "Manage", new { email });
            }
            
        }

        //Gets the selected car of the select list -> Used in Add function and Remove Function
        private string GetSelectedCarType(string CarSelected)
        {
            return dal.GetSelectedCar(CarSelected);
        }

        //Gets the list of users current cars
        private List<UsersCars> getCurrentUserCars(string email)
        {
            return dal.getCurrentUsersCars(email);
        }

        //Gets users cars
        private List<UsersCars> getUsersCars(string email)
        {
            return dal.getCurrentUsersCars(email);
        }

        //Gets the list of cars available to add
        public List<Cars> getCurrentCarList()
        {
            return dal.getCurrentCarList();
        }
    }
}