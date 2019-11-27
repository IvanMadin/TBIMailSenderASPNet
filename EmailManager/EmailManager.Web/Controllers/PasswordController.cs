using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailManager.Data.Entities;
using EmailManager.Service.DTOs;
using EmailManager.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Serilog;

namespace EmailManager.Web.Controllers
{
    public class PasswordController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IToastNotification toastNotification;

        public PasswordController(UserManager<User> userManager, 
                                  SignInManager<User> signInManager, 
                                  IToastNotification toastNotification)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.toastNotification = toastNotification;
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
            user.ChangedPassword = true;
            await this.userManager.UpdateAsync(user);

            this.toastNotification.AddSuccessToastMessage("Your password has been changed.");
            Log.Information($"{DateTime.Now} User with Id: {user.Id} has changed their password successfully.");
            return RedirectToAction("Index", "Home");
        }
    }
}