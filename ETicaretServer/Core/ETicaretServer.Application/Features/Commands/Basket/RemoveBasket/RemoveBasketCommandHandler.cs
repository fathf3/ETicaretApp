using ETicaretServer.Application.Abstractions.Services;
using MediatR;

namespace ETicaretServer.Application.Features.Commands.Basket.RemoveBasket
{
    public class RemoveBasketCommandHandler : IRequestHandler<RemoveBasketCommandRequest, RemoveBasketCommandResponse>
    {
        readonly IBasketService _basketService;

        public RemoveBasketCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<RemoveBasketCommandResponse> Handle(RemoveBasketCommandRequest request, CancellationToken cancellationToken)
        {
            await _basketService.RemoveBasketItemsAsync(request.BasketItemId);
            return new();
        }
    }
}
