using Bookstore.Domain.Entities.CartAggregate;
namespace Bookstore.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByCustomerIdAsync(int customerId);

        Task AddCartAsync(Cart cart);

        Task RemoveCartByIdAsync(int CartId);

        Task UpdateCartAsync(Cart cart); 
    }
}
