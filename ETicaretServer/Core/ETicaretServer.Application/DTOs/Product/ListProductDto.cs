using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.DTOs.Product
{
    public class ListProductDto
    {
        public int TotalProductCount { get; set; }
        public object Products { get; set; }
    }
}
