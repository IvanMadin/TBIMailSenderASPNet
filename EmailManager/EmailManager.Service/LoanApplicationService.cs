﻿using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Data.Entities.Types;
using EmailManager.Service.Contracts;
using EmailManager.Service.DTOs;
using EmailManager.Service.DTOs.ToEntities;
using EmailManager.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
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

        public async Task<LoanApplicationDTO> CreateLoanApplicationAsync(string emailId)
        {
            var loan = new LoanApplication
            {
                EmailId = emailId,
                ApplicationStatus = ApplicationStatus.New,
                CreatedOnDate = DateTime.Now,
            };

            this.context.LoanApplications.Add(loan);
            await this.context.SaveChangesAsync();

            Log.Information($"{loan.CreatedOnDate} Created new Loan Application with Email Id: {loan.EmailId}.");
            return loan.ToDTO();
        }
        public async Task<LoanApplicationDTO> CreateLoanApplicationAsync(string clientId, string emailId, string status, string operatorId, decimal amount)
        {
            var loan = await this.context.LoanApplications.FirstOrDefaultAsync(la => la.EmailId == emailId);

            loan.ClientDataId = clientId;
            loan.Amount = amount;
            loan.ApplicationStatus = (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), status, true);
            loan.ModifiedOnDate = DateTime.Now;
            loan.ModifiedByUserId = operatorId;
            loan.UserId = operatorId;

            await this.context.SaveChangesAsync();

            Log.Information($"{loan.ModifiedOnDate} Filled Loan Application with id: {loan.Id} by User Id: {loan.ModifiedByUserId}.");
            return loan.ToDTO();

        }

        public async Task<LoanApplicationDTO> GetLoanApplicationByEmailIdAsync(string emailId)
        {
            var loan = await this.context.LoanApplications
                .FirstOrDefaultAsync(la => la.EmailId == emailId && la.DeletedOnDate == null);

            Log.Information($"{DateTime.Now} Get Loan Application with Email Id: {emailId} by User Id: {loan.ModifiedByUserId}.");

            return loan.ToDTO();
        }
        public async Task<LoanApplicationDTO> GetLoanApplicationByIdAsync(string applicationId)
        {
            var application = await this.context.LoanApplications
                .FirstOrDefaultAsync(e => e.Id == applicationId && e.DeletedOnDate == null);

            Log.Information($"{DateTime.Now} Get Loan Application with Application Id: {application.Id} by UserId: {application.ModifiedByUserId}.");
            return application.ToDTO();
        }

        public async Task<LoanApplicationDTO> OpenLoanApplicationAsync(string emailId, string userId)
        {
            var loan = (await GetLoanApplicationByEmailIdAsync(emailId)).ToEntity();
            loan.ModifiedOnDate = DateTime.Now;
            loan.ModifiedByUserId = userId;
            loan.UserId = userId;

            await this.context.SaveChangesAsync();

            Log.Information($"{loan.ModifiedOnDate} Open Loan Application with Email ID: {loan.EmailId} by UserId: {loan.ModifiedByUserId}.");
            return loan.ToDTO();
        }

        public async Task<bool> RemoveOldApplicationByEmailIdAsync(string emailId, string operatorId)
        {
            try
            {
                var loan = await this.context.LoanApplications.FirstOrDefaultAsync(e => e.EmailId == emailId);
                if (loan is null)
                {
                    throw new ArgumentNullException($"{DateTime.Now} Remove Application could not found Loan with email Id {emailId}.");
                }

                loan.ChangedCloseToNewStatusForEmailId = loan.EmailId;
                loan.DeletedOnDate = DateTime.Now;
                loan.DeletedByUserId = operatorId;
                loan.EmailId = null;
                loan.ApplicationStatus = ApplicationStatus.Deleted;
                this.context.LoanApplications.Update(loan);
                await this.context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return false;
            }
        }
    }
}
