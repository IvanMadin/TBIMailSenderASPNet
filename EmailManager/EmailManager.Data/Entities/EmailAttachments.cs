using EmailManager.Data.Entities.BaseProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities
{
    public class EmailAttachments : IDeleted
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public double FileSize { get; set; }

        public string EmailId { get; set; }
        public ClientEmail Email { get; set; }
        public string DeletedByUserId { get; set; }
        public DateTime? DeletedOnDate { get; set; }
    }
}
