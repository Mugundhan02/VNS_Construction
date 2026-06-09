using System.Net;
using System.Text.Json;
using BuildManager.Exceptions;

namespace BuildManager.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                // Intercept 401/403 set by JWT middleware (no exception thrown)
                if (!context.Response.HasStarted)
                {
                    if (context.Response.StatusCode == 401)
                    {
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            statusCode = 401,
                            errorCode = "UNAUTHORIZED",
                            message = "Authentication required. Please provide a valid Bearer token.",
                            timestamp = DateTime.UtcNow,
                            traceId = context.TraceIdentifier
                        }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                    }
                    else if (context.Response.StatusCode == 403)
                    {
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            statusCode = 403,
                            errorCode = "FORBIDDEN",
                            message = "You do not have permission to access this resource.",
                            timestamp = DateTime.UtcNow,
                            traceId = context.TraceIdentifier
                        }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception on {Method} {Path}",
                    context.Request.Method, context.Request.Path);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            if (context.Response.HasStarted) return;

            var (statusCode, errorCode, message) = ex switch
            {
                EntityNotFoundException => (HttpStatusCode.NotFound, "NOT_FOUND", ex.Message),
                DuplicateEntityException => (HttpStatusCode.Conflict, "DUPLICATE", ex.Message),
                UnAuthorizedException => (HttpStatusCode.Unauthorized, "UNAUTHORIZED", ex.Message),
                ValidationException => (HttpStatusCode.UnprocessableEntity, "VALIDATION_ERROR", ex.Message),
                UnableToCreateEntityException => (HttpStatusCode.UnprocessableEntity, "CREATE_FAILED", ex.Message),
                ArgumentNullException => (HttpStatusCode.BadRequest, "BAD_REQUEST", "Required value was missing."),
                ArgumentException => (HttpStatusCode.BadRequest, "BAD_REQUEST", ex.Message),
                KeyNotFoundException => (HttpStatusCode.NotFound, "NOT_FOUND", "The requested resource was not found."),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "UNAUTHORIZED", "Unauthorized access."),
                InvalidOperationException => (HttpStatusCode.UnprocessableEntity, "INVALID_OPERATION", ex.Message),
                _ => (HttpStatusCode.InternalServerError, "INTERNAL_ERROR", "An unexpected error occurred. Please try again later.")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                statusCode = (int)statusCode,
                errorCode,
                message,
                timestamp = DateTime.UtcNow,
                traceId = context.TraceIdentifier
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }));
        }
    }
}