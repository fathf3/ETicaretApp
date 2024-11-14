using ETicaretServer.Application.Abstractions.Services;
using ETicaretServer.Application.DTOs.Order;
using ETicaretServer.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var orderCode = (new Random().NextDouble() * 1000000).ToString();
            orderCode = orderCode.Substring(0, orderCode.IndexOf(","));
            await _orderWriteRepository.AddAsync(new()
            {

                Address = createOrderDto.Address,
                Id = Guid.Parse(createOrderDto.BasketId),
                Description = createOrderDto.Description,
                OrderCode = orderCode

            });
            await _orderWriteRepository.SaveAsync();
        }

        public async Task<List<ListOrderDto>> GetAllOrdersAsync()
        {
           return await _orderReadRepository.Table.Include(o => o.Basket)
                .ThenInclude(b => b.User)
                .Include(o => o.Basket)
                    .ThenInclude(b => b.BasketItems)
                    .ThenInclude(bi => bi.Product)
                .Select(o => new ListOrderDto
                {
                    CreatedDate = o.CreatedDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price*bi.Quantity),
                    Username = o.Basket.User.UserName,
                })
                .ToListAsync();

        }
    }
}
