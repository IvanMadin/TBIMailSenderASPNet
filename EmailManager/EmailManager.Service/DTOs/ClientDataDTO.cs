﻿using EmailManager.Data.Entities.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Service.DTOs
{
    public class ClientDataDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EGN { get; set; }
        public string Phone { get; set; }
        public string OperatorId { get; set; }
    }
}
