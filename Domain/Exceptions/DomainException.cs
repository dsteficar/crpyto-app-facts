namespace Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }

    public class InvalidEntityStateException : DomainException
    {
        public InvalidEntityStateException(string message) : base(message) { }
    }
}
