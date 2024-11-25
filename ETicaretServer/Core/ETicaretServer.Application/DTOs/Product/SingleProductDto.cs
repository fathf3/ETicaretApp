using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.DTOs.Product
{
    public class SingleProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public object Images { get; set; }
       
    }
}
