namespace WatchPartyAPI.Exceptions
{

    public abstract class AppException : Exception
    {
        public int StatusCode { get; }
        public string ErrorCode { get; }

        protected AppException(string message, int statusCode, string errorCode)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }

    public class NotFoundException : AppException
    {
        public NotFoundException(string resource, object id)
            : base($"{resource} with id '{id}' was not found", 404, "NOT_FOUND") { }
    }

    public class ConflictException : AppException
    {
        public ConflictException(string message)
            : base(message, 409, "CONFLICT") { }
    }

    public class BusinessException : AppException
    {
        public BusinessException(string message)
            : base(message, 400, "BUSINESS_RULE_VIOLATION") { }
    }

    public class ForbiddenException : AppException
    {
        public ForbiddenException(string message = "Access denied")
            : base(message, 403, "FORBIDDEN") { }
    }

    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string message = "Unauthorized")
            : base(message, 401, "UNAUTHORIZED") { }
    }
}

