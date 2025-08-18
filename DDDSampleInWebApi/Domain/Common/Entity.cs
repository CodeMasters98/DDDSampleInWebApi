namespace DDDSampleInWebApi.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public int Version { get; protected set; }

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
