using ETicaretServer.Application.Abstractions.Services;
using ETicaretServer.Application.Consts;
using ETicaretServer.Application.CustomAttributes;
using ETicaretServer.Application.DTOs.Configurations;
using ETicaretServer.Application.Enums;
using ETicaretServer.Application.Features.Commands.Role.CreateRole;
using ETicaretServer.Application.Features.Commands.Role.DeleteRole;
using ETicaretServer.Application.Features.Commands.Role.UpdateRole;
using ETicaretServer.Application.Features.Queries.Role.GetAllRoles;
using ETicaretServer.Application.Features.Queries.Role.GetRoleById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RolesController : ControllerBase
    {
        readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitioonConstants.Roles, ActionType =ActionType.Reading, Definition ="Get Roles")]
        public async Task<IActionResult> GetRoles([FromQuery]GetAllRoleQueryRequest getAllRoleQueryRequest ) 
        {
            GetAllRoleQueryResponse response = await _mediator.Send(getAllRoleQueryRequest);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitioonConstants.Roles, ActionType = ActionType.Reading, Definition = "Get Roles by Id")]
        public async Task<IActionResult> GetRole([FromRoute]GetRoleByIdQueryRequest getRoleByIdQueryRequest)
        {
            GetRoleByIdQueryResponse response = await _mediator.Send(getRoleByIdQueryRequest);
            return Ok(response);
        }
        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitioonConstants.Roles, ActionType = ActionType.Writing, Definition = "Create Role")]
        public async Task<IActionResult> CreateRole([FromBody]CreateRoleCommandRequest createRoleCommandRequest)
        {
            CreateRoleCommandResponse response = await _mediator.Send(createRoleCommandRequest);
            return Ok(response);
        }
        [HttpPut("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitioonConstants.Roles, ActionType = ActionType.Updating, Definition = "Update Role" )]
        public async Task<IActionResult> UpdateRole([FromBody,FromRoute] UpdateRoleCommandRequest updateRoleCommandRequest)
        {
            UpdateRoleCommandResponse response = await _mediator.Send(updateRoleCommandRequest);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitioonConstants.Roles, ActionType = ActionType.Deleting, Definition = "Delete Role")]
        public async Task<IActionResult> DeleteRole([FromRoute]DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
            DeleteRoleCommandResponse response = await _mediator.Send(deleteRoleCommandRequest);
            return Ok(response);
        }



    }
}
