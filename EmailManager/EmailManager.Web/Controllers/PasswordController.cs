using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailManager.Data.Entities;
using EmailManager.Service.DTOs;
using EmailManager.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmailManager.Web.Controllers
{
    public class PasswordController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public PasswordController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Change(UserDTO userDTO)
        {
            var userVM = new UserChangePassViewModel
            {
                Id = userDTO.Id,
                UserName = userDTO.UserName,
                OldPassword = userDTO.Password
            };

            return View(userVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Change(UserChangePassViewModel userChangeVM)
        {
            var user = await this.userManager.FindByNameAsync(userChangeVM.UserName);
            var changePasswordResult = await this.userManager.ChangePasswordAsync(user, userChangeVM.OldPassword, userChangeVM.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(userChangeVM);
            }

            await signInManager.RefreshSignInAsync(user);
            //logger.LogInformation("User changed their password successfully."); -> SeriLog
            //StatusMessage = "Your password has been changed."; -> Toaster
            user.ChangedPassword = true;
            await this.userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Home");
        }
    }
}