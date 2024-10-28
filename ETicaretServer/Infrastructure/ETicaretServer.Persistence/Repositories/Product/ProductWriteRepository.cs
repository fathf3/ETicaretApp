using ETicaretServer.Application.Repositories;
using ETicaretServer.Domain.Entities;
using ETicaretServer.Persistence.Contexts;

namespace ETicaretServer.Persistence.Repositories
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
