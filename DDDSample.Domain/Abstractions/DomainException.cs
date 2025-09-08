namespace DDDSample.Domain.Abstractions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
    protected DomainException(string message, Exception? inner) : base(message, inner) { }

    public virtual string Code => GetType().Name;
}
