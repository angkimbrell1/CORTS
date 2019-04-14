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
    public class MaintenanceController : Controller
    {
        private Dal dal = new Dal();
        public ActionResult Maintenance(string email, string car)
        {
            //Get the users car list
            ViewBag.UsersCars = GetUsersCarsList(email);

            ViewBag.Email = email;
            //Get a list of all the maintenance items for the form
            //ViewBag.MaintenanceItems = GetMaintenanceItems();


            if (email != null && car!=null)
            {
                ViewBag.Email = email;
                ViewBag.Car = car;

                //Get the nickname to display at the table
                ViewBag.CarNickName = GetCarNickname(car, email);

                //Get the cars mileage to display at the top of the table
                ViewBag.CarsMileage = GetMileage(car, email);

                //Get Months owned to display at the top of the table
                ViewBag.MonthsOwned = GetMonthsOwned(car, email);

                //Get their inspection date
                ViewBag.InspectionDate = GetCarsInspectionDate(car, email);

                //Get their personal maintenance information
                ViewBag.UsersMaintenance = GetUsersCarsMaintenanceInformation(email, car);

                


                //Personal Maintenance Objects for car selected
                ViewBag.AirFilter = GetAirFilterInfo(email, car);
                ViewBag.OilChange = GetOilChangeInfo(email, car);
                ViewBag.PowerSteering = GetPowerSteeringInfo(email, car);
                ViewBag.Coolant = GetCoolantInfo(email, car);
                ViewBag.TransFluid = GetTransFluidInfo(email, car);
                ViewBag.FuelFilter = GetFuelFilterInfo(email, car);
                ViewBag.Battery = GetBatteryInfo(email, car);
                ViewBag.HVAC = GetHVACInfo(email, car);
                ViewBag.Brakes = GetBrakesInfo(email, car);
                ViewBag.RadiatorHoses = GetRadiatorHosesInfo(email, car);
                ViewBag.Suspension = GetSuspensionInfo(email, car);
                ViewBag.SparkPlugs = GetSparkPlugs(email, car);
                ViewBag.IgnitionSystem = GetIgnitionSystemInfo(email, car);
                ViewBag.EngineDriveBelts = GetEngineDBInfo(email, car);
                ViewBag.Tires = GetTiresInfo(email, car);

            }
            else
            {
                ViewBag.UsersEmail = null;
                ViewBag.Car = null;
            }

            
            return View();
        }

        public int GetMileage(string car, string email)
        {
            return dal.GetCarMileage(car, email);
        }
        public int GetMonthsOwned(string car, string email)
        {
            return dal.GetMonthsOwned(car, email);
        }

        //This gets the car selected nickname and displays it at the top of the table
        public string GetCarNickname(string car, string email)
        {
            return dal.GetCarNickname(car, email);
        }

        public string GetCarsInspectionDate(string car, string email)
        {
            return dal.GetCarsInspectionDate(car, email);
        }

        #region GetPersonalMaintenance
        public List<string> GetPowerSteeringInfo(string email, string CarID)
        {
            List<string> test = new List<string>();
            List<PersonalMaintenanceObject> pmObject = new List<PersonalMaintenanceObject>();
            List<PersonalMaintenance> UserCar = dal.GetUsersPersonalMaintenance(email, CarID);
            pmObject = dal.GetPowerSteering(UserCar);
            if (pmObject != null)
            {
                foreach (var item in pmObject)
                {
                    test.Add(item.Name);
                    test.Add(item.LastChecked.ToString());
                    test.Add(item.NxtNeeded.ToString());
                }
            }
            else
            {
                test = null;
            }
            return test;
        }

        public List<string> GetOilChangeInfo(string email, string CarID)
        {
            List<string> test = new List<string>();
            List<PersonalMaintenanceObject> pmObject = new List<PersonalMaintenanceObject>();
            List<PersonalMaintenance> UserCar = dal.GetUsersPersonalMaintenance(email, CarID);
            pmObject = dal.GetOilChangeInformation(UserCar);
            if (pmObject != null)
            {
                foreach (var item in pmObject)
                {
                    test.Add(item.Name);
                    test.Add(item.LastChecked.ToString());
                    test.Add(item.NxtNeeded.ToString());
                }
            }
            else
            {
                test = null;
            }
            return test;
        }
        public List<string> GetCoolantInfo(string email, string CarID)
        {
            List<string> test = new List<string>();
            List<PersonalMaintenanceObject> pmObject = new List<PersonalMaintenanceObject>();
            List<PersonalMaintenance> UserCar = dal.GetUsersPersonalMaintenance(email, CarID);
            pmObject = dal.GetCoolantInformation(UserCar);
            if(pmObject != null)
            {
                foreach (var item in pmObject)
                {
                    test.Add(item.Name);
                    test.Add(item.LastChecked.ToString());
                    test.Add(item.NxtNeeded.ToString());
                }
            }
            else
            {
                test = null;
            }
            
            return test;
        }
        public List<string> GetTransFluidInfo(string email, string CarID)
        {
            List<string> test = new List<string>();
            List<PersonalMaintenanceObject> pmObject = new List<PersonalMaintenanceObject>();
            List<PersonalMaintenance> UserCar = dal.GetUsersPersonalMaintenance(email, CarID);
            pmObject = dal.GetTransFluidInfo(UserCar);
            if (pmObject != null)
            {
                foreach (var item in pmObject)
                {
                    test.Add(item.Name);
                    test.Add(item.LastChecked.ToString());
                    test.Add(item.NxtNeeded.ToString());
                }
            }
            else
            {
                test = null;
            }
            return test;
        }
        public List<string> GetFuelFilterInfo(string email, string CarID)
        {
            List<string> test = new List<string>();
            List<PersonalMaintenanceObject> pmObject = new List<PersonalMaintenanceObject>();
            List<PersonalMaintenance> UserCar = dal.GetUsersPersonalMaintenance(email, CarID);
            pmObject = dal.GetFuelFilterInfo(UserCar);
            if (pmObject != null)
            {
                foreach (var item in pmObject)
                {
                    test.Add(item.Name);
                    test.Add(item.LastChecked.ToString());
                    test.Add(item.NxtNeeded.ToString());
                }
            }
            else
            {
                test = null;
            }
            return test;
        }
        public List<string> GetBatteryInfo(string email, string CarID)
        {
            List<string> test = new List<string>();
            List<PersonalMaintenanceObject> pmObject = new List<PersonalMaintenanceObject>();
            List<PersonalMaintenance> UserCar = dal.GetUsersPersonalMaintenance(email, CarID);
            pmObject = dal.GetBatteryInfo(UserCar);
            if (pmObject != null)
            {
                foreach (var item in pmObject)
                {
                    test.Add(item.Name);
                    test.Add(item.LastChecked.ToString());
                    test.Add(item.NxtNeeded.ToString());
                }
            }
            else
            {
                test = null;
            }
            return test;
        }
        public List<string> GetHVACInfo(string email, string CarID)
        {
            List<string> test = new List<string>();
            List<PersonalMaintenanceObject> pmObject = new List<PersonalMaintenanceObject>();
            List<PersonalMaintenance> UserCar = dal.GetUsersPersonalMaintenance(email, CarID);
            pmObject = dal.GetHVACInfo(UserCar);
            if (pmObject != null)
            {
                foreach (var item in pmObject)
                {
                    test.Add(item.Name);
                    test.Add(item.LastChecked.ToString());
                    test.Add(item.NxtNeeded.ToString());
                }
            }
            else
            {
                test = null;
            }
            return test;
        }
        public List<string> GetBrakesInfo(string email, string CarID)
        {
            List<string> test = new List<string>();
            List<PersonalMaintenanceObject> pmObject = new List<PersonalMaintenanceObject>();
            List<PersonalMaintenance> UserCar = dal.GetUsersPersonalMaintenance(email, CarID);
            pmObject = dal.GetBrakesInfo(UserCar);
            if (pmObject != null)
            {
                foreach (var item in pmObject)
                {
                    test.Add(item.Name);
                    test.Add(item.LastChecked.ToString());
                    test.Add(item.NxtNeeded.ToString());
                }
            }
            else
            {
                test = null;
            }
            return test;
        }
        public List<string> GetRadiatorHosesInfo(string email, string CarID)
        {
            List<string> test = new List<string>();
            List<PersonalMaintenanceObject> pmObject = new List<PersonalMaintenanceObject>();
            List<PersonalMaintenance> UserCar = dal.GetUsersPersonalMaintenance(email, CarID);
            pmObject = dal.GetRadiatorHosesInfo(UserCar);
            if (pmObject != null)
            {
                foreach (var item in pmObject)
                {
                    test.Add(item.Name);
                    test.Add(item.LastChecked.ToString());
                    test.Add(item.NxtNeeded.ToString());
                }
            }
            else
            {
                test = null;
            }
            return test;
        }
        public List<string> GetSuspensionInfo(string email, string CarID)
        {
            List<string> test = new List<string>();
            List<PersonalMaintenanceObject> pmObject = new List<PersonalMaintenanceObject>();
            List<PersonalMaintenance> UserCar = dal.GetUsersPersonalMaintenance(email, CarID);
            pmObject = dal.GetSuspensionInfo(UserCar);
            if (pmObject != null)
            {
                foreach (var item in pmObject)
                {
                    test.Add(item.Name);
                    test.Add(item.LastChecked.ToString());
                    test.Add(item.NxtNeeded.ToString());
                }
            }
            else
            {
                test = null;
            }
            return test;
        }
        public List<string> GetSparkPlugs(string email, string CarID)
        {
            List<string> test = new List<string>();
            List<PersonalMaintenanceObject> pmObject = new List<PersonalMaintenanceObject>();
            List<PersonalMaintenance> UserCar = dal.GetUsersPersonalMaintenance(email, CarID);
            pmObject = dal.GetSparkPlugs(UserCar);
            if (pmObject != null)
            {
                foreach (var item in pmObject)
                {
                    test.Add(item.Name);
                    test.Add(item.LastChecked.ToString());
                    test.Add(item.NxtNeeded.ToString());
                }
            }
            else
            {
                test = null;
            }
            return test;
        }
        public List<string> GetIgnitionSystemInfo(string email, string CarID)
        {
            List<string> test = new List<string>();
            List<PersonalMaintenanceObject> pmObject = new List<PersonalMaintenanceObject>();
            List<PersonalMaintenance> UserCar = dal.GetUsersPersonalMaintenance(email, CarID);
            pmObject = dal.GetIgnitionSystemInfo(UserCar);
            if (pmObject != null)
            {
                foreach (var item in pmObject)
                {
                    test.Add(item.Name);
                    test.Add(item.LastChecked.ToString());
                    test.Add(item.NxtNeeded.ToString());
                }
            }
            else
            {
                test = null;
            }
            return test;
        }
        public List<string> GetEngineDBInfo(string email, string carID)
        {
            List<string> test = new List<string>();
            List<PersonalMaintenanceObject> pmObject = new List<PersonalMaintenanceObject>();
            List<PersonalMaintenance> UserCar = dal.GetUsersPersonalMaintenance(email, carID);
            pmObject = dal.GetEngineDBInfo(UserCar);
            if (pmObject != null)
            {
                foreach (var item in pmObject)
                {
                    test.Add(item.Name);
                    test.Add(item.LastChecked.ToString());
                    test.Add(item.NxtNeeded.ToString());
                }
            }
            else
            {
                test = null;
            }
            return test;
        }
        public List<string> GetTiresInfo(string email, string CarID)
        {
            List<string> test = new List<string>();
            List<PersonalMaintenanceObject> pmObject = new List<PersonalMaintenanceObject>();
            List<PersonalMaintenance> UserCar = dal.GetUsersPersonalMaintenance(email, CarID);
            pmObject = dal.GetTiresInfo(UserCar);
            if (pmObject != null)
            {
                foreach (var item in pmObject)
                {
                    test.Add(item.Name);
                    test.Add(item.LastChecked.ToString());
                    test.Add(item.NxtNeeded.ToString());
                }
            }
            else
            {
                test = null;
            }
            return test;
        }
        public List<string> GetAirFilterInfo(string email, string CarID)
        {
            List<string> test = new List<string>();
            List<PersonalMaintenanceObject> pmObject = new List<PersonalMaintenanceObject>();
            List<PersonalMaintenance> UserCar = dal.GetUsersPersonalMaintenance(email, CarID);
            pmObject = dal.GetAirFilterInformation(UserCar);
            if (pmObject != null)
            {
                foreach (var item in pmObject)
                {
                    test.Add(item.Name);
                    test.Add(item.LastChecked.ToString());
                    test.Add(item.NxtNeeded.ToString());
                }
            }
            else
            {
                test = null;
            }
            return test;
        }
        private List<MaintenanceObject> GetMaintenanceItems()
        {

            return dal.GetMaintenanceItems();
        }
        //Create the information for the table on the MaintenancePage
        private List<PersonalMaintenance> GetUsersCarsMaintenanceInformation(string email, string carID)
        {
            return dal.GetUsersPersonalMaintenance(email, carID);
        }
        #endregion

        //This function pulls the cars selected information
        [HttpPost]
        public ActionResult GetCar(MaintenanceViewModel maintenance)
        {
            string CarSelected = maintenance.CarType;
            string usersEmail = maintenance.Email;
            Session["email"] = usersEmail;
            var email = (string)Session["email"];

            Session["car"] = CarSelected;
            var car = (string)Session["car"];
            return RedirectToAction("Maintenance", "Maintenance", new { email, car });
        }
      
        [HttpPost]
        public ActionResult Update(MaintenanceViewModel update)
        {
            string usersEmail = update.Email;
            try
            {
                dal.UpdateMileage(Int32.Parse(update.Mileage), update.CarType, update.Email);

                //Check to see if inspection is checked
                if(update.Inspection == true)
                {
                    dal.UpdateInspection(update.CarType, update.Email);
                }

                if(update.Spent != null)
                {
                    dal.UpdateTotalCost(update.CarType, update.Email, (Int32.Parse(update.Spent)));
                }
                string CarSelected = update.CarType;
                //string usersEmail = update.Email;
                Session["email"] = usersEmail;
                var email = (string)Session["email"];

                Session["car"] = CarSelected;
                var car = (string)Session["car"];
                //Check to see if maintenance items are selected
                //if (CheckAndUpdateMaintenanceItems(update))
                //{
                   
                //    return RedirectToAction("Maintenance", "Maintenance", new { email, car });
                //}
                return RedirectToAction("Maintenance", "Maintenance", new { email, car });

            }
            catch
            {
                return View("~/Maintenance/Maintenance.cshtml");
            }
            
        }
        //public bool CheckAndUpdateMaintenanceItems(MaintenanceViewModel update)
        //{
        //    int currentMileage = dal.GetCarMileage(update.CarType, update.Email);
        //    if (update.AirFilterBox == true)
        //    {
        //        int mileageNeededAt = dal.GetMileageNeeded(dal.GetPersonalMaintenanceObjectByName("Air Filter"));
        //        int nextNeeded = currentMileage + mileageNeededAt;
        //        try
        //        {
        //            List<PersonalMaintenanceObject> pmList = new List<PersonalMaintenanceObject>()
        //            {
        //                new PersonalMaintenanceObject()
        //                {
        //                    Name = "Air Filter",
        //                    LastChecked = Int32.Parse(update.Mileage),
        //                    NxtNeeded = currentMileage + mileageNeededAt
        //                }
        //            };
        //            //dal.UpdateAirFilterInformation(update.Email, update.CarType, pmList);
        //            return true;
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }
        //    throw new Exception("Something went wrong");
        //}        //public bool CheckAndUpdateMaintenanceItems(MaintenanceViewModel update)
        //{
        //    int currentMileage = dal.GetCarMileage(update.CarType, update.Email);
        //    if (update.AirFilterBox == true)
        //    {
        //        int mileageNeededAt = dal.GetMileageNeeded(dal.GetPersonalMaintenanceObjectByName("Air Filter"));
        //        int nextNeeded = currentMileage + mileageNeededAt;
        //        try
        //        {
        //            List<PersonalMaintenanceObject> pmList = new List<PersonalMaintenanceObject>()
        //            {
        //                new PersonalMaintenanceObject()
        //                {
        //                    Name = "Air Filter",
        //                    LastChecked = Int32.Parse(update.Mileage),
        //                    NxtNeeded = currentMileage + mileageNeededAt
        //                }
        //            };
        //            //dal.UpdateAirFilterInformation(update.Email, update.CarType, pmList);
        //            return true;
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }
        //    throw new Exception("Something went wrong");
        //}
        //Creates a select list of current users cars from the DB and gives it to them in a dropdown
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

        //Gets users cars from the database -> called in GetUsersCarsList()
        private List<UsersCars> getUsersCars(string email)
        {
            return dal.getCurrentUsersCars(email);
        }

        
    }
}