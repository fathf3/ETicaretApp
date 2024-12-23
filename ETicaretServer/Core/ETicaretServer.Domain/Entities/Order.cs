﻿using ETicaretServer.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        
        public string Address { get; set; }
        public string OrderCode { get; set; }
        // public Guid CustomerId { get; set; } 
        // public Customer Customer { get; set; }
        public Basket Basket { get; set; }
        public ComplatedOrder ComplatedOrder { get; set; }
        //public ICollection<Product> Products { get; set; }

    }
}
