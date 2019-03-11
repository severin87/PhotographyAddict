﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Services.DataServices;
using PhotographyAddicted.Services.Models.Users;
using PhotographyAddicted.Web.Areas.Identity.Data;
using PhotographyAddicted.Web.Models;

namespace PhotographyAddicted.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = $"Your application has {this.userService.GetCount()} users.";

            return View();
        }

        public IActionResult Contact()
        {

            var infos = userService.GetSpecificUser(2);

            var viewModel = new IndexViewModel()
            {
                UserInfos = infos
            };

            return View(viewModel);

            //ViewData["Message"] = "Your contact page.";

            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
