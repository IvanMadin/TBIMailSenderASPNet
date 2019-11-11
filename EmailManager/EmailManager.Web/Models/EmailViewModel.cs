using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Web.Models
{
    public class EmailViewModel
    {
        public string Id { get; set; }
        public string OriginalMailId { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string DateReceived { get; set; }
        public string Body { get; set; }
        public ICollection<AttachmentViewModel> Attachments { get; set; } = new List<AttachmentViewModel>();
        public string EmailStatusId { get; set; }
        public string EmailStatusName { get; set; }
    }
}
