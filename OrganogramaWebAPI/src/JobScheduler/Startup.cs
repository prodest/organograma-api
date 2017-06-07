using Hangfire;
using Hangfire.AspNetCore;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Organograma.Infraestrutura.Mapeamento;
using Organograma.JobScheduler.Commom;
using Organograma.JobScheduler.Commom.Config;
using Organograma.JobScheduler.Hangfire.Middleware;
using Organograma.Negocio.Commom.Base;
using System;
using IdentityModel;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Organograma.JobScheduler
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                ;
            Configuration = builder.Build();

            OrganogramaContext.ConnectionString = Environment.GetEnvironmentVariable("OrganogramaConnectionString");
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AutenticacaoIdentityServer>(Configuration.GetSection("AutenticacaoIdentityServer"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IClientAccessToken, AcessoCidadaoClientAccessToken>();
            services.AddScoped<ICurrentUserProvider, CurrentUser>();

            ConfiguracaoDependencias.InjetarDependencias(services);

            #region Hnagfire
            //Verica se está no ambiente de desenvolvimento para não iniciar o serviço
            //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development")
            //{
            var provider = services.BuildServiceProvider();
                var scopeFactory = (IServiceScopeFactory)provider.GetService(typeof(IServiceScopeFactory));

                services.AddHangfire(configuration =>
                {
                    configuration.UseSqlServerStorage(Environment.GetEnvironmentVariable("OrganogramaConnectionString"), new SqlServerStorageOptions { PrepareSchemaIfNecessary = false });
                    configuration.UseActivator<AspNetCoreJobActivator>(new AspNetCoreJobActivator(scopeFactory));
                });
            //}
            #endregion

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<AutenticacaoIdentityServer> autenticacaoIdentityServerConfig)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",

                AutomaticAuthenticate = true,

                ExpireTimeSpan = TimeSpan.FromMinutes(60),
                CookieName = "mvchybrid"
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            AutenticacaoIdentityServer autenticacaoIdentityServer = autenticacaoIdentityServerConfig.Value;
            app.UseOpenIdConnectAuthentication(options =>{});


            #region Hangfire
            //Verica se está no ambiente de desenvolvimento para não iniciar o serviço
            //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development")
            //{
            //var authorizationFilters = new IAuthorizationFilter[]
            //                                   {
            //                                       new AuthorizationFilter
            //                                       {
            //                                           Roles = "MyAuthorizedRole1;MyAuthorizedRole2"
            //                                       },
            //                                   };

            app.UseHangfireDashboard("/jobscheduler");
                app.UseHangfireServer();
                app.UseHangfire();
            //}
            #endregion

            app.UseMvc();
        }
    }
}
