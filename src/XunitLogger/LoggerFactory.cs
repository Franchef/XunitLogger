using Microsoft.Extensions.Logging;

namespace Xunit.Logging
{
    public class LoggerFactory : ILoggerFactory
    {
        ILoggerProvider _provider = null;
        public void AddProvider(ILoggerProvider provider)
        {
            _provider = provider;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _provider.CreateLogger(categoryName);
        }

        public void Dispose()
        {
            _provider?.Dispose();
        }
    }
}
