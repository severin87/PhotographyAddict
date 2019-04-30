using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.DataServices;
using PhotographyAddicted.Services.Models.News;

namespace PhotographyAddicted.Web.Controllers
{
    [Authorize]
    public class NewsController : BaseController
    {
        private INewService newService;

        public NewsController(INewService newService)
        {
            this.newService = newService;
        }

        public IActionResult UpdateNew(int id)
        {
            var updatedNew = this.newService.FindNewBy(id);

            if (updatedNew == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (updatedNew.PhotographyAddictedUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier) || this.User.IsInRole("Moderator"))
            {
                return this.View(updatedNew);
            }

            return this.RedirectToAction("PreviewNew", new { id = updatedNew.Id });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNew(PreviewNewViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            await newService.UpdateNew(input);

            return this.RedirectToAction("PreviewNew", new { id = input.Id });
        }

        public IActionResult DeleteNew(int Id)
        {
            var deletedNew = newService.FindNewBy(Id);

            if (deletedNew == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (deletedNew.PhotographyAddictedUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier) || this.User.IsInRole("Moderator"))
            {
                return View(deletedNew);
            }

            return this.RedirectToAction("PreviewNews", "News");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNew(PreviewNewViewModel input)
        {
            await newService.DeleteNew(input);

            return this.RedirectToAction("PreviewNews", "News");
        }

        [AllowAnonymous]
        public IActionResult PreviewNews(string input)
        {
            var allNews = newService.PreviewNews(input);
            return View(allNews);
        }

        [AllowAnonymous]
        public IActionResult PreviewNew(int Id)
        {
            var specificNew = newService.PreviewNew(Id);

            if (specificNew == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(specificNew);
        }
        
        public IActionResult AddNew()
        {
            return View();
        }

        [HttpPost]           
        public async Task<IActionResult> AddNew(AddNewViewModel input, IFormFile NewImage)
        {
            if (ModelState.IsValid)
            {
                input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);                

                var newId = await newService.AddNew(input, NewImage);
                if (newId == 0)
                {
                    ModelState.AddModelError(string.Empty, "Picture is bigger than 256kb");
                    return this.View(input);
                }

                return this.RedirectToAction("PreviewNews", "News");
            }
            else
            {
                return this.View(input);
            }
        }

        [AllowAnonymous]
        public IActionResult PreviewNewsSearch(string input)
        {
            return RedirectToAction("PreviewNews", "News", new { input });
        }
    }
}