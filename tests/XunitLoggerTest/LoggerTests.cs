using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using Xunit.Logging;

namespace XunitLoggerTest
{
    public class LoggerTests
    {
        private readonly ITestOutputHelper output;

        public LoggerTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void LogMessage()
        {
            ILogger logger = new Logger(output, nameof(LoggerTests));

            logger.LogTrace("Hello World");
        }
    }
}