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

        private readonly ConcurrentDictionary<string, ILogger> _loggers = new ConcurrentDictionary<string, ILogger>(StringComparer.OrdinalIgnoreCase);

        public LoggerProvider()
        {
        }

        public LoggerProvider(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(categoryName, name => Logger.CreateLogger(_testOutputHelper, _scopeProvider, name));
        public ILogger<T> CreateLogger<T>() => _loggers.GetOrAdd(typeof(T).Name, name => Logger.CreateLogger<T>(_testOutputHelper, _scopeProvider)) as ILogger<T>;

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
