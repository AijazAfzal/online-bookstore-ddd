using Bookstore.Domain.ValueObjects;
using online_bookstore_ddd.Models;
namespace Bookstore.Domain.Services
{
    public interface IOrderService
    {
        Task<ResponseMessage> PlaceOrderAsync(int customerId, Address shippingAddress);
        Task<ResponseMessage> CancelOrderAsync(int customerId);  
    }
}
