using EmailManager.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Service.Contracts
{
    public interface ILoanApplicationService
    {
        Task<LoanApplicationDTO> CreateLoanApplicationAsync(string clientId, string emailId, string operatorId, decimal amount);
        Task<LoanApplicationDTO> GetLoanApplicationByIdAsync(string applicationId);

    }
}
