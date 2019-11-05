
using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service.DTOs;
using EmailManager.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Service
{
    public class LoanApplicationService
    {
        private readonly EmailManagerDbContext context;
        private readonly EmailService emailService;
        private readonly ClientService clientService;

        public LoanApplicationService(EmailManagerDbContext context, EmailService emailService, ClientService clientService)
        {
            this.context = context;
            this.emailService = emailService;
            this.clientService = clientService;
        }

        public async Task<LoanApplicationDTO> CreateLoanApplicationAsync(string clientId, string emailId, string operatorId, decimal amount)
        {

            var loanApplication = new LoanApplication
            {
                ClientDataId = clientId,
                EmailId = emailId,
                Amount = amount,
                StatusApplicationId = "3a868cf4-ac9d-4936-9efe-5a17f56b998c",
                UserId = operatorId
            };

            this.context.LoanApplications.Add(loanApplication);

            await this.context.SaveChangesAsync();
            return loanApplication.ToDTO();

        }

        public async Task<LoanApplicationDTO> GetLoanApplicationByIdAsync(string applicationId)
        {
            var application = await this.context.LoanApplications.FindAsync(applicationId);

            return application.ToDTO();
        }
    }
}
