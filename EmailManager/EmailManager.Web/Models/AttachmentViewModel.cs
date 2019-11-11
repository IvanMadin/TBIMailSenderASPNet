using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmailManager.Web.Models
{
    public class AttachmentViewModel
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public double FileSize { get; set; }
    }
}
