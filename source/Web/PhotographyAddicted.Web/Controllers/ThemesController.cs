using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.Models.Themes;
using System.Security.Claims;
using PhotographyAddicted.Services.DataServices;

namespace PhotographyAddicted.Web.Controllers
{
    [Authorize]
    public class ThemesController : BaseController
    {
        private IThemeService themeService;

        public ThemesController(IThemeService themeService)
        {
            this.themeService = themeService;
        }
       
        public IActionResult DeleteTheme(int Id)
        {
            var deletedTheme = themeService.FindThemeBy(Id);

            if (deletedTheme.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return this.RedirectToAction("PreviewThemes", "Themes");
            }

            return View(deletedTheme);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTheme(PreviewThemeViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            await themeService.DeleteTheme(input);

            return this.RedirectToAction("PreviewThemes", "Themes");
        }

        public IActionResult UpdateTheme(int id)
        {
            var updatedTheme = this.themeService.FindThemeBy(id);

            if (updatedTheme.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return this.RedirectToAction("PreviewTheme", new { id = updatedTheme.Id });
            }

            return this.View(updatedTheme);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTheme(PreviewThemeViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }
            
            await themeService.UpdateTheme(input);

            return this.RedirectToAction("PreviewThemes", "Themes");
        }

        public IActionResult AddTheme()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTheme(AddThemeViewModel input) 
        {
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

            return this.View(specificTheme);
        }

        [AllowAnonymous]
        public IActionResult PreviewThemes(string input)
        {
            var themes = themeService.PreviewThemes(input);
            
            return View(themes);
        }       
    }
}