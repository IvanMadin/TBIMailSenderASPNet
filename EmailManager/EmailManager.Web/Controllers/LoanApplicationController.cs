using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailManager.Data.Entities;
using EmailManager.GmailConfig;
using EmailManager.Service;
using EmailManager.Web.Mappers;
using EmailManager.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EmailManager.Web.Controllers
{
    public class LoanApplicationController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly GmailConfigure gmailConfigure;
        private readonly EmailService emailService;
        private readonly LoanApplicationService loanApplicationService;
        private readonly ClientService clientService;

        public LoanApplicationController(UserManager<User> userManager, GmailConfigure gmailConfigure, EmailService emailService, LoanApplicationService loanApplicationService, ClientService clientService)
        {
            this.userManager = userManager;
            this.gmailConfigure = gmailConfigure;
            this.emailService = emailService;
            this.loanApplicationService = loanApplicationService;
            this.clientService = clientService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ApplicationForm(string emailId)
        {
            var email = await this.emailService.GetEmailByIdAsync(emailId);
            var newLoanApplication = new LoanApplicationViewModel
            {
                EmailId = email.Id
            };

            Log.Information("Application form for {@email} is loaded!", email);

            return View(newLoanApplication);
        }

        [HttpPost]
        public async Task<IActionResult> ApplicationForm(LoanApplicationViewModel loanModel)
        {
            var operatorId = userManager.GetUserId(User);
            string firstName = loanModel.FirstName;
            string lastName = loanModel.LastName;
            string egn = loanModel.EGN;
            string phone = loanModel.Phone;
            decimal amount = loanModel.Amount;
            var emailId = loanModel.EmailId;

            var clientData = await this.clientService.FindClientAsync(firstName, lastName, egn);
            Log.Information("Client with EGN: {@egn} is created!", egn);

            if (clientData is null)
            {
                Log.Error("Client data is null!");
                clientData = await this.clientService.CreateClientData(firstName, lastName, egn, phone);
            }

            var loanApplication = await this.loanApplicationService.CreateLoanApplicationAsync(clientData.Id, emailId, operatorId, amount);

            Log.Information("Loan application for client with EGN: {@egn} is created!", egn);
            return View();
        }
    }
}