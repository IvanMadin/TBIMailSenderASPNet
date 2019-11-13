
using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Data.Entities.Types;
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

        public LoanApplicationService(EmailManagerDbContext context)
        {
            this.context = context;
        }

        public async Task<LoanApplicationDTO> CreateLoanApplicationAsync(string emailId, string operatorId)
        {
            var loan = new LoanApplication
            {
                EmailId = emailId,
                ApplicationStatus = ApplicationStatus.New,
                CreatedOnDate = DateTime.Now,
                CreatedByUserId = operatorId
            };

            this.context.LoanApplications.Add(loan);
            await this.context.SaveChangesAsync();

            return loan.ToDTO();
        }
        public async Task<LoanApplicationDTO> CreateLoanApplicationAsync(string clientId, string emailId, string status, string operatorId, decimal amount)
        {
            //TODO: After the new application is applied to status Open you have access to that metod. Have to change status from Open to Close after that method is done.

            var loan = await this.context.LoanApplications.FirstOrDefaultAsync(la => la.EmailId == emailId);

            loan.ClientDataId = clientId;
            loan.Amount = amount;
            loan.ApplicationStatus = (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), status, true);
            loan.ModifiedOnDate = DateTime.Now;
            loan.ModifiedByUserId = operatorId;
            loan.UserId = operatorId;

            await this.context.SaveChangesAsync();
            Log.Information("Loan application with ID: {0} was successfully created", loan.Id);

            return loan.ToDTO();

        }

        public async Task<LoanApplication> GetLoanApplicationByEmailIdAsync(string emailId)
        {
            var loan = await this.context.LoanApplications.FirstOrDefaultAsync(la => la.EmailId == emailId);

            return loan;
        }
        public async Task<LoanApplicationDTO> GetLoanApplicationByIdAsync(string applicationId)
        {
            var application = await this.context.LoanApplications.FirstOrDefaultAsync(e => e.Id == applicationId);

            Log.Information("Loan application with ID: {0} was successfully taken", applicationId);

            return application.ToDTO();
        }

        public async Task<LoanApplicationDTO> OpenLoanApplication(string emailId, string userId)
        {
           var loan = await GetLoanApplicationByEmailIdAsync(emailId);
            loan.ModifiedOnDate = DateTime.Now;
            loan.ModifiedByUserId = userId;
            loan.UserId = userId;

            await this.context.SaveChangesAsync();

            return loan.ToDTO();
        }
    }
}
