using EmailManager.Data.Entities;
using EmailManager.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmailManager.Service.Mappers
{
    public static class StatusEmailDTOMapper
    {
        public static StatusEmailDTO ToDTO(this StatusEmail entity)
        {
            if(entity is null)
            {
                return null;
            }

            var statusEmail = new StatusEmailDTO
            {
                Id = entity.Id,
                StatusType = entity.StatusType
            };

            return statusEmail;
        }

        public static ICollection<StatusEmailDTO> ToDTO(this ICollection<StatusEmail> statusEmail)
        {
            return statusEmail.Select(s => s.ToDTO()).ToList();
        }
    }
}
