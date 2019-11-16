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
        Task<EmailDTO> CreateAsync(string originalMailId, string senderName, string senderEmail, DateTime dateReceived, string subject, string body);

        /// <summary>
        /// Get Emails By Id.
        /// </summary>
        Task<EmailDTO> GetEmailByIdAsync(string emailId);

        /// <summary>
        /// Get All Emails - No matter of the statuses.
        /// </summary>
        Task<ICollection<EmailDTO>> GetAllEmailsAsync();

        /// <summary>
        /// Get all emails by the given StatusName.
        /// </summary>
        Task<ICollection<EmailDTO>> GetAllEmailsByStatusNameAsync(string statusName);

        /// <summary>
        /// Updating Email Status.
        /// </summary>
        Task<EmailDTO> UpdateEmailStatus(EmailDTO emailDTO, StatusEmailDTO newEmailStatus, string userId);


        /// <summary>
        /// Helper Method for quick checks
        /// </summary>
        Task<bool> CheckIfEmailExists(string originalMailId);
    }
}
