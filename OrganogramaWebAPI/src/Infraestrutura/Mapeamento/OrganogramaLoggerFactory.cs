using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Infraestrutura.Mapeamento
{
    public class OrganogramaLoggerFactory : ILoggerFactory
    {
        public void AddProvider(ILoggerProvider provider) { }

        public ILogger CreateLogger(string categoryName)
        {
            if (categoryName == "Microsoft.EntityFrameworkCore.Storage.IRelationalCommandBuilderFactory")
            {
                return new OrganogramaLogger(categoryName);
            }

            return new NullLogger();
        }

        public void Dispose() { }

        public class OrganogramaLogger : ILogger
        {
            public string CategoryName { get; set; }

            public OrganogramaLogger(string category)
            {
                CategoryName = category;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(formatter(state, exception));
                Console.WriteLine();
                Console.WriteLine();
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        }

        private class NullLogger : ILogger
        {
            public bool IsEnabled(LogLevel logLevel)
            {
                return false;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            { }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        }
    }
}
