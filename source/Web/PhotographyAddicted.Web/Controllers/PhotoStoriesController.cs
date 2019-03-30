using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.DataServices.PhotoStoryService;
using PhotographyAddicted.Services.Models.PhotoStorys;

namespace PhotographyAddicted.Web.Controllers
{
    [Authorize]
    public class PhotoStoriesController : BaseController
    {
        private readonly IPhotoStoryService photoStoryService;

        public PhotoStoriesController(IPhotoStoryService photoStoryService)
        {
            this.photoStoryService = photoStoryService;
        }

        public IActionResult DeletePhotoStory(int id)
        {
            var photoStory = photoStoryService.FindPhotoStoryById(id);

            if (photoStory == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if ((photoStory.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
                || photoStory.PhotographyAddictedUserId == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return View(photoStory);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePhotoStory(PreviewPhotoStoryViewModel input)
        {
            await photoStoryService.DeletePreviewPhotoStory(input);

            return RedirectToAction("PreviewPhotoStories", "PhotoStories");
        }

        public IActionResult UpdatePhotoStory(int id)
        {
            var userPhotoStory = photoStoryService.FindPhotoStoryById(id);

            if (userPhotoStory.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return this.RedirectToAction("PreviewImage", new { id = userPhotoStory.Id });
            }

            return this.View(userPhotoStory);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhotoStory(PreviewPhotoStoryViewModel input)
        {
            if (ModelState.IsValid)
            {
                input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var photoStoryId = await photoStoryService.UpdatePreviewPhotoStory(input);
                return RedirectToAction("PreviewPhotoStory", "PhotoStories", new { id = photoStoryId });
            }
            else
            {
                return View(input);
            }
        }

        public IActionResult AddPhotoStory()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddPhotoStory(AddPhotoStoryViewModel input)
        {
            if (ModelState.IsValid)
            {
                input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var photoStoryId = await photoStoryService.AddPhotoStory(input);
                return RedirectToAction("PreviewPhotoStory", "PhotoStories", new {id =photoStoryId });
            }
            else
            {
                return View(input);
            }
        }

        [AllowAnonymous]
        public IActionResult PreviewPhotoStories(string input)
        {
            var photoStories = photoStoryService.PreviewPhotoStories(input);
            return View(photoStories);
        }

        [AllowAnonymous]
        public IActionResult PreviewPhotoStory(int id)
        {
            var photoStories = photoStoryService.PreviewPhotoStory(id);
            return View(photoStories);
        }

        public async Task<IActionResult> ChangeStatus(int id)
        {
            await photoStoryService.ChangeStatus(id);
            return RedirectToAction("PreviewPhotoStory", new { Id = id });
        }
    }
}