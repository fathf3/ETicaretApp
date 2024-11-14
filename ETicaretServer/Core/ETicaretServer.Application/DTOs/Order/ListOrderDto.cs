using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.DTOs.Order
{
    public class ListOrderDto
    {
        public string OrderCode { get; set; }
        public string Username { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
