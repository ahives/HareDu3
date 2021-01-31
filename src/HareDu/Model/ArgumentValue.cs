namespace HareDu.Model
{
    using Core;

    public record ArgumentValue<T>
    {
        public T Value { get; }
        public Error Error { get; }
        
        public ArgumentValue(T value, string errorMsg = null)
        {
            Value = value;

            if (!string.IsNullOrWhiteSpace(errorMsg))
                Error = new (){Reason = errorMsg};
        }
    }
}