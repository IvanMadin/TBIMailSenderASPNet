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
                Log.Error("Email is null");
                throw new ArgumentException("Invalid email");
            }

            this.context.Emails.Add(newEmail);

            await this.context.SaveChangesAsync();
            Log.Information("Email with Original Mail ID: {0} was created", newEmail.Id);

            return newEmail.ToDTO();
        }

        
        public async Task<EmailDTO> GetEmailByIdAsync(string emailId)
        {
            var email = await this.context.Emails.Include(e => e.Status).Include(a => a.EmailAttachments).FirstOrDefaultAsync(e => e.Id == emailId);

            var decryptedBody = this.encryptingHelper.DecryptingBase64Data(email.Body);
            var emailDTO = email.ToDTO();
            emailDTO.Body = decryptedBody;
            return emailDTO;
        }

        /// <summary>
        /// Get All Emails - No matter of the statuses.
        /// </summary>
        public async Task<ICollection<EmailDTO>> GetAllEmailsAsync()
        {
            var allEmails = await this.context.Emails.Include(e => e.Status).Include(a => a.EmailAttachments).Include(u=>u.User).OrderByDescending(e => e.DateReceived).ToListAsync();
            Log.Information("Аll emails successfully received");

            allEmails.Select(e => e.Body = this.encryptingHelper.DecryptingBase64Data(e.Body));

            var mappedEmails = allEmails.ToDTO();

            return mappedEmails;
        }

        
        public async Task<ICollection<EmailDTO>> GetAllEmailsByStatusNameAsync(string statusName)
        {
            var status = await this.emailStatusService.GetEmailStatusByNameAsync(statusName);
            var listOfEmails = await this.context.Emails
                .Include(e => e.Status)
                .Include(e => e.EmailAttachments)
                .Where(e => e.StatusEmailId == status.Id)
                .ToListAsync();

            return listOfEmails.ToDTO();
        }

        public async Task<EmailDTO> UpdateEmailStatus(EmailDTO emailDTO, StatusEmailDTO newEmailStatus, string userId)
        {
            var email = await this.context.Emails.FindAsync(emailDTO.Id);
            var oldEmailStatus = email.Status.StatusType;
            if (email is null)
            {
                throw new Exception("Email is not found");
            }

            email.StatusEmailId = newEmailStatus.Id;
            email.ModifiedOnDate = DateTime.Now;
            email.ModifiedByUserId = userId;

            if (newEmailStatus.StatusType == "Open Application")
            {
                email.UserId = userId;
            }

            Log.Logger.Information($"[{email.ModifiedOnDate}] - Email Status of Id: [{email.Id}] has been changed from [{oldEmailStatus}] to [{newEmailStatus.StatusType}] by userId: [{userId}]");
            await this.context.SaveChangesAsync();

            return email.ToDTO();
        }

        public async Task<bool> CheckIfEmailExists(string originalMailId)
        {
            var doesEmailExist = await this.context.Emails.AnyAsync(e => e.OriginalMailId == originalMailId);

            if (doesEmailExist)
                Log.Information("Email with ID: {0} exists", originalMailId);

            return doesEmailExist;
        }
    }
}
