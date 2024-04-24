using System.Net.Mime;

namespace CVManagementApplication.API.Middlewares
{
    public class CustomExceptionHandler
    {
        private readonly ILogger<CustomExceptionHandler> _logger;
        private readonly RequestDelegate _next;

        public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger,
                                          RequestDelegate next)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                context.Response.ContentType = MediaTypeNames.Application.Json;
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong").ConfigureAwait(false);
            }
        }
    }
}
