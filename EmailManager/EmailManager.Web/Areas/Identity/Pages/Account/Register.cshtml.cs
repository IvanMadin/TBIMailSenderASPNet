using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using EmailManager.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using EmailManager.Service.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace EmailManager.Web.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Manager")]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IToastNotification toast;

        public RegisterModel(
            UserManager<User> userManager,
            ILogger<RegisterModel> logger,
            IToastNotification toast)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.toast = toast;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            [StringLength(20,ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
            public string UserName { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string RoleName { get; set; }
        }

        public async Task OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var roleName = Input.RoleName;
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new User { UserName = Input.UserName, Email = Input.Email };
                var result = await userManager.CreateAsync(user, Input.Password);
                await userManager.AddToRoleAsync(user, Input.RoleName);
                if (result.Succeeded)
                {
                    logger.LogInformation("User created a new account with password.");
                    this.toast.AddSuccessToastMessage($"Account was created successfully!");
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
