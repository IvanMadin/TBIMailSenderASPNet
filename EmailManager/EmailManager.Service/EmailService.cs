using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service.Contracts.Factories;
using EmailManager.Service.DTOs;
using EmailManager.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Service
{
    public class EmailService
    {
        private readonly EmailManagerDbContext context;
        private readonly IEmailFactory emailFactory;

        public EmailService(EmailManagerDbContext context, IEmailFactory emailFactory)
        {
            this.context = context;
            this.emailFactory = emailFactory;
        }

        public async Task<EmailDTO> CreateAsync(string originalMailId, string senderName, string senderEmail, string dateReceived, string subject, string body)
        {
            var currentCultureDateFormat = this.ParseExactDateAsync(dateReceived);

            var newEmail = this.emailFactory.CreateEmail(originalMailId, senderName, senderEmail, currentCultureDateFormat, subject, body);

            if (newEmail is null)
                throw new ArgumentException("Invalid email");

            this.context.Emails.Add(newEmail);
            await this.context.SaveChangesAsync();


            return newEmail.ToDTO();
        }

        public async Task<EmailDTO> GetEmailByIdAsync(string emailId)
        {
            var email = await this.context.Emails.FindAsync(emailId);

            return email.ToDTO();
        }
        public async Task<ClientEmail> GetEmailByOriginalIdAsync(string originalMailId)
        {
            var email = await this.context.Emails.FirstOrDefaultAsync(e => e.OriginalMailId == originalMailId);

            return email;
        }

        public async Task<ICollection<EmailDTO>> GetAllEmailsAsync()
        {
            var allEmails = await this.context.Emails.ToListAsync();

            var mappedEmails = allEmails.ToDTO();

            return mappedEmails;
        }
        public async Task<bool> CheckIfEmailExists(string originalMailId)
        {
            var email = await this.GetEmailByOriginalIdAsync(originalMailId);

            if (email is null)
                return false;

            return true;
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
