using EmailManager.Service.DTOs;
using EmailManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Web.Extensions.Mappers
{
    public static class ApplicationStatusMapper
    {
        public static ApplicationStatusViewModel ToVM(this StatusApplicationDTO applicationStatusDTO)
        {
            var status = new ApplicationStatusViewModel
            {
                Id = applicationStatusDTO.Id,
                StatusType = applicationStatusDTO.StatusType
            };

            return status;
        }

        public static ICollection<ApplicationStatusViewModel> ToVM(this ICollection<StatusApplicationDTO> applicationStatusDTO)
        {
            return applicationStatusDTO.Select(e => e.ToVM()).ToList();
        }
    }
}
