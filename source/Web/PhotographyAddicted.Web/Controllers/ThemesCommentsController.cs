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
    public class ThemesCommentsController : BaseController
    {
        private readonly IThemeCommentService themeCommentService;

        public ThemesCommentsController(IThemeCommentService themeCommentService)
        {
            this.themeCommentService = themeCommentService;
        }

        public IActionResult UpdateThemeComment(int id)
        {

            var updatedThemeComment = this.themeCommentService.ViewUpdateThemeById(id);

            if (updatedThemeComment.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return this.RedirectToAction("PreviewTheme", "Themes", new { id = updatedThemeComment.ThemeId });
            }

            return this.View(updatedThemeComment);
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

        [Authorize]
        public IActionResult AddThemeComment(int Id)
        {
            var newThemeComment = new AddThemeCommentViewModel()
            {
                ThemeId = Id,
            };
         
            return View(newThemeComment);
        }

        [Authorize]
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

        [Authorize]
        public IActionResult DeleteThemeComment(int Id)
        {
            var deletedThemeComment = themeCommentService.FindThemeCommentById(Id);

            if (deletedThemeComment == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (deletedThemeComment.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return this.RedirectToAction("PreviewTheme", "Themes", new { id = deletedThemeComment.ThemeId });
            }

            return View(deletedThemeComment);
        }

        [Authorize]
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