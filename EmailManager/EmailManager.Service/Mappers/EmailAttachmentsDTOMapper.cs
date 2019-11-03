using EmailManager.Data.Entities;
using EmailManager.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Service.Mappers
{
    public static class EmailAttachmentsDTOMapper
    {
        public static EmailAttachmentsDTO ToDTO(this EmailAttachments entity)
        {
            if (entity is null)
                return null;

            var emailAttachment = new EmailAttachmentsDTO
            {
                Id = entity.Id,
                EmailId = entity.EmailId,
                FileName = entity.FileName,
                FileSize = entity.FileSize
            };

            return emailAttachment;
        }
    }
}
