using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Services.EntityLogger
{
    public class EntityFrameworkLoggerOptions
    {
        public Dictionary<string, LogLevel> Filters { get; set; }
    }
}
