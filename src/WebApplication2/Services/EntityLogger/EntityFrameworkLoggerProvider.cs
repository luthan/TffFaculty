using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Services.EntityLogger
{
    public class EntityFrameworkLoggerProvider<TDbContext, TLog> : ILoggerProvider
        where TLog : EntityFrameworkLog, new()
        where TDbContext : DbContext
    {
        readonly Func<string, LogLevel, bool> _filter;
        readonly IServiceProvider _serviceProvider;

        public EntityFrameworkLoggerProvider(IServiceProvider serviceProvider, Func<string, LogLevel, bool> filter)
        {
            _filter = filter;
            _serviceProvider = serviceProvider;
        }

        public ILogger CreateLogger(string name)
        {
            return new EntityFrameworkLogger<TDbContext, TLog>(name, _filter, _serviceProvider);
        }

        public void Dispose() { }
    }
}
