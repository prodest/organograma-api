using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Organograma.WebAPI.Middleware
{
    public class RequestUserInfoMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RequestUserInfoOptions _options;

        public RequestUserInfoMiddleware(RequestDelegate next, RequestUserInfoOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            UserInfoClient userInfoClient = new UserInfoClient(_options.UserInfoEndpoint);

            string accessToken = await context.Authentication.GetTokenAsync("access_token");
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                UserInfoResponse userInfoResponse = await userInfoClient.GetAsync(accessToken);

                var id = new ClaimsIdentity();
                id.AddClaim(new Claim("accessToken", accessToken));

                var userInfoList = userInfoResponse.Claims.ToList();
                foreach (var ui in userInfoList)
                {
                    if (ui.Type != "permissao")
                    {
                        id.AddClaim(new Claim(ui.Type, ui.Value));
                    }
                }

                var permissaoClaims = userInfoResponse.Claims.Where(x => x.Type == "permissao").ToList();
                foreach (var permissaoClaim in permissaoClaims)
                {
                    dynamic objetoPermissao = JsonConvert.DeserializeObject(permissaoClaim.Value.ToString());
                    string recurso = objetoPermissao.Recurso;
                    id.AddClaim(new Claim("Recurso", recurso));
                    var listaAcoes = ((JArray)objetoPermissao.Acoes).Select(x => x.ToString()).ToList();
                    foreach (var acao in listaAcoes)
                    {
                        id.AddClaim(new Claim("Acao$" + recurso, acao));
                    }
                }

                context.User.AddIdentity(id);
            }

            await _next.Invoke(context);
        }
    }
}
