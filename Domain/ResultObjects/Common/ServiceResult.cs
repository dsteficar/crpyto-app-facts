namespace Application.DTOs.Response
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string ErrorMessage { get; }
        public string SuccessMessage { get; }

        protected ServiceResult(bool isSuccess, T value, string errorMessage, string successMessage)
        {
            IsSuccess = isSuccess;
            Value = value;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }

        public static ServiceResult<T> Success(T value, string successMessage = "") =>
            new ServiceResult<T>(true, value, null, successMessage);

        public static ServiceResult<T> Failure(string errorMessage)
        {
            return new ServiceResult<T>(false, GetDefaultValue<T>(), errorMessage, null);
        }

        private static T GetDefaultValue<T>()
        {
            // Check if T is a generic collection (IEnumerable<T>)
            if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                // Create an empty List<T> to match IEnumerable<T>
                var elementType = typeof(T).GetGenericArguments()[0];
                var listType = typeof(List<>).MakeGenericType(elementType);
                return (T)Activator.CreateInstance(listType);
            }

            // Return default(T) for non-collection types
            return default(T);
        }
        //public static Result<T> Failure(string errorMessage) =>
        //    new Result<T>(false, default, errorMessage, null);
    }
}
