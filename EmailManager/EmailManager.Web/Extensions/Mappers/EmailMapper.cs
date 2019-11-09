using EmailManager.Service.DTOs;
using EmailManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Web.Extensions.Mappers
{
    public static class EmailMapper
    {
        public static EmailViewModel ToVM(this EmailDTO emailDTO)
        {
            var emailModel = new EmailViewModel
            {
                Body = emailDTO.Body,
                DateReceived = emailDTO.DateReceived,
                Id = emailDTO.Id,
                OriginalMailId = emailDTO.OriginalMailId,
                Sender = emailDTO.Sender,
                Subject = emailDTO.Subject
            };

            return emailModel;
        }

        public static ICollection<EmailViewModel> ToVM(this ICollection<EmailDTO> emailDTOs)
        {
            return emailDTOs.Select(e => e.ToVM()).ToList();
        }
    }
}
