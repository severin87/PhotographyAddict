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

        [Authorize]
        public IActionResult DeleteTheme(int Id)
        {
            var deletedTheme = themeService.FindDeletingThemeById(Id);

            if (deletedTheme.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return this.RedirectToAction("PreviewAllThemes", "Themes");
            }

            return View(deletedTheme);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteTheme(DeleteThemeViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            await themeService.DeleteTheme(input);

            return this.RedirectToAction("PreviewAllThemes", "Themes");
        }

        public IActionResult UpdateTheme(int id)
        {

            var updatedTheme = this.themeService.ViewUpdateThemeById(id);

            if (updatedTheme.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return this.RedirectToAction("SpecificTheme", new { id = updatedTheme.Id });
            }

            return this.View(updatedTheme);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTheme(UpdateTheme input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }
            
            int themeId = await themeService.UpdateTheme(input);

            return this.RedirectToAction("SpecificTheme", new { id = themeId });           
        }

        public IActionResult CreateTheme()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTheme(CreateThemeInputViewModel input) 
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int themeId = await themeService.CreateTheme(input);

            return this.RedirectToAction("SpecificTheme", new { id = themeId });
        }

        [AllowAnonymous]
        public IActionResult SpecificTheme(int id)
        {
            var specificTheme = themeService.ViewSpecificTheme(id);

            return this.View(specificTheme);
        }

        [AllowAnonymous]
        public IActionResult PreviewAllThemes()
        {
            var themes = themeService.GetAllThemes();

            var allThemes = new ThemesViewModel()
            {
                ThemeInfos = themes
            };

            return View(allThemes);
        }

        

    }
}