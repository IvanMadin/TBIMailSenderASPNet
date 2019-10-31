using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities
{
    public class EmailAttachments
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public double FileSize { get; set; }

        public string EmailId { get; set; }
        public Email Email { get; set; }
    }
}
