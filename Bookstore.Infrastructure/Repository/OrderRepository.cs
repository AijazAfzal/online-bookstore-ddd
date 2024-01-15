using Bookstore.Domain.Entities.OrderAggregate;
using Bookstore.Domain.Interfaces;
using Bookstore.Domain.SeedWork;
using Bookstore.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
namespace Bookstore.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context; 
        public OrderRepository(ApplicationDbContext context)
        {
                _context = context; 
        }
        public async Task AddAsync(Order entity, CancellationToken cancellationToken = default)
        {
            await _context.Orders.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Order entity, CancellationToken cancellationToken = default)
        {
            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetAllAsync(IEnumerable<Order> entities, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.ToListAsync(cancellationToken);
        }

        public async Task<Order> GetByIdAsync(int Id, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.FindAsync(Id);
        }

        public async Task<Order> GetCustomerOrderAsync(int customerId)
        {
            return await _context.Orders.Where(x => x.Customer.Id == customerId).FirstOrDefaultAsync(); 
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Order entity, CancellationToken cancellationToken = default)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
