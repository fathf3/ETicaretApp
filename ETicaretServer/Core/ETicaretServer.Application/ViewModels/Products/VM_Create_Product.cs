﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.ViewModels.Products
{
    public class VM_Create_Product
    {
        public  string Name { get; set; }
        public long Price { get; set; }
        public int Stock { get; set; }
       
    }
}
