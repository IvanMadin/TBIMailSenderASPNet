using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailManager.Data.Entities;
using EmailManager.GmailConfig;
using EmailManager.Service;
using EmailManager.Service.Contracts;
using EmailManager.Web.Extensions.Mappers;
using EmailManager.Web.Models;
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

        public async Task<IActionResult> AllEmails()
        {
            var list = (await this.emailService.GetAllEmailsAsync()).ToVM();
            Log.Information("All applications are loaded on {0}!", DateTime.UtcNow);

            return View(list);
        }

        public async Task<IActionResult> SetToNew(string emailId)
        {
            var userId = this.userManager.GetUserId(User);
            var emailDTO = await this.emailService.GetEmailByIdAsync(emailId);
            var newStatus = await this.emailStatusService.GetEmailStatusByName("New Application");
            await this.emailService.UpdateEmailStatus(emailDTO, newStatus, userId);
            await this.applicationService.CreateLoanApplicationAsync(emailDTO.Id, userId);
            return RedirectToAction("Application", new { id = emailDTO.Id });
        }
        public async Task<IActionResult> SetToOpen(string emailId)
        {
            var userId = this.userManager.GetUserId(User);
            var emailDTO = await this.emailService.GetEmailByIdAsync(emailId);
            var newStatus = await this.emailStatusService.GetEmailStatusByName("Open Application");
            await this.emailService.UpdateEmailStatus(emailDTO, newStatus, userId);
            await this.applicationService.OpenLoanApplication(emailDTO.Id, userId);
            return RedirectToAction("Application", new { id = emailDTO.Id });
        }

        public async Task<IActionResult> SetToInvalid(string emailId, string status)
        {
            var email = await this.emailService.GetEmailByIdAsync(emailId);
            await this.emailStatusService.UpdateToInvalid(email, status);

            return RedirectToAction(nameof(Application), new { id = emailId });
        }

        public async Task<IActionResult> AllEmailStatus()
        {
            var list = (await this.emailStatusService.AllEmailStatusAsync()).ToVM();

            return View(list);
        }
    }
}