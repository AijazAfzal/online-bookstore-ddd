using Bookstore.Domain.DomainEvents;
using Bookstore.Domain.Interfaces;
using Bookstore.Domain.SeedWork;
namespace online_bookstore_ddd.Events.EventHandlers
{
    public class OrderPlacedEventHandler : IDomainEventHandler<OrderPlacedEvent>
    {
        public readonly IAppLogger<OrderPlacedEventHandler> _logger;
        public OrderPlacedEventHandler(IAppLogger<OrderPlacedEventHandler> logger)
        {
            _logger = logger;    
        }
        public async Task Handle(OrderPlacedEvent domainEvent)
        {
            _logger.LogInformation($"Order with ID {domainEvent.order_Id} was placed on {domainEvent.OrderCreatedDate}.");
             await Task.CompletedTask;  
        }
    }
}
