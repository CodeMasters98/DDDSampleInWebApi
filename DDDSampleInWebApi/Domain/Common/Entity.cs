namespace DDDSampleInWebApi.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public int Version { get; protected set; }

        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


        protected void Raise(IDomainEvent @event) => _domainEvents.Add(@event);
        public void ClearDomainEvents() => _domainEvents.Clear();

        // Entity equality is determined by the ID
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (Entity)obj;
            return Id == other.Id;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
