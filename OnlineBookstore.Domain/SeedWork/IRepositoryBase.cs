namespace Bookstore.Domain.SeedWork
{
    public interface IRepositoryBase<T> where T : class
    {
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(int Id, CancellationToken cancellationToken = default);
    }
}
