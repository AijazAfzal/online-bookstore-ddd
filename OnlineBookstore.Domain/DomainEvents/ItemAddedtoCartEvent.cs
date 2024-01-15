using Bookstore.Domain.SeedWork;
namespace Bookstore.Domain.DomainEvents
{
    public class ItemAddedtoCartEvent : IDomainEvent
    {
        public int CustomerId { get; }
        public int BookId { get; }
        public int Quantity { get; }
        public DateTime AddedAt { get; }
        public ItemAddedtoCartEvent(int customerId, int bookId, int quantity, DateTime addedAt)
        {
            CustomerId = customerId;
            BookId = bookId;
            Quantity = quantity;
            AddedAt = addedAt; 
        }
    }
}
