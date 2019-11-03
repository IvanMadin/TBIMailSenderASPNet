using EmailManager.Data.Entities;
using EmailManager.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Service.Mappers
{
    public static class StatusApplicationDTOMapper
    {
        public static StatusApplicationDTO ToDTO(this StatusApplication entity)
        {
            if (entity is null)
            {
                return null;
            }

            var statusApplication = new StatusApplicationDTO
            {
                Id = entity.Id,
                StatusType = entity.StatusType
            };

            return statusApplication;
        }
    }
}
