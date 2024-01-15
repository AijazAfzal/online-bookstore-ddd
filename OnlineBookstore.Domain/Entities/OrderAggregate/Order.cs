using Bookstore.Domain.DomainEvents;
using Bookstore.Domain.SeedWork;
using Bookstore.Domain.ValueObjects;
namespace Bookstore.Domain.Entities.OrderAggregate
{
    public class Order : BaseEntity,IAggregateRoot 
    {
        public Order(Address shipToAddress, List<OrderItem> items,Customer customer,decimal orderamount, DateTime orderdate)
        {
            ShipToAddress = shipToAddress;
            _orderItems = items;
            Customer = customer;
            OrderAmount = orderamount; 
            OrderDate = orderdate;
            AddDomainEvent(new OrderPlacedEvent(Id, customer.Id, orderamount, OrderDate));

        }
        public int Id { get; private set; }

        private readonly List<OrderItem> _orderItems = new List<OrderItem>();

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public Address ShipToAddress { get; private set; }

        public Customer Customer { get; private set; } 

        public decimal OrderAmount { get; private set; } 

        public DateTime OrderDate { get; private set; } 

        public decimal Total()
        {
            var total = 0m;
            foreach (var item in _orderItems)
            {
                total += item.Book.Price * item.Quantity;
            }
            return total;
        }
    }
}
