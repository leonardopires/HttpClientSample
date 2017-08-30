using System;

namespace HttpClientSample.Core
{
    class ConsoleLogger : ILogger
    {
        public void WriteLine(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }
    }
}