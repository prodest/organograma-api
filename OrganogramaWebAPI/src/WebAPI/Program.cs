using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace Organograma.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8935";
            var requestPath = Environment.GetEnvironmentVariable("REQUEST_PATH");
            var url = $"http://*:{port}{requestPath}";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(url)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
