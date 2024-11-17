using ETicaretServer.Application.Abstractions.Services;
using ETicaretServer.Application.CustomAttributes;
using ETicaretServer.Application.Enums;
using ETicaretServer.Application.Features.Commands.AppUser.AssignRoleToUser;
using ETicaretServer.Application.Features.Commands.AppUser.CreateUser;
using ETicaretServer.Application.Features.Commands.AppUser.UpdatePassword;
using ETicaretServer.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint;
using ETicaretServer.Application.Features.Queries.User.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IMailService mailService;

        public UsersController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            this.mailService = mailService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response =  await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody]UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
            return Ok(response);

        }
        [HttpGet]
        [Authorize(AuthenticationSchemes ="Admin")]
        [AuthorizeDefinition(Menu = "Users", ActionType = ActionType.Reading, Definition = "Get All Users")]
        public async Task<IActionResult> GetAllUsers([FromQuery]GetAllUsersQueryRequest getAllOrderQueryRequest)
        {
            GetAllUsersQueryResponse response = await _mediator.Send(getAllOrderQueryRequest);

            return Ok(response);
        }
        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = "Users", ActionType = ActionType.Writing, Definition = "Assign Role To User")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest  assignRoleToUserCommandRequest)
        {
            AssignRoleToUserCommandResponse response = await _mediator.Send(assignRoleToUserCommandRequest);
            return Ok(response);
        }
    }
}
