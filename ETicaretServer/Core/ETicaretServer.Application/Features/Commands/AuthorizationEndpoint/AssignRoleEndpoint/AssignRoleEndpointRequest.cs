using MediatR;

namespace ETicaretServer.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint
{
    public class AssignRoleEndpointRequest : IRequest<AssignRoleEndpointResponse>
    {
        public string[] Roles { get; set; }
        public string EndpointCode { get; set; }
        public string Menu { get; set; }
        public Type Type { get; set; } = typeof(AssignRoleEndpointResponse);
    }
}
