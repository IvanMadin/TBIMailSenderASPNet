using EmailManager.Data.Entities;
using EmailManager.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Service.Mappers
{
    public static class ClientDataDTOMapper
    {
        public static ClientDataDTO ToDTO(this ClientData entity)
        {
            if (entity is null)
                return null;

            var clientData = new ClientDataDTO
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                EGN = entity.EGN,
                Phone = entity.Phone
            };

            return clientData;
        }
    }
}
