using Application.DTOs;
using Domain.Exceptions;
using Infrastructure.Exceptions;
using System.Net;
using System.Text.Json;

namespace WebAdminUI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "A domain exception occurred.");
                //await HandleExceptionAsync(context, HttpStatusCode.BadRequest, dex.Message);
            }
            catch (ApplicationException aex)
            {
                _logger.LogWarning(aex, "An application exception occurred.");
                //await HandleExceptionAsync(context, HttpStatusCode.UnprocessableEntity, aex.Message);
            }
            catch (InfrastructureException iex)
            {
                _logger.LogError(iex, "An infrastructure exception occurred.");
                //await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "A server error occurred.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                //await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            //context.Response.ContentType = "application/json";
            //context.Response.StatusCode = (int)statusCode;

            //var response = ApiResponse<string>.Failure(message, (int)statusCode);

            //var result = JsonSerializer.Serialize(response);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync("message");
        }
    }
}
