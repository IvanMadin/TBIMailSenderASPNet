using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service.Contracts;
using EmailManager.Service.Contracts.Factories;
using EmailManager.Service.DTOs;
using EmailManager.Service.Mappers;
using EmailManager.Service.Providers;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailManagerDbContext context;
        private readonly IEmailFactory emailFactory;
        private readonly EncryptingHelper encryptingHelper;
        private readonly IEmailStatusService emailStatusService;

        public EmailService(EmailManagerDbContext context,
            IEmailFactory emailFactory,
            EncryptingHelper encryptingHelper,
            IEmailStatusService emailStatusService)
        {
            this.context = context;
            this.emailFactory = emailFactory;
            this.encryptingHelper = encryptingHelper;
            this.emailStatusService = emailStatusService;
        }

        public async Task<EmailDTO> CreateAsync(string originalMailId, string senderName, string senderEmail, DateTime dateReceived, string subject, string body)
        {
            var newEmail = this.emailFactory.CreateEmail(originalMailId, senderName, senderEmail, dateReceived, subject, body);

            if (newEmail == null)
            {
                Log.Error($"{DateTime.Now} Email is not found");
                throw new ArgumentException("Invalid email");
            }

            this.context.Emails.Add(newEmail);
            await this.context.SaveChangesAsync();

            Log.Information($"{newEmail.ModifiedOnDate} Create Email with Id: {newEmail.Id} by {newEmail.ModifiedByUserName}.");
            return newEmail.ToDTO();
        }


        public async Task<EmailDTO> GetEmailByIdAsync(string emailId)
        {
            var email = await this.context.Emails.Include(e => e.Status).Include(a => a.EmailAttachments).FirstOrDefaultAsync(e => e.Id == emailId);

            var decryptedBody = this.encryptingHelper.DecryptingBase64Data(email.Body);
            var emailDTO = email.ToDTO();
            emailDTO.Body = decryptedBody;

            Log.Information($"{DateTime.Now} Get Email with Id: {email.Id} by {email.ModifiedByUserName}.");
            return emailDTO;
        }

        /// <summary>
        /// Get All Emails - No matter of the statuses.
        /// </summary>
        public async Task<ICollection<EmailDTO>> GetAllEmailsAsync()
        {
            var allEmails = await this.context.Emails.Include(e => e.Status).Include(a => a.EmailAttachments).Include(u => u.User).OrderByDescending(e => e.DateReceived).ToListAsync();
            allEmails.Select(e => e.Body = this.encryptingHelper.DecryptingBase64Data(e.Body));
            var mappedEmails = allEmails.ToDTO();

            Log.Information($"{DateTime.Now} Get All Emails.");
            return mappedEmails;
        }


        public async Task<ICollection<EmailDTO>> GetAllEmailsByStatusNameAsync(string statusName)
        {
            var status = await this.emailStatusService.GetEmailStatusByNameAsync(statusName);
            var listOfEmails = await this.context.Emails
                .Include(e => e.Status)
                .Include(e => e.EmailAttachments)
                .Include(e => e.LoanApplication)
                .Where(e => e.StatusEmailId == status.Id)
                .ToListAsync();

            Log.Information($"{DateTime.Now} Get All Emails with Status: {statusName}.");
            return listOfEmails.ToDTO();
        }

        public async Task<EmailDTO> UpdateEmailStatus(EmailDTO emailDTO, StatusEmailDTO newEmailStatus, string userId)
        {
            var email = await this.context.Emails.FindAsync(emailDTO.Id);
            var oldEmailStatus = email.Status.StatusType;
            if (email is null)
            {
                Log.Information($"{DateTime.Now} Email not found.");
                throw new Exception("Email is not found");
            }

            email.StatusEmailId = newEmailStatus.Id;
            email.ModifiedOnDate = DateTime.Now;
            email.ModifiedByUserId = userId;

            if (newEmailStatus.StatusType == "Open Application")
            {
                email.UserId = userId;
            }

            Log.Information($"{email.ModifiedOnDate} Update Emails by User Id: {userId}, from: {oldEmailStatus} to {newEmailStatus.StatusType}.");
            await this.context.SaveChangesAsync();

            return email.ToDTO();
        }

        public async Task<bool> CheckIfEmailExists(string originalMailId)
        {
            var doesEmailExist = await this.context.Emails.AnyAsync(e => e.OriginalMailId == originalMailId);

            if (doesEmailExist)
            {

            }

            return doesEmailExist;
        }
    }
}
