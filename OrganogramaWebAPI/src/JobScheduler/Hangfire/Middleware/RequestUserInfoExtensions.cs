using Microsoft.AspNetCore.Builder;

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
