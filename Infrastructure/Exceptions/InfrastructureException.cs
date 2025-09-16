namespace Infrastructure.Exceptions
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException(string message) : base(message) { }
    }

    public class DatabaseException : InfrastructureException
    {
        public DatabaseException(string message) : base(message) { }
    }

    public class ExternalServiceException : InfrastructureException
    {
        public ExternalServiceException(string message) : base(message) { }
    }
}
