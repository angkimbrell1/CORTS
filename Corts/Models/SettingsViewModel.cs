using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Corts.Models
{
    
    public class SettingsViewModel
    { 
        [Display(Name = "Car")]
        public string Car { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Current Email")]
        [EmailAddress]
        public string CurrEmail { get; set; }
        [Display(Name = "New Email")]
        [EmailAddress]
        public string NewEmail { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        //[Display(Name = "UsersCars")]
        //public IEnumerable<SelectListItem> UsersCars { get; set; }

        [Display(Name = "Months Owned")]
        public string MonthsOwned { get; set; }

        [Display(Name = "Mileage")]
        public string Mileage { get; set; }

        [Display(Name = "Estimated Total Spent")]
        public string TotalSpent { get; set; }

        [Display(Name = "Inspection")]
        public string InspectionDate { get; set; }

        [Display(Name = "Car")]
        public string CarType { get; set; }


    }
    public enum Car
    {
        FordFusion2018,
        ChevyCruze2018,
        SuburuImpreza2018
    }
    public enum Notifications
    {
        daily,
        weekly,
        monthly,
        annually
    }
}