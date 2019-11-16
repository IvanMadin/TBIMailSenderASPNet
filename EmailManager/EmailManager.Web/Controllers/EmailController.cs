using System;
using System.Threading.Tasks;
using EmailManager.Data.Entities;
using EmailManager.Service.Contracts;
using EmailManager.Web.Extensions.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EmailManager.Web.Controllers
{
    public class EmailController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;
        private readonly IEmailStatusService emailStatusService;
        private readonly ILoanApplicationService applicationService;

        public EmailController(UserManager<User> userManager,
            IEmailService emailService,
            IEmailStatusService emailStatusService,
            ILoanApplicationService applicationService)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.emailStatusService = emailStatusService;
            this.applicationService = applicationService;
        }

        public async Task<IActionResult> BodyModal(string id)
        {
            var email = (await this.emailService.GetEmailByIdAsync(id)).ToVM();

            Log.Information("Body modal for email with ID: {0} was loaded on {1}!", email.Id, DateTime.UtcNow);

            return View(email);
        }

        public async Task<IActionResult> Application(string id)
        {
            var email = (await this.emailService.GetEmailByIdAsync(id)).ToVM();
            Log.Information("Application for email with ID: {0} was loaded on {1}!", email.Id, DateTime.UtcNow);

            return View(email);
        }

        /// <summary>
        /// List All Emails.
        /// </summary>
        public async Task<IActionResult> AllEmails()
        {
            var list = (await this.emailService.GetAllEmailsAsync()).ToVM();
            Log.Information("All applications are loaded on {0}!", DateTime.UtcNow);

            return View(list);
        }

        public async Task<IActionResult> ShowEmailsByStatus(string statusName)
        {
            try
            {
                var list = (await this.emailService.GetAllEmailsByStatusNameAsync(statusName)).ToVM();
                Log.Information($"All emails with status name: {statusName} are loaded on {DateTime.UtcNow}!");

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
            }
            else if (newStatusName == "Open Application")
            {
                await this.applicationService.OpenLoanApplication(emailDTO.Id, userId);
            }

            return RedirectToAction("Application", new { id = emailDTO.Id });
        }
    }
}