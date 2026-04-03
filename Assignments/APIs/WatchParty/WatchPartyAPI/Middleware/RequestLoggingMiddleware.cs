using System.Diagnostics;
using System.Security.Claims;

namespace WatchPartyAPI.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var method = context.Request.Method;
            var path = context.Request.Path;
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            await _next(context);

            stopwatch.Stop();
            var statusCode = context.Response.StatusCode;
            var elapsed = stopwatch.ElapsedMilliseconds;
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "anonymous";

            _logger.LogInformation(
                "HTTP {Method} {Path} => {StatusCode} | {ElapsedMs}ms | UserId: {UserId} | IP: {IP}",
                method, path, statusCode, elapsed, userId, ip);

            if (elapsed > 1000)
                _logger.LogWarning(
                    "Slow request: {Method} {Path} took {ElapsedMs}ms",
                    method, path, elapsed);

            if (statusCode >= 500)
                _logger.LogError(
                    "Server error on {Method} {Path} => {StatusCode}",
                    method, path, statusCode);
        }
    }
}
