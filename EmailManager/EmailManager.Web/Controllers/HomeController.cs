using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmailManager.Web.Models;
using EmailManager.GmailConfig;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using NToastNotify;
using EmailManager.Service.Contracts;

namespace EmailManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly GmailConfigure gmailConfigure;
        private readonly IToastNotification toast;
        private readonly IEGNChecker eGNChecker;

        public HomeController(GmailConfigure gmailConfigure, IToastNotification toast, IEGNChecker eGNChecker)
        {
            this.gmailConfigure = gmailConfigure;
            this.toast = toast;
            this.eGNChecker = eGNChecker;
        }

        [Authorize]
        public IActionResult Index()
        {
            return RedirectToAction("AllEmails", "Email");
        }


        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 3600)]
        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<bool> CheckEGN(string egn)
        {
            bool result = await this.eGNChecker.IsRealAsync(egn);

            return result;
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
            catch (Exception ex)
            {
                this.toast.AddWarningToastMessage("Oops... Something went wrong. Please, call your system administrator.");
                Log.Error($"{DateTime.Now} {ex.Message}");
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
