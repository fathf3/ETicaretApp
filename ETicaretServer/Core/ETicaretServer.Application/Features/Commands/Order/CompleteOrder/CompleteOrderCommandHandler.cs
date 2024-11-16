using ETicaretServer.Application.Abstractions.Services;
using ETicaretServer.Application.DTOs.Order;
using MediatR;

namespace ETicaretServer.Application.Features.Commands.Order.CompleteOrder
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IMailService _mailService;

        public CompleteOrderCommandHandler(IOrderService orderService, IMailService mailService)
        {
            _orderService = orderService;
            _mailService = mailService;
        }

        public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            (bool succeeded,CompletedOrderDto dto) result =  await _orderService.CompletedOrderAsync(request.Id);
            if (result.succeeded)
            {
                _mailService.SendCompletedOrderMailAsync(result.dto);
            }

            return new();
        }
    }
}
