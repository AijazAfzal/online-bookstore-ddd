using Bookstore.Domain.Entities;
using Bookstore.Domain.Entities.CartAggregate;
using Bookstore.Domain.Interfaces;
using Bookstore.Domain.SeedWork;
using online_bookstore_ddd.Models.DTOs;
namespace Bookstore.Domain.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartrepository;
        private readonly IRepository<Book> _bookrepository;
        private readonly IRepository<Customer> _customerrepository;
        private readonly IAppLogger<CartService> _logger;
        public CartService(ICartRepository cartrepository, IRepository<Book> bookrepository, IRepository<Customer> customerrepository, IAppLogger<CartService> logger)
        {
            _cartrepository = cartrepository;
            _bookrepository = bookrepository;
            _customerrepository = customerrepository; 
            _logger = logger; 
        }
        public async Task<Cart> AddDifferentBooksToCartAsync(int customerId, Dictionary<int, int> bookQuantities)
        {
            var customer = await _customerrepository.GetByIdAsync(customerId);
            var cart = await _cartrepository.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
            {
                cart = new Cart(customerId, new List<CartItem>());
                await _cartrepository.AddCartAsync(cart);
            }
            foreach (var (bookId, quantity) in bookQuantities)
            {
                var book = await _bookrepository.GetByIdAsync(bookId);
                if (book == null)
                {
                    _logger.LogWarning($"Book Not Found with Book Id {bookId}");
                    continue; 
                }
                cart.AddItem(book, quantity,customer.Id); 
            }
            await _cartrepository.UpdateCartAsync(cart);
            _logger.LogWarning($"Items added to cart for user {customer.Name}");
            return cart;
        }
        public async Task<Cart> AddItemToCartAsync(int customerId,int bookId, decimal price, int quantity)
        {
            var customer = await _customerrepository.GetByIdAsync(customerId); 
            var cart = await _cartrepository.GetCartByCustomerIdAsync(customerId); 
            if (cart == null)
            {
                cart = new Cart(customerId,new List<CartItem>());
                await _cartrepository.AddCartAsync(cart); 
            }
            var book = await _bookrepository.GetByIdAsync(bookId); 
            if (book == null)
            {
                _logger.LogWarning($"Book Not Found with Book Name {book.Name}"); 
            }
            cart.AddItem(book, quantity,customer.Id); 
            await _cartrepository.UpdateCartAsync(cart);
            _logger.LogWarning($"Item added to basket for user : {customer.Name} with BookName : {book.Name}, Quantity : {quantity}"); 
            return cart; 
        }
        public async Task DeleteEntireCartAsync(int cartId, int customerId)
        {
            var customer = await _customerrepository.GetByIdAsync(customerId);
            var cart = await _cartrepository.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
            {
                _logger.LogWarning($"Cart Not Found for customer");
                return; 
            }
              await _cartrepository.RemoveCartByIdAsync(cartId);
             _logger.LogInformation($"Deleted entire cart for customer {customer.Name}"); 
        }
        public async Task RemoveItemfromCartAsync(int bookId,int customerId) 
        {
            var book = await _bookrepository.GetByIdAsync(bookId);
            var cart = await _cartrepository.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
            {
                _logger.LogWarning($"Cart Not Found");
                return;
            }
            var itemToRemove = cart.Items.FirstOrDefault(item => item.Book.Id == bookId);
            if (itemToRemove != null)
            {
                cart.RemoveItem(itemToRemove, customerId);
                await _cartrepository.UpdateCartAsync(cart);
                _logger.LogInformation($"Removed item with name {book.Name} from the cart.");
            }
            else
            {
                _logger.LogWarning($"Item with name {book.Name} not found in the cart.");
            }
        }
        public async Task<CartDTO> GetCustomerCartDetailsAsync(int customerId)
        {
            var customer = await _customerrepository.GetByIdAsync(customerId); 
            var cart = await _cartrepository.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
            {
                _logger.LogWarning($"Cart not found for customer {customer.Name}"); 
                return null; 
            }
            var cartItemsDTO = new List<CartItemDTO>();
            foreach (var cartItem in cart.Items)
            {
                var booksDTO = new List<BookDTO>();
                foreach (var book in cartItem.Items)
                {
                    var bookDTO = new BookDTO
                    {
                        Id = book.Id,
                        Name = book.Name,
                        AuthorName = book.authorName,
                        Genre = book.Genre,
                        Price = book.Price
                    };
                    booksDTO.Add(bookDTO);
                }
                var cartItemDTO = new CartItemDTO
                {
                    quantity = cartItem.Quantity,
                    Books = booksDTO
                };

                cartItemsDTO.Add(cartItemDTO);
            }
            var cartDTO = new CartDTO
            {
                customerName = customer.Name,
                cartItems = cartItemsDTO,
                TotalAmount = cart.CalculateTotalAmount() 
            };
            return cartDTO;
        }
    }
}
