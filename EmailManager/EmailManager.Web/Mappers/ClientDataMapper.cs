using EmailManager.Service.DTOs;
using EmailManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Web.Mappers
{
    public static class ClientDataMapper
    {
        public static ClientDataViewModel ToVM(this ClientDataDTO emailDTO)
        {
            var clientModel = new ClientDataViewModel
            {
                Id=emailDTO.Id,
                Names=emailDTO.FirstName,
                EGN=emailDTO.EGN,
                Phone=emailDTO.Phone,
            };

            return clientModel;
        }

        public static ICollection<ClientDataViewModel> ToVM(this ICollection<ClientDataDTO> clientDTOs)
        {
            return clientDTOs.Select(e => e.ToVM()).ToList();
        }
    }
}
