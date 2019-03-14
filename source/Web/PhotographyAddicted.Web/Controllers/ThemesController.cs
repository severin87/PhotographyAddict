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
    public class ThemesController : BaseController
    {

        private IThemeService themeService;

        public ThemesController(IThemeService themeService)
        {
            this.themeService = themeService;
        }

        
        public IActionResult UpdateTheme(int id)
        {
            var updatedTheme = this.themeService.ViewUpdateThemeById(id);

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

            return this.RedirectToAction("Details", new { id = themeId });           
        }

        [Authorize]
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

            return this.RedirectToAction("Details", new { id = themeId });
        }

        public IActionResult Details(int id)
        {
            var specificTheme = themeService.ViewSpecificDetailsTheme(id);

            return this.View(specificTheme);
        }

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