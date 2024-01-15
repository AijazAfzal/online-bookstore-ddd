namespace Bookstore.Domain.SeedWork
{
    public abstract class BaseEntity
    {
        private readonly List<object> _domainEvents = new List<object>();
        public IReadOnlyList<object> DomainEvents => _domainEvents;
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        protected void AddDomainEvent(object domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
