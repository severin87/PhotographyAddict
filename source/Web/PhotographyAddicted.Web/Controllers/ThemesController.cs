﻿using System;
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
            var specificTheme = themeService.ViewSpecificTheme(id);
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