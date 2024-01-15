using Bookstore.Domain.Entities;
using Bookstore.Domain.Entities.CartAggregate;
using Bookstore.Domain.Entities.OrderAggregate;
using Bookstore.Domain.Interfaces;
using Bookstore.Domain.SeedWork;
using Bookstore.Domain.Services;
using Bookstore.Domain.ValueObjects;
using Moq;
using NUnit.Framework;
namespace Bookstore.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        [Test]
        public async Task PlaceOrderAsync_ValidData_OrderPlacedSuccessfully()
        {
            var customerId = 1;
            var shippingAddress = new Address("123 Main St", "City", "state", "Country","12345"); 

            var customer = new Customer("john doe", "john@gmail.com");
            var cartItem = new CartItem(new Book("Sample Book", new AuthorName("John", "Doe"), new Genre("Fiction", "testdescp"), 29.99m), 2);
            var cart = new Cart(customerId, new List<CartItem> { cartItem });

            var bookRepositoryMock = new Mock<IRepository<Book>>();
            var customerRepositoryMock = new Mock<IRepository<Customer>>();  
            var cartRepositoryMock = new Mock<ICartRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>(); 
            var loggerMock = new Mock<IAppLogger<OrderService>>();

            customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customerId, default)).ReturnsAsync(customer);
            cartRepositoryMock.Setup(repo => repo.GetCartByCustomerIdAsync(customerId)).ReturnsAsync(cart);
            bookRepositoryMock.Setup(repo => repo.GetByIdAsync(cartItem.Book.Id, default)).ReturnsAsync(cartItem.Book);

            var orderService = new OrderService(cartRepositoryMock.Object, bookRepositoryMock.Object, customerRepositoryMock.Object, orderRepositoryMock.Object, loggerMock.Object);

            var result = await orderService.PlaceOrderAsync(customerId, shippingAddress);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Order placed successfully.", result.Message);
            orderRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Order>(), default), Times.Once);
            cartRepositoryMock.Verify(repo => repo.RemoveCartByIdAsync(cart.Id), Times.Once);
        }

        [Test]
        public async Task PlaceOrderAsync_EmptyCart_ReturnsErrorMessage()
        {
            var customerId = 1;
            var shippingAddress = new Address("123 Main St", "City", "state", "Country","12345"); 

            var customer = new Customer("john doe", "john@gmail.com");
            var cart = new Cart(customerId, new List<CartItem>());

            var bookRepositoryMock = new Mock<IRepository<Book>>();
            var customerRepositoryMock = new Mock<IRepository<Customer>>();
            var cartRepositoryMock = new Mock<ICartRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var loggerMock = new Mock<IAppLogger<OrderService>>();

            customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customerId, default)).ReturnsAsync(customer);
            cartRepositoryMock.Setup(repo => repo.GetCartByCustomerIdAsync(customerId)).ReturnsAsync(cart);

            var orderService = new OrderService(cartRepositoryMock.Object, bookRepositoryMock.Object, customerRepositoryMock.Object, orderRepositoryMock.Object, loggerMock.Object);

            var result = await orderService.PlaceOrderAsync(customerId, shippingAddress);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Cannot place an empty order.", result.Message);
            orderRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Order>(), default), Times.Never);
            cartRepositoryMock.Verify(repo => repo.RemoveCartByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public async Task CancelOrderAsync_ExistingOrder_OrderCancelledSuccessfully()
        {
            var customerId = 1;
            var customer = new Customer("john doe", "john@gmail.com");
            var order = new Order(new Address("123 Main St", "City", "state", "Country","12345"), new List<OrderItem>(), customer, 50.0m, DateTime.Now);

            var customerRepositoryMock = new Mock<IRepository<Customer>>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var loggerMock = new Mock<IAppLogger<OrderService>>();

            customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customerId, default)).ReturnsAsync(customer);
            orderRepositoryMock.Setup(repo => repo.GetCustomerOrderAsync(customerId)).ReturnsAsync(order);

            var orderService = new OrderService(null, null, customerRepositoryMock.Object, orderRepositoryMock.Object, loggerMock.Object);

            var result = await orderService.CancelOrderAsync(customerId);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Cancelled order", result.Message);
            orderRepositoryMock.Verify(repo => repo.DeleteAsync(order,default), Times.Once);
        }

        [Test]
        public async Task CancelOrderAsync_NoExistingOrder_ReturnsErrorMessage()
        {
            var customerId = 1;
            var customer = new Customer("john doe", "john@gmail.com");

            var customerRepositoryMock = new Mock<IRepository<Customer>>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var loggerMock = new Mock<IAppLogger<OrderService>>();

            customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customerId, default)).ReturnsAsync(customer);
            orderRepositoryMock.Setup(repo => repo.GetCustomerOrderAsync(customerId)).ReturnsAsync((Order)null);

            var orderService = new OrderService(null, null, customerRepositoryMock.Object, orderRepositoryMock.Object, loggerMock.Object);

            var result = await orderService.CancelOrderAsync(customerId);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("No current order for this customer", result.Message); 
        }
    }
}
