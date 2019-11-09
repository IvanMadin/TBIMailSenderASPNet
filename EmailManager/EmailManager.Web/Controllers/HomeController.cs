using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmailManager.Web.Models;
using EmailManager.GmailConfig;
using EmailManager.Service;
using EmailManager.Web.Extensions.Mappers;

namespace EmailManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly GmailConfigure gmailConfigure;
        private readonly EmailService emailService;

        public HomeController(GmailConfigure gmailConfigure, EmailService emailService)
        {
            this.gmailConfigure = gmailConfigure;
            this.emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect("~/Identity/Account/Login");
            }

            var model = (await emailService.GetAllEmailsAsync()).ToVM();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> UpdateEmails()
        {
            await gmailConfigure.GmailAPI();
            return RedirectToAction(nameof(Index));
        }
    }
}
