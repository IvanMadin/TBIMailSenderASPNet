using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities.BaseProperties
{
    public abstract class ModifierEntityProperties : ICreated, IModified, IDeleted
    {
        public string CreatedByUserId { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public DateTime? ModifiedOnDate { get; set; }
        public string ModifiedByUserId { get; set; }
        public string DeletedByUserId { get; set; }
        public DateTime? DeletedOnDate { get; set; }
    }
}
