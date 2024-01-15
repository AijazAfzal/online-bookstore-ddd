namespace Bookstore.Domain.Entities.CartAggregate
{
    public class CartItem
    {
        public int Id { get; private set; } 
        public Book Book { get; private set; }

        private readonly List<Book> _books = new List<Book>();
        public IReadOnlyCollection<Book> Items => _books.AsReadOnly();
        public int Quantity { get; private set; }
        public CartItem(Book book, int quantity)
        {
            Book = book;
            Quantity = quantity; 
        }
        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }
        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
        public decimal CalculateSubtotal()
        {
            return Book.Price * Quantity;
        }
    }
}
