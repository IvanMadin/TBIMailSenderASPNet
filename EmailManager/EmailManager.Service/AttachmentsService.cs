using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service.Contracts;
using EmailManager.Service.DTOs;
using EmailManager.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Service
{
    public class AttachmentsService : IAttachmentsService
    {
        private readonly EmailManagerDbContext context;

        public AttachmentsService(EmailManagerDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(string emailId, List<string> attachmentNames, List<double> attachmentSizes)
        {
            for (int i = 0; i < attachmentNames.Count; i++)
            {
                var newAttachment = new EmailAttachments
                {
                    FileName = attachmentNames[i],
                    FileSize = attachmentSizes[i],
                    EmailId = emailId
                };

                this.context.EmailAttachments.Add(newAttachment);
            }
            await this.context.SaveChangesAsync();
        }

        public async Task<ICollection<EmailAttachmentsDTO>> GetEmailAttachmentsByEmailIdAsync(string emailId)
        {
            var allAttachmentsForSpecifiedEmail = await this.context.EmailAttachments.Where(ea => ea.EmailId == emailId).ToListAsync();

            return allAttachmentsForSpecifiedEmail.ToDTO();
        }
    }
}
