namespace backend.Services.ErrorService
{
    public class ErrorCustomException(Dictionary<string, IEnumerable<string>> errors) : Exception
    {
        public Dictionary<string, IEnumerable<string>> Errors { get; } = errors;
    }
}