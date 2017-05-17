using Newtonsoft.Json;
using Organograma.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Organograma.Negocio
{
    public class BaseNegocio : IBaseNegocio
    {
        private readonly string UrlApiOrganograma = Environment.GetEnvironmentVariable("UrlApiOrganograma");
        private List<KeyValuePair<string, string>> usuario;
        private string usuarioCpf;
        private string usuarioNome;
        private string clientAccessToken;
        private List<Guid> usuarioGuidOrganizacoes;
        private List<Guid> usuarioGuidOrganizacoesPatriarca;

        public List<KeyValuePair<string, string>> Usuario
        {
            get
            {
                return usuario;
            }

            set
            {
                usuario = value;
            }
        }

        public string UsuarioCpf
        {
            get
            {
                if (usuarioCpf == null)
                    usuarioCpf = Usuario.Where(u => u.Key.Equals("cpf")).SingleOrDefault().Value;

                return usuarioCpf;
            }
        }
        public string UsuarioNome
        {
            get
            {
                if (usuarioNome == null)
                    usuarioNome = Usuario.Where(u => u.Key.Equals("nome")).SingleOrDefault().Value;

                return usuarioNome;
            }
        }
        public string ClientAccessToken
        {
            get
            {
                if (clientAccessToken == null)
                    clientAccessToken = Usuario.Where(u => u.Key.Equals("clientAccessToken")).SingleOrDefault().Value;

                return clientAccessToken;
            }
        }

        public List<Guid> UsuarioGuidOrganizacoes
        {
            get
            {
                if (usuarioGuidOrganizacoes == null || UsuarioGuidOrganizacoes.Count <= 0)
                {
                    List<Guid> guidOrganizacoes = Usuario.Where(u => u.Key.Equals("guidOrganizacao"))
                                                          .Select(u => new Guid(u.Value))
                                                          .ToList();

                    if (guidOrganizacoes != null && guidOrganizacoes.Count > 0)
                        usuarioGuidOrganizacoes = guidOrganizacoes;
                }

                return usuarioGuidOrganizacoes;
            }
        }
        public List<Guid> UsuarioGuidOrganizacoesPatriarca
        {
            get
            {
                if (usuarioGuidOrganizacoesPatriarca == null || usuarioGuidOrganizacoesPatriarca.Count <= 0)
                {
                    List<Guid> guidOrganizacoesPatriarca = Usuario.Where(u => u.Key.Equals("guidOrganizacaoPatriarca"))
                                                                 .Select(u => new Guid(u.Value))
                                                                 .ToList();

                    if (guidOrganizacoesPatriarca != null && guidOrganizacoesPatriarca.Count > 0)
                        usuarioGuidOrganizacoesPatriarca = guidOrganizacoesPatriarca;
                }

                return usuarioGuidOrganizacoesPatriarca;
            }
        }

        private T DownloadJsonData<T>(string url) where T : new()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrWhiteSpace(ClientAccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ClientAccessToken);
                }
                var result = client.GetAsync(url).Result;



                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return default(T);
                }
            }

        }

        public OrganizacaoOrganogramaModelo PesquisarOrganizacaoPatriarca(Guid guidOrganizacao)
        {
            OrganizacaoOrganogramaModelo organizacao = DownloadJsonData<OrganizacaoOrganogramaModelo>(UrlApiOrganograma + "organizacoes/" + guidOrganizacao.ToString("D") + "/patriarca");
            return organizacao;
        }

        public OrganizacaoOrganogramaModelo PesquisarOrganizacao(Guid guidOrganizacao)
        {
            OrganizacaoOrganogramaModelo organizacao = DownloadJsonData<OrganizacaoOrganogramaModelo>(UrlApiOrganograma + "organizacoes/" + guidOrganizacao.ToString("D"));
            return organizacao;
        }

        public UnidadeOrganogramaModelo PesquisarUnidade(Guid guidUnidade)
        {
            UnidadeOrganogramaModelo unidade = DownloadJsonData<UnidadeOrganogramaModelo>(UrlApiOrganograma + "unidades/" + guidUnidade.ToString("D"));
            return unidade;
        }

        public MunicipioOrganogramaModelo PesquisarMunicipio(Guid guidMunicipio)
        {
            MunicipioOrganogramaModelo municipio = DownloadJsonData<MunicipioOrganogramaModelo>(UrlApiOrganograma + "municipios/" + guidMunicipio.ToString("D"));
            return municipio;
        }

        public class OrganizacaoOrganogramaModelo
        {
            public string guid { get; set; }
            public string razaoSocial { get; set; }
            public string sigla { get; set; }
        }

        public class UnidadeOrganogramaModelo
        {
            public string guid { get; set; }
            public string nome { get; set; }
            public string sigla { get; set; }
            public OrganizacaoOrganogramaModelo organizacao { get; set; }
        }

        public class MunicipioOrganogramaModelo
        {
            public string guid { get; set; }
            public string nome { get; set; }
            public string uf { get; set; }
        }
    }
}
