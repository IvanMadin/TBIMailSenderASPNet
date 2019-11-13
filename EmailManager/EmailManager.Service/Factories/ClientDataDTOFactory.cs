using EmailManager.Data.Entities;
using EmailManager.Data.Entities.Types;
using EmailManager.Service.Contracts.Factories;
using EmailManager.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Service.Factories
{
    public class ClientDataFactory : IClientDataFactory
    {
        public ClientDataDTO Create(string firstName, string lastName, string egn, string phone, string operatorId)
        {
            var newClientDataDTO = new ClientDataDTO
            {
                FirstName = firstName,
                LastName = lastName,
                EGN = egn,
                Phone = phone,
                OperatorId = operatorId
            };

            return newClientDataDTO;
        }

        public ClientData Create(ClientDataDTO clientDataDTO)
        {
            var newClientData = new ClientData
            {
                FirstName = clientDataDTO.FirstName,
                LastName = clientDataDTO.LastName,
                EGN = clientDataDTO.EGN,
                Phone = clientDataDTO.Phone,
                CreatedByUserId = clientDataDTO.OperatorId
            };

            return newClientData;
        }
    }
}
