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

        public async Task<StatusEmailDTO> GetEmailStatusByNameAsync(string emailStatusName)
        {
            var emailStatus = await this.context.StatusEmails.FirstOrDefaultAsync(se => se.StatusType.ToLower() == emailStatusName.ToLower());

            if(emailStatus is null)
            {
                Log.Information($"{DateTime.Now} Email with Status Name: {emailStatusName} not found.");
                throw new ArgumentNullException("EmailStatus with that name does not exist!");
            }

            Log.Information($"{DateTime.Now} Get Emails with Status Name: {emailStatusName}.");
            return emailStatus.ToDTO();
        }
        public async Task<StatusEmailDTO> GetEmailStatusByIdAsync(string emailStatusId)
        {
            var emailStatus = await this.context.StatusEmails.FindAsync(emailStatusId);

            if (emailStatus == null)
            {
                Log.Information($"{DateTime.Now} Email with Status Id: {emailStatusId} not found.");
                throw new Exception("Emails status is null");
            }

            Log.Information($"{DateTime.Now} Get Emails with Status Id: {emailStatusId}.");
            return emailStatus.ToDTO();
        }
    }
}
