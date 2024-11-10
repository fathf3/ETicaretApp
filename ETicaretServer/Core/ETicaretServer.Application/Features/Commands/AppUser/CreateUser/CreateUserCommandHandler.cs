using ETicaretServer.Application.Abstractions.Services;
using ETicaretServer.Application.DTOs.User;
using ETicaretServer.Application.Exceptions.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponse response =  await _userService.CreateAsync(new()
            {
                Email = request.Email,
                NameSurname = request.NameSurname,
                Password = request.Password,
                Username = request.Username,
                PasswordConfirm = request.PasswordConfirm,
            });
            return new()
            {
                Message = response.Message,
                Succeed = response.Succeed,
            };
        }
    }
}
