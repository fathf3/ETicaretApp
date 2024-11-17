using ETicaretServer.Application.RequestParameters;
using MediatR;

namespace ETicaretServer.Application.Features.Queries.User.GetAllUsers
{
    public class GetAllUsersQueryRequest : IRequest<GetAllUsersQueryResponse>
    {

        public int Page { get; set; } = 0;
        public int Size { get; set; } = 10;
    }
}
