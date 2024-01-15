namespace Bookstore.Domain.SeedWork
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
    }
}
