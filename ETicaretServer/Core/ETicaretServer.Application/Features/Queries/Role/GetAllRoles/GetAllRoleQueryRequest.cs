using MediatR;

namespace ETicaretServer.Application.Features.Queries.Role.GetAllRoles
{
    public class GetAllRoleQueryRequest : IRequest<GetAllRoleQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
