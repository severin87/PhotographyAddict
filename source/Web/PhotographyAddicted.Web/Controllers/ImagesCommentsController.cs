using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.DataServices;
using PhotographyAddicted.Services.DataServices.ImageService;
using PhotographyAddicted.Services.Models.ImagesComments;

namespace PhotographyAddicted.Web.Controllers
{
    [Authorize]
    public class ImagesCommentsController : BaseController
    {
        private readonly IImageCommentService imageCommentService;
        private readonly IImageService imageService;

        public ImagesCommentsController(IImageCommentService imageCommentService, IImageService imageService)
        {
            this.imageCommentService = imageCommentService;
            this.imageService = imageService;
        }

        public IActionResult UpdateImageComment(int id)
        {

            var updatedImageComment = this.imageCommentService.ViewUpdateImageById(id);

            if (updatedImageComment == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (updatedImageComment.PhotographyAddictedUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier) || this.User.IsInRole("Moderator"))
            {
                return this.View(updatedImageComment);
            }

            return this.RedirectToAction("PreviewImage", "Images", new { id = updatedImageComment.ImageId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateImageComment(UpdateImageCommentViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            int imageId = await imageCommentService.UpdateImageComment(input);

            return this.RedirectToAction("PreviewImage", "Images", new { id = imageId });
        }

        public IActionResult DeleteImageComment(int Id)
        {
            var deletedImageComment = imageCommentService.FindImageCommentById(Id);

            if (deletedImageComment == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (deletedImageComment.PhotographyAddictedUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier) || this.User.IsInRole("Moderator"))
            {
                return View(deletedImageComment);
            }

            return this.RedirectToAction("PreviewImage", "Images", new { id = deletedImageComment.ImageId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImageComment(DeleteImageCommentViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            int imageId = await imageCommentService.DeleteUserImageComment(input);

            return this.RedirectToAction("PreviewImage", "Images", new { id = imageId });
        }

        public IActionResult AddImageComment(int Id)
        {
            var image = imageService.PreviewImage(Id);

            if (image == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var imageComment = new AddImageCommentViewModel()
            {
                ImageId = Id,
            };

            return View(imageComment);
        }

        [HttpPost]
        public async Task<IActionResult> AddImageComment(AddImageCommentViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await imageCommentService.AddImageComment(input);
            return this.RedirectToAction("PreviewImage", "Images", new { id = input.ImageId });
        }
    }
}