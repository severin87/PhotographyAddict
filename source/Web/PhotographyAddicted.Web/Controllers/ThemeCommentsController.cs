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
    public class ThemeCommentsController : BaseController
    {
        private readonly IThemeCommentService themeCommentService;

        public ThemeCommentsController(IThemeCommentService themeCommentService)
        {
            this.themeCommentService = themeCommentService;
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

            return this.RedirectToAction("SpecificTheme", "Themes", new { id = input.ThemeId });
        }
    }
}