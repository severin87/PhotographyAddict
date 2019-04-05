using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            var photoFragment = new AddPhotoStoryFragmentViewModel()
            {
                PhotoStoryId = id,
            };

            return View(photoFragment);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoStoryFragment(PreviewPhotoStoryFragmentViewModel input, IFormFile Picture)
        {
            var photoStoryId = await photoStoryFragmentService.AddPhotoStoryFragment(input, Picture);

            return RedirectToAction("PreviewPhotoStory", "PhotoStories", new { id = photoStoryId });
        }

        public IActionResult UpdatePhotoStoryFragment(int id)
        {
            var photoFragment = photoStoryFragmentService.FindPhotoStoryFragmenById(id);

            if (photoFragment.Picture == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if ((photoFragment.PhotoStory.PhotographyAddictedUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier)) 
                || photoFragment.PhotoStory.PhotographyAddictedUserId != null || this.User.IsInRole("Moderator"))
            {
                return View("UpdatePhotoStoryFragment", photoFragment);
            }

            return this.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhotoStoryFragment(PreviewPhotoStoryFragmentViewModel input)
        {
            var photoStoryId = await photoStoryFragmentService.UpdatePhotoStoryFragment(input);

            return RedirectToAction("PreviewPhotoStory", "PhotoStories", new { id = photoStoryId });
        }

        public IActionResult DeletePhotoStoryFragment(int Id)
        {
            var photoFragment = photoStoryFragmentService.FindPhotoStoryFragmenById(Id);

            if (photoFragment == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if ((photoFragment.PhotoStory.PhotographyAddictedUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier))
                || photoFragment.PhotoStory.PhotographyAddictedUserId != null  || this.User.IsInRole("Moderator"))
            {
                return View(photoFragment);
            }

            return this.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePhotoStoryFragment(PreviewPhotoStoryFragmentViewModel input)
        {
            var photoStoryId = await photoStoryFragmentService.DeletePhotoStoryFragment(input);

            return RedirectToAction("PreviewPhotoStory", "PhotoStories", new { id = photoStoryId });
        }
    }
}