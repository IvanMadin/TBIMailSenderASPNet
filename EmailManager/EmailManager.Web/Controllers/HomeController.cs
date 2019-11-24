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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmailManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly GmailConfigure gmailConfigure;
        private readonly IRolesService rolesService;

        public HomeController(GmailConfigure gmailConfigure, IRolesService rolesService)
        {
            this.gmailConfigure = gmailConfigure;
            this.rolesService = rolesService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var rolesVM = (await this.rolesService.GetRolesAsync());
            SelectList selectList = new SelectList(rolesVM, "Id", "Name");

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
            await gmailConfigure.GmailAPI();

            Log.Information($"{DateTime.Now} Update Emails by {User}.");
            return RedirectToAction("AllEmails", "Email");
        }
    }
}
