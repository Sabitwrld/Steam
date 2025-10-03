using FluentValidation;
using Steam.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace Steam.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var statusCode = HttpStatusCode.InternalServerError;
            object response;

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    response = new
                    {
                        StatusCode = (int)statusCode,
                        Message = "One or more validation errors occurred.",
                        Errors = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                    };
                    break;

                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    response = new { StatusCode = (int)statusCode, Message = notFoundException.Message };
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    response = new { StatusCode = (int)statusCode, Message = "An internal server error has occurred." };
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
