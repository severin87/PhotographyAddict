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

                return RedirectToAction("ViewUserProfile", "User"); //, new { area = "" }
            }
            else
            {
                return this.View(input); // this.View(sev);
            }

        }

    }
}
