using Microsoft.Extensions.Logging;

namespace WebApplication2.Services
{
    public interface IEntityFrameworkLoggerProvider
    {
        ILogger CreateLogger(string name);
        void Dispose();
    }
}