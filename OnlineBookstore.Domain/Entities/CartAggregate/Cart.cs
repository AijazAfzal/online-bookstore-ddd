using Bookstore.Domain.DomainEvents;
using Bookstore.Domain.SeedWork;
namespace Bookstore.Domain.Entities.CartAggregate
{
    public class Cart : BaseEntity,IAggregateRoot
    {
        public int Id { get; private set; } 
        public int CustomerId { get; private set; } 

        private readonly List<CartItem> _items = new List<CartItem>();
        public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
        public Cart(int customerId, List<CartItem> cartItems) 
        {
            CustomerId = customerId;
            _items.AddRange(cartItems); 
        }
        public void AddItem(Book book, int quantity,int customerId)
        {
            if (!Items.Any(i => i.Book.Id == book.Id))
            {
                _items.Add(new CartItem(book, quantity)); 
                return;
            }
            var existingItem = Items.First(i => i.Book.Id == book.Id);
            existingItem.AddQuantity(quantity);
            var currentdate = DateTime.Now; 
            AddDomainEvent(new ItemAddedtoCartEvent(customerId,book.Id,quantity,currentdate)); 
        }
        public void RemoveEmptyItems()
        {
            _items.RemoveAll(i => i.Quantity == 0);
        }
        public void RemoveItem(CartItem item,int customerId)
        {
            var existingItem = _items.FirstOrDefault(i => i.Book.Id == item.Book.Id);
            var currentdate = DateTime.Now; 
            if (existingItem != null)
            {
                if (item.Quantity > 1)
                {
                    existingItem.SetQuantity(existingItem.Quantity - 1);
                }
                else
                {
                    _items.Remove(existingItem);
                }
            }
            AddDomainEvent(new ItemRemovedFromCartEvent(customerId,existingItem.Book.Id,existingItem.Quantity,currentdate));
        }
        public decimal CalculateTotalAmount()
        {
            decimal totalAmount = 0;

            foreach (var cartItem in _items)
            {
                totalAmount += cartItem.CalculateSubtotal();
            }

            return totalAmount;
        }
    }
}
