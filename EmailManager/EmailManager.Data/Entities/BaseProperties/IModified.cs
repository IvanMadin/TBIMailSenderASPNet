using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities.BaseProperties
{
    public interface IModified
    {
        string ModifiedByUserId { get; set; }
        DateTime? ModifiedOnDate { get; set; }
    }
}
