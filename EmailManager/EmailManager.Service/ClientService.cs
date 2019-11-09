using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service.DTOs;
using EmailManager.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EmailManager.Service
{
    public class ClientService
    {
        private readonly EmailManagerDbContext context;

        public ClientService(EmailManagerDbContext context)
        {
            this.context = context;
        }

        public async Task<ClientDataDTO> CreateClientData(ClientDataDTO clientDataDTO)
        {
            var clientData = new ClientData
            {
                FirstName = clientDataDTO.FirstName,
                LastName = clientDataDTO.LastName,
                EGN = clientDataDTO.EGN,
                Phone = clientDataDTO.Phone,
                CreatedByUserId = clientDataDTO.OperatorId,
                CreatedOnDate = DateTime.UtcNow
            };

            this.context.ClientDatas.Add(clientData);
            await this.context.SaveChangesAsync();

            return clientData.ToDTO();
        }

        public async Task<ClientDataDTO> GetClientDataByIdAsync(string clientId)
        {
            var clientData = await this.context.ClientDatas.FindAsync(clientId);

            return clientData.ToDTO();
        }

        public async Task<ClientDataDTO> FindClientAsync(string firstName, string lastName, string egn)
        {
            var client = await this.context.ClientDatas
                 .FirstOrDefaultAsync(cd => cd.FirstName == firstName && cd.LastName == lastName && cd.EGN == egn);

            return client.ToDTO();
        }

    }
}
