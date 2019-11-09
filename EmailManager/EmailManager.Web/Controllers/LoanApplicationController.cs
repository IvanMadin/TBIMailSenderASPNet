using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailManager.Data.Entities;
using EmailManager.GmailConfig;
using EmailManager.Service;
using EmailManager.Service.DTOs;
using EmailManager.Service.Factories;
using EmailManager.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmailManager.Web.Controllers
{
    public class LoanApplicationController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly GmailConfigure gmailConfigure;
        private readonly EmailService emailService;
        private readonly LoanApplicationService loanApplicationService;
        private readonly ClientService clientService;
        private readonly ClientDataDTOFactory clientDataDTOFactory;

        public LoanApplicationController(UserManager<User> userManager, 
                                         GmailConfigure gmailConfigure, 
                                         EmailService emailService, 
                                         LoanApplicationService loanApplicationService, 
                                         ClientService clientService,
                                         ClientDataDTOFactory clientDataDTOFactory)
        {
            this.userManager = userManager;
            this.gmailConfigure = gmailConfigure;
            this.emailService = emailService;
            this.loanApplicationService = loanApplicationService;
            this.clientService = clientService;
            this.clientDataDTOFactory = clientDataDTOFactory;
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
            }

            await this.loanApplicationService.CreateLoanApplicationAsync(clientData.Id, loanModel.EmailId, operatorId, loanModel.Amount);

            return RedirectToAction("Application", "Email", new { id = loanModel.EmailId });
        }
    }
}