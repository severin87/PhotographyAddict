using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.DataServices;
using PhotographyAddicted.Services.Models.Images;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhotographyAddicted.Web.Controllers
{
    public class ImageController : BaseController
    {

        private readonly IRepository<Image> imageInfo;

        private readonly IImageService imageService;

        public ImageController(IImageService imageService , IRepository<Image> imageInfo)
        {
            this.imageService = imageService;
            this.imageInfo = imageInfo;
        }

        [Authorize]
        public IActionResult DeleteImage(int Id)
        {
            var deletedImage = imageService.FindDeletingImageById(Id);

            if (deletedImage.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return this.RedirectToAction("ViewUsersPictures", "Image", new { id = deletedImage.PhotographyAddictedUserId });
            }

            return View(deletedImage);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteImage(DeleteImageViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            await imageService.DeleteImage(input);
            input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return this.RedirectToAction("ViewUsersPictures", "Image", new { id = input.PhotographyAddictedUserId });
        }

        

        public IActionResult ViewUsersPictures(string Id)
        {
            PreviewUsersImages userPictures = new PreviewUsersImages
            {
                PreviewImages = imageService.GetImagesByUser(Id)
            };

            return View(userPictures);
        }

        public IActionResult ViewPictureDetails(int id)
        {
            var userPictures = imageService.GetImageById(id);

            return View(userPictures);
        }

        [Authorize]
        public IActionResult EditPictureInfo(int id)
        {
            var userPictures = imageService.GetImageById(id);

            if (userPictures.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return this.RedirectToAction("ViewPictureDetails", new { id = userPictures.Id });
            }            

            return this.View(userPictures);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditPictureInfo(ImagePreviewViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            int imageId = await imageService.UpdateImage(input);

            return this.RedirectToAction("ViewPictureDetails", new { id = imageId });
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddImage()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddImage(AddImageViewModel input, IFormFile Picture)
        {
            if (ModelState.IsValid)
            {
                    if (Picture.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await Picture.CopyToAsync(stream).ConfigureAwait(false);
                            input.Picture = stream.ToArray();
                        }
                    }

                input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var imageId = await imageService.AddImage(input);

                return RedirectToAction("ViewUserProfile", "User", new { Id = input.PhotographyAddictedUserId }); //, new { area = "" }
            }
            else
            {
                return this.View(input); // this.View(sev);
            }

        }

    }
}
