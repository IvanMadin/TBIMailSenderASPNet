using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailManager.Data.Entities;
using EmailManager.Service.Contracts;
using EmailManager.Web.Extensions.Mappers;
using EmailManager.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Serilog;

namespace EmailManager.Web.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;
        private readonly IEmailStatusService emailStatusService;
        private readonly ILoanApplicationService applicationService;
        private readonly IAttachmentsService attachmentsService;
        private readonly IToastNotification toast;

        public EmailController(UserManager<User> userManager,
            IEmailService emailService,
            IEmailStatusService emailStatusService,
            ILoanApplicationService applicationService,
            IAttachmentsService attachmentsService,
            IToastNotification toast)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.emailStatusService = emailStatusService;
            this.applicationService = applicationService;
            this.attachmentsService = attachmentsService;
            this.toast = toast;
        }

        public async Task<IActionResult> BodyModal(string id)
        {
            try
            {
                var email = (await this.emailService.GetEmailByIdAsync(id)).ToVM();
                Log.Information($"{DateTime.Now} Body modal with email ID: {email.Id} was accessible by {email.ModifiedByUserName}.");

                return View(email);
            }
            catch (Exception ex)
            {
                this.toast.AddWarningToastMessage("Oops... Something went wrong.");
                Log.Error($"[{DateTime.Now}]: {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Application(string id)
        {
            try
            {
                var email = (await this.emailService.GetEmailByIdAsync(id)).ToVM();
                Log.Information($"{DateTime.Now} Application with emailID: {email.Id} has been accessed by {email.ModifiedByUserName}.");
                email.Attachments = (await this.attachmentsService.GetEmailAttachmentsByEmailIdAsync(email.Id)).ToVM();
                return View(email);
            }
            catch (Exception ex)
            {
                this.toast.AddWarningToastMessage("Oops... Something went wrong.");
                Log.Error($"Application wasn't accessible");
                return RedirectToAction("Error", "Home");
            }
        }

        /// <summary>
        /// List All Emails.
        /// </summary>
        public async Task<IActionResult> AllEmails()
        {
            try
            {
                var list = (await this.emailService.GetAllEmailsAsync()).ToVM();
                foreach (var email in list)
                {
                    email.Attachments = (await this.attachmentsService.GetEmailAttachmentsByEmailIdAsync(email.Id)).ToVM();
                }
                Log.Information($"{DateTime.Now} All emails has been accessed by {User}.");

                return View(list);
            }
            catch (Exception ex)
            {
                this.toast.AddWarningToastMessage("Oops... Something went wrong.");
                Log.Error($"All emails weren't accessible");
                return RedirectToAction("Error", "Home");
            }
        }


        /// <summary>
        /// Changing status with the given StatusName.
        /// </summary>
        public async Task<IActionResult> ChangeStatus(string emailId, string newStatusName)
        {
            try
            {
                var userId = this.userManager.GetUserId(User);
                var emailDTO = await this.emailService.GetEmailByIdAsync(emailId);
                var statusToSet = await this.emailStatusService.GetEmailStatusByNameAsync(newStatusName);
                if (!newStatusName.StartsWith("New"))
                {
                    await this.emailService.UpdateEmailStatus(emailDTO, statusToSet, userId);
                }

                if (newStatusName == "New Application")
                {
                    await this.emailService.UpdateEmailStatus(emailDTO, newStatusName);

                    await this.applicationService.CreateLoanApplicationAsync(emailDTO.Id);

                    Log.Information($"{DateTime.Now} Created Loan Application by User Id: {userId} with Status: {newStatusName}.");
                }
                else if (newStatusName == "Open Application")
                {
                    await this.applicationService.OpenLoanApplicationAsync(emailDTO.Id, userId);

                    Log.Information($"{emailDTO.ModifiedOnDate} Open Loan Application by User Id: {userId}.");
                }

                this.toast.AddSuccessToastMessage($"Status was changed successfully!");
                Log.Information($"{emailDTO.ModifiedOnDate} Changed Status by User Id: {userId}, from: {emailDTO.EmailStatusName} to {newStatusName}.");
                return RedirectToAction("Application", new { id = emailDTO.Id });
            }
            catch (Exception ex)
            {
                this.toast.AddWarningToastMessage("Oops... Something went wrong.");
                Log.Error($"Status wasn't changed!");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}