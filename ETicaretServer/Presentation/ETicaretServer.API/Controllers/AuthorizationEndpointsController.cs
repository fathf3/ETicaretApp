using ETicaretServer.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint;
using ETicaretServer.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthorizationEndpointsController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthorizationEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetRolesToEndpoint(GetRolesToEndpointQueryRequest getRolesToEndpoint)
        {
            GetRolesToEndpointQueryResponse response =  await _mediator.Send(getRolesToEndpoint);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointRequest assignRoleEndpointRequest)
        {
            assignRoleEndpointRequest.Type = typeof(Program);
            AssignRoleEndpointResponse response =  await _mediator.Send(assignRoleEndpointRequest);
            return Ok(response);
        }



    }
}
