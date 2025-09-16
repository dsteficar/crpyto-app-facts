namespace Application.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string message) : base(message) { }
    }

    public class BusinessException : ApplicationException
    {
        public BusinessException(string message) : base(message) { }
    }

    public class ValidationException : ApplicationException
    {
        public ValidationException(string message) : base(message) { }
    }
}
