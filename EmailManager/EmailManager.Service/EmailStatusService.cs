using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service.Contracts;
using EmailManager.Service.DTOs;
using EmailManager.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Service
{
    public class EmailStatusService : IEmailStatusService
    {
        private readonly EmailManagerDbContext context;
        public EmailStatusService(EmailManagerDbContext context)
        {
            this.context = context;
        }


        // maybe is not necessary
        public async Task<StatusEmailDTO> CreateStatusEmailAsync(string statusEmail)
        {
            var status = new StatusEmail() { StatusType = statusEmail };

            await this.context.SaveChangesAsync();
            return status.ToDTO();
        }

        public async Task<StatusEmailDTO> GetEmailStatusByIdAsync(string emailStatusId)
        {
            var emailStatus = await this.context.StatusEmails.FindAsync(emailStatusId);

            if (emailStatus == null)
            {
                Log.Error("Email status is null");
                throw new Exception("Emails status is null");
            }

            return emailStatus.ToDTO();
        }

        public async Task<ICollection<EmailDTO>> GetAllEmailByStatusIdAsync(string emailStatusId)
        {
            var emailStatus = await this.context.StatusEmails.FindAsync(emailStatusId);

            if (emailStatus == null)
            {
                throw new Exception();
            }

            var status = await this.context.Emails
                         .Include(s => s.Status)
                         .Where(e => e.StatusEmailId == emailStatusId)
                         .ToListAsync();

            return status.ToDTO();
        }

        public async Task<StatusEmailDTO> UpdateEmailStatusAsync(EmailDTO emailDTO, string newStatusEmail)
        {
            var statusEmail = await this.context.StatusEmails.FirstOrDefaultAsync(x => x.StatusType == newStatusEmail);

            if(statusEmail is null)
            {
                throw new Exception("Email status is not found");
            }

            var email = await this.context.Emails.FindAsync(emailDTO.Id);

            if(email is null)
            {
                throw new Exception("Email is not found");
            }

            email.StatusEmailId = statusEmail.Id;
            await this.context.SaveChangesAsync();

            return statusEmail.ToDTO();
        }

        public async Task<ICollection<StatusEmailDTO>> AllEmailStatusAsync()
        {
            var statusEmails = await this.context.StatusEmails.ToListAsync();

            return statusEmails.ToDTO();
        }
    }
}
