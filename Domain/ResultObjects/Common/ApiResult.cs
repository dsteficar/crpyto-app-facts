using System.Net;

namespace Domain.DTOs.Common
{
    public class ApiResult<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string Error { get; set; }

        public ApiResult(HttpStatusCode statusCode, T data = default, string message = null, string error = null)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
            Error = error;
        }

        public static ApiResult<T> Success(T data, string message = null)
        {
            return new ApiResult<T>(HttpStatusCode.OK, data, message);
        }

        public static ApiResult<T> Failure(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ApiResult<T>(statusCode, default, null, error);
        }
    }
}
