
using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service.Contracts;
using EmailManager.Service.DTOs;
using EmailManager.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Service
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly EmailManagerDbContext context;
        private readonly IEmailService emailService;
        private readonly IClientService clientService;

        public LoanApplicationService(EmailManagerDbContext context, IEmailService emailService, IClientService clientService)
        {
            this.context = context;
            this.emailService = emailService;
            this.clientService = clientService;
        }

        public async Task<LoanApplicationDTO> CreateLoanApplicationAsync(string clientId, string emailId, string operatorId, decimal amount)
        {
            //TODO: After the new application is applied to status Open you have access to that metod. Have to change status from Open to Close after that method is done.
            
            var loanApplication = new LoanApplication
            {
                ClientDataId = clientId,
                EmailId = emailId,
                Amount = amount,
                StatusApplicationId = "61cb6584-591b-4560-bc4a-a89950b15cc3",
                UserId = operatorId,
                CreatedOnDate = DateTime.UtcNow,
                CreatedByUserId = operatorId
            };

            this.context.LoanApplications.Add(loanApplication);

            await this.context.SaveChangesAsync();
            Log.Information("Loan application with ID: {0} was successfully created", loanApplication.Id);

            return loanApplication.ToDTO();

        }

        public async Task<LoanApplicationDTO> GetLoanApplicationByIdAsync(string applicationId)
        {
            var application = await this.context.LoanApplications.Include(e => e.StatusApplication).FirstOrDefaultAsync(e => e.Id == applicationId);

            Log.Information("Loan application with ID: {0} was successfully taken", applicationId);

            return application.ToDTO();
        }
    }
}
