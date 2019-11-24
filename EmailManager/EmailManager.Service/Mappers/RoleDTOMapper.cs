using EmailManager.Service.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmailManager.Service.Mappers
{
    public static class RoleDTOMapper
    {
        public static RoleDTO ToDTO(this IdentityRole entity)
        {
            var newRole = new RoleDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
            return newRole;
        }
        public static ICollection<RoleDTO> ToDTO(this ICollection<IdentityRole> roles)
        {
            return roles.Select(r => r.ToDTO()).ToList();
        }
    }
}
