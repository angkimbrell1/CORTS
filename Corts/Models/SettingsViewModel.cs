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

        //Profile Information
        //This holds all the information for the profile form
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        //Current Email
        [Required]
        [Display(Name = "Current Email")]
        [EmailAddress]
        public string CurrEmail { get; set; }

        //New Email
        [Display(Name = "New Email")]
        [EmailAddress]
        public string NewEmail { get; set; }

        //Current Password
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrPassword { get; set; }

        //New Password
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        //Car information
        [Required]
        [Display(Name = "Car Nickname")]
        public string CarNickname { get; set; }

        [Required]
        [Display(Name = "Months Owned")]
        public string MonthsOwned { get; set; }

        [Required]
        [Display(Name = "Mileage")]
        public string Mileage { get; set; }

        [Required]
        [Display(Name = "Estimated Total Spent")]
        public string TotalSpent { get; set; }

        [Required]
        [Display(Name = "Inspection")]
        public string InspectionDate { get; set; }

        [Required]
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