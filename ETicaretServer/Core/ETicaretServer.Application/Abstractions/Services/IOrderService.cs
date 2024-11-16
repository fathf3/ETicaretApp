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
        Task<ListOrderDto> GetAllOrdersAsync(int page, int size);
        Task CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<SingleOrderDto> GetOrderById(string id);
        Task<(bool, CompletedOrderDto)> CompletedOrderAsync(string id);
    }
}
