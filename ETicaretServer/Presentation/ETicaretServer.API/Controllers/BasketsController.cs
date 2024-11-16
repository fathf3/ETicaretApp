using ETicaretServer.Application.Consts;
using ETicaretServer.Application.CustomAttributes;
using ETicaretServer.Application.Enums;
using ETicaretServer.Application.Features.Commands.Basket.AddItemToBasket;
using ETicaretServer.Application.Features.Commands.Basket.RemoveBasket;
using ETicaretServer.Application.Features.Commands.Basket.UpdateQuantity;
using ETicaretServer.Application.Features.Queries.Basket.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class BasketsController : ControllerBase
    {
        readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitioonConstants.Baskets, ActionType = ActionType.Reading, Definition = "Get Basket Items")]
        public async Task<IActionResult> GetBasketItem([FromQuery]GetBasketItemsQueryRequest getBasketItemsQueryRequest)
        {
            List<GetBasketItemsQueryResponse> response = await _mediator.Send(getBasketItemsQueryRequest);
            return Ok(response);
        }
        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitioonConstants.Baskets, ActionType = ActionType.Writing, Definition = "Add Item to Basket")]
        public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
        {
            AddItemToBasketCommandResponse response = await _mediator.Send(addItemToBasketCommandRequest);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(Menu = AuthorizeDefinitioonConstants.Baskets, ActionType = ActionType.Updating, Definition = "Update Quantity")]
        public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest updateQuantityCommandRequest)
        {
            UpdateQuantityCommandResponse response = await _mediator.Send(updateQuantityCommandRequest);
            return Ok(response);
        }
        [HttpDelete("{BasketItemId}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitioonConstants.Baskets, ActionType = ActionType.Deleting, Definition = "Remove Basket Items")]
        public async Task<IActionResult> RemoveBasketItem([FromRoute]RemoveBasketCommandRequest removeBasketCommandRequest)
        {
            RemoveBasketCommandResponse response = await _mediator.Send(removeBasketCommandRequest);
            return Ok(response);
        }






    }
}
