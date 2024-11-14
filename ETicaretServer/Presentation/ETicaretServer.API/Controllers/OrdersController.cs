using ETicaretServer.Application.Features.Commands.Order;
using ETicaretServer.Application.Features.Queries.Order.GetAllOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : ControllerBase
    {
        readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders(GetAllOrderQueryRequest getAllOrderQueryRequest)
        {
            List<GetAllOrderQueryResponse> response = await _mediator.Send(getAllOrderQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateOrderCommandRequest createOrderCommandRequest)
        {

            CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);
            return Ok(response);

        }



    }
}
