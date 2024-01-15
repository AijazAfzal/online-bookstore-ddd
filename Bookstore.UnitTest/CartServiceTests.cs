using Bookstore.Domain.Entities;
using Bookstore.Domain.Entities.CartAggregate;
using Bookstore.Domain.Interfaces;
using Bookstore.Domain.SeedWork;
using Bookstore.Domain.Services;
using Bookstore.Domain.ValueObjects;
using Moq;
using NUnit.Framework;
namespace Bookstore.Tests
{
    [TestFixture]
    public class CartServiceTests
    {
        private readonly Mock<IRepository<Book>> bookRepositoryMock;
        private readonly Mock<IRepository<Customer>> customerRepositoryMock;
        private readonly Mock<ICartRepository> cartRepositoryMock;
        private readonly Mock<IAppLogger<CartService>> loggerMock;

        public CartServiceTests()
        {
            bookRepositoryMock = new Mock<IRepository<Book>>();
            customerRepositoryMock = new Mock<IRepository<Customer>>();
            cartRepositoryMock = new Mock<ICartRepository>();
            loggerMock = new Mock<IAppLogger<CartService>>();
        }

        [Test]
        public async Task AddDifferentBooksToCartAsync_ValidData_CartUpdated()
        {
            var customerId = 1;
            var bookQuantities = new Dictionary<int, int> { { 1, 2 }, { 2, 1 } };

            var customer = new Customer("john doe", "john@gmail.com");
            var cart = new Cart(customerId, new List<CartItem>());

            customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customerId, default)).ReturnsAsync(customer);
            cartRepositoryMock.Setup(repo => repo.GetCartByCustomerIdAsync(customerId)).ReturnsAsync(cart);
            bookRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), default)).ReturnsAsync(new Book("Sample Book", new AuthorName("John", "Doe"), new Genre("Fiction", "testdescp"), 29.99m));

            var cartService = new CartService(cartRepositoryMock.Object, bookRepositoryMock.Object, customerRepositoryMock.Object, loggerMock.Object);

            var result = await cartService.AddDifferentBooksToCartAsync(customerId, bookQuantities);

            Assert.IsNotNull(result);
            Assert.AreEqual(customerId, result.CustomerId);
            Assert.AreEqual(bookQuantities.Sum(b => b.Value), result.Items.Sum(item => item.Quantity));
        }



        [Test]
        public async Task AddItemToCartAsync_ValidData_CartUpdated()
        {
            var customerId = 1;
            var bookId = 1;
            var price = 25.99m;
            var quantity = 3;

            var customer = new Customer("john doe", "john@gmail.com");
            var cart = new Cart(customerId, new List<CartItem>());
            var authorName = new AuthorName("John", "Doe");
            var genre = new Genre("Fiction", "testdescp");
            var book = new Book("Sample Book", authorName, genre, 29.99m);

            bookRepositoryMock.Setup(repo => repo.GetByIdAsync(bookId, default)).ReturnsAsync(book);
            customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customerId, default)).ReturnsAsync(customer);
            cartRepositoryMock.Setup(repo => repo.GetCartByCustomerIdAsync(customerId)).ReturnsAsync(cart);

            var cartService = new CartService(cartRepositoryMock.Object, bookRepositoryMock.Object, customerRepositoryMock.Object, loggerMock.Object);

            var result = await cartService.AddItemToCartAsync(customerId, bookId, price, quantity);

            Assert.IsNotNull(result);
            Assert.AreEqual(customerId, result.CustomerId);
            Assert.AreEqual(1, result.Items.Count);
        }

        [Test]
        public async Task DeleteEntireCartAsync_ValidData_CartDeleted()
        {
            var customerId = 1;
            var cartId = 1001;

            var customer = new Customer("john doe", "john@gmail.com");
            var cart = new Cart(customerId, new List<CartItem>());

            customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customerId, default)).ReturnsAsync(customer);
            cartRepositoryMock.Setup(repo => repo.GetCartByCustomerIdAsync(customerId)).ReturnsAsync(cart);

            var cartService = new CartService(cartRepositoryMock.Object, null, customerRepositoryMock.Object, loggerMock.Object);

            await cartService.DeleteEntireCartAsync(cartId, customerId);
            cartRepositoryMock.Verify(repo => repo.RemoveCartByIdAsync(cartId), Times.Once);
        }

        [Test]
        public async Task RemoveItemfromCartAsync_ItemExists_ItemRemoved()
        {
            var customerId = 1;
            var bookId = 1;

            var customer = new Customer("john doe", "john@gmail.com");
            var authorName = new AuthorName("John", "Doe");
            var genre = new Genre("Fiction", "testdescp");
            var sampleBook = new Book("Sample Book", authorName, genre, 29.99m);

            var cartItem = new CartItem(sampleBook, 2);
            var cart = new Cart(customerId, new List<CartItem> { cartItem });

            bookRepositoryMock.Setup(repo => repo.GetByIdAsync(bookId, default)).ReturnsAsync(sampleBook);
            customerRepositoryMock.Setup(repo => repo.GetByIdAsync(customerId, default)).ReturnsAsync(customer);
            cartRepositoryMock.Setup(repo => repo.GetCartByCustomerIdAsync(customerId)).ReturnsAsync(cart);

            var cartService = new CartService(cartRepositoryMock.Object, bookRepositoryMock.Object, customerRepositoryMock.Object, loggerMock.Object);

            await cartService.RemoveItemfromCartAsync(bookId, customerId);
            Assert.AreEqual(1, cart.Items.Count);
        }
    }
}
