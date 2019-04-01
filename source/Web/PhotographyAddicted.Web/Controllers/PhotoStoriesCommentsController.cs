using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.DataServices.PhotoStoryCommentService;
using PhotographyAddicted.Services.Models.PhotoStoryComments;

namespace PhotographyAddicted.Web.Controllers
{
    [Authorize]
    public class PhotoStoriesCommentsController : BaseController
    {
        private readonly IPhotoStoryCommentService photoStoryCommentService;

        public PhotoStoriesCommentsController(IPhotoStoryCommentService photoStoryCommentService)
        {
            this.photoStoryCommentService = photoStoryCommentService;
        }

        [AllowAnonymous]
        public IActionResult PreviewPhotoStoryComment(int Id)
        {
            var photoStoryComment = photoStoryCommentService.PreviewPhotoStoryCommentById(Id);

            return View(photoStoryComment);
        }
                
        public IActionResult AddPhotoStoryComment(int id)
        {
            var photoStoryComment = new AddPhotoStoryCommentViewModel()
            {
                PhotoStoryId = id,
            };

            return View(photoStoryComment);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoStoryComment(AddPhotoStoryCommentViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await photoStoryCommentService.AddPhotoStoryComment(input);

            return this.RedirectToAction("PreviewPhotoStory", "PhotoStories", new { id = input.PhotoStoryId });
        }

        public IActionResult DeletePhotoStoryComment(int Id)
        {
            var photoStory = photoStoryCommentService.PreviewPhotoStoryCommentById(Id);

            if (photoStory == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (photoStory.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return this.RedirectToAction("PreviewPhotoStory", "PhotoStories", new { id = photoStory.PhotoStoryId });
            }

            return View(photoStory);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePhotoStoryComment(PreviewPhotoStoryCommentViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            int photoStoryId = await photoStoryCommentService.DeletePhotoStoryComment(input);

            return this.RedirectToAction("PreviewPhotoStory", "PhotoStories", new { id = photoStoryId });
        }

        public IActionResult UpdatePhotoStoryComment(int id)
        {

            var photoStory = this.photoStoryCommentService.PreviewPhotoStoryCommentById(id);

            if (photoStory.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return this.RedirectToAction("PreviewPhotoStory", "PhotoStories", new { id = photoStory.PhotoStoryId });
            }

            return this.View(photoStory);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhotoStoryComment(PreviewPhotoStoryCommentViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            int photoStoryId = await photoStoryCommentService.UpdatePhotoStoryComment(input);

            return this.RedirectToAction("PreviewPhotoStory", "PhotoStories", new { id = photoStoryId });
        }
    }
}