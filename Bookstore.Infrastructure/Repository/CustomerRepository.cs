using Bookstore.Domain.Entities;
using Bookstore.Domain.SeedWork;
using Bookstore.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
namespace Bookstore.Infrastructure.Repository
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly ApplicationDbContext _context; 
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context; 
        }
        public async Task AddAsync(Customer entity, CancellationToken cancellationToken = default)
        {
           await _context.Customers.AddAsync(entity, cancellationToken);
           await _context.SaveChangesAsync(cancellationToken); 
        }
        public async Task DeleteAsync(Customer entity, CancellationToken cancellationToken = default)
        {
            _context.Customers.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<IEnumerable<Customer>> GetAllAsync(IEnumerable<Customer> entities, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.ToListAsync(cancellationToken);
        }

        public async Task<Customer> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateAsync(Customer entity, CancellationToken cancellationToken = default)
        {
            _context.Customers.Update(entity);
            await _context.SaveChangesAsync(cancellationToken); 
        }
    }
}
