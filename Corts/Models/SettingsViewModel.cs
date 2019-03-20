using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Corts.Models
{
    public class UsersCars
    {
        public int ID { get; set; }
        public string Type { get; set; }
    }
    public class SettingsViewModel
    { 
   
        
        [Display(Name = "Car")]
        public string Car { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


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