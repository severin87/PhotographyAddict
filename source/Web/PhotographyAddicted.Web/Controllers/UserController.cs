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

        [AllowAnonymous]
        public IActionResult PreviewUsers(string input)
        {
            PreviewUsersViewModel usersProfiles = userService.PreviewUsers(input);
            
            return View(usersProfiles);
        }

        [AllowAnonymous]
        public IActionResult PreviewUser(string Id)
        {
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

                return RedirectToAction("PreviewUser", "User", new { id = input.Id  }); 
            }
            else
            {
                return this.View(input); 
            }
        }
    }
}