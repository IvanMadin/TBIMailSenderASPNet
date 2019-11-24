using System.Collections.Generic;
using System.Threading.Tasks;
using EmailManager.Service.DTOs;

namespace EmailManager.Service.Contracts
{
    public interface IRolesService
    {
        Task<ICollection<RoleDTO>> GetRolesAsync();
    }
}