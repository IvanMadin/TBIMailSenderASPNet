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
            await this.context.SaveChangesAsync();

            Log.Information($"{clientData.CreatedOnDate} Create Client Data by User Id: {clientData.CreatedByUserId}.");
            return clientData.ToDTO();
        }

        public async Task<ClientDataDTO> GetClientDataByIdAsync(string clientId)
        {
            var clientData = await this.context.ClientDatas.FindAsync(clientId);

            if (clientData != null)
            {
                Log.Information($"{DateTime.Now} Get Client Data with ID: {clientId} by User Id: {clientData.ModifiedByUserId}.");
            }

            return clientData.ToDTO();
        }

        public async Task<ClientDataDTO> FindClientAsync(string firstName, string lastName, string egn)
        {
            var client = await this.context.ClientDatas
                 .FirstOrDefaultAsync(cd => cd.FirstName == firstName && cd.LastName == lastName && cd.EGN == egn);

            if (client != null)
            {
                Log.Information($"{DateTime.Now} Find Client Data.");
            }

            return client.ToDTO();
        }

    }
}
