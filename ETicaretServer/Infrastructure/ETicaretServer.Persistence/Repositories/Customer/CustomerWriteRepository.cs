using ETicaretServer.Application.Repositories;
using ETicaretServer.Domain.Entities;
using ETicaretServer.Persistence.Contexts;

namespace ETicaretServer.Persistence.Repositories
{
    public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
