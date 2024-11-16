using ETicaretServer.Application.Consts;
using ETicaretServer.Application.CustomAttributes;
using ETicaretServer.Application.Enums;
using ETicaretServer.Application.Features.Commands.Order.CompleteOrder;
using ETicaretServer.Application.Features.Commands.Order.CreateOrder;
using ETicaretServer.Application.Features.Queries.Order.GetAllOrders;
using ETicaretServer.Application.Features.Queries.Order.GetOrderById;
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

        [HttpGet("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitioonConstants.Orders, ActionType = ActionType.Reading, Definition = "Get Order by Id")]
        public async Task<IActionResult> GetOrderById([FromRoute]GetOrderByIdQueryRequest getOrderByIdQueryRequest)
        {
            GetOrderByIdQueryResponse response = await _mediator.Send(getOrderByIdQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitioonConstants.Orders, ActionType = ActionType.Reading, Definition = "Get All Order")]
        public async Task<IActionResult> GetAllOrders(GetAllOrderQueryRequest getAllOrderQueryRequest)
        {
            GetAllOrderQueryResponse response = await _mediator.Send(getAllOrderQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitioonConstants.Orders, ActionType = ActionType.Writing, Definition = "Create Order")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
        {

            CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);
            return Ok(response);

        }

        [HttpGet("complete-order/{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitioonConstants.Orders, ActionType = ActionType.Updating, Definition = "Complete Order")]
        public async Task<IActionResult> CompleteOrder([FromRoute]CompleteOrderCommandRequest completeOrderCommandRequest)
        {
            CompleteOrderCommandResponse response =  await _mediator.Send(completeOrderCommandRequest);
            return Ok(response);
        }

    }
}
