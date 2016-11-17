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
    public static class EntityFrameworkLoggerFactoryExtensions
    {
        public static ILoggerFactory AddEntityFramework<TDbContext, TLog>(this ILoggerFactory factory, IServiceProvider serviceProvider, Func<string, LogLevel, bool> filter = null)
            where TDbContext : DbContext
            where TLog : EntityFrameworkLog, new()
        {
            if (factory == null) throw new ArgumentNullException("factory");

            factory.AddProvider(new EntityFrameworkLoggerProvider<TDbContext, TLog>(serviceProvider, filter));

            return factory;
        }
    }
}
