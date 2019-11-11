using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service.Contracts;
using EmailManager.Service.DTOs;
using EmailManager.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

namespace EmailManager.Service
{
    public class ClientService : IClientService
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

            Log.Information("Client data with EGN: {0} was successfully added", clientData.EGN);

            await this.context.SaveChangesAsync();
            Log.Information("Client data with EGN: {0} was created by operator with ID: {1} on {2}", clientDataDTO.EGN, clientDataDTO.OperatorId, DateTime.Now);

            return clientData.ToDTO();
        }

        public async Task<ClientDataDTO> GetClientDataByIdAsync(string clientId)
        {
            var clientData = await this.context.ClientDatas.FindAsync(clientId);
            Log.Information("Client data with ID: {0} was found", clientData.Id, DateTime.Now);

            return clientData.ToDTO();
        }

        public async Task<ClientDataDTO> FindClientAsync(string firstName, string lastName, string egn)
        {
            var client = await this.context.ClientDatas
                 .FirstOrDefaultAsync(cd => cd.FirstName == firstName && cd.LastName == lastName && cd.EGN == egn);
            Log.Information("Client data with EGN: {0} was found", client.EGN, DateTime.Now);

            return client.ToDTO();
        }

    }
}
