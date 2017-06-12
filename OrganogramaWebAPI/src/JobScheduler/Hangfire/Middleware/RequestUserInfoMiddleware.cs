using Apresentacao.Base;
using Hangfire;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Organograma.JobScheduler.Hangfire.Middleware
{
    public class HangfireMiddleware
    {
        private readonly RequestDelegate _next;

        public HangfireMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            RecurringJob.AddOrUpdate<IOrganizacaoWorkService>("Integração Siarhes",
                x => x.IntegarSiarhes(), Cron.Daily(4, 4), TimeZoneInfo.Local);

            await _next.Invoke(context);
        }
    }
}
