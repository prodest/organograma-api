using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Organograma.WebAPI.Config;
using Swashbuckle.Swagger.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using Organograma.Infraestrutura.Mapeamento;
using Organograma.WebAPI.Middleware;

namespace Organograma.WebAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();


            OrganogramaContext.ConnectionString = Environment.GetEnvironmentVariable("OrganogramaConnectionString");
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configurar o objeto AutenticacaoIdentityServer para ser usado na autenticação
            services.Configure<AutenticacaoIdentityServer>(Configuration.GetSection("AutenticacaoIdentityServer"));

            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            ConfiguracaoDependencias.InjetarDependencias(services);
            ConfiguracaoAutoMapper.CriarMapeamento();

            #region Políticas que serão concedidas
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Esfera.Inserir", policy => policy.RequireClaim("Acao$Esfera", "Inserir"));
                options.AddPolicy("Esfera.Alterar", policy => policy.RequireClaim("Acao$Esfera", "Alterar"));
                options.AddPolicy("Esfera.Excluir", policy => policy.RequireClaim("Acao$Esfera", "Excluir"));
                options.AddPolicy("Municipio.Inserir", policy => policy.RequireClaim("Acao$Municipio", "Inserir"));
                options.AddPolicy("Municipio.Alterar", policy => policy.RequireClaim("Acao$Municipio", "Alterar"));
                options.AddPolicy("Municipio.Excluir", policy => policy.RequireClaim("Acao$Municipio", "Excluir"));
                options.AddPolicy("Organizacao.Inserir", policy => policy.RequireClaim("Acao$Organizacao", "Inserir"));
                options.AddPolicy("Organizacao.Alterar", policy => policy.RequireClaim("Acao$Organizacao", "Alterar"));
                options.AddPolicy("Organizacao.Excluir", policy => policy.RequireClaim("Acao$Organizacao", "Excluir"));
                options.AddPolicy("Poder.Inserir", policy => policy.RequireClaim("Acao$Poder", "Inserir"));
                options.AddPolicy("Poder.Alterar", policy => policy.RequireClaim("Acao$Poder", "Alterar"));
                options.AddPolicy("Poder.Excluir", policy => policy.RequireClaim("Acao$Poder", "Excluir"));
                options.AddPolicy("TipoOrganizacao.Inserir", policy => policy.RequireClaim("Acao$TipoOrganizacao", "Inserir"));
                options.AddPolicy("TipoOrganizacao.Alterar", policy => policy.RequireClaim("Acao$TipoOrganizacao", "Alterar"));
                options.AddPolicy("TipoOrganizacao.Excluir", policy => policy.RequireClaim("Acao$TipoOrganizacao", "Excluir"));
                options.AddPolicy("TipoUnidade.Inserir", policy => policy.RequireClaim("Acao$TipoUnidade", "Inserir"));
                options.AddPolicy("TipoUnidade.Alterar", policy => policy.RequireClaim("Acao$TipoUnidade", "Alterar"));
                options.AddPolicy("TipoUnidade.Excluir", policy => policy.RequireClaim("Acao$TipoUnidade", "Excluir"));
                options.AddPolicy("Unidade.Inserir", policy => policy.RequireClaim("Acao$Unidade", "Inserir"));
                options.AddPolicy("Unidade.Alterar", policy => policy.RequireClaim("Acao$Unidade", "Alterar"));
                options.AddPolicy("Unidade.Excluir", policy => policy.RequireClaim("Acao$Unidade", "Excluir"));
            }
            );
            #endregion

            #region Configuração do Swagger
            // Inject an implementation of ISwaggerProvider with defaulted settings applied
            services.AddSwaggerGen();

            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Organograma Web API",
                    Description = "Núcleo de serviço do sistema Organograma implementado pelo Governo do Estado do Espírito Santo.",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "PRODEST",
                        Email = "atendimento@prodest.es.gov.br",
                        Url = "http://prodest.es.gov.br"
                    }
                });

                //Determine base path for the application.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;

                //Set the comments path for the swagger json and ui.
                var xmlPath = Path.Combine(basePath, "WebAPI.xml");
                options.IncludeXmlComments(xmlPath);
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<AutenticacaoIdentityServer> autenticacaoIdentityServerConfig)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            #region Configurações de autenticação
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            AutenticacaoIdentityServer autenticacaoIdentityServer = autenticacaoIdentityServerConfig.Value;
            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = autenticacaoIdentityServer.Authority,
                RequireHttpsMetadata = autenticacaoIdentityServer.RequireHttpsMetadata,

                AllowedScopes = autenticacaoIdentityServer.AllowedScopes,
                AutomaticAuthenticate = autenticacaoIdentityServer.AutomaticAuthenticate
            });
            #endregion

            #region Configuração para buscar as permissões do usuário
            app.UseRequestUserInfo(new RequestUserInfoOptions
            {
                UserInfoEndpoint = autenticacaoIdentityServer.Authority + "connect/userinfo"
            });
            #endregion

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            var requestPath = Environment.GetEnvironmentVariable("REQUEST_PATH") ?? string.Empty;
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi("api/documentation", requestPath + "/swagger/v1/swagger.json");
        }
    }
}
