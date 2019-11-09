using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities.BaseProperties
{
    public interface IDeleted
    {
        string DeletedByUserId { get; set; }
        DateTime? DeletedOnDate { get; set; }
    }
}
