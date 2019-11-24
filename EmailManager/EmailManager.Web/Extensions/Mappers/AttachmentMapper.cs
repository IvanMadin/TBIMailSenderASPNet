using EmailManager.Service.DTOs;
using EmailManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Web.Extensions.Mappers
{
    public static class AttachmentMapper
    {
        public static AttachmentViewModel ToVM(this EmailAttachmentsDTO attachmentDTO)
        {
            var model = new AttachmentViewModel
            {
                Id = attachmentDTO.Id,
                FileName = attachmentDTO.FileName,
                FileSize = attachmentDTO.FileSize,
                EmailId = attachmentDTO.EmailId
            };

            return model;
        }
        public static ICollection<AttachmentViewModel> ToVM(this ICollection<EmailAttachmentsDTO> attachments)
        {
            return attachments.Select(a => a.ToVM()).ToList();
        }
    }
}
