using System.Text.Json;
using WatchPartyAPI.DTOs.Responses;
using WatchPartyAPI.Exceptions;

namespace WatchPartyAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
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
            ErrorResponse response;

            switch (ex)
            {
                case AppException appEx:
                    _logger.LogWarning(
                        "Application error [{ErrorCode}] on {Path}: {Message} | TraceId: {TraceId}",
                        appEx.ErrorCode, context.Request.Path, appEx.Message, traceId);

                    response = new ErrorResponse
                    {
                        StatusCode = appEx.StatusCode,
                        ErrorCode = appEx.ErrorCode,
                        Message = appEx.Message,
                        TraceId = traceId
                    };
                    break;

                default:
                    _logger.LogError(ex,
                        "Unhandled exception on {Method} {Path} | TraceId: {TraceId}",
                        context.Request.Method, context.Request.Path, traceId);

                    response = new ErrorResponse
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

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }
}
