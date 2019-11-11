using EmailManager.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Service.Contracts
{
    public interface IClientService
    {
        Task<ClientDataDTO> CreateClientData(ClientDataDTO clientDataDTO);
        Task<ClientDataDTO> GetClientDataByIdAsync(string clientId);
        Task<ClientDataDTO> FindClientAsync(string firstName, string lastName, string egn);
    }
}
