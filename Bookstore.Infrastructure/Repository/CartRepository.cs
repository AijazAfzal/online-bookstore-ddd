using Bookstore.Domain.Entities.CartAggregate;
using Bookstore.Domain.Interfaces;
using Bookstore.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
namespace Bookstore.Infrastructure.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
                _context = context; 
        }
        public async Task AddCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync(); 
        }
        public async Task<Cart> GetCartByCustomerIdAsync(int customerId)
        {
            return await _context.Carts.Include(x=>x.Items).Where(x=>x.CustomerId == customerId).FirstOrDefaultAsync(); 
        }
        public async Task RemoveCartByIdAsync(int CartId)
        {
            var cart = await _context.Carts.FindAsync(CartId); 
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync(); 
        }
        public async Task UpdateCartAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync(); 
        }
    }
}
