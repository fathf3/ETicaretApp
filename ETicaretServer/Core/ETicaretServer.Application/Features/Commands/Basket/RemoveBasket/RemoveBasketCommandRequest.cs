using MediatR;

namespace ETicaretServer.Application.Features.Commands.Basket.RemoveBasket
{
    public class RemoveBasketCommandRequest : IRequest<RemoveBasketCommandResponse> 
    {
        public string BasketItemId { get; set; }
    }
}
