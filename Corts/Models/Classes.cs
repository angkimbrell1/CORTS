using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corts.Models
{
    public class Classes
    {
        public class UsersCars
        {
            [BsonId(IdGenerator = typeof(CombGuidGenerator))]
            public Guid ID { get; set; }
            public string Type { get; set; }
            public int mileage { get; set; }
            public string InspectionDue { get; set; }
            public int monthsOwned { get; set; }
            public int totalSpent { get; set; }
            public string CarID { get; set; }
            public string CarNickname { get; set; }
            public List<PersonalMaintenance> PersonalMaintenance { get; set; }
        }
        public class PersonalMaintenance
        {
            public List<PersonalMaintenanceObject> OilChange { get; set; }
            public List<PersonalMaintenanceObject> WindShieldFluid { get; set; }
            public List<PersonalMaintenanceObject> PowerSteering { get; set; }
            public List<PersonalMaintenanceObject> Coolant { get; set; }
            public List<PersonalMaintenanceObject> TransFluid { get; set; }
            public List<PersonalMaintenanceObject> AirFilter { get; set; }
            public List<PersonalMaintenanceObject> Battery { get; set; }
            public List<PersonalMaintenanceObject> FuelFilter { get; set; }
            public List<PersonalMaintenanceObject> HVAC { get; set; }
            public List<PersonalMaintenanceObject> Brakes { get; set; }
            public List<PersonalMaintenanceObject> RadiatorHoses { get; set; }
            public List<PersonalMaintenanceObject> Suspension { get; set; }
            public List<PersonalMaintenanceObject> SparkPlugs { get; set; }
            public List<PersonalMaintenanceObject> SparkPlugWires { get; set; }
            public List<PersonalMaintenanceObject> IgnitionSystem { get; set; }
            public List<PersonalMaintenanceObject> EngineDriveBelts { get; set; }
            public List<PersonalMaintenanceObject> Tires { get; set; }

        }
        public class Users
        {
            [BsonId(IdGenerator = typeof(CombGuidGenerator))]
            public Guid Id { get; set; }

            [BsonElement("Username")]
            public string Username { get; set; }

            [BsonElement("email")]
            public string email { get; set; }

            [BsonElement("password")]
            public string password { get; set; }

            [BsonElement("FirstLoggedIn")]
            public string FirstLoggedIn { get; set; }

            [BsonElement("Cars")]
            public List<UsersCars> Cars { get; set; }

        }
    }

    public class PersonalMaintenanceObject
    {
        public int Cost { get; set; }
        public int LastChecked { get; set; }
        public int NxtNeeded { get; set; }
    }
    public class Cars
    {
        public ObjectId Id { get; set; }
        public int carid { get; set; }
        public string type { get; set; }
        public double OverallRating { get; set; }
        public double Reliability { get; set; }
        public double OverallCost { get; set; }
        public int highway { get; set; }
        public int city { get; set; }
        public string CollectionID { get; set; }
        public string id { get; set; }
    }
}