using EmailManager.Data.Entities;
using EmailManager.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Service.Mappers
{
    public static class UserDTOMapper
    {
        public static UserDTO ToDTO(this User entity)
        {
            if(entity is null)
            {
                return null;
            }

            var user = new UserDTO
            {
                Id = entity.Id,
                UserName = entity.UserName,
                Email = entity.Email,
                ChangedPassword = entity.ChangedPassword
            };

            return user;
        }
    }
}
