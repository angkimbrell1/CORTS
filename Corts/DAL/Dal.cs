using Corts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;
using System.Security.Authentication;
using System.Security.Cryptography;
using MongoDB.Driver.Linq;
using static Corts.Models.Classes;
using System.Web.Script.Serialization;
using MongoDB.Bson.IO;
using Newtonsoft;

namespace Corts.DAL
{
    public class Dal : IDisposable
    {
        //THIS FILE HANDLES ALL DB CALLS
        private bool disposed = false;


        


        private string dbName = "CortsDB";
        private string collectionName = "Users";





        // Default constructor.        
        public Dal()
        {
        }

        //Gets the cars selected nickname for display at the top of the table in the maintenance page
        public string GetCarNickname(string car, string email)
        {
            List<UsersCars> usersCars = getCurrentUsersCars(email);
            string CarNickname = null;
            if(usersCars != null)
            {
                for (int i = 0; i < usersCars.Count; i++)
                {
                    if(usersCars[i].CarID == car)
                    {
                        CarNickname = usersCars[i].CarNickname;
                        return CarNickname;
                    }
                    else
                    {
                        CarNickname = "Car Not Found";
                    }
                }
            }
            return CarNickname;
            
        }
        //Get cars mileage
        public int GetCarMileage(string car, string email)
        {
            List<UsersCars> usersCars = getCurrentUsersCars(email);
            int CarMileage = 0;
            if (usersCars != null)
            {
                for (int i = 0; i < usersCars.Count; i++)
                {
                    if (usersCars[i].CarID == car)
                    {
                        CarMileage = usersCars[i].mileage;
                        return CarMileage;
                    }
                    else
                    {
                        CarMileage = 0;
                    }
                }
            }
            return CarMileage;
        }
        //Get Months Owned
        public int GetMonthsOwned(string car, string email)
        {
            List<UsersCars> usersCars = getCurrentUsersCars(email);
            int MonthsOwned = 0;
            if (usersCars != null)
            {
                for (int i = 0; i < usersCars.Count; i++)
                {
                    if (usersCars[i].CarID == car)
                    {
                        MonthsOwned = usersCars[i].monthsOwned;
                        return MonthsOwned;
                    }
                    else
                    {
                        MonthsOwned = 0;
                    }
                }
            }
            return MonthsOwned;
        }
        //Get List of all Maintenance Items Names in DB for the Update Maintenance Form
        public List<MaintenanceObject> GetMaintenanceItems()
        {
            var collection = GetMaintenanceScheduleCollection();
            var coll = collection.AsQueryable();
            List<MaintenanceObject> MaintenanceItems = new List<MaintenanceObject>();
            foreach (var item in coll)
            {
                MaintenanceItems.Add(item);
            }
            return MaintenanceItems;

        }
        //Get current list of available cars in database
        public List<Cars> getCurrentCarList()
        {
            var collection = GetCarsCollection();
            var CollectionIDCars = "Cars";
            var builder = Builders<Cars>.Filter;
            var filt = builder.Where(x => x.CollectionID == CollectionIDCars);
            var list = collection.Find(filt).ToList();

            if (list.Count == 0)
            {
                return null;
            }
            else
            {
                List<Cars> carList = list;
                return carList;
            }
        }
        //Get list of current cars user has for settings and maintenance page
        public List<UsersCars> getCurrentUsersCars(string email)
        {
            var collection = GetUsersCollection();
            string usersemail = email;


            var builder = Builders<Users>.Filter;
            var filt = builder.Where(x => x.email == usersemail);
            var list = collection.Find(filt).ToList();

            if (list.Count == 0)
            {
                return null;
            }
            else
            {
                List<UsersCars> usersCars = list[0].Cars;
                return usersCars;
            }
        }

        #region UsersPersonalMaintenaceRetrival (NOT UPDATE)
        //This is the car personal maintenance information -> Same function for each, just pulls a different maintenance item and its corresponding maintenance
        //Get list of users personal maintenance items
        public List<PersonalMaintenance> GetUsersPersonalMaintenance(string email, string CarID)
        {

            List<UsersCars> usersCars = getCurrentUsersCars(email);
            List<PersonalMaintenance> personalMaintenance = new List<PersonalMaintenance>();

            for (int i = 0; i < usersCars.Count; i++)
            {
                if (usersCars[i].CarID == CarID)
                {
                    personalMaintenance = usersCars[i].PersonalMaintenance.ToList();
                    return personalMaintenance;
                }
                else
                {
                    personalMaintenance = null;
                }

            }
            return personalMaintenance;
        }

        public List<PersonalMaintenanceObject> GetAirFilterInformation(List<PersonalMaintenance> CarPM)
        {
            List<PersonalMaintenanceObject> aFilter = new List<PersonalMaintenanceObject>();
            for (int i = 0; i < CarPM.Count; i++)
            {
                aFilter = CarPM[i].AirFilter.ToList();
            }
            return aFilter;
        }
        public List<PersonalMaintenanceObject> GetOilChangeInformation(List<PersonalMaintenance> CarPM)
        {
            List<PersonalMaintenanceObject> oilChange = new List<PersonalMaintenanceObject>();
            for (int i = 0; i < CarPM.Count; i++)
            {
                if (CarPM[i].OilChange != null)
                {
                    oilChange = CarPM[i].OilChange.ToList();
                }
                else
                {
                    oilChange = null;
                }
            }
            return oilChange;
        }
        public List<PersonalMaintenanceObject> GetCoolantInformation(List<PersonalMaintenance> CarPM)
        {
            List<PersonalMaintenanceObject> oilChange = new List<PersonalMaintenanceObject>();
            for (int i = 0; i < CarPM.Count; i++)
            {
                if (CarPM[i].Coolant != null)
                {
                    oilChange = CarPM[i].Coolant.ToList();
                }
                else
                {
                    oilChange = null;
                }
            }
            return oilChange;
        }
        public List<PersonalMaintenanceObject> GetTransFluidInfo(List<PersonalMaintenance> CarPM)
        {
            List<PersonalMaintenanceObject> oilChange = new List<PersonalMaintenanceObject>();
            for (int i = 0; i < CarPM.Count; i++)
            {
                if (CarPM[i].TransFluid != null)
                {
                    oilChange = CarPM[i].TransFluid.ToList();
                }
                else
                {
                    oilChange = null;
                }
            }
            return oilChange;
        }
        public List<PersonalMaintenanceObject> GetFuelFilterInfo(List<PersonalMaintenance> CarPM)
        {
            List<PersonalMaintenanceObject> oilChange = new List<PersonalMaintenanceObject>();
            for (int i = 0; i < CarPM.Count; i++)
            {
                if (CarPM[i].FuelFilter != null)
                {
                    oilChange = CarPM[i].FuelFilter.ToList();
                }
                else
                {
                    oilChange = null;
                }
            }
            return oilChange;
        }
        public List<PersonalMaintenanceObject> GetBatteryInfo(List<PersonalMaintenance> CarPM)
        {
            List<PersonalMaintenanceObject> oilChange = new List<PersonalMaintenanceObject>();
            for (int i = 0; i < CarPM.Count; i++)
            {
                if (CarPM[i].Battery != null)
                {
                    oilChange = CarPM[i].Battery.ToList();
                }
                else
                {
                    oilChange = null;
                }
            }
            return oilChange;
        }
        public List<PersonalMaintenanceObject> GetHVACInfo(List<PersonalMaintenance> CarPM)
        {
            List<PersonalMaintenanceObject> oilChange = new List<PersonalMaintenanceObject>();
            for (int i = 0; i < CarPM.Count; i++)
            {
                if (CarPM[i].HVAC != null)
                {
                    oilChange = CarPM[i].HVAC.ToList();
                }
                else
                {
                    oilChange = null;
                }
            }
            return oilChange;
        }
        public List<PersonalMaintenanceObject> GetBrakesInfo(List<PersonalMaintenance> CarPM)
        {
            List<PersonalMaintenanceObject> oilChange = new List<PersonalMaintenanceObject>();
            for (int i = 0; i < CarPM.Count; i++)
            {
                if (CarPM[i].Brakes != null)
                {
                    oilChange = CarPM[i].Brakes.ToList();
                }
                else
                {
                    oilChange = null;
                }
            }
            return oilChange;
        }
        public List<PersonalMaintenanceObject> GetRadiatorHosesInfo(List<PersonalMaintenance> CarPM)
        {
            List<PersonalMaintenanceObject> oilChange = new List<PersonalMaintenanceObject>();
            for (int i = 0; i < CarPM.Count; i++)
            {
                if (CarPM[i].RadiatorHoses != null)
                {
                    oilChange = CarPM[i].RadiatorHoses.ToList();
                }
                else
                {
                    oilChange = null;
                }
            }
            return oilChange;
        }
        public List<PersonalMaintenanceObject> GetSuspensionInfo(List<PersonalMaintenance> CarPM)
        {
            List<PersonalMaintenanceObject> oilChange = new List<PersonalMaintenanceObject>();
            for (int i = 0; i < CarPM.Count; i++)
            {
                if (CarPM[i].Suspension != null)
                {
                    oilChange = CarPM[i].Suspension.ToList();
                }
                else
                {
                    oilChange = null;
                }
            }
            return oilChange;
        }
        public List<PersonalMaintenanceObject> GetSparkPlugs(List<PersonalMaintenance> CarPM)
        {
            List<PersonalMaintenanceObject> oilChange = new List<PersonalMaintenanceObject>();
            for (int i = 0; i < CarPM.Count; i++)
            {
                if (CarPM[i].SparkPlugs != null)
                {
                    oilChange = CarPM[i].SparkPlugs.ToList();
                }
                else
                {
                    oilChange = null;
                }
            }
            return oilChange;
        }
        public List<PersonalMaintenanceObject> GetIgnitionSystemInfo(List<PersonalMaintenance> CarPM)
        {
            List<PersonalMaintenanceObject> oilChange = new List<PersonalMaintenanceObject>();
            for (int i = 0; i < CarPM.Count; i++)
            {
                if (CarPM[i].IgnitionSystem != null)
                {
                    oilChange = CarPM[i].IgnitionSystem.ToList();
                }
                else
                {
                    oilChange = null;
                }
            }
            return oilChange;
        }
        public List<PersonalMaintenanceObject> GetEngineDBInfo(List<PersonalMaintenance> CarPM)
        {
            List<PersonalMaintenanceObject> oilChange = new List<PersonalMaintenanceObject>();
            for (int i = 0; i < CarPM.Count; i++)
            {
                if (CarPM[i].EngineDriveBelts != null)
                {
                    oilChange = CarPM[i].EngineDriveBelts.ToList();
                }
                else
                {
                    oilChange = null;
                }
            }
            return oilChange;
        }
        public List<PersonalMaintenanceObject> GetTiresInfo(List<PersonalMaintenance> CarPM)
        {
            List<PersonalMaintenanceObject> oilChange = new List<PersonalMaintenanceObject>();
            for (int i = 0; i < CarPM.Count; i++)
            {
                if (CarPM[i].Tires != null)
                {
                    oilChange = CarPM[i].Tires.ToList();
                }
                else
                {
                    oilChange = null;
                }
            }
            return oilChange;
        }
        public List<PersonalMaintenanceObject> GetPowerSteering(List<PersonalMaintenance> CarPM)
        {
            List<PersonalMaintenanceObject> pSteering = new List<PersonalMaintenanceObject>();
            for (int i = 0; i < CarPM.Count; i++)
            {
                if (CarPM[i].PowerSteering != null)
                {
                    pSteering = CarPM[i].PowerSteering.ToList();
                }
                else
                {
                    pSteering = null;
                }
            }
            return pSteering;
        }
        //End of users personal maintenance retrieval
        #endregion

        #region Update Maintenance Information
        #endregion

        #region EmailReminders
        //Get list of users emails for email reminders
        public List<string> GetUsersEmails()
        {
            var collection = GetUsersCollection();
            var coll = collection.AsQueryable();
            List<string> Emails = new List<string>();
            foreach (var item in coll)
            {
                Emails.Add(item.email);
            }
            return Emails;
        }
        #endregion

        #region SettingsPageFunctions

        public List<MaintenanceObject> GetPersonalMaintenanceObjectByName(string name)
        {
            var collection = GetMaintenanceScheduleCollection();
            var maintenanceObjectName = name;
            var builder = Builders<MaintenanceObject>.Filter;
            var filt = builder.Where(x => x.Name == maintenanceObjectName);
            var list = collection.Find(filt).ToList();

            if (list.Count == 0)
            {
                return null;
            }
            else
            {
                return list;
            }

        }
        public List<PersonalMaintenance> InitializePersonalMaintenance(int mileage, int totalSpent)
        {
            List<PersonalMaintenance> pmList = new List<PersonalMaintenance>() {
                new PersonalMaintenance() {
                    AirFilter = new List<PersonalMaintenanceObject>() { new PersonalMaintenanceObject ()
                    {
                         Name = "Air Filter", LastChecked = 0, Cost = totalSpent, NxtNeeded = GetMileageNeeded(GetPersonalMaintenanceObjectByName("Air Filter"))}
                    },
                    Battery = new List<PersonalMaintenanceObject>() { new PersonalMaintenanceObject ()
                    {
                         Name = "Battery", LastChecked = 0, Cost = totalSpent, NxtNeeded = GetMileageNeeded(GetPersonalMaintenanceObjectByName("Battery"))}
                    },
                    Brakes = new List<PersonalMaintenanceObject>() { new PersonalMaintenanceObject ()
                    {
                         Name = "Brakes", LastChecked = 0, Cost = totalSpent, NxtNeeded = GetMileageNeeded(GetPersonalMaintenanceObjectByName("Brakes"))}
                    },
                    Coolant = new List<PersonalMaintenanceObject>() { new PersonalMaintenanceObject ()
                    {
                         Name = "Coolant", LastChecked = 0, Cost = totalSpent, NxtNeeded = GetMileageNeeded(GetPersonalMaintenanceObjectByName("Coolant"))}
                    },
                    EngineDriveBelts = new List<PersonalMaintenanceObject>() { new PersonalMaintenanceObject ()
                    {
                         Name = "Engine Drive Belts", LastChecked = 0, Cost = totalSpent, NxtNeeded = GetMileageNeeded(GetPersonalMaintenanceObjectByName("Engine Drive Belts"))}
                    },
                    FuelFilter = new List<PersonalMaintenanceObject>() { new PersonalMaintenanceObject ()
                    {
                         Name = "Fuel Filter", LastChecked = 0, Cost = totalSpent, NxtNeeded = GetMileageNeeded(GetPersonalMaintenanceObjectByName("Fuel Filter"))}
                    },
                    HVAC = new List<PersonalMaintenanceObject>() { new PersonalMaintenanceObject ()
                    {
                         Name = "HVAC", LastChecked = 0, Cost = totalSpent, NxtNeeded = GetMileageNeeded(GetPersonalMaintenanceObjectByName("HVAC"))}
                    },
                    IgnitionSystem = new List<PersonalMaintenanceObject>() { new PersonalMaintenanceObject ()
                    {
                         Name = "Ignition System", LastChecked = 0, Cost = totalSpent, NxtNeeded = GetMileageNeeded(GetPersonalMaintenanceObjectByName("Ignition System"))}
                    },
                    OilChange = new List<PersonalMaintenanceObject>() { new PersonalMaintenanceObject ()
                    {
                         Name = "Oil Change", LastChecked = 0, Cost = totalSpent, NxtNeeded = GetMileageNeeded(GetPersonalMaintenanceObjectByName("Oil Change"))}
                    },
                    PowerSteering = new List<PersonalMaintenanceObject>() { new PersonalMaintenanceObject ()
                    {
                         Name = "Power Steering", LastChecked = 0, Cost = totalSpent, NxtNeeded = GetMileageNeeded(GetPersonalMaintenanceObjectByName("Power Steering"))}
                    },
                    RadiatorHoses = new List<PersonalMaintenanceObject>() { new PersonalMaintenanceObject ()
                    {
                         Name = "Radiator Hoses", LastChecked = 0, Cost = totalSpent, NxtNeeded = GetMileageNeeded(GetPersonalMaintenanceObjectByName("Radiator Hoses"))}
                    },
                    SparkPlugs = new List<PersonalMaintenanceObject>() { new PersonalMaintenanceObject ()
                    {
                         Name = "Spark Plugs", LastChecked = 0, Cost = totalSpent, NxtNeeded = GetMileageNeeded(GetPersonalMaintenanceObjectByName("Spark Plugs"))}
                    },
                    Suspension = new List<PersonalMaintenanceObject>() { new PersonalMaintenanceObject ()
                    {
                         Name = "Suspension", LastChecked = 0, Cost = totalSpent, NxtNeeded = GetMileageNeeded(GetPersonalMaintenanceObjectByName("Suspension"))}
                    },
                    Tires = new List<PersonalMaintenanceObject>() { new PersonalMaintenanceObject ()
                    {
                         Name = "Tires", LastChecked = 0, Cost = totalSpent, NxtNeeded = GetMileageNeeded(GetPersonalMaintenanceObjectByName("Tires"))}
                    },
                    TransFluid = new List<PersonalMaintenanceObject>() { new PersonalMaintenanceObject ()
                    {
                         Name = "Transmission Fluid", LastChecked = 0, Cost = totalSpent, NxtNeeded = GetMileageNeeded(GetPersonalMaintenanceObjectByName("Transmission Fluid"))}
                    }
            }

            };

            return pmList;

        }
        //Add a car -> Completes Add Form on SettingsPage
        public bool AddCar(string usersEmail, UsersCars newCar)
        {
            //Grab the UsersCollectionForEdit -> Allows us to modify instead of just read
            var collection = GetUsersCollectionForEdit();
            //Create a filter object
            var builder = Builders<Users>.Filter;
            //Filter to the correct user (found by usersEmail)
            var filt = builder.Where(x => x.email == usersEmail);
            //Convert user selected to list object
            var list = collection.Find(filt).ToList();

            //If user has no cars -> We must first delete the "null" list in the DB and then add a new car
            if (list[0].Cars == null)
            {
                try
                {
                    //Delete "null" object in DB for Cars
                    var update = Builders<Users>.Update.Unset(e => e.Cars);
                    collection.UpdateOne(filt, update, new UpdateOptions { IsUpsert = true });

                    //Add new car
                    var updateCar = Builders<Users>.Update.Push(e => e.Cars, newCar);
                    collection.UpdateOne(filt, updateCar);
                }
                catch
                {
                    return false;
                }
            }
            //User already has a car in the database and we can simply just update document to add another
            else
            {
                try
                {
                    //Adds car
                    var update = Builders<Users>.Update.Push(e => e.Cars, newCar);
                    collection.UpdateOne(filt, update);
                }
                catch
                {
                    return false;
                }
            }

            return true;

        }
        //Remove a car
        public bool RemoveCar(string removeID, string usersEmail)
        {
            //Grab collection
            var collection = GetUsersCollectionForEdit();
            //Create builder of type User
            var builder = Builders<Users>.Filter;
            //Filter to user specific document
            var filt = builder.Where(x => x.email == usersEmail);
            //Grab document and convert to list
            var list = collection.Find(filt).ToList();

            try
            {
                //Removes car with specific carID
                var update = Builders<Users>.Update.PullFilter(x => x.Cars, Cars => Cars.CarID == removeID);
                collection.UpdateOne(filt, update);
            }
            catch
            {
                return false;
            }
            return true;


        }
        //Check password before account updates
        public bool CheckPassword(string usersEmail, string passwordInserted)
        {
            //Grab user collection
            var collection = GetUsersCollection();
            //Get user by email
            string email = usersEmail;
            //Get password that was inserted into EditProfile form
            string password = passwordInserted;

            //Create builder of type User
            var builder = Builders<Users>.Filter;
            //Filter to user specific 
            var filt = builder.Where(x => x.email == email);
            //Grab document and all of its fixins
            var list = collection.Find(filt).ToList();

            //If user is not found -> return false
            if (list.Count == 0)
            {
                return false;
            }

            //Unhash password and check to make sure it matches the password that was inserted
            //Get savedPasswordHash from the data base
            string passwordFromDB = list[0].password;

            //Convert the password from the DB to an array of bytes
            byte[] hashBytes = Convert.FromBase64String(passwordFromDB);

            //Get the salt from the hashbytes
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            //Use the same variable to hash the password that the user entered
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);

            //Convert the hashed password from the user to an array of bytes
            byte[] hash = pbkdf2.GetBytes(20);

            //Set a flag 'ok' to validate the password that the user entered
            //with the password in the database
            int ok = 1;

            //Loop that checks the validity of the password by comparing each byte
            //of the db password with the user password. The hashBytes starts at 16
            //because the salt value is stored in the first 16 bytes and we aren't
            //comparing that with anything.
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    ok = 0;
                }
            }

            //log the user in
            if (ok == 1)
            {
                return true;
            }

            //Deny the user
            else
            {
                return false;
            }
        }
        //Update Users Email
        public bool UpdateEmail(string usersEmail, string newEmail)
        {
            //Grab the UsersCollectionForEdit -> Allows us to modify instead of just read
            var collection = GetUsersCollectionForEdit();
            //Create a filter object
            var builder = Builders<Users>.Filter;
            //Filter to the correct user (found by usersEmail)
            var filt = builder.Where(x => x.email == usersEmail);
            //Convert user selected to list object
            var list = collection.Find(filt).ToList();

            try
            {
                //Update users email
                var updateEmail = Builders<Users>.Update.Set(e => e.email, newEmail);
                collection.UpdateOne(filt, updateEmail);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public string GetUsername(string email)
        {
            var collection = GetUsersCollection();
            string usersemail = email;


            var builder = Builders<Users>.Filter;
            var filt = builder.Where(x => x.email == usersemail);
            var list = collection.Find(filt).ToList();

            //Can't find user -> return null
            if (list.Count == 0)
            {
                return null;
            }
            else
            {
                string username = list[0].Username;
                return username;
            }
        }
        public bool UpdateUsername(string usersEmail, string username)
        {
            //Grab the UsersCollectionForEdit -> Allows us to modify instead of just read
            var collection = GetUsersCollectionForEdit();
            //Create a filter object
            var builder = Builders<Users>.Filter;
            //Filter to the correct user (found by usersEmail)
            var filt = builder.Where(x => x.email == usersEmail);
            //Convert user selected to list object
            var list = collection.Find(filt).ToList();

            try
            {
                //Update users email
                var updateUsername = Builders<Users>.Update.Set(e => e.Username, username);
                collection.UpdateOne(filt, updateUsername);
            }
            catch
            {
                return false;
            }
            return true;
        }
        //Get selected car from add form and return CarType
        public string GetSelectedCar(string CarSelected)
        {
            //Grab the cars collection
            var collection = GetCarsCollection();
            //Create builder of type Cars
            var builder = Builders<Cars>.Filter;
            //Grab the ID of the car that was selected 
            var filt = builder.Where(x => x.id == CarSelected);
            //Grab what was found and set its fixins to a list
            var list = collection.Find(filt).ToList();

            //Create variable called CarType
            string CarType = null;
            //Can't find the car listed? Return null.
            if (list.Count == 0)
            {
                return null;
            }
            else
            {
                //Set CarType = CarType from DB
                CarType = list[0].type;
            }
            return CarType;
        }
        #endregion

        #region Login and Register Functionality
        //Login user
        public bool LoginUser(Users user)
        {
            //Grab users collection
            var collection = GetUsersCollection();

            //Grab email that was inserted into form
            string email = user.email;
            //Grab password that was inserted into form
            string password = user.password;

            //Create builder of type Users
            var builder = Builders<Users>.Filter;
            //Filter to User where email = email that was inserted
            var filt = builder.Where(x => x.email == email);
            //Send what was found to a list
            var list = collection.Find(filt).ToList();

            //If user was not found -> return false: DENY ENTRY
            if (list.Count == 0)
            {
                return false;
            }


            //Unhash password and allow user to login if password is correct, if not correct return false
            //Get savedPasswordHash from the data base
            string passwordFromDB = list[0].password;

            //Convert the password from the DB to an array of bytes
            byte[] hashBytes = Convert.FromBase64String(passwordFromDB);

            //Get the salt from the hashbytes
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            //Use the same variable to hash the password that the user entered
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);

            //Convert the hashed password from the user to an array of bytes
            byte[] hash = pbkdf2.GetBytes(20);

            //Set a flag 'ok' to validate the password that the user entered
            //with the password in the database
            int ok = 1;

            //Loop that checks the validity of the password by comparing each byte
            //of the db password with the user password. The hashBytes starts at 16
            //because the salt value is stored in the first 16 bytes and we aren't
            //comparing that with anything.
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    ok = 0;
                }
            }

            //log the user in
            if (ok == 1)
            {
                return true;
            }

            //Deny the user
            else
            {
                return false;
            }
        }
        //Check User Registration Date -> This is in case they registered that day, in which case it will send them to the settings page. 
        public bool getCreatedDate(Users user)
        {
            var collection = GetUsersCollection();
            string email = user.email;
            string password = user.password;

            //Get savedPasswordHash from the data base
            var builder = Builders<Users>.Filter;
            var filt = builder.Where(x => x.email == email);
            var list = collection.Find(filt).ToList();
            string passwordFromDB = list[0].password;

            //Convert the password from the DB to an array of bytes
            byte[] hashBytes = Convert.FromBase64String(passwordFromDB);

            //Get the salt from the hashbytes
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            //Use the same variable to hash the password that the user entered
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);

            //Convert the hashed password from the user to an array of bytes
            byte[] hash = pbkdf2.GetBytes(20);

            //Set a flag 'ok' to validate the password that the user entered
            //with the password in the database
            int ok = 1;

            //Loop that checks the validity of the password by comparing each byte
            //of the db password with the user password. The hashBytes starts at 16
            //because the salt value is stored in the first 16 bytes and we aren't
            //comparing that with anything.
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    ok = 0;
                }
            }

            string TodaysDate = DateTime.Today.ToString();

            if (list.Count == 0)
            {
                return false;
            }
            else if (ok == 0)
            {
                return false;
            }
            else if (list[0].FirstLoggedIn == TodaysDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Register User
        public bool CreateUser(Users user)
        {
            //New byte array to hold the salt
            byte[] salt;

            //Generate a random salt
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            //Concatenate salt and user password then hash it using Rfc...Bytes
            var pdkdf2 = new Rfc2898DeriveBytes(user.password, salt, 10000);

            //Place the concatenated string in the byte array hash
            byte[] hash = pdkdf2.GetBytes(20);

            //New byte array to store hashed password + salt
            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            user.password = savedPasswordHash;

            //Get the collection for edit -> allows us to edit the users collection
            var collection = GetUsersCollectionForEdit();
            try
            {
                //Insert new collection -> user
                collection.InsertOne(user);
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region MaintenanceInformation
        public int GetMileageNeeded(List<MaintenanceObject> mainObject)
        {
            var collection = GetMaintenanceScheduleCollection();
            var maintenanceObjectID = mainObject[0].Id;
            var builder = Builders<MaintenanceObject>.Filter;
            var filt = builder.Where(x => x.Id == maintenanceObjectID);
            var list = collection.Find(filt).ToList();

            if (list.Count == 0)
            {
                return 0;
            }
            else
            {
                int mileage = list[0].Mileage;
                return mileage;
            }
        }
        #endregion

        #region DB Collection Retrievers
        //Database Collection Retrievers
        private IMongoCollection<MaintenanceObject> GetMaintenanceScheduleCollection()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            MongoClient client = new MongoClient(settings);
            var database = client.GetDatabase(dbName);
            var maintenanceCollection = database.GetCollection<MaintenanceObject>("MaintenanceSchedule");
            return maintenanceCollection;
        }
        private IMongoCollection<Cars> GetCarsCollection()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            MongoClient client = new MongoClient(settings);
            var database = client.GetDatabase(dbName);
            var carsCollection = database.GetCollection<Cars>("Cars");
            return carsCollection;
        }
        private IMongoCollection<Users> GetUsersCollection()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            MongoClient client = new MongoClient(settings);
            var database = client.GetDatabase(dbName);
            var usersCollection = database.GetCollection<Users>(collectionName);
            return usersCollection;
        }

        private IMongoCollection<Users> GetUsersCollectionForEdit()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            MongoClient client = new MongoClient(settings);
            var database = client.GetDatabase(dbName);
            var usersCollection = database.GetCollection<Users>(collectionName);
            return usersCollection;
        }
        #endregion


        #region IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }
            }

            this.disposed = true;
        }


        #endregion
    }
}