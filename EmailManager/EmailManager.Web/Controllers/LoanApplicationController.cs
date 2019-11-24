using System;
using System.Threading.Tasks;
using EmailManager.Data.Entities;
using EmailManager.Service.Contracts;
using EmailManager.Service.Contracts.Factories;
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

        public LoanApplicationController(UserManager<User> userManager,
                                         IEmailService emailService,
                                         ILoanApplicationService loanApplicationService,
                                         IClientService clientService,
                                         IClientDataFactory clientDataDTOFactory,
                                         IToastNotification toast)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.loanApplicationService = loanApplicationService;
            this.clientService = clientService;
            this.clientDataDTOFactory = clientDataDTOFactory;
            this.toast = toast;
        }

        [HttpGet]
        public async Task<IActionResult> ApplicationForm(string emailId)
        {
            try
            {
                var email = await this.emailService.GetEmailByIdAsync(emailId);

                var newLoanApplication = new LoanApplicationViewModel
                {
                    EmailId = email.Id
                };

                Log.Information($"{DateTime.Now} Application Form with email ID: {emailId} has been accessed by {User}.");
                return View(newLoanApplication);
            }
            catch
            {
                this.toast.AddWarningToastMessage("Oops... Something went wrong.");
                Log.Error($"Application form wasn't accessible!");
                return RedirectToAction("Error", "Home");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> ApplicationForm(LoanApplicationViewModel loanModel)
        {
            try
            {
                var operatorId = userManager.GetUserId(User);
                var clientData = await this.clientService.FindClientAsync(loanModel.FirstName, loanModel.LastName, loanModel.EGN);

                if (!ModelState.IsValid || clientData.FirstName == null || clientData.LastName == null || clientData.EGN == null)
                {
                    var clientDataDTO = this.clientDataDTOFactory.Create(loanModel.FirstName, loanModel.LastName, loanModel.EGN, loanModel.Phone, operatorId);

                    if (string.IsNullOrEmpty(clientData.FirstName))
                    {
                        TempData["firstNameMessage"] = "FirstName must be between 3 and 50 symbols.";
                    }

                    if (string.IsNullOrEmpty(clientData.LastName))
                    {
                        TempData["lastNameMessage"] = "LastName must be between 3 and 50 symbols.";
                    }

                    if (string.IsNullOrEmpty(clientData.EGN))
                    {
                        TempData["egnMessage"] = "EGN must be 10 symbols.";
                    }

                    if (string.IsNullOrEmpty(clientData.Phone))
                    {
                        TempData["phoneMessage"] = "Phone must be between 10 and 13 symbols.";
                    }

                    clientData = await this.clientService.CreateClientData(clientDataDTO);
                    Log.Information($"{DateTime.Now} Client Data has been created by {User}.");
                }

                await this.loanApplicationService.CreateLoanApplicationAsync(clientData.Id, loanModel.EmailId, loanModel.Status, operatorId, loanModel.Amount);
                this.toast.AddSuccessToastMessage($"Client data was created successfully!");
                Log.Information($"{DateTime.Now} Loan Application has been created by {User}.");

                //TODO: Have to change status of Email to Closed no matter what operation I take.

                return RedirectToAction("Application", "Email", new { id = loanModel.EmailId });
            }
            catch (Exception ex)
            {
                this.toast.AddWarningToastMessage("Oops... Something went wrong.");
                TempData["errorMessage"] = ex.Message;
            }
            // should return the same page with open client form
            return View(loanModel);
        }

    }
}