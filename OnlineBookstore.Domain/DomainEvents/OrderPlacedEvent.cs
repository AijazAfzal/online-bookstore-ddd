using Bookstore.Domain.SeedWork;
namespace Bookstore.Domain.DomainEvents
{
    public record OrderPlacedEvent : IDomainEvent
    {
        public int order_Id { get; set; }

        public int customer_Id { get; set; }

        public decimal  Amount { get; set; }

        public DateTime OrderCreatedDate { get; set; } 
        public OrderPlacedEvent(int orderId,int customerId,decimal amount,DateTime createddate)
        {
            order_Id = orderId;
            customer_Id = customerId;
            Amount = amount;
            OrderCreatedDate = createddate; 
        }
    }
}
