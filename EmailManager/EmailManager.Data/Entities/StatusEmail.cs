using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities
{
    public class StatusEmail
    {
        public string Id { get; set; }
        public string StatusType { get; set; }
        public ICollection<ClientEmail> Emails { get; set; }
    }
}
