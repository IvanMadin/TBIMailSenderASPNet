using EmailManager.Data.Entities;
using EmailManager.Data.Entities.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmailManager.Service.DTOs.ToEntities
{
    public static class LoanApplicationEntityMapper
    {
        public static LoanApplication ToEntity(this LoanApplicationDTO dto)
        {
            if (dto is null)
                return null;

            var entity = new LoanApplication
            {
                Id = dto.Id,
                Amount = dto.LoanAmount,
                ApplicationStatus = (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), dto.ApplicationStatusName),
                EmailId = dto.EmailId,
            };
            return entity;
        }
        public static ICollection<LoanApplication> ToEntity(this ICollection<LoanApplicationDTO> dtos)
        {
            return dtos.Select(l => l.ToEntity()).ToList();
        }
    }
}
