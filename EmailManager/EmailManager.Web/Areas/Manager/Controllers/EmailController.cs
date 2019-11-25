using System;
using System.Linq;
using System.Threading.Tasks;
using EmailManager.Data.Entities;
using EmailManager.Service.Contracts;
using EmailManager.Web.Extensions.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Serilog;

namespace EmailManager.Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class EmailController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;
        private readonly IUsersService usersService;
        private readonly ILoanApplicationService applicationService;
        private readonly IToastNotification toast;

        public EmailController(UserManager<User> userManager,
            IEmailService emailService,
            IUsersService usersService,
            ILoanApplicationService applicationService,
            IToastNotification toast)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.usersService = usersService;
            this.applicationService = applicationService;
            this.toast = toast;
        }
        public async Task<IActionResult> ShowEmailsByStatus(string statusName)
        {
            try
            {
                var user = await this.userManager.GetUserAsync(User);
                var list = (await this.emailService.GetAllEmailsByStatusNameAsync(statusName)).ToVM();
                if (statusName.StartsWith("Open") || statusName.StartsWith("Closed"))
                {

                    foreach (var email in list)
                    {
                        var userName = (await this.usersService.GetUserByIdAsync(email.ModifiedByUserId)).UserName;
                        email.ModifiedByUserName = userName;
                    }
                }

                Log.Information($"{DateTime.Now} Show Emails with Status: {statusName} asked by Manager with Id: {user.Id}.");

                return View("AllEmails", list);
            }
            catch (ArgumentNullException ex)
            {
                this.toast.AddWarningToastMessage("Oops... Something went wrong.");
                Log.Error(ex.Message);
            }
            return LocalRedirect("~");
        }

        public async Task<IActionResult> ReturnStatusFromClosedToNew(string emailId, string newStatusName)
        {
            try
            {
                var userId = this.userManager.GetUserId(User);
                var emailDTO = await this.emailService.GetEmailByIdAsync(emailId);

                await this.applicationService.RemoveOldApplicationByEmailIdAsync(emailDTO.Id, userId);
                if (newStatusName.StartsWith("New"))
                {
                    var newApplication = await this.applicationService.CreateLoanApplicationAsync(emailDTO.Id);
                }
                await this.emailService.UpdateEmailStatus(emailDTO, newStatusName);

                this.toast.AddSuccessToastMessage($"Status was changed successfully!");
                return RedirectToAction("Application", new { id = emailDTO.Id });
            }
            catch
            {
                this.toast.AddWarningToastMessage("Oops... Something went wrong.");
                Log.Error($"{DateTime.Now} Status wasn't changed!");
                return RedirectToAction("Index", "Home");
            }
        }
    }
}