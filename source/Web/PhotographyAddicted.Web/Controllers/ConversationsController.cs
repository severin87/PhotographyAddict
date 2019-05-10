using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographyAddicted.Services.DataServices.ConversationService;

namespace PhotographyAddicted.Web.Controllers
{
    [Authorize]
    public class ConversationsController : BaseController
    {
        private readonly IConversationService conversationService;

        public ConversationsController(IConversationService conversationService)
        {
            this.conversationService = conversationService;
        }

        public IActionResult PreviewUserConversations(string userId)
        {
            if (userId == this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                var userConversations = conversationService.PreviewUsersConversations(userId);

                return View(userConversations);
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }           
        }

        public async Task<IActionResult> PreviewConversation(int conversationId)
        {
            var userConversation = await conversationService.PreviewConversation(conversationId, this.User.FindFirstValue(ClaimTypes.NameIdentifier));            

            if ((userConversation.RecepientPhotographyAddictedUser.Id == this.User.FindFirstValue(ClaimTypes.NameIdentifier))
                || (userConversation.SenderPhotographyAddictedUser.Id == this.User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                return View(userConversation);
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }
    }
}