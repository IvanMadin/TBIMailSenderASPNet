using System;
using System.Threading.Tasks;
using EmailManager.Data.Entities;
using EmailManager.Service.Contracts;
using EmailManager.Service.Contracts.Factories;
using EmailManager.Service.DTOs;
using EmailManager.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
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
        private readonly IToastNotification toast;
        private readonly IEmailStatusService emailStatusService;

        public LoanApplicationController(UserManager<User> userManager,
                                         IEmailService emailService,
                                         ILoanApplicationService loanApplicationService,
                                         IClientService clientService,
                                         IClientDataFactory clientDataDTOFactory,
                                         IToastNotification toast,
                                         IEmailStatusService emailStatusService)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.loanApplicationService = loanApplicationService;
            this.clientService = clientService;
            this.clientDataDTOFactory = clientDataDTOFactory;
            this.toast = toast;
            this.emailStatusService = emailStatusService;
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ApplicationForm(LoanApplicationViewModel loanModel)
        {
            if (!ModelState.IsValid 
                || loanModel.FirstName == null 
                || loanModel.LastName == null 
                || loanModel.EGN == null 
                || loanModel.Phone == null)
            {
                this.toast.AddWarningToastMessage("Not valid data.");
                return RedirectToAction("Application", "Email", new { id = loanModel.EmailId });
            }

            try
            {
                var operatorId = userManager.GetUserId(User);

                var clientData = await this.clientService.FindClientAsync(loanModel.FirstName, loanModel.LastName, loanModel.EGN);
                if (clientData is null)
                {
                    var clientDataDTO = this.clientDataDTOFactory.Create(loanModel.FirstName, loanModel.LastName, loanModel.EGN, loanModel.Phone, operatorId);
                    clientData = await this.clientService.CreateClientData(clientDataDTO);
                    Log.Information($"{DateTime.Now} Client Data has been created by {operatorId}.");
                }

                var loanApplicationDTO = await this.loanApplicationService.CreateLoanApplicationAsync(clientData.Id, loanModel.EmailId, loanModel.Status, operatorId, loanModel.Amount);
                this.toast.AddSuccessToastMessage($"Loan Application has been created successfully!");
                Log.Information($"{DateTime.Now} Loan Application has been created by {operatorId}.");

                var emailStatus = await this.emailStatusService.GetEmailStatusByNameAsync("Closed Application");
                await this.emailService.UpdateEmailStatus(new EmailDTO { Id = loanModel.EmailId }, emailStatus, operatorId);

                return RedirectToAction("Application", "Email", new { id = loanModel.EmailId });
            }
            catch (Exception ex)
            {
                this.toast.AddWarningToastMessage("Oops... Something went wrong. Please, call your system administrator.");
                Log.Error($"{DateTime.Now} {ex.Message}");
            }

            return RedirectToAction("Application", "Email", new { id = loanModel.EmailId });
        }
    }
}