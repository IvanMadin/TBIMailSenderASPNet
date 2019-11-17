using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmailManager.Web.Models;
using EmailManager.GmailConfig;
using EmailManager.Service;
using Serilog;
using EmailManager.Web.Extensions.Mappers;
using EmailManager.Service.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace EmailManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly GmailConfigure gmailConfigure;

        public HomeController(GmailConfigure gmailConfigure)
        {
            this.gmailConfigure = gmailConfigure;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("AllEmails", "Email");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> UpdateEmails()
        {
            Log.Information("All emails are updated on {0}!", DateTime.UtcNow);
            await gmailConfigure.GmailAPI();
            return RedirectToAction("AllEmails", "Email");
        }
    }
}
