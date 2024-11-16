using ETicaretServer.Domain.Entities.Common;
using ETicaretServer.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Domain.Entities
{
    public class EndPoint : BaseEntity
    {
        public EndPoint()
        {
            AppRoles = new HashSet<AppRole>();
        }
        public string ActionType { get; set; }
        public string HttpType { get; set; }
        public string Definition { get; set; }
        public string Code { get; set; }
        public Menu Menu { get; set; }

        public ICollection<AppRole> AppRoles { get; set; }
    }
}
