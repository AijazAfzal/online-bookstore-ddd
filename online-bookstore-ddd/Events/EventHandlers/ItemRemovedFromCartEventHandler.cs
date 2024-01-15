using Bookstore.Domain.DomainEvents;
using Bookstore.Domain.Interfaces;
using Bookstore.Domain.SeedWork;
namespace online_bookstore_ddd.Events.EventHandlers
{
    public class ItemRemovedFromCartEventHandler : IDomainEventHandler<ItemRemovedFromCartEvent>
    {
        public readonly IAppLogger<ItemRemovedFromCartEventHandler> _logger; 
        public ItemRemovedFromCartEventHandler(IAppLogger<ItemRemovedFromCartEventHandler> logger)
        {
            _logger = logger; 
        }
        public async Task Handle(ItemRemovedFromCartEvent domainEvent)
        {
            _logger.LogInformation($"Item with bookID {domainEvent.BookId} was removed from cart on {domainEvent.RemovedAt}.");
            await Task.CompletedTask;
        } 
    }
}
