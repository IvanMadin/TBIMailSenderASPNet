using EmailManager.Data;
using EmailManager.Service.Contracts;
using EmailManager.Service.DTOs;
using EmailManager.Service.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Service
{
    public class UsersService : IUsersService, IRolesService
    {
        private readonly EmailManagerDbContext context;

        public UsersService(EmailManagerDbContext context)
        {
            this.context = context;
        }
        public async Task<UserDTO> GetUserById(string userId)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            return user.ToDTO();
        }

        public async Task<ICollection<RoleDTO>> GetRolesAsync()
        {
            var roles = await this.context.Roles.ToListAsync();

            return roles.ToDTO();
        }
    }
}
