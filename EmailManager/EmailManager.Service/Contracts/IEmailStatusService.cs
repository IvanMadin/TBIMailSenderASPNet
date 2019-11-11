using EmailManager.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Service.Contracts
{
    public interface IEmailStatusService
    {
        Task<StatusEmailDTO> CreateStatusEmailAsync(string statusEmail);
        Task<StatusEmailDTO> GetEmailStatusByIdAsync(string emailStatusId);
        Task<StatusEmailDTO> UpdateEmailStatusAsync(EmailDTO emailDTO, string newStatusEmail);
        Task<ICollection<EmailDTO>> GetAllEmailByStatusIdAsync(string emailStatusId);
        Task<ICollection<StatusEmailDTO>> AllEmailStatusAsync();
    }
}
