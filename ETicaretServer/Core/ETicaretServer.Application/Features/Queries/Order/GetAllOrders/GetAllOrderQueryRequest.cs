using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.Features.Queries.Order.GetAllOrders
{
    public class GetAllOrderQueryRequest : IRequest<List<GetAllOrderQueryResponse>>
    {
    }
}
