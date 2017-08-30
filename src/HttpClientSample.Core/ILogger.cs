namespace HttpClientSample.Core
{
    public interface ILogger
    {
        void WriteLine(string message, params object[] args);
    }
}