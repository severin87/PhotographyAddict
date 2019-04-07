using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.DataServices;
using PhotographyAddicted.Services.DataServices.ImageService;
using PhotographyAddicted.Services.Models.Images;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhotographyAddicted.Web.Controllers
{
    [Authorize]
    public class ImagesController : BaseController
    {
        private readonly IImageService imageService;

        public ImagesController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        public IActionResult DeleteImage(int Id)
        {
            var deletedImage = imageService.FindImageById(Id);

            if (deletedImage == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (deletedImage.PhotographyAddictedUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier) 
                || deletedImage.PhotographyAddictedUserId != null || this.User.IsInRole("Moderator"))
            {
                return View(deletedImage);
            }

            return this.RedirectToAction("PreviewImages", "Images", new { id = this.User.FindFirstValue(ClaimTypes.NameIdentifier) });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(PreviewImageViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            await imageService.DeleteImage(input);
            input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return this.RedirectToAction("PreviewUser", "Users", new { id = input.PhotographyAddictedUserId });
        }

        [AllowAnonymous]
        public IActionResult PreviewUserImages(string Id)
        {
            var userPictures = imageService.PreviewUserImages(Id);

            if (userPictures.PreviewImages.Count() == 0)
            {
                return this.RedirectToAction("PreviewUser", "Users", new { id = Id });
                // <a asp-controller="Users" asp-action="PreviewUser" asp-route-id="@user.Id">Go to profile!!!</a>
            }

            return View("PreviewImages", userPictures);
        }
        
        [AllowAnonymous]
        public IActionResult PreviewImage(int id)
        {
            var userPictures = imageService.PreviewImage(id);

            return View(userPictures);
        }
        
        public IActionResult UpdateImage(int id)
        {
            var userPictures = imageService.FindImageById(id);

            if (userPictures.PhotographyAddictedUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier) || this.User.IsInRole("Moderator"))
            {
                return this.View(userPictures);
            }

            return this.RedirectToAction("PreviewImage", new { id = userPictures.Id });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateImage(PreviewImageViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            int imageId = await imageService.UpdateImage(input);

            return this.RedirectToAction("PreviewImage", new { id = imageId });
        }
        
        [HttpGet]
        public IActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(AddImageViewModel input, IFormFile Picture)
        {
            if (ModelState.IsValid)
            {
                input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var imageId = await imageService.AddImage(input, Picture);

                return RedirectToAction("PreviewUser", "Users", new { Id = input.PhotographyAddictedUserId });
            }
            else
            {
                return this.View(input);
            }
        }

        [AllowAnonymous]
        public IActionResult PreviewImages(string input)
        {
            var images = imageService.PreviewImages(input);         

            return this.View(images);
        }
                
        [AllowAnonymous]
        public IActionResult PreviewImageByCategoryAndDate()
        {
            var images = imageService.PreviewImagesByCategoriesAndDates();

            return View("PreviewImages",images);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult PreviewCategoryImages(int id)
        {
            var images = imageService.PreviewCategoryImages(id);

            return View("PreviewImages", images);
        }

        [AllowAnonymous]
        public IActionResult PreviewTopImagesLasтThirtyDaysByCategory()
        {
            var images = imageService.PreviewTopImagesLasтThirtyDaysByCategory();

            return View("PreviewImages", images);
        }

        [AllowAnonymous]
        public IActionResult PreviewTopImagesLasтThirtyDays(int id)
        {
            var images = imageService.PreviewTopImagesLasтThirtyDays(id);

            return View("PreviewImages", images);
        }

        public async  Task<IActionResult> AddImageToFavourites(string userId, int imageId)
        {
            await imageService.AddImageToFavourites(userId, imageId);

            return this.RedirectToAction("PreviewImage", new { id = imageId });
        }

        public async Task<IActionResult> DeleteImageToFavourites(string userId, int imageId)
        {
            await imageService.DeleteImageToFavourites(userId, imageId);

            return this.RedirectToAction("PreviewImage", new { id = imageId });
        }
    }
}
