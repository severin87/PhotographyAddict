﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PhotographyAddicted.Services.DataServices;
using PhotographyAddicted.Web.Areas.Identity.Data;

namespace PhotographyAddicted.Web.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<PhotographyAddictedUser> _userManager;
        private readonly SignInManager<PhotographyAddictedUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private IImageCommentService imageComments;
        private IThemeCommentService themeComments;

        public DeletePersonalDataModel(
            UserManager<PhotographyAddictedUser> userManager,
            SignInManager<PhotographyAddictedUser> signInManager,
            ILogger<DeletePersonalDataModel> logger, IImageCommentService imageComments,
            IThemeCommentService themeComments)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            this.imageComments =imageComments;
            this.themeComments = themeComments;
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