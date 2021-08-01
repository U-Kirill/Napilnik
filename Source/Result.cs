namespace IMJunior
{
    public class Result<T> where T : class, IResultReason
    {
        private Result(bool isSuccess, T reason)
        {
            IsSuccess = isSuccess;
            Reson = reason;
        }

        public bool IsSuccess { get; }

        public T Reson { get; }

        public static implicit operator bool(Result<T> result) =>
            result.IsSuccess;

        public static Result<T> CreateSuccessful(T reason) =>
            new Result<T>(true, reason);

        public static Result<T> CreateFailed(T reason) =>
            new Result<T>(false, reason);
    }
}