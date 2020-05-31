namespace TestMovieSite.Domain.Helpers
{
    public class OperationResult<T>
    {
        public T Data { get; private set; }
        public bool IsSuccess { get; private set; }

        public OperationResult(T data, bool isSuccess)
        {
            Data = data;
            IsSuccess = isSuccess;
        }
    }
}