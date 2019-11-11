using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailManager.GmailConfig;
using EmailManager.Service;
using EmailManager.Service.Contracts;
using EmailManager.Web.Extensions.Mappers;
using EmailManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EmailManager.Web.Controllers
{
    public class EmailController : Controller
    {
        private readonly GmailConfigure gmailConfigure;
        private readonly IEmailService emailService;
        private readonly IEmailStatusService emailStatusService;
        public EmailController(GmailConfigure gmailConfigure, IEmailService emailService, IEmailStatusService emailStatusService)
        {
            this.gmailConfigure = gmailConfigure;
            this.emailService = emailService;
            this.emailStatusService = emailStatusService;
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

        public async Task<IActionResult> AllApplications()
        {
            var list = (await this.emailService.GetAllEmailsAsync()).ToVM();
            Log.Information("All applications are loaded on {0}!", DateTime.UtcNow);

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> EmailStatus(string emailId, string emailStatusId)
        {
            var email = (await this.emailService.GetEmailByIdAsync(emailId)).ToVM();
            var emailStatus = await this.emailStatusService.GetEmailStatusByIdAsync(emailStatusId);

            return View();
        }

        public async Task<IActionResult> UpdateStatusEmail(string emailId, string status)
        {
            //var allStatus = await this.emailStatusService.AllEmailStatusAsync();

            var email = await this.emailService.GetEmailByIdAsync(emailId);
            await this.emailStatusService.UpdateEmailStatusAsync(email, status);

            return View(email);
        }

        public async Task<IActionResult> AllEmailStatus()
        {
            var list = (await this.emailStatusService.AllEmailStatusAsync()).ToVM();

            return View(list);
        }
    }
}