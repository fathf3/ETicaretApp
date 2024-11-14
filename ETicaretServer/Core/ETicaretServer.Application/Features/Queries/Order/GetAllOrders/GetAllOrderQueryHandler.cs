using ETicaretServer.Application.Abstractions.Services;
using MediatR;

namespace ETicaretServer.Application.Features.Queries.Order.GetAllOrders
{
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQueryRequest, List<GetAllOrderQueryResponse>>
    {
        readonly IOrderService _orderService;

        public GetAllOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<List<GetAllOrderQueryResponse>> Handle(GetAllOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _orderService.GetAllOrdersAsync();
            return data.Select(o => new GetAllOrderQueryResponse
            {
                CreatedDate = o.CreatedDate,
                OrderCode = o.OrderCode,
                TotalPrice = o.TotalPrice,
                Username = o.Username,
            }).ToList();

        }
    }
}
