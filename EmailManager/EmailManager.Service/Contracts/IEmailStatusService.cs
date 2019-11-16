using EmailManager.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Service.Contracts
{
    public interface IEmailStatusService
    {
        Task<StatusEmailDTO> GetEmailStatusByNameAsync(string emailStatusName);
        Task<StatusEmailDTO> GetEmailStatusByIdAsync(string emailStatusId);
    }
}
