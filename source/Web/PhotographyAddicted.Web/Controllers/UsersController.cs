using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.DataServices;
using PhotographyAddicted.Services.DataServices.CommonService;
using PhotographyAddicted.Services.Models.Users;

namespace PhotographyAddicted.Web.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserService userService;
        private readonly ICommonService commonService;

        public UsersController(IUserService userService, ICommonService commonService)
        {
            this.userService = userService;
            this.commonService = commonService;
        }

        [AllowAnonymous]
        public IActionResult PreviewUsers(string input)
        {
            PreviewUsersViewModel usersProfiles = userService.PreviewUsers(input);
            
            return View(usersProfiles);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> PreviewUser(string Id)
        {
            var user = userService.PreviewUser(Id);
            if (await commonService.IsUserBanned(this.User.Identity.Name))
            {
                return Redirect("../../Identity/Account/Login");
            }

            if (user == null)
            {
                return this.RedirectToAction("Index", "Home");
            }
           
            return View(user);
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult PreviewUserPost(string Id)
        {
            return RedirectToAction("PreviewUser", "Users", new { Id });
        }
        
        public IActionResult UpdateProfilePicture()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfilePicture(PreviewUserViewModel input, IFormFile ProfilePicture)
        {
            if (ModelState.IsValid)
            {
                input.Id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                await userService.UpdateProfilePicture(input, ProfilePicture);

                return RedirectToAction("PreviewUser", "Users", new { id = input.Id  }); 
            }
            else
            {
                return this.View(input); 
            }
        }

        [AllowAnonymous]
        public IActionResult PreviewUsersSearch(string input)
        {
            return RedirectToAction("PreviewUsers", "Users", new { input });
        }
    }
}