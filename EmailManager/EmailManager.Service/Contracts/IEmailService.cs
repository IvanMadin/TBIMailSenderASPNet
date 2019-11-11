using EmailManager.Data.Entities;
using EmailManager.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Service.Contracts
{
    public interface IEmailService
    {
        Task<EmailDTO> CreateAsync(string originalMailId, string senderName, string senderEmail, string dateReceived, string subject, string body);
        Task<EmailDTO> GetEmailByIdAsync(string emailId);
        Task<ClientEmail> GetEmailByOriginalIdAsync(string originalMailId);
        Task<ICollection<EmailDTO>> GetAllEmailsAsync();
        Task<bool> CheckIfEmailExists(string originalMailId);
    }
}
