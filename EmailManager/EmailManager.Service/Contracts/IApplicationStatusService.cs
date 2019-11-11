using EmailManager.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Service.Contracts
{
    public interface IApplicationStatusService
    {
        Task<StatusApplicationDTO> CreateApplicationStatusAsync(string statusApplication);
        Task<StatusApplicationDTO> GetApplicationStatusByIdAsync(string statusApplicationId);
        Task<StatusApplicationDTO> UpdateApplicationStatusAsync(LoanApplicationDTO loanApplication, string newStatusApplication);
        Task<ICollection<StatusApplicationDTO>> AllApplicationStatusAsync();
    }
}
