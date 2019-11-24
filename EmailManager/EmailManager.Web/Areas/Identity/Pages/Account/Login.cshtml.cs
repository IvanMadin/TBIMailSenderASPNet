using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using EmailManager.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using EmailManager.Service.Contracts;

namespace EmailManager.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<LoginModel> logger;
        private readonly IUsersService usersService;

        public LoginModel(SignInManager<User> signInManager, ILogger<LoginModel> logger, IUsersService usersService)
        {
            this.signInManager = signInManager;
            this.logger = logger;
            this.usersService = usersService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await this.usersService.CheckUserCredentialsAsync(Input.UserName, Input.Password);

                    if (user.ChangedPassword == false)
                    {
                        return RedirectToActionPermanent("Change", "Password", user);
                    }
                }
                catch(ArgumentNullException ex)
                {
                    //TODO: log Exception;
                    //TODO: Show Message for the client with Toast;
                }

                var result = await signInManager.PasswordSignInAsync(Input.UserName, Input.Password, true, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            return Page();
        }
    }
}
