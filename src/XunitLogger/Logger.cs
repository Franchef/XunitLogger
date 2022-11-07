using Microsoft.Extensions.Logging;
using System;
using Xunit.Abstractions;

namespace Xunit.Logging
{
    public class XUnitLogger : ILogger
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly LoggerExternalScopeProvider _scopeProvider;
        private readonly string _name;

        public static ILogger CreateLogger(ITestOutputHelper testOutputHelper) => new XUnitLogger(testOutputHelper, new LoggerExternalScopeProvider(), "");
        public static ILogger<T> CreateLogger<T>(ITestOutputHelper testOutputHelper) => new XUnitLogger<T>(testOutputHelper, new LoggerExternalScopeProvider());

        public XUnitLogger(ITestOutputHelper testOutputHelper, LoggerExternalScopeProvider scopeProvider, string name)
        {
            _testOutputHelper = testOutputHelper;
            _scopeProvider = scopeProvider;
            _name = name;
        }

        public IDisposable BeginScope<TState>(TState state) => _scopeProvider.Push(state);
        public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

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

    internal sealed class XUnitLogger<T> : Logger, ILogger<T>
    {
        public XUnitLogger(ITestOutputHelper testOutputHelper, LoggerExternalScopeProvider scopeProvider) : base(testOutputHelper, scopeProvider, typeof(T).FullName)
        {

        }
    }
}
