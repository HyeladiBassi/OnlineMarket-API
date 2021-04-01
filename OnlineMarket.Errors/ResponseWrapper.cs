namespace OnlineMarket.Errors
{
    public class ResponseWrapper<T, E> where E: System.Enum
    {
        public bool Success { get; set; }
        public T Result { get; set; }
        public APIError<E> Error { get; set; }

        public ResponseWrapper(bool success)
        {
            Success = success;
        }
    }
}