using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailManager.Data.Entities;
using EmailManager.GmailConfig;
using EmailManager.Service;
using EmailManager.Service.Contracts;
using EmailManager.Service.Contracts.Factories;
using EmailManager.Service.DTOs;
using EmailManager.Service.Factories;
using EmailManager.Web.Extensions.Mappers;
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
        private readonly IEmailService emailService;
        private readonly ILoanApplicationService loanApplicationService;
        private readonly IClientService clientService;
        private readonly IApplicationStatusService applicationStatusService;
        private readonly IClientDataFactory clientDataDTOFactory;

        public LoanApplicationController(UserManager<User> userManager, 
                                         GmailConfigure gmailConfigure, 
                                         IEmailService emailService, 
                                         ILoanApplicationService loanApplicationService, 
                                         IClientService clientService,
                                         IApplicationStatusService applicationStatusService,
                                         IClientDataFactory clientDataDTOFactory)
        {
            this.userManager = userManager;
            this.gmailConfigure = gmailConfigure;
            this.emailService = emailService;
            this.loanApplicationService = loanApplicationService;
            this.clientService = clientService;
            this.applicationStatusService = applicationStatusService;
            this.clientDataDTOFactory = clientDataDTOFactory;
        }

        [HttpGet]
        public async Task<IActionResult> ApplicationForm(string emailId)
        {
            var email = await this.emailService.GetEmailByIdAsync(emailId);

            var newLoanApplication = new LoanApplicationViewModel
            {
                EmailId = email.Id
            };

            Log.Information("Email for application form was found successfully!");
            return View(newLoanApplication);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplicationForm(LoanApplicationViewModel loanModel)
        {
            var operatorId = userManager.GetUserId(User);

            var clientData = await this.clientService.FindClientAsync(loanModel.FirstName, loanModel.LastName, loanModel.EGN);
            if (clientData is null)
            {
                var clientDataDTO = this.clientDataDTOFactory.Create(loanModel.FirstName, loanModel.LastName, loanModel.EGN, loanModel.Phone, operatorId);

                clientData = await this.clientService.CreateClientData(clientDataDTO);
                Log.Information("Client with EGN: {0} was created by operator with ID: {1} on {2}!", loanModel.EGN, operatorId, DateTime.UtcNow);
            }

            await this.loanApplicationService.CreateLoanApplicationAsync(clientData.Id, loanModel.EmailId, operatorId, loanModel.Amount);

            Log.Information("Loan application for client with EGN: {0} is created by operator with ID: {1} on {2}!", loanModel.EGN, operatorId, DateTime.UtcNow);
            return RedirectToAction("Application", "Email", new { id = loanModel.EmailId });
        }

        [HttpGet]
        public async Task<IActionResult> ApplicationStatus(string applicationId, string applicationStatusId)
        {
            var application = await this.loanApplicationService.GetLoanApplicationByIdAsync(applicationId);
            var applicationStatus = await this.applicationStatusService.GetApplicationStatusByIdAsync(applicationStatusId);

            return View();
        }

        public async Task<IActionResult> UpdateStatusApplication(string loanApplicationId, string status)
        {
            var loanApplication = await this.loanApplicationService.GetLoanApplicationByIdAsync(loanApplicationId);
            await this.applicationStatusService.UpdateApplicationStatusAsync(loanApplication, status);

            return View(loanApplication);
        }

        public async Task<IActionResult> AllApplicationStatus()
        {
            var list = (await this.applicationStatusService.AllApplicationStatusAsync()).ToVM();

            return View(list);
        }
    }
}