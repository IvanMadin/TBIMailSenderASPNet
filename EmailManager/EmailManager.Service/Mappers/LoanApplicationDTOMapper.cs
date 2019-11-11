using EmailManager.Data.Entities;
using EmailManager.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmailManager.Service.Mappers
{
    public static class LoanApplicationDTOMapper
    {
        public static LoanApplicationDTO ToDTO(this LoanApplication entity)
        {
            if (entity is null)
            {
                return null;
            }

            var loanApplication = new LoanApplicationDTO
            {
                Id = entity.Id,
                LoanAmount = entity.Amount,
                ClientEGN = entity.ClientData?.EGN,
                ClientFristName = entity.ClientData?.FirstName,
                ClientLastName = entity.ClientData?.LastName,
                ClientPhone = entity.ClientData?.Phone,
                EmployeeName = entity.User?.UserName,
                EmailId = entity.EmailId,
                ApplicationStatusId = entity.StatusApplicationId,
                //ApplicationStatusName=entity.StatusApplication.StatusType
            };

            return loanApplication;
        }

        public static ICollection<LoanApplicationDTO> ToDTO(this ICollection<LoanApplication> loanApplications)
        {
            return loanApplications.Select(s => s.ToDTO()).ToList();
        }
    }
}
