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