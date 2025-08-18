using DDDSampleInWebApi.Domain.Common;

namespace DDDSampleInWebApi.Domain.Models
{
    public class ProductId : ValueObject
    {
        public Guid Value { get; }

        public ProductId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("ProductId cannot be an empty GUID.", nameof(value));

            Value = value;
        }

        protected override bool EqualsCore(ValueObject other)
        {
            var otherProductId = other as ProductId;
            return otherProductId != null && Value.Equals(otherProductId.Value);
        }

        protected override int GetHashCodeCore() => Value.GetHashCode();
    }
}
