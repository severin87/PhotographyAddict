using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.DataServices;
using PhotographyAddicted.Services.Models.ThemesComments;

namespace PhotographyAddicted.Web.Controllers
{
    [Authorize]
    public class ThemesCommentsController : BaseController
    {
        private readonly IThemeCommentService themeCommentService;
        private IThemeService themeService;

        public ThemesCommentsController(IThemeCommentService themeCommentService, IThemeService themeService)
        {
            this.themeService = themeService;
            this.themeCommentService = themeCommentService;
        }

        public IActionResult UpdateThemeComment(int Id)
        {
            var updateThemeComment = themeCommentService.FindThemeCommentById(Id);

            if (updateThemeComment == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var updatedThemeComment = this.themeCommentService.ViewUpdateThemeById(Id);

            if ((updatedThemeComment.PhotographyAddictedUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier) || this.User.IsInRole("Moderator")))
            {
                return this.View(updatedThemeComment);
            }

            return this.RedirectToAction("PreviewTheme", "Themes", new { id = updatedThemeComment.ThemeId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateThemeComment(UpdateThemeCommentViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            int themeId = await themeCommentService.UpdateThemeComment(input);

            return this.RedirectToAction("PreviewTheme", "Themes", new { Id = themeId });
        }
        
        public IActionResult AddThemeComment(int Id)
        {
            var isThemeExist = themeService.FindThemeBy(Id);

            if (isThemeExist == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var newThemeComment = new AddThemeCommentViewModel()
            {
                ThemeId = Id,
            };
         
            return View(newThemeComment);
        }

        [HttpPost]
        public async Task<IActionResult> AddThemeComment(AddThemeCommentViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }
            
            input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await themeCommentService.AddThemeComment(input);

            return this.RedirectToAction("PreviewTheme", "Themes", new { id = input.ThemeId });
        }
        
        public IActionResult DeleteThemeComment(int Id)
        {
            var deletedThemeComment = themeCommentService.FindThemeCommentById(Id);

            if (deletedThemeComment == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if ((deletedThemeComment.PhotographyAddictedUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier) || this.User.IsInRole("Moderator")))
            {
                return View(deletedThemeComment);
            }

            return this.RedirectToAction("PreviewTheme", "Themes", new { id = deletedThemeComment.ThemeId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteThemeComment(DeleteThemeCommentViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            int themeId = await themeCommentService.DeleteThemeComment(input);

            return this.RedirectToAction("PreviewTheme", "Themes", new { id = themeId });
        }
    }
}