using Bookstore.Domain.Entities.CartAggregate;
using online_bookstore_ddd.Models.DTOs;
namespace Bookstore.Domain.Services
{
    public interface ICartService
    {
        Task<Cart> AddItemToCartAsync(int customerId, int bookId, decimal price, int quantity);
        Task<Cart> AddDifferentBooksToCartAsync(int customerId, Dictionary<int, int> bookQuantities);
        Task RemoveItemfromCartAsync(int bookId, int customerId);
        Task DeleteEntireCartAsync(int cartId, int customerId);
        Task<CartDTO> GetCustomerCartDetailsAsync(int customerId);
    }
}
