using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Service.DTOs
{
    public class EmailAttachmentsDTO
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public double FileSize { get; set; }
        public string EmailId { get; set; }
    }
}
