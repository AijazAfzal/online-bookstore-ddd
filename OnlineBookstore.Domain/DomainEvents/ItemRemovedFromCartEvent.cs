using Bookstore.Domain.SeedWork;
namespace Bookstore.Domain.DomainEvents
{
    public class ItemRemovedFromCartEvent : IDomainEvent
    {
        public int CustomerId { get; }
        public int BookId { get; }
        public int Quantity { get; }

        public DateTime RemovedAt { get; }
        public ItemRemovedFromCartEvent(int customerId,int bookId,int quantity,DateTime removedat)
        {
            CustomerId = customerId;
            BookId = bookId;
            Quantity = quantity; 
            RemovedAt = removedat; 
        }
    }
}
