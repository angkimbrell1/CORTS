using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corts.Models
{
    public class HomeViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Car")]
        public string Car { get; set; }

        [JsonProperty(PropertyName = "Maintenance Cost")]
        public int MaintenanceCost { get; set; }

        [JsonProperty(PropertyName = "Highway")]
        public int Highway { get; set; }

        [JsonProperty(PropertyName = "City")]
        public int City { get; set; }

        [JsonProperty(PropertyName = "Overall Rating")]
        public double OverallRating { get; set; }

        [JsonProperty(PropertyName = "Reliability")]
        public double Reliability { get; set; }
        
        
    }
}