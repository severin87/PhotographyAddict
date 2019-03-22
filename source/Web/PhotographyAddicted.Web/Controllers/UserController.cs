using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.DataServices;
using PhotographyAddicted.Services.Models.Users;

namespace PhotographyAddicted.Web.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {

        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult PreviewUsers()
        {
            IndexUserProfileViewModel usersProfiles = new IndexUserProfileViewModel
            {
                UserInfos = userService.GetUsersInfos(),
            };

            return View(usersProfiles);
        }
        
        public IActionResult ViewUserProfile()
        {
            var userProfile = userService.GetCurrentUserProfile(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
           
            return View(userProfile);
        }
        
        public IActionResult ChangeProfilePicture()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfilePicture( EditUserViewModel input, IFormFile ProfilePicture)
        {
            if (ModelState.IsValid)
            {
                input.Id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                await userService.AddProfilePicture(input, ProfilePicture);

                return RedirectToAction("ViewUserProfile", "User"); //, new { area = "" }
            }
            else
            {
                return this.View(input); // this.View(sev);
            }

        }
    }
}