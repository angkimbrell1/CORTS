using Corts.DAL;
using Corts.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Corts.Controllers
{
    public class HomeController : Controller
    {
        private Dal dal = new Dal();
        public ActionResult Index(string email)
        {
            //Get car list function 
            // getCarList();
            //Get list of current cars in the database
            List<Cars> currentCarList = new List<Cars>();
            List<string> typeOfCar = new List<string>();
            List<double> overallRating = new List<double>();
            List<double> reliability = new List<double>();
            List<double> overallCost = new List<double>();
            List<int> highway = new List<int>();
            List<int> city = new List<int>();
            currentCarList = getCurrentCarList();
            foreach (var car in currentCarList)
            {
                typeOfCar.Add(car.type);
                overallRating.Add(car.OverallRating);
                reliability.Add(car.Reliability);
                overallCost.Add(car.OverallCost);
                highway.Add(car.highway);
                city.Add(car.city);
            }

            ViewBag.Car = typeOfCar;
            ViewBag.MaintenanceCost = overallCost;
            ViewBag.Highway = highway;
            ViewBag.City = city;
            ViewBag.Reliability = reliability;
            ViewBag.OverallRating = overallRating;

            ViewBag.Email = email;
            return View();
        }

        public ActionResult Maintenance()
        {
            ViewBag.Message = "Maintenance Page.";

            return View();
        }

        public List<Cars> getCurrentCarList()
        {
            return dal.getCurrentCarList();
        }

    }
}