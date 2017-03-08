﻿using Apresentacao.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Organograma.Apresentacao.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Organograma.WebAPI.Base
{
    [Authorize]
    public class BaseController : Controller
    {
        private IOrganizacaoWorkService service;

        private List<KeyValuePair<string, string>> usuarioAutenticado;
        public List<KeyValuePair<string, string>> UsuarioAutenticado
        {
            get
            {
                return usuarioAutenticado;
            }
        }

        public BaseController(IOrganizacaoWorkService service, IHttpContextAccessor httpContextAccessor)
        {
            this.service = service;
            PreencherUsuario(httpContextAccessor.HttpContext.User);
        }

        private T DownloadJsonData<T>(string url, string acessToken) where T : new()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrWhiteSpace(acessToken))
                    client.SetBearerToken(acessToken);

                var result = client.GetAsync(url).Result;

                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return new T();
                }
            }
        }

        private void PreencherUsuario(ClaimsPrincipal user)
        {
            if (user != null)
            {
                Claim claimCpf = user.FindFirst("cpf");
                Claim claimNome = user.FindFirst("nome");
                if (claimCpf != null && claimNome != null)
                {
                    usuarioAutenticado = new List<KeyValuePair<string, string>>();

                    usuarioAutenticado.Add(new KeyValuePair<string, string>("cpf", claimCpf.Value));
                    usuarioAutenticado.Add(new KeyValuePair<string, string>("nome", claimNome.Value));

                    string accessToken = user.FindFirst("accessToken").Value;
                    usuarioAutenticado.Add(new KeyValuePair<string, string>("accessToken", accessToken));

                    List<Claim> claimsOrganizacao = user.FindAll("orgao").ToList();

                    if (claimsOrganizacao != null && claimsOrganizacao.Count > 0)
                    {
                        foreach (Claim c in claimsOrganizacao)
                        {
                            FillOrgaoEPatriarca(c.Value, accessToken);
                        }
                    }
                }
            }
        }


        private void FillOrgaoEPatriarca(string organizacaoSigla, string accessToken)
        {
            string urlApiOrganograma = Environment.GetEnvironmentVariable("UrlApiOrganograma");

            var url = urlApiOrganograma + "organizacoes/sigla/" + organizacaoSigla;

            //Organizacao organizacaoUsuario = DownloadJsonData<Organizacao>(urlApiOrganograma + "organizacoes/sigla/" + organizacaoSigla, accessToken);

            OrganizacaoModeloGet organizacaoUsuario = service.PesquisarPorSigla(organizacaoSigla);

            usuarioAutenticado.Add(new KeyValuePair<string, string>("guidOrganizacao", organizacaoUsuario.Guid));

            var organizacoesPatriarcas = usuarioAutenticado.Where(x => x.Key.Equals("guidOrganizacaoPatriarca"))
                                                           .Select(x => new OrganizacaoModeloGet { Guid = x.Value })
                                                           .ToList();

            //Organizacao organizacaoPatriarca = null;
            OrganizacaoModeloGet organizacaoPatriarca = null;

            if (organizacoesPatriarcas != null && organizacoesPatriarcas.Count > 0)
                organizacaoPatriarca = organizacoesPatriarcas.Where(op => op.Guid.Equals(organizacaoUsuario.OrganizacaoPai.Guid)).SingleOrDefault();

            if (organizacaoPatriarca == null)
            {
                url = urlApiOrganograma + "organizacoes/" + organizacaoUsuario.Guid + "/patriarca";

                //organizacaoPatriarca = DownloadJsonData<Organizacao>(url, accessToken);
                organizacaoPatriarca = service.PesquisarPatriarca(organizacaoUsuario.Guid);

                usuarioAutenticado.Add(new KeyValuePair<string, string>("guidOrganizacaoPatriarca", organizacaoPatriarca.Guid));
            }
        }

        class Organizacao
        {
            public string guid { get; set; }
            public Organizacao organizacaoPai { get; set; }
        }
    }
}
