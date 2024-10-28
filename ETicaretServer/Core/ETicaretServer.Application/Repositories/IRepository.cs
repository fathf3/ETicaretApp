using ETicaretServer.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ETicaretServer.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table {  get; } 
    }
}
