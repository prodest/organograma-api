using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.JobScheduler.Hangfire.Middleware
{
    public static class HangfireExtensions
    {
        public static IApplicationBuilder UseHangfire(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HangfireMiddleware>();
        }
    }
}
