using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.Models.Themes;
using System.Security.Claims;
using PhotographyAddicted.Services.DataServices;
using PhotographyAddicted.Services.DataServices.CommonService;

namespace PhotographyAddicted.Web.Controllers
{
    [Authorize]
    public class ThemesController : BaseController
    {
        private IThemeService themeService;
        private readonly ICommonService commonService;

        public ThemesController(IThemeService themeService, ICommonService commonService)
        {
            this.themeService = themeService;
            this.commonService = commonService;
        }

        public IActionResult DeleteTheme(int Id)
        {
            var deletedTheme = themeService.FindThemeBy(Id);

            if (deletedTheme == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (deletedTheme.PhotographyAddictedUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier) || this.User.IsInRole("Moderator"))
            {
                return View(deletedTheme);
            }

            return this.RedirectToAction("PreviewThemes", "Themes");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTheme(PreviewThemeViewModel deletedTheme)
        {
            await themeService.DeleteTheme(deletedTheme);

            return this.RedirectToAction("PreviewThemes", "Themes");
        }

        public IActionResult UpdateTheme(int id)
        {
            var updatedTheme = this.themeService.FindThemeBy(id);

            if (updatedTheme == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (updatedTheme.PhotographyAddictedUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier) || this.User.IsInRole("Moderator"))
            {
                return this.View(updatedTheme);
            }

            return this.RedirectToAction("PreviewTheme", new { id = updatedTheme.Id });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTheme(PreviewThemeViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }
            
            await themeService.UpdateTheme(input);

            return this.RedirectToAction("PreviewTheme", "Themes", new { id = input.Id });
        }

        public IActionResult AddTheme()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTheme(AddThemeViewModel input) 
        {
            if (input.Title== null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await themeService.AddTheme(input);

            return this.RedirectToAction("PreviewThemes", "Themes");
        }

        [AllowAnonymous]
        public IActionResult PreviewTheme(int id)
        {
            var specificTheme = themeService.PreviewTheme(id);

            if (specificTheme == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(specificTheme);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> PreviewThemes(string input)
        {
            if (await commonService.IsUserBanned(this.User.Identity.Name))
            {
                return Redirect("../../Identity/Account/Login");
            }

            var themes = themeService.PreviewThemes(input);
            
            return View(themes);
        }

        [AllowAnonymous]
        public IActionResult PreviewThemesSearch(string input)
        {
            return RedirectToAction("PreviewThemes", "Themes", new { input });
        }
    }
}