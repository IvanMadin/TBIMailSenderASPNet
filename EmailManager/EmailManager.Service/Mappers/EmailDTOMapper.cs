using EmailManager.Data.Entities;
using EmailManager.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmailManager.Service.Mappers
{
    public static class EmailDTOMapper
    {
        public static EmailDTO ToDTO(this Email entity)
        {
            if(entity is null)
            {
                return null;
            }

            var email = new EmailDTO
            {
                Id = entity.Id,
                OriginalMailId = entity.OriginalMailId,
                Subject = entity.Subject,
                Sender = entity.Sender,
                DateReceived = entity.DateReceived,
                Body = entity.Body
            };

            return email;
        }

        public static ICollection<EmailDTO> ToDTO(this ICollection<Email> emails)
        {
            return emails.Select(e => e.ToDTO()).ToList();
        }
    }
}
