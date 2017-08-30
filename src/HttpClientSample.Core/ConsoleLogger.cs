using System;
using System.IO;
using Autofac.Features.Indexed;

namespace HttpClientSample.Core
{
    class ConsoleLogger : ILogger
    {
        private readonly TextWriter output;

        public ConsoleLogger(IIndex<string, TextWriter> output)
        {
            this.output = output["LoggerWriter"];
        }

        public void WriteLine(string message, params object[] args)
        {
            output.WriteLine(message, args);
        }
    }
}