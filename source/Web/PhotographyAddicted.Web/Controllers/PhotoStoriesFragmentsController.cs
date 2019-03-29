using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.DataServices.PhotoStoryFragmentService;
using PhotographyAddicted.Services.Models.PhotoStoryFragments;

namespace PhotographyAddicted.Web.Controllers
{
    [Authorize]
    public class PhotoStoriesFragmentsController : BaseController
    {
        private readonly IPhotoStoryFragmentService photoStoryFragmentService;

        public PhotoStoriesFragmentsController(IPhotoStoryFragmentService photoStoryFragmentService)
        {
            this.photoStoryFragmentService = photoStoryFragmentService;
        }

        public IActionResult AddPhotoStoryFragment(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoStoryFragment(PreviewPhotoStoryFragmentViewModel input, IFormFile Picture)
        {
            var photoStoryId = await photoStoryFragmentService.AddPhotoStoryFragment(input, Picture);

            return RedirectToAction("PreviewPhotoStory", "PhotoStories", new { id = photoStoryId });
        }
    }
}