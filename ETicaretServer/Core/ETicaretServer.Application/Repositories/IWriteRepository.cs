using ETicaretServer.Domain.Entities.Common;

namespace ETicaretServer.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entity);
        bool Update(T entity);
        bool Remove(T entity);
        Task<bool> RemoveAsync(string id);
        bool RemoveRange(List<T> entity);

        Task<int> SaveAsync();

    }
}
