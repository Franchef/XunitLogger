using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using Xunit.Abstractions;

namespace Xunit.Logging
{
    public class LoggerProvider : ILoggerProvider
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly LoggerExternalScopeProvider _scopeProvider = new LoggerExternalScopeProvider();

        private readonly ConcurrentDictionary<string, Logger> _loggers = new ConcurrentDictionary<string, Logger>(StringComparer.OrdinalIgnoreCase);

        public LoggerProvider()
        {
        }

        public LoggerProvider(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(categoryName, name => new Logger(_testOutputHelper, _scopeProvider, name));
        public ILogger<T> CreateLogger<T>() => _loggers.GetOrAdd(typeof(T).Name, name => new Logger<T>(_testOutputHelper, _scopeProvider));

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
