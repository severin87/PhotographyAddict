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

        [HttpGet]
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
                return RedirectToAction("Index", "Home");
                //return this.RedirectToAction("PreviewThemes", "Themes");
            }
            else
            {
                return View(input);
            }
        }

        [AllowAnonymous]
        public IActionResult PreviewPhotoStories(string input)
        {
            var sev = photoStoryService.PreviewPhotoStories(input);
            return View(sev);
        }

        //[HttpGet]
        //public IActionResult PreviewPhotoStory()
        //{
        //    string sev = photoStoryService.PreviewPhotoStory();
        //    return Json(sev);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddPhotoStory(AddPhotoStoryViewModel input, List<IFormFile> Picture)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //        var imageId = await photoStoryService.AddPhotoStory(input, Picture);

        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}
    }
}