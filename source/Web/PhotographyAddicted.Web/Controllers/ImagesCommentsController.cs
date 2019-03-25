using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.DataServices;
using PhotographyAddicted.Services.Models.ImagesComment;

namespace PhotographyAddicted.Web.Controllers
{
    public class ImagesCommentsController : BaseController
    {
        private readonly IImageCommentService imageCommentService;

        public ImagesCommentsController(IImageCommentService imageCommentService)
        {
            this.imageCommentService = imageCommentService;
        }

        [Authorize]
        public IActionResult AddImageComment(int Id)
        {
            var imageComment = new AddImageCommentViewModel()
            {
                ImageId = Id,
            };

            return View(imageComment);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddImageComment(AddImageCommentViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await imageCommentService.AddImageComment(input);
            return this.RedirectToAction("ViewPictureDetails", "Image", new { id = input.ImageId });
        }
    }
}