using System;
using System.Threading.Tasks;
using EmailManager.Data.Entities;
using EmailManager.Service.Contracts;
using EmailManager.Web.Extensions.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUsersService usersService;
        private readonly IAttachmentsService attachmentsService;

        public EmailController(UserManager<User> userManager,
            IEmailService emailService,
            IEmailStatusService emailStatusService,
            ILoanApplicationService applicationService,
            IUsersService usersService,
            IAttachmentsService attachmentsService)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.emailStatusService = emailStatusService;
            this.applicationService = applicationService;
            this.usersService = usersService;
            this.attachmentsService = attachmentsService;
        }

        public async Task<IActionResult> BodyModal(string id)
        {
            var email = (await this.emailService.GetEmailByIdAsync(id)).ToVM();
            Log.Information($"{DateTime.Now} Body modal with email ID: {email.Id} was accessible by {email.ModifiedByUserName}.");

            return View(email);
        }

        public async Task<IActionResult> Application(string id)
        {
            var email = (await this.emailService.GetEmailByIdAsync(id)).ToVM();
            Log.Information($"{DateTime.Now} Application with emailID: {email.Id} has been accessed by {email.ModifiedByUserName}.");
            email.Attachments = (await this.attachmentsService.GetEmailAttachmentsByEmailIdAsync(email.Id)).ToVM();

            return View(email);
        }

        /// <summary>
        /// List All Emails.
        /// </summary>
        public async Task<IActionResult> AllEmails()
        {
            var list = (await this.emailService.GetAllEmailsAsync()).ToVM();
            foreach (var email in list)
            {
                email.Attachments = (await this.attachmentsService.GetEmailAttachmentsByEmailIdAsync(email.Id)).ToVM();
            }
            Log.Information($"{DateTime.Now} All emails has been accessed by {User}.");

            return View(list);
        }

        public async Task<IActionResult> ShowEmailsByStatus(string statusName)
        {
            try
            {
                var list = (await this.emailService.GetAllEmailsByStatusNameAsync(statusName)).ToVM();
                foreach (var email in list)
                {
                    var userName = (await this.usersService.GetUserByIdAsync(email.ModifiedByUserId)).UserName;
                    email.ModifiedByUserName = userName;
                }
                

                Log.Information($"{DateTime.Now} Show Emails with Status: {statusName} by User Id: {User}.");

                return View("AllEmails", list);
            }
            catch (ArgumentNullException ex)
            {
                Log.Information(ex.Message);
                //TODO: Have to make a custom User Message.
            }
            return LocalRedirect("~");
        }

        /// <summary>
        /// Changing status with the given StatusName.
        /// </summary>
        public async Task<IActionResult> ChangeStatus(string emailId, string newStatusName)
        {
            var userId = this.userManager.GetUserId(User);
            var emailDTO = await this.emailService.GetEmailByIdAsync(emailId);
            var statusToSet = await this.emailStatusService.GetEmailStatusByNameAsync(newStatusName);
            await this.emailService.UpdateEmailStatus(emailDTO, statusToSet, userId);

            if (newStatusName == "New Application")
            {
                await this.applicationService.CreateLoanApplicationAsync(emailDTO.Id, userId);
                Log.Information($"{DateTime.Now} Create Loan Application by User Id: {userId} with Status: {newStatusName}.");
            }
            else if (newStatusName == "Open Application")
            {
                await this.applicationService.OpenLoanApplication(emailDTO.Id, userId);
                Log.Information($"{emailDTO.ModifiedOnDate} Open Loan Application by User Id: {userId}.");
            }

            Log.Information($"{emailDTO.ModifiedOnDate} Changed Status by User Id: {userId}, from: {emailDTO.EmailStatusName} to {newStatusName}.");
            return RedirectToAction("Application", new { id = emailDTO.Id });
        }
    }
}