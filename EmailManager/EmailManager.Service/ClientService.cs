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
        private readonly IValidation validation;
        private readonly IEncryptingHelper encrypting;

        public ClientService(EmailManagerDbContext context, IValidation validation, IEncryptingHelper encrypting)
        {
            this.context = context;
            this.validation = validation;
            this.encrypting = encrypting;
        }

        public async Task<ClientDataDTO> CreateClientData(ClientDataDTO clientDTO)
        {
            this.validation.IsNameInRange(clientDTO.FirstName);
            this.validation.IsNameInRange(clientDTO.LastName);
            this.validation.IsEGNInRange(clientDTO.EGN);
            this.validation.IsPhoneInRange(clientDTO.Phone);

            var clientData = new ClientData
            {
                FirstName = clientDTO.FirstName,
                LastName = this.encrypting.Encrypt(clientDTO.LastName),
                EGN = this.encrypting.Encrypt(clientDTO.EGN),
                Phone = this.encrypting.Encrypt(clientDTO.Phone),
                CreatedByUserId = clientDTO.OperatorId,
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

                var clientDTO = clientData.ToDTO();
                clientDTO.LastName = this.encrypting.Decrypt(clientData.LastName);
                clientDTO.EGN = this.encrypting.Decrypt(clientData.EGN);
                clientDTO.Phone = this.encrypting.Decrypt(clientData.Phone);
                return clientDTO;
            }
            return null;
        }

        public async Task<ClientDataDTO> FindClientAsync(string firstName, string lastName, string egn)
        {
            this.validation.IsNameInRange(firstName);
            this.validation.IsNameInRange(lastName);
            this.validation.IsEGNInRange(egn);
            var lasasd = this.encrypting.Encrypt(lastName);
            var egnasd = this.encrypting.Encrypt(egn);
            var client = await this.context.ClientDatas
                 .FirstOrDefaultAsync(cd => cd.FirstName == firstName 
                 && cd.LastName == lasasd
                 && cd.EGN == egnasd);

            if (client != null)
            {
                Log.Information($"{DateTime.Now} Find Client Data.");
                var clientDTO = client.ToDTO();
                clientDTO.LastName = this.encrypting.Decrypt(client.LastName);
                clientDTO.EGN = this.encrypting.Decrypt(client.EGN);
                clientDTO.Phone = this.encrypting.Decrypt(client.Phone);
                return clientDTO;
            }

            return null;
        }

    }
}
