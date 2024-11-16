using ETicaretServer.Application.DTOs.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.Configurations
{
    public interface IApplicationService
    {
        List<Menu> GetAuthorizeDefinitionEndPoints(Type type);
    }
}
