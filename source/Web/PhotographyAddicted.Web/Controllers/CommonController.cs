using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.DataServices.CommonService;
using PhotographyAddicted.Services.Models.Common;

namespace PhotographyAddicted.Web.Controllers
{
    public class CommonController : BaseController
    {
        private readonly ICommonService commonService;

        public CommonController(ICommonService commonService)
        {
            this.commonService = commonService;
        }

        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult BanUser(string userId, bool isBanned, string name)
        {
            var banUser = new BanUserViewModel()
            {
                UserId = userId,
                IsBanned = isBanned,
                UserName = name,
            };

            return View(banUser);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> BanUser(BanUserViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            await commonService.BanUser(input);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ModerationRights(string userId)
        {
            if (this.User.IsInRole("Admin"))
            {
                return View("ModerationRights", userId);
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BecomeModerator(string userId)
        {
            if (this.User.IsInRole("Admin"))
            {
                await commonService.BecomeModerator(userId);

                return RedirectToAction("PreviewUser", "Users", new { Id = userId });
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> StopModeratorRights(string userId)
        {
            if (this.User.IsInRole("Admin"))
            {
                await commonService.StopModeratorRights(userId);

                return RedirectToAction("PreviewUser", "Users", new { Id = userId });
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }
    }
}