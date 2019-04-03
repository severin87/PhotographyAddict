﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PhotographyAddicted.Services.DataServices;
using PhotographyAddicted.Services.DataServices.ConversationService;
using PhotographyAddicted.Services.DataServices.MessageService;
using PhotographyAddicted.Services.DataServices.PhotoStoryCommentService;
using PhotographyAddicted.Web.Areas.Identity.Data;

namespace PhotographyAddicted.Web.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<PhotographyAddictedUser> _userManager;
        private readonly SignInManager<PhotographyAddictedUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly IImageCommentService imageComments;
        private readonly IThemeCommentService themeComments;
        private readonly INewCommentService newComments;
        private readonly IPhotoStoryCommentService photoStoryComment;
        private readonly IConversationService conversationService;
        private readonly IMessageService messageService;

        public DeletePersonalDataModel(
            UserManager<PhotographyAddictedUser> userManager,
            SignInManager<PhotographyAddictedUser> signInManager,
            ILogger<DeletePersonalDataModel> logger, IImageCommentService imageComments,
            IThemeCommentService themeComments, INewCommentService newComments, IPhotoStoryCommentService photoStoryComment,
            IConversationService conversationService, IMessageService messageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            this.imageComments =imageComments;
            this.themeComments = themeComments;
            this.newComments = newComments;
            this.photoStoryComment = photoStoryComment;
            this.conversationService = conversationService;
            this.messageService = messageService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Password not correct.");
                    return Page();
                }
            }

            await imageComments.DeleteUserImagesComments(user.Id);
            await themeComments.DeleteUserThemesComments(user.Id);
            await newComments.DeleteUserNewComments(user.Id);
            await photoStoryComment.DeleteUserPhotoStoryComments(user.Id);
            await messageService.DeleteUserMessages(user.Id);
            await conversationService.DeleteUserConversations(user.Id);

            var result = await _userManager.DeleteAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleteing user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }
    }
}