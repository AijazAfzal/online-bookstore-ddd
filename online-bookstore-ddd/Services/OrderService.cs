using Bookstore.Domain.Entities;
using Bookstore.Domain.Entities.OrderAggregate;
using Bookstore.Domain.Interfaces;
using Bookstore.Domain.SeedWork;
using Bookstore.Domain.ValueObjects;
using online_bookstore_ddd.Models;
namespace Bookstore.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartrepository;
        private readonly IRepository<Book> _bookrepository;
        private readonly IRepository<Customer> _customerrepository;
        private readonly IOrderRepository _orderrepository;
        private readonly IAppLogger<OrderService> _logger; 
        public OrderService(ICartRepository cartRepository
            ,IRepository<Book> bookrepository
            , IRepository<Customer> customerrepository
            , IOrderRepository orderrepository 
            , IAppLogger<OrderService> logger)
        {
            _cartrepository = cartRepository;
            _bookrepository = bookrepository;
            _customerrepository = customerrepository;
            _orderrepository = orderrepository; 
            _logger = logger; 
        }

        public async Task<ResponseMessage> CancelOrderAsync(int customerId)
        {
            var customer = await _customerrepository.GetByIdAsync(customerId);
            var customerorder = await _orderrepository.GetCustomerOrderAsync(customerId);
            if (customerorder == null)
            {
                return new ResponseMessage { IsSuccess = false, Message = "No current order for this customer" };
            }
            await _orderrepository.DeleteAsync(customerorder);
            return new ResponseMessage { IsSuccess = true, Message = "Cancelled order" }; 
        }

        public async Task<ResponseMessage> PlaceOrderAsync(int customerId, Address shippingAddress)
        {
            var customer = await _customerrepository.GetByIdAsync(customerId);
            var cart = await _cartrepository.GetCartByCustomerIdAsync(customerId);
            var currentdate = DateTime.Now; 
            if (cart == null || !cart.Items.Any())
            {
                return new ResponseMessage { IsSuccess = false, Message = "Cannot place an empty order." };
            }
            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0m;
            foreach (var cartItem in cart.Items)
            {
                var book = await _bookrepository.GetByIdAsync(cartItem.Book.Id);
                if (book == null)
                {
                    _logger.LogWarning($"Book Not Found with Book Id {cartItem.Book.Id}");
                    continue;
                }
                var orderItem = new OrderItem(book, cartItem.Quantity);
                orderItems.Add(orderItem);
                totalAmount += book.Price * cartItem.Quantity;
            }
            if (cart.Items.Sum(item => item.Quantity) >= 3)
            {
                totalAmount *= 0.9m;
            }
            var order = new Order(shippingAddress, orderItems,customer,totalAmount, currentdate);
            await _orderrepository.AddAsync(order); 
            await _cartrepository.RemoveCartByIdAsync(cart.Id); 
            return new ResponseMessage { IsSuccess = true, Message = "Order placed successfully." }; 
        }

    }
}
