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

            if (updatedNew.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return this.RedirectToAction("PreviewNew", new { id = updatedNew.Id });
            }

            return this.View(updatedNew);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNew(PreviewNewViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            await newService.UpdateNew(input);

            return this.RedirectToAction("PreviewNews", "News");
        }

        [Authorize]
        public IActionResult DeleteNew(int Id)
        {
            var deletedNew = newService.FindNewBy(Id);

            if (deletedNew == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (deletedNew.PhotographyAddictedUserId != this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return this.RedirectToAction("PreviewNews", "News");
            }

            return View(deletedNew);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteNew(PreviewNewViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

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
                await newService.AddNew(input, NewImage);

                return this.RedirectToAction("PreviewNews", "News");
            }
            else
            {
                return this.View(input);
            }
        }
    }
}