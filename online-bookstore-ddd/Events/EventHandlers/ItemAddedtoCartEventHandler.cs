using Bookstore.Domain.DomainEvents;
using Bookstore.Domain.Interfaces;
using Bookstore.Domain.SeedWork;
namespace online_bookstore_ddd.Events.EventHandlers
{
    public class ItemAddedtoCartEventHandler : IDomainEventHandler<ItemAddedtoCartEvent>
    {
        public readonly IAppLogger<ItemAddedtoCartEventHandler> _logger; 
        public ItemAddedtoCartEventHandler(IAppLogger<ItemAddedtoCartEventHandler> logger)
        {
            _logger = logger;   
        }
        public async Task Handle(ItemAddedtoCartEvent domainEvent)
        {
            _logger.LogInformation($"Item with bookID {domainEvent.BookId} was added to cart on {domainEvent.AddedAt}.");
            await Task.CompletedTask; 
        }
    }
}
