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
        public bool LoginUser(Users user)
        {
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