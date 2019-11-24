using EmailManager.Service.DTOs;
using EmailManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Web.Extensions.Mappers
{
    public static class RoleVMMapper
    {
        public static RoleViewModel ToVM(this RoleDTO roleDTO)
        {
            var newRole = new RoleViewModel
            {
                Id = roleDTO.Id,
                Name = roleDTO.Name
            };

            return newRole;
        }

        public static ICollection<RoleViewModel> ToVM(this ICollection<RoleDTO> roles)
        {
            return roles.Select(r => r.ToVM()).ToList();
        }
    }
}
