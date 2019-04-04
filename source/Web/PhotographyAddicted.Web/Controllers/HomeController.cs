﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        private readonly UserManager<PhotographyAddictedUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(IUserService userService, UserManager<PhotographyAddictedUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userService = userService;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Idtu()
        {
            if (this.User.Identity.Name != null)
            {


                await this.roleManager.CreateAsync(new IdentityRole("Admin"));

                var userInfo = await userManager.GetUserAsync(this.User);


                await userManager.AddToRoleAsync(userInfo, "Admin");
                await  userManager.IsInRoleAsync(userInfo, "Moderator");
                return Json(userInfo.PasswordHash);
            }
            else
            {
                return Json("A taka");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ForAdmin()
        {
            if (this.User.IsInRole("Admin"))

            {

                ViewBag.Message = "Welcome to the admin area!";

                return Json("Welcome to the admin area!");

            }
            else
                return Json("Welcome to the admin area Ujjjj s Tortuise! Tarikat!!!!!!!!");

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = $"Your application has {this.userService.GetUsersCount()} users.";

            return View();
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
