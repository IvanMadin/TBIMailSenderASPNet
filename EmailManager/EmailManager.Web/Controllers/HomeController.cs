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
using NToastNotify;
using Microsoft.AspNetCore.SignalR;
using EmailManager.Web.SignalR;

namespace EmailManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly GmailConfigure gmailConfigure;
        private readonly IToastNotification toast;
        private readonly IHubContext<TestHub> hubContext;

        public HomeController(GmailConfigure gmailConfigure, IToastNotification toast, IHubContext<TestHub> hubContext)
        {
            this.gmailConfigure = gmailConfigure;
            this.toast = toast;
            this.hubContext = hubContext;
        }

        [Authorize]
        public IActionResult Index()
        {
            //this.hubContext.Clients.All.SendAsync("Testing","zdravei");
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
            try
            {
                await gmailConfigure.GmailAPI();

                this.toast.AddSuccessToastMessage($"Emails were updated successfully!");
                Log.Information($"{DateTime.Now} Update Emails by {User}.");
                return RedirectToAction("AllEmails", "Email");
            }
            catch
            {
                this.toast.AddWarningToastMessage("Oops... Something went wrong.");
                Log.Error($"Emails weren't updated!");
                return RedirectToAction("Error", "Home");
            }
            
        }
    }
}
