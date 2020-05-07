﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = $"Hello {name}";
            ViewData["NumTimes"] = numTimes;
            return View();
        }

        /*
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //public string Index()
        //{
        //    return "This is the default action.";
        //}

        //public string Welcome()
        //{
        //    return "This is the Welcome action method";
        //}

        public string Welcome(int number)
        {
            return $"This {number} is the number you sent";
        }
        */
    }
}