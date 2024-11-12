using ETicaretServer.Application.Abstractions.Services;
using ETicaretServer.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.Features.Commands.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginLoginCommandHandler : IRequestHandler<RefreshTokenLoginLoginCommandRequest, RefreshTokenLoginLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public RefreshTokenLoginLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenLoginLoginCommandResponse> Handle(RefreshTokenLoginLoginCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
            return new()
            {
                Token = token,
            };

        }
    }
}
