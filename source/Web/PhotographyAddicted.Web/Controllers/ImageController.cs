﻿using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ImageController : BaseController
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
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

            if (deletedImage.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier) || deletedImage.PhotographyAddictedUserId == null)
            {
                return this.RedirectToAction("PreviewImages", "Image", new { id = this.User.FindFirstValue(ClaimTypes.NameIdentifier) });
            }

            return View(deletedImage);
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
            return this.RedirectToAction("PreviewImages", "Image", new { id = input.PhotographyAddictedUserId });
        }

        [AllowAnonymous]
        public IActionResult PreviewUserImages(string Id)
        {
            PreviewImagesViewModel userPictures = new PreviewImagesViewModel
            {
                PreviewImages = imageService.GetImagesByUser(Id)
            };

            return View(userPictures);
        }
        
        [AllowAnonymous]
        public IActionResult PreviewImage(int id)
        {
            var userPictures = imageService.PreviewImage(id);

            return View(userPictures);
        }

        [Authorize]
        public IActionResult UpdateImage(int id)
        {
            var userPictures = imageService.FindImageById(id);

            if (userPictures.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return this.RedirectToAction("PreviewImage", new { id = userPictures.Id });
            }            

            return this.View(userPictures);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateImage(PreviewImageViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            int imageId = await imageService.UpdateImage(input);

            return this.RedirectToAction("PreviewImage", new { id = imageId });
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
                input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var imageId = await imageService.AddImage(input, Picture);

                return RedirectToAction("PreviewUser", "User", new { Id = input.PhotographyAddictedUserId }); //, new { area = "" }
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
    }
}
