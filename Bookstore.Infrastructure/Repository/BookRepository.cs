using Bookstore.Domain.Entities;
using Bookstore.Domain.SeedWork;
using Bookstore.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
namespace Bookstore.Infrastructure.Repository
{
    public class BookRepository : IRepository<Book>
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
                _context = context; 
        }
        public async Task AddAsync(Book entity, CancellationToken cancellationToken = default)
        {
            await _context.Books.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(Book entity, CancellationToken cancellationToken = default)
        {
            _context.Books.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<IEnumerable<Book>> GetAllAsync(IEnumerable<Book> entities, CancellationToken cancellationToken = default)
        {
            return await _context.Books.ToListAsync(cancellationToken);
        }
        public async Task<Book> GetByIdAsync(int Id, CancellationToken cancellationToken = default)
        {
            return await _context.Books.FindAsync(Id); 
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken); 
        }
        public async Task UpdateAsync(Book entity, CancellationToken cancellationToken = default)
        {
            _context.Books.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
