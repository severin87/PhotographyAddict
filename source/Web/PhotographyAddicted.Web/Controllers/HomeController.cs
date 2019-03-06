using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Web.Areas.Identity.Data;
using PhotographyAddicted.Web.Models;

namespace PhotographyAddicted.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<PhotographyAddictedUser> userInfo;

        public HomeController(IRepository<PhotographyAddictedUser> userInfo)
        {
            this.userInfo = userInfo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = $"Your application has {this.userInfo.All().Count()} users.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
