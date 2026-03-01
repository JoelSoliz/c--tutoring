namespace Classes.ExercisesWeek4
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Balance { get; set; }
    }

    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = "";
    }

    public class PaymentManager
    {
        public async Task<Transaction> ProcessPaymentAsync(User user, decimal amount)
        {
            try
            {
                if (amount <= 0)
                {
                    throw new ValidationException("The amount must be a positive number");
                }

                await Task.Delay(200);
                if (user.Balance < amount)
                {
                    throw new ValidationException("Insufficient balance");
                }
                var userAmount = user.Balance;
                var newBalance = userAmount - amount;
                user.Balance = newBalance;

                var transaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Amount = amount,
                    IsSuccess = true
                };
                return transaction;
            }
            catch (ValidationException exception)
            {
                var transaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    IsSuccess = false,
                    ErrorMessage = exception.Message
                };
                return transaction;
            }
            finally
            {
                Console.WriteLine("Closing auditory log");
            }
        }

        public async Task<Transaction> ProcessPaymentAsyncDelay(User user, decimal amount)
        {
            int delayTime = 100;
            TimeoutException? lastException = null;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    if (Random.Shared.Next(0, 3) == 0) // 0 1 or 2, if equals to 0 throw 
                    {
                        throw new TimeoutException("Payment service timeout");
                    }
                    var result = await ProcessPaymentAsync(user, amount);
                    return result;
                }
                catch (TimeoutException exception)
                {
                    lastException = exception;
                    Console.WriteLine(exception.Message);
                    await Task.Delay(delayTime);
                    delayTime *= 2; //100 200 400
                }
            }
            throw lastException!; //i swear it's not null
        }
    }

    /*
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // First use case
            var user1 = new User { Id = Guid.NewGuid(), Name = "Fernando", Balance = 800 };
            var user2 = new User { Id = Guid.NewGuid(), Name = "Luciana", Balance = 500 };
            var user3 = new User { Id = Guid.NewGuid(), Name = "Acacia", Balance = 50 };
            var user4 = new User { Id = Guid.NewGuid(), Name = "Sally", Balance = 20 };
            var user5 = new User { Id = Guid.NewGuid(), Name = "Elizabeth", Balance = 900 };

            var paymentManager = new PaymentManager();
            var task1 = paymentManager.ProcessPaymentAsync(user1, 600);
            var task2 = paymentManager.ProcessPaymentAsync(user2, 100);
            var task3 = paymentManager.ProcessPaymentAsync(user3, 100);
            var task4 = paymentManager.ProcessPaymentAsync(user4, 100);
            var task5 = paymentManager.ProcessPaymentAsync(user1, 200);

            var tasks = new[] { task1, task2, task3, task4, task5 }; // TRANSACTION ARRAY

            await Task.WhenAll(tasks);
            int counter = 0;
            foreach (var transaction in tasks)
            {
                if (transaction.Result.IsSuccess)
                {
                    counter++;
                }
                else
                {
                    Console.WriteLine(transaction.Result.ErrorMessage);
                }
            }
            Console.WriteLine($"Success payments:{counter.ToString()}");

            //second use case
            try
            {
                var retryResult = await paymentManager.ProcessPaymentAsyncDelay(user1, 100);
                Console.WriteLine($"Retry success: {retryResult.IsSuccess}");
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine($"Exhausted retries: {ex.Message}");
            }
        }
    }
    */
}
