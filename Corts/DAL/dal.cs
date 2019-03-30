using Corts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;
using System.Security.Authentication;
using MongoDB.Driver.Linq;
using static Corts.Models.Classes;
using MongoDB.Driver.Builders;

namespace Corts.DAL
{
    public class Dal : IDisposable
    {
        //THIS FILE HANDLES ALL DB CALLS
        private bool disposed = false;


        private string userName = "";
        private string host = "";
        private string password = "";


        private string dbName = "CortsDB";
        private string collectionName = "Users";



        // Default constructor.        
        public Dal()
        {
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

            if(list.Count == 0)
            {
                return null;
            }
            else
            {
                List<UsersCars> usersCars = list[0].Cars;
                return usersCars;
            }
        }
        

        //Add a car -> Completes Add Form on SettingsPage
        public bool AddCar(string usersEmail, UsersCars newCar)
        {
            var collection = GetUsersCollectionForEdit();
            var builder = Builders<Users>.Filter;
            var filt = builder.Where(x => x.email == usersEmail);
            var list = collection.Find(filt).ToList();
            if(list[0].Cars == null)
            {
                try
                {
                    var update = Builders<Users>.Update.Unset(e => e.Cars);
                    collection.UpdateOne(filt, update, new UpdateOptions { IsUpsert = true });
                    var updateCar = Builders<Users>.Update.Push(e => e.Cars, newCar);
                    collection.UpdateOne(filt, updateCar);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                try
                {
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
        //Get selected car from add form and return CarType
        public string GetSelectedCar(string CarSelected)
        {
            var collection = GetCarsCollection();
            var builder = Builders<Cars>.Filter;
            var filt = builder.Where(x => x.id == CarSelected);
            var list = collection.Find(filt).ToList();
            string CarType = null;
            if (list.Count == 0)
            {
                return null;
            }
            else
            {
                CarType = list[0].type;
            }
            return CarType;
        }

        //Login user
        public bool LoginUser(Users user)
        {
            //We still need unhashing the passwords - but first hash passwords upon registration
            var collection = GetUsersCollection();
            string email = user.email;
            string password = user.password;

            var builder = Builders<Users>.Filter;
            var filt = builder.Where(x => x.email == email);
            var list = collection.Find(filt).ToList();

            if (list.Count == 0)
            {
                return false;
            }
            string passwordFromDB = list[0].password;

            if (passwordFromDB == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Check User Registration Date
        public bool getCreatedDate(Users user)
        {
            //We still need unhashing the passwords - but first hash passwords upon registration
            var collection = GetUsersCollection();
            string email = user.email;
            string password = user.password;

            var builder = Builders<Users>.Filter;
            var filt = builder.Where(x => x.email == email);
            var list = collection.Find(filt).ToList();
            string TodaysDate = DateTime.Today.ToString();

            if (list.Count == 0)
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
           
            //TODO: HASH PASSWORDS UPON REGISTRATION
            var collection = GetUsersCollectionForEdit();
            try
            {
                collection.InsertOne(user);
            }
            catch
            {
                return false;
            }
            return true;
        }
       


        //Database Collection Retrievers

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
            var todoTaskCollection = database.GetCollection<Cars>("Cars");
            return todoTaskCollection;
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
            var todoTaskCollection = database.GetCollection<Users>(collectionName);
            return todoTaskCollection;
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
            var todoTaskCollection = database.GetCollection<Users>(collectionName);
            return todoTaskCollection;
        }

        # region IDisposable

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