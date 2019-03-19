﻿using Corts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;
using System.Security.Authentication;
using MongoDB.Driver.Linq;

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


        public bool LoginUser(Users user)
        {
            //We still need unhashing the passwords - but first hash passwords upon registration
            var collection = GetUsersCollection();
            string email = user.email;
            string password = user.password;
            
            var builder = Builders<Users>.Filter;
            var filt = builder.Where(x => x.email == email);
            var list = collection.Find(filt).ToList();

            if (list == null)
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

            if (list == null)
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
        public void CreateUser(Users user)
        {
           
            //TODO: HASH PASSWORDS UPON REGISTRATION
            var collection = GetUsersCollectionForEdit();
            try
            {
                collection.InsertOne(user);
            }
            catch
            {
                throw new System.ArgumentException("DB Update Fail", "users");
            }
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