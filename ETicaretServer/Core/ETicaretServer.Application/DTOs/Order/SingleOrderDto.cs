﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.DTOs.Order
{
    public class SingleOrderDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string OrderCode { get; set; }
        public object BasketItems { get; set; }
        public DateTime CreatedDate { get; set; }
        

    }
}
