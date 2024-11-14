using ETicaretServer.Domain.Entities.Common;
using ETicaretServer.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public string UserId { get; set; }
        
        public AppUser User { get; set; }
        public Order Order { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; }

    }
}
