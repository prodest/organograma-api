using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace Organograma.JobScheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var port = Environment.GetEnvironmentVariable("PORT") ?? "9935";
            var requestPath = Environment.GetEnvironmentVariable("REQUEST_PATH");
            var url = $"http://*:{port}{requestPath}";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(url)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
