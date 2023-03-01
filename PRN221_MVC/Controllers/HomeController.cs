﻿using Microsoft.AspNetCore.Mvc;
using PRN221_MVC.Models;
using System.Diagnostics;

namespace PRN221_MVC.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            //return View("/Views/Home/LoginClient.cshtml");
            return View();
        }

        public IActionResult LoginClient() {
            return View();
        }
        public IActionResult Category() {
            return View();
        }


        public IActionResult Privacy() {
            return View();
        }
        public IActionResult Details(int id) {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}