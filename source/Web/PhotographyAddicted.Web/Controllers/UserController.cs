using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.DataServices;

namespace PhotographyAddicted.Web.Controllers
{
    public class UserController : BaseController
    {

        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult ViewUserProfile()
        {
            var userProfile = userService.GetCurrentUserProfile(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
           
            return View(userProfile);
        }
    }
}