using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.DataServices;
using PhotographyAddicted.Services.Models.NewsComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhotographyAddicted.Web.Controllers
{
    public class NewsCommentsController : BaseController
    {
        private INewCommentService newCommentService;

        public NewsCommentsController(INewCommentService newCommentService)
        {
            this.newCommentService = newCommentService;
        }

        public IActionResult UpdateNewComment(int id)
        {

            var updatedNew = this.newCommentService.ViewUpdateNewById(id);

            if (updatedNew.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return this.RedirectToAction("ViewSpecificNew", "News", new { id = updatedNew.NewId });
            }

            return this.View(updatedNew);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNewComment(UpdateNewCommentViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            int themeId = await newCommentService.UpdateNewComment(input);

            return this.RedirectToAction("ViewSpecificNew", "News", new { id = themeId });
        }

        [Authorize]
        public IActionResult AddNewComment(int Id)
        {
            var newThemeComment = new AddNewCommentViewModel()
            {
                NewId = Id,
            };

            return View(newThemeComment);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNewComment(AddNewCommentViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await newCommentService.AddNewComment(input);

            return this.RedirectToAction("ViewSpecificNew", "News", new { id = input.NewId });
        }

        [Authorize]
        public IActionResult DeleteNewComment(int Id)
        {
            var deletedNewComment = newCommentService.FindNewCommentById(Id);

            if (deletedNewComment.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return RedirectToAction("Index", "Home");
            }

            return View(deletedNewComment);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteNewComment(DeleteNewCommentsViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            int newId = await newCommentService.DeleteUserNewComment(input);

            return this.RedirectToAction("ViewSpecificNew", "News", new { id = newId });
        }
    }
}
