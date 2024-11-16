using ETicaretServer.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Domain.Entities
{
    public class ComplatedOrder : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

    }
}
