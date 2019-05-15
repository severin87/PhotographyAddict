using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.DataServices;
using PhotographyAddicted.Web.Areas.Identity.Data;
using PhotographyAddicted.Web.Models;
using PhotographyAddicted.Services.DataServices.CommonService;

namespace PhotographyAddicted.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ICommonService commonService;
        private readonly IUserService userService;
        public IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<PhotographyAddictedUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(IUserService userService, ICommonService commonService, UserManager<PhotographyAddictedUser> userManager
            , RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            this.userService = userService;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.commonService = commonService;
        }

        public async Task<IActionResult> Idtu()
        {
            if (this.User.Identity.Name != null)
            {


                await this.roleManager.CreateAsync(new IdentityRole("Admin"));

                await this.roleManager.CreateAsync(new IdentityRole("Moderator"));
                var userInfo = await userManager.GetUserAsync(this.User);


                await userManager.AddToRoleAsync(userInfo, "Admin");
               //await  userManager.IsInRoleAsync(userInfo, "Moderator");
                return this.RedirectToAction("Index", "Home");
                //  return Json(userInfo.PasswordHash);
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

        public async Task<IActionResult> Index()
        {
            if (await commonService.IsUserBanned(this.User.Identity.Name))
            {
                return Redirect("../Identity/Account/Login");
            }

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
