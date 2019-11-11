using EmailManager.Service.DTOs;
using EmailManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Web.Extensions.Mappers
{
    public static class EmailStatusMapper
    {
        public static EmailStatusViewModel ToVM(this StatusEmailDTO emailStatusDTO)
        {
            var status = new EmailStatusViewModel
            {
                Id = emailStatusDTO.Id,
                StatusType = emailStatusDTO.StatusType
            };

            return status;
        }

        public static ICollection<EmailStatusViewModel> ToVM(this ICollection<StatusEmailDTO> emailStatusDTO)
        {
            return emailStatusDTO.Select(e => e.ToVM()).ToList();
        }
    }
}
