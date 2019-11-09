using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities.BaseProperties
{
    public interface ICreated
    {
        string CreatedByUserId { get; set; }
        DateTime? CreatedOnDate { get; set; }
    }
}
