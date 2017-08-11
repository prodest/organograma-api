using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Infraestrutura.Comum;
using Organograma.Negocio.Base;
using Organograma.Negocio.Commom.Base;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Modelos.Siarhes;
using Organograma.Negocio.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Negocio
{
    public class UnidadeNegocio : IUnidadeNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<Unidade> repositorioUnidades;
        private IRepositorioGenerico<Endereco> repositorioEnderecos;
        private IRepositorioGenerico<Contato> repositorioContatos;
        private IRepositorioGenerico<ContatoUnidade> repositorioContatosUnidades;
        private IRepositorioGenerico<Email> repositorioEmails;
        private IRepositorioGenerico<EmailUnidade> repositorioEmailsUnidades;
        private IRepositorioGenerico<Historico> repositorioHistoricos;
        private IRepositorioGenerico<IdentificadorExterno> repositorioIdentificadoresExternos;
        private IRepositorioGenerico<Municipio> repositorioMunicipios;
        private IRepositorioGenerico<Organizacao> repositorioOrganizcoes;
        private IRepositorioGenerico<Site> repositorioSites;
        private IRepositorioGenerico<SiteUnidade> repositorioSitesUnidades;
        private UnidadeValidacao unidadeValidacao;
        private TipoUnidadeValidacao tipoUnidadeValidacao;
        private OrganizacaoValidacao organizacaoValidacao;
        private EnderecoValidacao enderecoValidacao;
        private ContatoValidacao contatoValidacao;
        private EmailValidacao emailValidacao;
        private SiteValidacao siteValidacao;

        private IClientAccessToken _clientAccessToken;

        public UnidadeNegocio(IOrganogramaRepositorios repositorios, IClientAccessToken clientAccessToken)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioUnidades = repositorios.Unidades;
            repositorioEnderecos = repositorios.Enderecos;
            repositorioContatos = repositorios.Contatos;
            repositorioContatosUnidades = repositorios.ContatosUnidades;
            repositorioEmails = repositorios.Emails;
            repositorioEmailsUnidades = repositorios.EmailsUnidades;
            repositorioHistoricos = repositorios.Historicos;
            repositorioIdentificadoresExternos = repositorios.IdentificadoresExternos;
            repositorioMunicipios = repositorios.Municipios;
            repositorioOrganizcoes = repositorios.Organizacoes;
            repositorioSites = repositorios.Sites;
            repositorioSitesUnidades = repositorios.SitesUnidades;


            unidadeValidacao = new UnidadeValidacao(repositorioUnidades, repositorios.TiposUnidades, repositorios.Organizacoes);
            tipoUnidadeValidacao = new TipoUnidadeValidacao(repositorios.TiposUnidades);
            organizacaoValidacao = new OrganizacaoValidacao(repositorios.Organizacoes);
            enderecoValidacao = new EnderecoValidacao(repositorios.Enderecos, repositorios.Municipios);
            contatoValidacao = new ContatoValidacao(repositorios.Contatos, repositorios.TiposContatos);
            emailValidacao = new EmailValidacao();
            siteValidacao = new SiteValidacao();

            _clientAccessToken = clientAccessToken;
        }

        public void Alterar(string guid, UnidadeModeloNegocio unidade)
        {
            #region Verificação básica de Id
            unidadeValidacao.NaoNula(unidade);

            unidadeValidacao.GuidValido(guid);
            unidadeValidacao.GuidAlteracaoValido(guid, unidade);
            #endregion

            Guid g = new Guid(unidade.Guid);
            var unidadeDominio = repositorioUnidades.Where(u => u.IdentificadorExterno.Guid.Equals(g))
                                                    .Include(un => un.IdentificadorExterno)
                                                    .Include(un => un.Organizacao).ThenInclude(o => o.IdentificadorExterno)
                                                    .Include(un => un.TipoUnidade)
                                                    .Include(un => un.UnidadePai).ThenInclude(up => up.IdentificadorExterno)
                                                    .SingleOrDefault();

            //Verificação da unidade na base de dados
            unidadeValidacao.NaoEncontrado(unidadeDominio);

            Mapper.Map(unidadeDominio, unidade);

            #region Verificação de campos obrigatórios
            unidadeValidacao.NaoNula(unidade);
            unidadeValidacao.Preenchida(unidade);

            tipoUnidadeValidacao.NaoNulo(unidade.TipoUnidade);
            tipoUnidadeValidacao.IdPreenchido(unidade.TipoUnidade);

            organizacaoValidacao.NaoNulo(unidade.Organizacao);
            organizacaoValidacao.IdPreenchido(unidade.Organizacao);

            unidadeValidacao.UnidadePaiPreenchida(unidade.UnidadePai);
            #endregion

            #region Validação de Negócio
            unidadeValidacao.Valida(unidade);

            tipoUnidadeValidacao.Existe(unidade.TipoUnidade);

            organizacaoValidacao.Existe(unidade.Organizacao);

            unidadeValidacao.UnidadePaiValida(unidade.UnidadePai);
            if (unidade.UnidadePai != null)
                unidade.UnidadePai.Guid = null;

            unidade.Guid = null;
            #endregion

            DateTime agora = DateTime.Now;

            InserirHistorico(unidadeDominio, "Edição", agora);

            Mapper.Map(unidade, unidadeDominio);

            unidadeDominio.InicioVigencia = agora;

            unitOfWork.Save();
        }

        public void Excluir(string guid)
        {
            unidadeValidacao.GuidValido(guid);

            Guid g = new Guid(guid);
            var unidade = repositorioUnidades.Where(un => un.IdentificadorExterno.Guid.Equals(g))
                                             .Include(u => u.IdentificadorExterno)
                                             .Include(u => u.Endereco)
                                             .Include(u => u.ContatosUnidade).ThenInclude(cu => cu.Contato)
                                             .Include(u => u.EmailsUnidade).ThenInclude(eu => eu.Email)
                                             .Include(u => u.SitesUnidade).ThenInclude(su => su.Site)
                                             .SingleOrDefault();

            unidadeValidacao.NaoEncontrado(unidade);

            unidadeValidacao.PossuiFilho(unidade.Id);

            InserirHistorico(unidade, "Exclusão", null);

            if (unidade.Endereco != null)
                ExcluirEndereco(unidade);

            foreach (var cu in unidade.ContatosUnidade)
            {
                ExcluirContato(cu);
            }

            foreach (var eu in unidade.EmailsUnidade)
            {
                ExcluirEmail(eu);
            }

            foreach (var su in unidade.SitesUnidade)
            {
                ExcluirSite(su);
            }

            repositorioUnidades.Remove(unidade);

            unitOfWork.Save();
        }

        public UnidadeModeloNegocio Inserir(UnidadeModeloNegocio unidade)
        {
            #region Verificação de campos obrigatórios

            unidadeValidacao.NaoNula(unidade);
            unidadeValidacao.Preenchida(unidade);

            tipoUnidadeValidacao.NaoNulo(unidade.TipoUnidade);
            tipoUnidadeValidacao.IdPreenchido(unidade.TipoUnidade);

            organizacaoValidacao.NaoNulo(unidade.Organizacao);
            organizacaoValidacao.GuidPreenchido(unidade.Organizacao);

            unidadeValidacao.UnidadePaiPreenchida(unidade.UnidadePai);

            enderecoValidacao.Preenchido(unidade.Endereco);

            contatoValidacao.Preenchido(unidade.Contatos);

            emailValidacao.Preenchido(unidade.Emails);

            siteValidacao.Preenchido(unidade.Sites);

            #endregion

            #region Validação de Negócio

            unidadeValidacao.Valida(unidade);

            tipoUnidadeValidacao.Existe(unidade.TipoUnidade);

            organizacaoValidacao.GuidValido(unidade.Organizacao.Guid);
            organizacaoValidacao.ExistePorGuid(unidade.Organizacao);

            unidadeValidacao.UnidadePaiValida(unidade.UnidadePai);

            enderecoValidacao.Valido(unidade.Endereco);

            contatoValidacao.Valido(unidade.Contatos);

            emailValidacao.Valido(unidade.Emails);

            siteValidacao.Valido(unidade.Sites);

            #endregion
            Guid guidOrganziacao = new Guid(unidade.Organizacao.Guid);
            unidade.Organizacao.Id = repositorioOrganizcoes.Where(o => o.IdentificadorExterno.Guid.Equals(guidOrganziacao))
                                                           .Select(o => o.Id)
                                                           .Single();

            if (unidade.UnidadePai != null)
            {
                Guid guidUnidadePai = new Guid(unidade.UnidadePai.Guid);
                unidade.UnidadePai.Id = repositorioUnidades.Where(u => u.IdentificadorExterno.Guid.Equals(guidUnidadePai))
                                                           .Select(u => u.Id)
                                                           .Single();
            }

            if (unidade.Endereco != null)
            {
                Guid guidMunicipio = new Guid(unidade.Endereco.Municipio.Guid);
                unidade.Endereco.Municipio.Id = repositorioMunicipios.Where(m => m.IdentificadorExterno.Guid.Equals(guidMunicipio))
                                                                  .Select(m => m.Id)
                                                                  .Single();
            }

            unidade.Guid = Guid.NewGuid().ToString("D");

            var unid = Mapper.Map<UnidadeModeloNegocio, Unidade>(unidade);
            unid.InicioVigencia = DateTime.Now;

            repositorioUnidades.Add(unid);

            unitOfWork.Save();

            return Mapper.Map<Unidade, UnidadeModeloNegocio>(unid);
        }

        public List<UnidadeModeloNegocio> Listar()
        {
            var unidades = repositorioUnidades.ToList();

            unidadeValidacao.NaoEncontrado(unidades);

            return Mapper.Map<List<Unidade>, List<UnidadeModeloNegocio>>(unidades);
        }

        public UnidadeModeloNegocio Pesquisar(string guid)
        {
            unidadeValidacao.GuidValido(guid);

            Guid g = new Guid(guid);

            var unidade = repositorioUnidades.Where(u => u.IdentificadorExterno.Guid.Equals(g))
                                             .Include(u => u.TipoUnidade)
                                             .Include(u => u.Organizacao).ThenInclude(o => o.IdentificadorExterno)
                                             .Include(u => u.UnidadePai)
                                             .Include(u => u.Endereco).ThenInclude(u => u.Municipio).ThenInclude(m => m.IdentificadorExterno)
                                             .Include(u => u.ContatosUnidade).ThenInclude(u => u.Contato).ThenInclude(u => u.TipoContato)
                                             .Include(u => u.EmailsUnidade).ThenInclude(u => u.Email)
                                             .Include(u => u.SitesUnidade).ThenInclude(u => u.Site)
                                             .Include(u => u.IdentificadorExterno)
                                             .SingleOrDefault();

            unidadeValidacao.NaoEncontrado(unidade);

            return Mapper.Map<Unidade, UnidadeModeloNegocio>(unidade); ;
        }

        public List<UnidadeModeloNegocio> PesquisarPorOrganizacao(string guid)
        {
            unidadeValidacao.GuidValido(guid);

            Guid g = new Guid(guid);

            var unidades = repositorioUnidades.Where(u => u.Organizacao.IdentificadorExterno.Guid.Equals(g))
                                             .Include(u => u.TipoUnidade)
                                             .Include(u => u.UnidadePai)
                                             //.Include(u => u.Endereco).ThenInclude(u => u.Municipio).ThenInclude(m => m.IdentificadorExterno)
                                             //.Include(u => u.ContatosUnidade).ThenInclude(u => u.Contato).ThenInclude(u => u.TipoContato)
                                             //.Include(u => u.EmailsUnidade).ThenInclude(u => u.Email)
                                             //.Include(u => u.SitesUnidade).ThenInclude(u => u.Site)
                                             .Include(u => u.IdentificadorExterno)
                                             .OrderBy(u => u.Nome)
                                             .ToList();

            var un = Mapper.Map<List<Unidade>, List<UnidadeModeloNegocio>>(unidades);

            return un;

        }

        private void ExcluirIdentificadorExterno(Unidade unidade)
        {
            repositorioIdentificadoresExternos.Remove(unidade.IdentificadorExterno);
        }

        private void ExcluirEndereco(Unidade unidade)
        {
            repositorioEnderecos.Remove(unidade.Endereco);
        }

        private void ExcluirContato(ContatoUnidade contatoUnidade)
        {
            repositorioContatos.Remove(contatoUnidade.Contato);
            repositorioContatosUnidades.Remove(contatoUnidade);
        }

        private void ExcluirEmail(EmailUnidade emailUnidade)
        {
            repositorioEmails.Remove(emailUnidade.Email);
            repositorioEmailsUnidades.Remove(emailUnidade);
        }

        private void ExcluirSite(SiteUnidade siteUnidade)
        {
            repositorioSites.Remove(siteUnidade.Site);
            repositorioSitesUnidades.Remove(siteUnidade);
        }

        private void InserirHistorico(Unidade unidade, string obsFimVigencia, DateTime? now)
        {
            string municipioJson = JsonData.SerializeObject(unidade);

            Historico historico = new Historico
            {
                Json = municipioJson,
                InicioVigencia = unidade.InicioVigencia,
                FimVigencia = now.HasValue ? now.Value : DateTime.Now,
                ObservacaoFimVigencia = obsFimVigencia,
                IdIdentificadorExterno = unidade.IdentificadorExterno.Id
            };

            repositorioHistoricos.Add(historico);
        }

        public async Task<UnidadeModeloNegocio.Responsavel> PesquisarResponsavel(string guid)
        {
            Guid g = new Guid(guid);

            Unidade unidade = await repositorioUnidades.Where(u => u.IdentificadorExterno.Guid.Equals(g))
                                                       .Include(u => u.Organizacao)
                                                       .SingleOrDefaultAsync();

            UnidadeModeloNegocio.Responsavel responsavel = null;

            if (unidade != null)
            {
                string _baseUrlSiarhes = "https://api.es.gov.br/siarhes/v1/";

                List<GestorSiarhes> gestor = await JsonData.DownloadAsync<List<GestorSiarhes>>($"{_baseUrlSiarhes}organograma/setor/gestor?empresa={unidade.Organizacao.IdEmpresaSiarhes}&setor={unidade.Sigla}", _clientAccessToken.AccessToken);

                if (gestor != null && gestor.Count == 1)
                {
                    List<FuncionarioSiarhes> funcionario = await JsonData.DownloadAsync<List<FuncionarioSiarhes>>($"{_baseUrlSiarhes}funcionarios?numfunc={gestor[0].NumFunc}", _clientAccessToken.AccessToken);

                    if (funcionario != null && funcionario.Count == 1)
                    {
                        responsavel = Mapper.Map<FuncionarioSiarhes, UnidadeModeloNegocio.Responsavel>(funcionario[0]);
                    }
                }
            }

            return responsavel;
        }
    }
}
