using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Service.DTOs
{
    public class EmailDTO
    {
        public string Id { get; set; }
        public string OriginalMailId { get; set; }
        public string Sender { get; set; }
        public string DateReceived { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string EmailStatusId { get; set; }
        //public string EmailStatusName { get; set; }
    }
}
