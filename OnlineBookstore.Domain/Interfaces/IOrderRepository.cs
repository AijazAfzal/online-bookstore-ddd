using Bookstore.Domain.Entities.OrderAggregate;
namespace Bookstore.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Order entity, CancellationToken cancellationToken = default);

        Task<IEnumerable<Order>> GetAllAsync(IEnumerable<Order> entities, CancellationToken cancellationToken = default);

        Task<Order> GetByIdAsync(int Id, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        Task UpdateAsync(Order entity, CancellationToken cancellationToken = default);

        Task<Order> GetCustomerOrderAsync(int customerId); 
    } 

}
