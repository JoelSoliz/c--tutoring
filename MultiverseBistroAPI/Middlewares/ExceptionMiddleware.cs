using MultiverseBistroAPI.DTOs;
using System.Text.Json;

namespace MultiverseBistroAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var traceId = context.TraceIdentifier;
            ErrorResponseDto response;
            switch (ex)
            {
                default:
                    _logger.LogError(ex,
                    "Unhandled exception on {Method} {Path} | TraceId: {TraceId}",
                    context.Request.Method, context.Request.Path, traceId);
                    response = new ErrorResponseDto
                    {
                        StatusCode = 500,
                        ErrorCode = "INTERNAL_SERVER_ERROR",
                        Message = _env.IsDevelopment()
                                ? ex.Message
                                : "An unexpected error occurred. Please try again later.",
                                    TraceId = traceId
                    };
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.StatusCode;
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }
}
