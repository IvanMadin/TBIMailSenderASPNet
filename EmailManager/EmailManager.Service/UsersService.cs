using EmailManager.Data;
using EmailManager.Data.Entities;
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
        private readonly UserManager<User> userManager;

        public UsersService(EmailManagerDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<UserDTO> CheckUserCredentialsAsync(string userName, string password)
        {
            var user = await this.userManager.FindByNameAsync(userName);

            if(user is null)
            {
                //TODO: Have to add SeriLogging.
                throw new ArgumentNullException("Invalid account details");
            }

            var validPassword = userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (validPassword == PasswordVerificationResult.Failed)
            {
                //TODO: Have to add SeriLogging.
                throw new ArgumentNullException("Invalid account details");
            }

            var userDTO = user.ToDTO();
            userDTO.Password = password;

            return userDTO;
        }

        public async Task<UserDTO> GetUserByIdAsync(string userId)
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
