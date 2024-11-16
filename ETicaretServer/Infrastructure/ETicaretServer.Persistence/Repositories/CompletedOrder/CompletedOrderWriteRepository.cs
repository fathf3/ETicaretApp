using ETicaretServer.Application.Repositories;
using ETicaretServer.Domain.Entities;
using ETicaretServer.Persistence.Contexts;

namespace ETicaretServer.Persistence.Repositories
{
    public class CompletedOrderWriteRepository : WriteRepository<ComplatedOrder>, ICompletedOrdeWriteRepository
    {
        public CompletedOrderWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
