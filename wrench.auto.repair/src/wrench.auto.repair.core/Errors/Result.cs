namespace wrench.auto.repair.core.Errors
{
    public class Result
    {
        public Guid Id { get; private set; }
        public DateTime Timestamp { get; private set; }
        public IEnumerable<string>? Errors { get; private set; }
        public string? Message { get; private set; }
        public bool Success { get; private set; }

        private Result()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;

        }

        public static Result WithFailure(IEnumerable<string> errors)
        {
            return new Result
            {
                Errors = errors,
                Success = false,
                Message = null
            };
        }

        public static Result WithFailure(string message)
        {
            return new Result
            {
                Errors = null,
                Success = false,
                Message = message
            };
        }

        public static Result WithFailure(string message, IEnumerable<string> errors)
        {
            return new Result
            {
                Errors = errors,
                Success = false,
                Message = message
            };
        }

        public static Result WithSuccess(string? message = null)
        {
            return new Result
            {
                Success = true,
                Message = message
            };
        }
    }
}
