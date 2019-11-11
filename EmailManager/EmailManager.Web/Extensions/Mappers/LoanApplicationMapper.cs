using EmailManager.Service.DTOs;
using EmailManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Web.Extensions.Mappers
{
    public static class LoanApplicationMapper
    {
        public static LoanApplicationViewModel ToVM(this LoanApplicationDTO dataDTO)
        {
            var dataModel = new LoanApplicationViewModel
            {
                Id = dataDTO.Id,
                FirstName = dataDTO.ClientFristName,
                LastName = dataDTO.ClientLastName,
                EGN = dataDTO.ClientEGN,
                Phone = dataDTO.ClientPhone,
                Amount = dataDTO.LoanAmount,
                EmailId = dataDTO.EmailId,
                ApplicationStatusId=dataDTO.ApplicationStatusId,
                //ApplicationStatusName=dataDTO.ApplicationStatusName
            };

            return dataModel;
        }

        public static ICollection<LoanApplicationViewModel> ToVM(this ICollection<LoanApplicationDTO> dataDTOs)
        {
            return dataDTOs.Select(e => e.ToVM()).ToList();
        }
    }
}
