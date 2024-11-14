using ETicaretServer.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task<List<ListOrderDto>> GetAllOrdersAsync();
        Task CreateOrderAsync(CreateOrderDto createOrderDto);
    }
}
