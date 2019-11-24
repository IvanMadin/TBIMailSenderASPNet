using EmailManager.Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailManager.Service.Contracts
{
    public interface IAttachmentsService
    {
        Task CreateAsync(string emailId, List<string> attachmentNames, List<double> attachmentSizes);
        Task<ICollection<EmailAttachmentsDTO>> GetEmailAttachmentsByEmailIdAsync(string emailId);
    }
}