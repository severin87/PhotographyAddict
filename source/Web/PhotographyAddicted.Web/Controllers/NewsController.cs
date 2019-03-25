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
    public class NewsController : BaseController
    {
        private INewService newService;

        public NewsController(INewService newService)
        {
            this.newService = newService;
        }

        public IActionResult PreviewAllNews()
        {
            PreviewAllNewsViewModel allNews = new PreviewAllNewsViewModel()
            {
                PreviewAllNews = newService.PreviewAllNews(),
            };

            return View(allNews);
        }
               

        public IActionResult ViewSpecificNew(int Id)
        {
            var specificNew = newService.ViewSpecificNew(Id);

            return this.View(specificNew);
        }

        [Authorize]
        public IActionResult AddNew()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNew(AddNewViewModel input, IFormFile NewImage)
        {
            if (ModelState.IsValid)
            {
                input.PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                await newService.AddNew(input, NewImage);

                return RedirectToAction("Index", "Home"); //, new { area = "" }
            }
            else
            {
                return this.View(input); // this.View(sev);
            }

        }
    }
}