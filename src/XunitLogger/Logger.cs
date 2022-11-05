using Microsoft.Extensions.Logging;
using System;
using Xunit.Abstractions;

namespace Xunit.Logging
{
    public class Logger : ILogger
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly string _name;

        public Logger(ITestOutputHelper testOutputHelper, string name)
        {
            _testOutputHelper = testOutputHelper;
            _name = name;
        }

        public IDisposable BeginScope<TState>(TState state) => default;
        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            _testOutputHelper.WriteLine($"[{eventId.Id,2}: {logLevel,-12}]");

            _testOutputHelper.WriteLine($"     {_name} - {formatter(state, exception)}");
            if (exception != null)
                _testOutputHelper.WriteLine(exception.ToString());
        }
    }
}
