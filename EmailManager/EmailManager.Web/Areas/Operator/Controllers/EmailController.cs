using System;
using System.Linq;
using System.Threading.Tasks;
using EmailManager.Data.Entities;
using EmailManager.Service.Contracts;
using EmailManager.Web.Extensions.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Serilog;

namespace EmailManager.Web.Areas.Operator.Controllers
{
    [Area("Operator")]
    [Authorize(Roles = "Operator")]
    public class EmailController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;
        private readonly IUsersService usersService;
        private readonly IToastNotification toast;

        public EmailController(UserManager<User> userManager, 
            IEmailService emailService, 
            IUsersService usersService,
            IToastNotification toast)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.usersService = usersService;
            this.toast = toast;
        }
        public async Task<IActionResult> ShowEmailsByStatus(string statusName)
        {
            try
            {
                var user = await this.userManager.GetUserAsync(User);
                var list = (await this.emailService.GetAllEmailsByStatusNameAsync(statusName))
                        .Where(e => e.ModifiedByUserId == user.Id)
                        .ToList()
                        .ToVM();

                foreach (var email in list)
                {
                    var userName = (await this.usersService.GetUserByIdAsync(email.ModifiedByUserId)).UserName;
                    email.ModifiedByUserName = userName;
                }

                Log.Information($"{DateTime.Now} Show Emails with Status: {statusName} asked by Operator with Id: {user.Id}.");

                return View("AllEmails", list);
            }
            catch (ArgumentNullException ex)
            {
                this.toast.AddWarningToastMessage("Oops... Something went wrong. Please, call your system administrator.");
                Log.Error(ex.Message);
            }
            return LocalRedirect("~");
        }

        public async Task<IActionResult> ShowEmailsWithStatusNewForOperator(string statusName)
        {
            try
            {
                var user = await this.userManager.GetUserAsync(User);
                var list = (await this.emailService.GetAllEmailsByStatusNameAsync(statusName))
                        .ToList()
                        .ToVM();

                Log.Information($"{DateTime.Now} Show Emails with Status: {statusName} asked by Operator with Id: {user.Id}.");

                return View("AllEmails", list);
            }
            catch (ArgumentNullException ex)
            {
                this.toast.AddWarningToastMessage("Oops... Something went wrong. Please, call your system administrator.");
                Log.Error(ex.Message);
            }
            return LocalRedirect("~");
        }
    }
}