using ETicaretServer.Application.Abstractions.Services;
using ETicaretServer.Application.Configurations;
using ETicaretServer.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint
{
    public class AssignRoleEndpointHandler : IRequestHandler<AssignRoleEndpointRequest, AssignRoleEndpointResponse>
    {
       readonly IAuthorizationEndpointService _authorizationEndpointService;

        public AssignRoleEndpointHandler(IAuthorizationEndpointService authorizationEndpointService)
        {
            _authorizationEndpointService = authorizationEndpointService;
        }

        public async Task<AssignRoleEndpointResponse> Handle(AssignRoleEndpointRequest request, CancellationToken cancellationToken)
        {
            await _authorizationEndpointService.AssignRoleEndpointAsync(request.Roles, request.Menu, request.EndpointCode, request.Type);
            return new() { };
        }
    }
}
