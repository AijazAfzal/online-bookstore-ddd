using Bookstore.Domain.ValueObjects;
namespace online_bookstore_ddd.Models.DTOs
{
    public class CartDTO
    {
        public string customerName { get; set; } 
        public IList<CartItemDTO> cartItems { get; set; } 
        public decimal TotalAmount { get; set; }
    }

    public class CartItemDTO
    {
        public int quantity { get; set; }

        public List<BookDTO> Books { get; set; }
    }

    public class BookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AuthorName AuthorName { get; set; }
        public Genre Genre { get; set; }
        public decimal Price { get; set; }
    }
}
