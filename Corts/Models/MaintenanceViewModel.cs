using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Corts.Models.Classes;

namespace Corts.Models
{
    public class MaintenanceViewModel
    {
        [Display(Name = "Car")]
        public string Car { get; set; }
        [Display(Name = "Car")]
        public string CarType { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Mileage")]
        public string Mileage { get; set; }

        [Display(Name = "Did you get your inspection done?")]
        public bool Inspection { get; set; }

        // This property will hold all available maintenanceobjects for selection
        public List<MaintenanceObject> MaintenanceItems { get; set; }

        public List<MaintenanceObject> DefaultMaintenance { get; set; }

        public List<string> SubmittedMaintenance { get; set; }
    }
}