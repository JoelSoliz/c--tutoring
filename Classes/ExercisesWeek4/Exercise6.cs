namespace Classes.ExercisesWeek4
{
    public class ApiException : Exception
    {
        public int StatusCode { get; }
        public ApiException(string message) : base(message) { }

        public ApiException(int statusCode, string message, Exception innerException) : base(message, innerException) { StatusCode = statusCode; }

        public ApiException(int statusCode, string message) : base(message) { StatusCode = statusCode; }
    }

    public class DatabaseException : ApiException
    {
        public DatabaseException(string message, Exception inner) : base(500, "We couldn't process your request. Please try again", inner) { }
    }

    public class NotFoundException : ApiException
    {
        public NotFoundException(string message) : base(404, "We couldn't found the requested resource. Please, try with a valid one") { }
    }

    public class ValidationException : ApiException
    {
        public ValidationException(string message) : base(400, "Bad request. Please try a valid operation") { }
    }

    public class ApiResponse
    {
        public int StatusCode { get; init; }
        public string Message { get; init; } = "";
        public string TraceId { get; init; } = "";
    }

    public class RequestHandler
    {
        public async Task<ApiResponse> HandleRequestAsync(Func<Task> handler)
        {
            try
            {
                await handler();
                var response = new ApiResponse
                {
                    StatusCode = 200,
                    Message = "Successfully request",
                    TraceId = Guid.NewGuid().ToString()
                };
                return response;
            }
            catch (ApiException apiException)
            {
                var response = new ApiResponse
                {
                    StatusCode = apiException.StatusCode,
                    Message = apiException.Message,
                    TraceId = Guid.NewGuid().ToString()
                };
                return response;
            }
            catch (Exception unexpectedException)
            {
                Console.WriteLine($"Unexcpected Exception: {unexpectedException.InnerException}");
                var response = new ApiResponse
                {
                    StatusCode = 500,
                    Message = $"Unexcpected Exception: {unexpectedException.InnerException}",
                    TraceId = Guid.NewGuid().ToString()
                };
                return response;
            }
        }
    }
    /*
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var sqlException = new Exception("SQL connection timeout");
            var databaseException = new DatabaseException("Sql Exception found!", sqlException);

            var handler = new RequestHandler();
            var response = await handler.HandleRequestAsync(async () =>
                           {
                               throw databaseException;
                           });
            Console.WriteLine($"StatusCode: {response.StatusCode}, Message: {response.Message}, TraceId: {response.TraceId}");

            Exception? currentException = databaseException; //it can be null
            while (currentException != null)
            {
                Console.WriteLine($"{currentException.Message}");
                currentException = currentException.InnerException; //iterate until null
            }
        }
    }
    */
}
