using DDDSampleInWebApi.Domain.Common;

namespace DDDSampleInWebApi.Domain.Models
{
    public class Money 
    {
        public decimal Amount { get; }
        public string Currency { get; }

        private Money(decimal amount, string currency)
        {
            if (amount < 0) throw new ArgumentException("Amount cannot be negative.", nameof(amount));
            if (string.IsNullOrEmpty(currency)) throw new ArgumentException("Currency cannot be null or empty.", nameof(currency));

            Amount = amount;
            Currency = currency;
        }

        public static Money Of(decimal amount, string currency) => new Money(amount, currency);
        public static Money Zero(string currency) => new Money(0m, currency);

        public Money Add(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot add amounts with different currencies.");

            return new Money(Amount + other.Amount, Currency);
        }

        public Money Multiply(int quantity)
        {
            return new Money(Amount * quantity, Currency);
        }

        //protected override bool EqualsCore(ValueObject other)
        //{
        //    var otherMoney = other as Money;
        //    return otherMoney != null && Amount == otherMoney.Amount && Currency == otherMoney.Currency;
        //}

        //protected override int GetHashCodeCore() => (Amount, Currency).GetHashCode();
    }
}
