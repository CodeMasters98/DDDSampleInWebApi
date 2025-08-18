namespace DDDSampleInWebApi.Domain.Common
{
    public abstract class ValueObject
    {
        // Define equality for value objects based on their attributes
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;
            return EqualsCore(other);
        }

        public override int GetHashCode() => GetHashCodeCore();

        // Derived classes should implement this method for custom equality checks
        protected abstract bool EqualsCore(ValueObject other);

        // Derived classes should implement this method for custom hashing
        protected abstract int GetHashCodeCore();
    }
}
