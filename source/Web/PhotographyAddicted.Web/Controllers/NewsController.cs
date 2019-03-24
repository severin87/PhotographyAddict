using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        public IActionResult AddNew()
        {
            return View();
        }

        public IActionResult ViewSpecificNew(int Id)
        {
            var specificNew = newService.ViewSpecificNew(Id);

            return this.View(specificNew);
        }

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