using System.Net;
using System.Text.Json;

namespace BuildManager.Middleware
{
    /// <summary>
    /// Global exception handling middleware.
    /// Catches all unhandled exceptions and returns a consistent JSON error response.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next   = next;
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
                _logger.LogError(ex, "Unhandled exception on {Method} {Path}",
                    context.Request.Method, context.Request.Path);

                await WriteErrorResponseAsync(context, ex);
            }
        }

        private static async Task WriteErrorResponseAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, message) = ex switch
            {
                ArgumentNullException      => (HttpStatusCode.BadRequest,            "Required value was missing."),
                ArgumentException          => (HttpStatusCode.BadRequest,            ex.Message),
                KeyNotFoundException       => (HttpStatusCode.NotFound,              "The requested resource was not found."),
                UnauthorizedAccessException=> (HttpStatusCode.Unauthorized,          "Unauthorized access."),
                InvalidOperationException  => (HttpStatusCode.UnprocessableEntity,   ex.Message),
                _                          => (HttpStatusCode.InternalServerError,   "An unexpected error occurred. Please try again later.")
            };

            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new
            {
                statusCode = context.Response.StatusCode,
                message,
                timestamp  = DateTime.UtcNow
            };

            var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }
}
