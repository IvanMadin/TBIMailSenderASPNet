using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service.Contracts;
using EmailManager.Service.Contracts.Factories;
using EmailManager.Service.DTOs;
using EmailManager.Service.Mappers;
using EmailManager.Service.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailManagerDbContext context;
        private readonly IEmailFactory emailFactory;
        private readonly EncryptingHelper encryptingHelper;

        public EmailService(EmailManagerDbContext context, IEmailFactory emailFactory, EncryptingHelper encryptingHelper)
        {
            this.context = context;
            this.emailFactory = emailFactory;
            this.encryptingHelper = encryptingHelper;
        }

        public async Task<EmailDTO> CreateAsync(string originalMailId, string senderName, string senderEmail, string dateReceived, string subject, string body)
        {
            var currentCultureDateFormat = this.ParseExactDateAsync(dateReceived);

            var newEmail = this.emailFactory.CreateEmail(originalMailId, senderName, senderEmail, currentCultureDateFormat, subject, body);

            if (newEmail == null)
            {
                Log.Error("Email is null");
                throw new ArgumentException("Invalid email");
            }

            this.context.Emails.Add(newEmail);

            await this.context.SaveChangesAsync();
            Log.Information("Email with Original Mail ID: {0} was created",newEmail.Id);

            return newEmail.ToDTO();
        }

        public async Task<EmailDTO> GetEmailByIdAsync(string emailId)
        {
            var email = await this.context.Emails.Include(e=> e.Status).FirstOrDefaultAsync(e=> e.Id == emailId);
           // Log.Information("Email with ID: {0} was found", email.Id);

            email.Body = this.encryptingHelper.DecryptingBase64Data(email.Body);

            return email.ToDTO();
        }
        public async Task<ClientEmail> GetEmailByOriginalIdAsync(string originalMailId)
        {
            var email = await this.context.Emails.FirstOrDefaultAsync(e => e.OriginalMailId == originalMailId);
            
            email.Body = this.encryptingHelper.DecryptingBase64Data(email.Body);

            Log.Information("Email with ID: {0} was successfully taken", email.Id);

            return email;
        }

        public async Task<ICollection<EmailDTO>> GetAllEmailsAsync()
        {
            var allEmails = await this.context.Emails.ToListAsync();
            Log.Information("Аll emails successfully received");

            allEmails.Select(e => e.Body = this.encryptingHelper.DecryptingBase64Data(e.Body));

            var mappedEmails = allEmails.ToDTO();

            return mappedEmails;
        }
        public async Task<bool> CheckIfEmailExists(string originalMailId)
        {
            var doesEmailExist = await this.context.Emails.AnyAsync(e => e.OriginalMailId == originalMailId);

            if(doesEmailExist)
            Log.Information("Email with ID: {0} exists", originalMailId);

            return doesEmailExist;
        }

        //public async Task<EmailDTO> ChangeEmailStatusAsync(string emailId)
        //{
        //    var email = await this.GetEmailByIdAsync(emailId);
        //}
        private string ParseExactDateAsync(string dateReceived)
        {
            //TODO: Have to find another way. To ignore the exact formatting.
            var format = "";
            if (dateReceived.Contains("GMT"))
            {
                format = "ddd, d MMM yyyy HH:mm:ss K (GMT)";
            }
            else
            {
                format = "ddd, d MMM yyyy HH:mm:ss K";
            }
            var date = DateTime.ParseExact(dateReceived, format, CultureInfo.InvariantCulture);

            return date.ToString("d/MM/yyyy H:mm:ss");
        }

    }
}
