using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailManager.GmailConfig;
using EmailManager.Service;
using EmailManager.Web.Mappers;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EmailManager.Web.Controllers
{
    public class EmailController : Controller
    {
        private readonly GmailConfigure gmailConfigure;
        private readonly EmailService emailService;

        public EmailController(GmailConfigure gmailConfigure, EmailService emailService)
        {
            this.gmailConfigure = gmailConfigure;
            this.emailService = emailService;
        }

        public async Task<IActionResult> BodyModal(string id)
        {
            var email = (await this.emailService.GetEmailByIdAsync(id)).ToVM();

            Log.Information("Body modal for {@email} is loaded!", email);

            return View(email);
        }

        public async Task<IActionResult> Application(string id)
        {
            var email = (await this.emailService.GetEmailByIdAsync(id)).ToVM();
            Log.Information("Application for {@email} is loaded!", email);

            return View(email);
        }

        public async Task<IActionResult> AllApplications()
        {
            var list = (await this.emailService.GetAllEmailsAsync()).ToVM();
            Log.Information("All applications are loaded!");

            return View(list);
        }
    }
}