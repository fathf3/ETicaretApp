using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.Abstractions.Services
{
    public interface IRoleService
    { 
        object GetAllRoles(int page, int size);
        Task<(string id, string name)> GetRoleById(string id);
        Task<bool> CreateRole(string roleName);
        Task<bool> DeleteRole(string id);
        Task<bool> UpdateRole(string id, string rolName);

    }
}
