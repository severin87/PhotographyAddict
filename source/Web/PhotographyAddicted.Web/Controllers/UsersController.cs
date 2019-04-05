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
        public async Task<IActionResult> PreviewUser(string Id)
        {
            if (await commonService.IsUserBanned(this.User.Identity.Name))
            {
                return Redirect("../../Identity/Account/Login");
            }

            var userProfile = userService.PreviewUser(Id);
           
            return View(userProfile);
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
    }
}