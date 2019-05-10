using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.DataServices.MessageService;
using PhotographyAddicted.Services.Models.Messages;

namespace PhotographyAddicted.Web.Controllers
{
    [Authorize]
    public class MessagesController : BaseController
    {
        private readonly IMessageService messageService;

        public MessagesController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public IActionResult AddMessage(string recepientId)
        {
            var message = new AddMessageViewModel()
            {
                RecepientId = recepientId,
                PhotographyAddictedUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),                          
            };

            return View(message);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(AddMessageViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }
            if (input.RecepientId == input.PhotographyAddictedUserId)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var conversationId = await messageService.AddMessageAsync(input);

            return this.RedirectToAction("PreviewConversation", "Conversations", new { conversationId });
        }
    }
}