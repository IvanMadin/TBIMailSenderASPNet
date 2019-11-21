using System;
using System.Threading.Tasks;
using EmailManager.Data.Entities;
using EmailManager.Service.Contracts;
using EmailManager.Service.Contracts.Factories;
using EmailManager.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EmailManager.Web.Controllers
{
    [Authorize]
    public class LoanApplicationController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;
        private readonly ILoanApplicationService loanApplicationService;
        private readonly IClientService clientService;
        private readonly IClientDataFactory clientDataDTOFactory;

        public LoanApplicationController(UserManager<User> userManager,
                                         IEmailService emailService,
                                         ILoanApplicationService loanApplicationService,
                                         IClientService clientService,
                                         IClientDataFactory clientDataDTOFactory)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.loanApplicationService = loanApplicationService;
            this.clientService = clientService;
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

            Log.Information($"{DateTime.Now} Application Form with email ID: {emailId} has been accessed by {User}.");
            return View(newLoanApplication);
        }

        [HttpPost]
        public async Task<IActionResult> ApplicationForm(LoanApplicationViewModel loanModel)
        {
            var operatorId = userManager.GetUserId(User);

            var clientData = await this.clientService.FindClientAsync(loanModel.FirstName, loanModel.LastName, loanModel.EGN);
            if (clientData is null)
            {
                var clientDataDTO = this.clientDataDTOFactory.Create(loanModel.FirstName, loanModel.LastName, loanModel.EGN, loanModel.Phone, operatorId);

                clientData = await this.clientService.CreateClientData(clientDataDTO);
                Log.Information($"{DateTime.Now} Client Data has been created by {User}.");
            }

            await this.loanApplicationService.CreateLoanApplicationAsync(clientData.Id, loanModel.EmailId, loanModel.Status, operatorId, loanModel.Amount);
            Log.Information($"{DateTime.Now} Loan Application has been created by {User}.");

            //TODO: Have to change status of Email to Closed no matter what operation I take.

            return RedirectToAction("Application", "Email", new { id = loanModel.EmailId });
        }

        [HttpGet]
        public async Task<IActionResult> ApplicationStatus(string applicationId, string applicationStatusId)
        {
            var application = await this.loanApplicationService.GetLoanApplicationByIdAsync(applicationId);
            //TODO: ApplicationStatus have to changed.

           // Log.Information($"{DateTime.Now} Changed Application Status by {User}, from: {application.ApplicationStatusName} to {newStatusName}.");
            return View();
        }

        public async Task<IActionResult> UpdateStatusApplication(string loanApplicationId, string status)
        {
            var loanApplication = await this.loanApplicationService.GetLoanApplicationByIdAsync(loanApplicationId);
            //TODO: ApplicationStatus have to changed.

            Log.Information($"{DateTime.Now} Update Application Status by {User}, from: {loanApplication.ApplicationStatusName} to {status}.");
            return View(loanApplication);
        }
    }
}