using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Infraestrutura.Comum;
using Organograma.Negocio.Base;
using Organograma.Negocio.Commom.Base;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Negocio
{
    public class OrganizacaoNegocio : BaseNegocio, IOrganizacaoNegocio
    {
        private IOrganogramaRepositorios repositorios;
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<Organizacao> repositorioOrganizacoes;
        private IRepositorioGenerico<Contato> repositorioContatos;
        private IRepositorioGenerico<ContatoOrganizacao> repositorioContatosOrganizacoes;
        private IRepositorioGenerico<Email> repositorioEmails;
        private IRepositorioGenerico<EmailOrganizacao> repositorioEmailsOrganizacoes;
        private IRepositorioGenerico<Endereco> repositorioEnderecos;
        private IRepositorioGenerico<Historico> repositorioHistoricos;
        private IRepositorioGenerico<IdentificadorExterno> repositorioIdentificadoresExternos;
        private IRepositorioGenerico<Municipio> repositorioMunicipios;
        private IRepositorioGenerico<Site> repositorioSites;
        private IRepositorioGenerico<SiteOrganizacao> repositorioSitesOrganizacoes;
        private IRepositorioGenerico<Unidade> repositorioUnidades;
        private OrganizacaoValidacao validacao;
        private CnpjValidacao cnpjValidacao;
        private ContatoValidacao contatoValidacao;
        private EmailValidacao emailValidacao;
        private EnderecoValidacao enderecoValidacao;
        private EsferaOrganizacaoValidacao esferaValidacao;
        private PoderValidacao poderValidacao;
        private SiteValidacao siteValidacao;
        private TipoOrganizacaoValidacao tipoOrganizacaoValidacao;
        private ICurrentUserProvider _currentUser;
        private IClientAccessToken _clientAccessToken;

        public OrganizacaoNegocio(IOrganogramaRepositorios repositorios, ICurrentUserProvider currentUser, IClientAccessToken clientAccessToken)
        {
            this.repositorios = repositorios;
            unitOfWork = repositorios.UnitOfWork;
            repositorioOrganizacoes = repositorios.Organizacoes;
            repositorioContatos = repositorios.Contatos;
            repositorioContatosOrganizacoes = repositorios.ContatosOrganizacoes;
            repositorioEmails = repositorios.Emails;
            repositorioEmailsOrganizacoes = repositorios.EmailsOrganizacoes;
            repositorioEnderecos = repositorios.Enderecos;
            repositorioHistoricos = repositorios.Historicos;
            repositorioIdentificadoresExternos = repositorios.IdentificadoresExternos;
            repositorioMunicipios = repositorios.Municipios;
            repositorioSites = repositorios.Sites;
            repositorioSitesOrganizacoes = repositorios.SitesOrganizacoes;
            repositorioUnidades = repositorios.Unidades;

            validacao = new OrganizacaoValidacao(repositorioOrganizacoes);
            cnpjValidacao = new CnpjValidacao(repositorioOrganizacoes);
            contatoValidacao = new ContatoValidacao(repositorios.Contatos, repositorios.TiposContatos);
            emailValidacao = new EmailValidacao();
            enderecoValidacao = new EnderecoValidacao(repositorios.Enderecos, repositorios.Municipios);
            esferaValidacao = new EsferaOrganizacaoValidacao(repositorios.EsferasOrganizacoes);
            poderValidacao = new PoderValidacao(repositorios.Poderes);
            siteValidacao = new SiteValidacao();
            tipoOrganizacaoValidacao = new TipoOrganizacaoValidacao(repositorios.TiposOrganizacoes);

            _currentUser = currentUser;
            _clientAccessToken = clientAccessToken;
        }

        #region Alterar
        public void Alterar(string guid, OrganizacaoModeloNegocio organizacaoNegocio)
        {
            validacao.GuidPreenchido(guid);
            validacao.GuidValido(guid);
            validacao.NaoNulo(organizacaoNegocio);
            validacao.GuidPreenchido(organizacaoNegocio.Guid);
            validacao.GuidValido(organizacaoNegocio);
            validacao.GuidAlteracaoValido(guid, organizacaoNegocio);

            Organizacao organizacao = BuscaObjetoDominio(organizacaoNegocio);

            validacao.Preenchido(organizacaoNegocio);
            validacao.Valido(organizacaoNegocio);
            validacao.PaiValido(organizacaoNegocio.OrganizacaoPai);

            cnpjValidacao.CnpjExiste(organizacaoNegocio);
            cnpjValidacao.CnpjValido(organizacaoNegocio.Cnpj);
            esferaValidacao.IdPreenchido(organizacaoNegocio.Esfera);
            esferaValidacao.IdValido(organizacaoNegocio.Esfera);
            poderValidacao.IdPreenchido(organizacaoNegocio.Poder);
            poderValidacao.IdValido(organizacaoNegocio.Poder);
            tipoOrganizacaoValidacao.IdPreenchido(organizacaoNegocio.TipoOrganizacao);
            tipoOrganizacaoValidacao.IdValido(organizacaoNegocio.TipoOrganizacao);

            validacao.Existe(organizacaoNegocio);
            esferaValidacao.Existe(organizacaoNegocio.Esfera);
            poderValidacao.Existe(organizacaoNegocio.Poder);
            tipoOrganizacaoValidacao.Existe(organizacaoNegocio.TipoOrganizacao);

            DateTime agora = DateTime.Now;

            InserirHistorico(organizacao, "Edição", agora);

            Mapper.Map(organizacaoNegocio, organizacao);

            organizacao.InicioVigencia = agora;

            unitOfWork.Save();
        }

        #endregion

        #region Excluir
        public void Excluir(string guid)
        {
            ExcluirSemSalvar(guid);

            unitOfWork.Save();
        }

        public void ExcluirSemSalvar(string guid)
        {
            validacao.GuidValido(guid);
            validacao.Existe(guid);
            Guid g = new Guid(guid);

            validacao.UsuarioTemPermissao(_currentUser.UserGuidsOrganizacao, guid);

            Organizacao organizacao = repositorioOrganizacoes.Where(o => o.IdentificadorExterno.Guid.Equals(g))
                                                             .Include(o => o.IdentificadorExterno)
                                                             .Include(i => i.Endereco)
                                                             .Include(i => i.ContatosOrganizacao).ThenInclude(c => c.Contato)
                                                             .Include(i => i.SitesOrganizacao).ThenInclude(s => s.Site)
                                                             .Include(i => i.EmailsOrganizacao).ThenInclude(s => s.Email).Single();

            validacao.PossuiFilho(organizacao.Id);
            validacao.PossuiUnidade(organizacao.Id);

            InserirHistorico(organizacao, "Exclusão", null);

            ExcluiContatos(organizacao);
            ExcluiEndereco(organizacao);
            ExcluiEmails(organizacao);
            ExcluiSites(organizacao);
            repositorioOrganizacoes.Remove(organizacao);
        }
        #endregion

        #region Inserir
        public OrganizacaoModeloNegocio InserirFilha(OrganizacaoModeloNegocio organizacaoNegocio)
        {
            //Preenchimentos primeiro (pois não interagem com banco de dados nem fazem validações complexas)
            validacao.Preenchido(organizacaoNegocio);
            validacao.PaiPreenchido(organizacaoNegocio.OrganizacaoPai);
            contatoValidacao.Preenchido(organizacaoNegocio.Contatos);
            emailValidacao.Preenchido(organizacaoNegocio.Emails);
            enderecoValidacao.NaoNulo(organizacaoNegocio.Endereco);
            enderecoValidacao.Preenchido(organizacaoNegocio.Endereco);
            //esferaValidacao.IdPreenchido(organizacaoNegocio.Esfera);
            //poderValidacao.IdPreenchido(organizacaoNegocio.Poder);
            siteValidacao.Preenchido(organizacaoNegocio.Sites);
            tipoOrganizacaoValidacao.IdPreenchido(organizacaoNegocio.TipoOrganizacao);

            //Validações utilizam cálculos e/ou interagem com o banco de dados
            validacao.PaiValido(organizacaoNegocio.OrganizacaoPai);
            validacao.Valido(organizacaoNegocio);

            if (organizacaoNegocio.OrganizacaoPai != null)
                validacao.UsuarioTemPermissao(_currentUser.UserGuidsOrganizacao, organizacaoNegocio.OrganizacaoPai.Guid);

            contatoValidacao.Valido(organizacaoNegocio.Contatos);
            emailValidacao.Valido(organizacaoNegocio.Emails);
            enderecoValidacao.Valido(organizacaoNegocio.Endereco);
            //esferaValidacao.Existe(organizacaoNegocio.Esfera);
            //poderValidacao.Existe(organizacaoNegocio.Poder);
            siteValidacao.Valido(organizacaoNegocio.Sites);
            tipoOrganizacaoValidacao.Existe(organizacaoNegocio.TipoOrganizacao);


            Organizacao organizacao = PreparaInsercao(organizacaoNegocio);
            organizacao.InicioVigencia = DateTime.Now;

            repositorioOrganizacoes.Add(organizacao);
            unitOfWork.Attach(organizacao.TipoOrganizacao);
            unitOfWork.Attach(organizacao.Esfera);
            unitOfWork.Attach(organizacao.Poder);
            unitOfWork.Save();

            return Mapper.Map<Organizacao, OrganizacaoModeloNegocio>(organizacao);
        }

        public OrganizacaoModeloNegocio InserirPatriarca(OrganizacaoModeloNegocio organizacaoNegocio)
        {
            //Preenchimentos primeiro (pois não interagem com banco de dados nem fazem validações complexas)
            validacao.Preenchido(organizacaoNegocio);
            contatoValidacao.Preenchido(organizacaoNegocio.Contatos);
            emailValidacao.Preenchido(organizacaoNegocio.Emails);
            enderecoValidacao.NaoNulo(organizacaoNegocio.Endereco);
            enderecoValidacao.Preenchido(organizacaoNegocio.Endereco);
            esferaValidacao.IdPreenchido(organizacaoNegocio.Esfera);
            poderValidacao.IdPreenchido(organizacaoNegocio.Poder);
            siteValidacao.Preenchido(organizacaoNegocio.Sites);
            tipoOrganizacaoValidacao.IdPreenchido(organizacaoNegocio.TipoOrganizacao);

            //Validações utilizam cálculos e/ou interagem com o banco de dados
            validacao.Valido(organizacaoNegocio);
            validacao.PaiValido(organizacaoNegocio.OrganizacaoPai);
            contatoValidacao.Valido(organizacaoNegocio.Contatos);
            emailValidacao.Valido(organizacaoNegocio.Emails);
            enderecoValidacao.Valido(organizacaoNegocio.Endereco);
            esferaValidacao.Existe(organizacaoNegocio.Esfera);
            poderValidacao.Existe(organizacaoNegocio.Poder);
            siteValidacao.Valido(organizacaoNegocio.Sites);
            tipoOrganizacaoValidacao.Existe(organizacaoNegocio.TipoOrganizacao);

            if (organizacaoNegocio.OrganizacaoPai != null)
                validacao.UsuarioTemPermissao(_currentUser.UserGuidsOrganizacao, organizacaoNegocio.OrganizacaoPai.Guid);

            Organizacao organizacao = PreparaInsercao(organizacaoNegocio);
            organizacao.InicioVigencia = DateTime.Now;

            repositorioOrganizacoes.Add(organizacao);
            unitOfWork.Attach(organizacao.TipoOrganizacao);
            unitOfWork.Attach(organizacao.Esfera);
            unitOfWork.Attach(organizacao.Poder);
            unitOfWork.Save();

            return Mapper.Map<Organizacao, OrganizacaoModeloNegocio>(organizacao);
        }

        public SiteModeloNegocio InserirSite(SiteModeloNegocio siteModeloNegocio)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Listar

        public List<OrganizacaoModeloNegocio> Listar(string esfera, string poder, string uf, int codIbgeMunicipio)
        {
            IQueryable<Organizacao> query = repositorioOrganizacoes;

            if (!string.IsNullOrWhiteSpace(esfera))
            {
                query = query.Where(o => o.Esfera.Descricao.ToUpper().Equals(esfera.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(poder))
            {
                query = query.Where(o => o.Poder.Descricao.ToUpper().Equals(poder.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(uf))
            {
                query = query.Where(o => o.Endereco.Municipio.Uf.ToUpper().Equals(uf.ToUpper()));
            }

            if (codIbgeMunicipio > 0)
            {
                query = query.Where(o => o.Endereco.Municipio.CodigoIbge == codIbgeMunicipio);
            }

            query = query.Where(o => o.OrganizacaoPai == null);

            query = query.Include(o => o.Esfera)
                         .Include(o => o.Poder)
                         .Include(o => o.IdentificadorExterno)
                         .OrderBy(o => o.RazaoSocial);

            List<Organizacao> organizacoes = query.ToList();

            var orgs = Mapper.Map<List<Organizacao>, List<OrganizacaoModeloNegocio>>(organizacoes);

            return orgs;

        }

        #endregion

        #region Pesquisar
        public OrganizacaoModeloNegocio Pesquisar(string guid)
        {
            validacao.GuidValido(guid);

            Guid g = new Guid(guid);

            Organizacao organizacao = repositorioOrganizacoes.Where(o => o.IdentificadorExterno.Guid.Equals(g))
                                                             .Include(e => e.Endereco).ThenInclude(m => m.Municipio).ThenInclude(m => m.IdentificadorExterno)
                                                             .Include(e => e.Esfera)
                                                             .Include(p => p.Poder)
                                                             .Include(c => c.ContatosOrganizacao).ThenInclude(co => co.Contato).ThenInclude(tc => tc.TipoContato)
                                                             .Include(eo => eo.EmailsOrganizacao).ThenInclude(e => e.Email)
                                                             .Include(so => so.SitesOrganizacao).ThenInclude(s => s.Site)
                                                             .Include(to => to.TipoOrganizacao)
                                                             .Include(to => to.IdentificadorExterno)
                                                             .SingleOrDefault();

            validacao.NaoEncontrado(organizacao);

            PreencheOrganizacaoPai(organizacao);

            OrganizacaoModeloNegocio organizacaoNegocio = new OrganizacaoModeloNegocio();

            return Mapper.Map(organizacao, organizacaoNegocio);

        }

        public OrganizacaoModeloNegocio PesquisarPorSigla(string sigla)
        {
            OrganizacaoModeloNegocio organizacaoNegocio = new OrganizacaoModeloNegocio();

            Organizacao organizacao = repositorioOrganizacoes.Where(o => o.Sigla.Trim().ToUpper().Equals(sigla.Trim().ToUpper()))
                                                             .Include(e => e.Endereco).ThenInclude(m => m.Municipio).ThenInclude(m => m.IdentificadorExterno)
                                                             .Include(e => e.Esfera)
                                                             .Include(p => p.Poder)
                                                             .Include(c => c.ContatosOrganizacao).ThenInclude(co => co.Contato).ThenInclude(tc => tc.TipoContato)
                                                             .Include(eo => eo.EmailsOrganizacao).ThenInclude(e => e.Email)
                                                             .Include(so => so.SitesOrganizacao).ThenInclude(s => s.Site)
                                                             .Include(to => to.TipoOrganizacao)
                                                             .Include(to => to.IdentificadorExterno)
                                                             .SingleOrDefault();

            validacao.NaoEncontrado(organizacao);

            PreencheOrganizacaoPai(organizacao);

            return Mapper.Map(organizacao, organizacaoNegocio);

        }

        public OrganizacaoModeloNegocio PesquisarPatriarca(string guid)
        {
            OrganizacaoModeloNegocio organizacaoNegocio = new OrganizacaoModeloNegocio();

            Guid g = new Guid(guid);
            Organizacao organizacao = repositorioOrganizacoes.Where(o => o.IdentificadorExterno.Guid.Equals(g))
                                                             .SingleOrDefault();

            validacao.NaoEncontrado(organizacao);

            int idOrganizacaoPatriarca = ObterOrganizacaoPatriarca(organizacao);

            Organizacao organizacaoPatriarca = repositorioOrganizacoes.Where(o => o.Id == idOrganizacaoPatriarca)
                                                                      .Include(e => e.Endereco).ThenInclude(m => m.Municipio).ThenInclude(m => m.IdentificadorExterno)
                                                                      .Include(e => e.Esfera)
                                                                      .Include(p => p.Poder)
                                                                      .Include(c => c.ContatosOrganizacao).ThenInclude(co => co.Contato).ThenInclude(tc => tc.TipoContato)
                                                                      .Include(eo => eo.EmailsOrganizacao).ThenInclude(e => e.Email)
                                                                      .Include(so => so.SitesOrganizacao).ThenInclude(s => s.Site)
                                                                      .Include(to => to.TipoOrganizacao)
                                                                      .Include(to => to.IdentificadorExterno)
                                                                      .SingleOrDefault();

            return Mapper.Map(organizacaoPatriarca, organizacaoNegocio);
        }

        public List<OrganizacaoModeloNegocio> PesquisarFilhas(string guid)
        {
            Guid g = new Guid(guid);
            Organizacao organizacao = repositorioOrganizacoes.Where(o => o.IdentificadorExterno.Guid.Equals(g))
                                                             .Include(o => o.OrganizacoesFilhas)
                                                             .Include(o => o.Esfera)
                                                             .Include(o => o.Poder)
                                                             .Include(o => o.IdentificadorExterno)
                                                             .Include(o => o.Endereco).ThenInclude(e => e.Municipio).ThenInclude(m => m.IdentificadorExterno)
                                                             .SingleOrDefault();

            validacao.NaoEncontrado(organizacao);

            List<Organizacao> organizacoesFilhas = ObterOrganizacoesFilhas(organizacao);

            return Mapper.Map<List<Organizacao>, List<OrganizacaoModeloNegocio>>(organizacoesFilhas);
        }

        public OrganizacaoModeloNegocio PesquisarOrganograma(string guid, bool filhas)
        {
            Guid g = new Guid(guid);
            Organizacao organizacao = repositorioOrganizacoes.Where(o => o.IdentificadorExterno.Guid.Equals(g))
                                                             .Include(o => o.Esfera)
                                                             .Include(o => o.Poder)
                                                             .Include(o => o.IdentificadorExterno)
                                                             .SingleOrDefault();

            validacao.NaoEncontrado(organizacao);

            OrganizacaoModeloNegocio omn = Mapper.Map<Organizacao, OrganizacaoModeloNegocio>(organizacao);

            if (filhas)
            {
                var organizacoes = repositorioOrganizacoes.Where(o => o.Id != organizacao.Id)
                                                 .Include(o => o.Esfera)
                                                 .Include(o => o.Poder)
                                                 .Include(o => o.IdentificadorExterno)
                                                 .OrderBy(o => o.RazaoSocial)
                                                 .ToList();

                List<OrganizacaoModeloNegocio> omns = Mapper.Map<List<Organizacao>, List<OrganizacaoModeloNegocio>>(organizacoes);

                MontarOrganograma(omn, omns);
            }

            List<int> idsOrganizacoes = IdsOrganizacoesOrganograma(omn);

            var unidades = repositorioUnidades.Where(u => idsOrganizacoes.Contains(u.IdOrganizacao))
                                              .Include(o => o.Organizacao)
                                              .Include(o => o.UnidadePai)
                                              .Include(o => o.IdentificadorExterno)
                                              .OrderBy(u => u.Nome)
                                              .ToList();

            List<UnidadeModeloNegocio> umns = Mapper.Map<List<Unidade>, List<UnidadeModeloNegocio>>(unidades);

            MontarOrganograma(omn, umns);

            return omn;
        }

        public List<OrganizacaoModeloNegocio> PesquisarOrganograma()
        {
            List<Organizacao> organizacoesPatriarcas = repositorioOrganizacoes.Where(o => !o.IdOrganizacaoPai.HasValue)
                                                                              .Include(o => o.Esfera)
                                                                              .Include(o => o.Poder)
                                                                              .Include(o => o.IdentificadorExterno)
                                                                              .OrderBy(o => o.RazaoSocial)
                                                                              .ToList();

            List<OrganizacaoModeloNegocio> organizacoesPatriarcasNegocio = null;
            if (organizacoesPatriarcas != null)
            {
                organizacoesPatriarcasNegocio = Mapper.Map<List<Organizacao>, List<OrganizacaoModeloNegocio>>(organizacoesPatriarcas);

                var idsOrganizacoesPatriarcas = organizacoesPatriarcasNegocio.Select(op => op.Id)
                                                                             .ToList();

                var organizacoesFilhas = repositorioOrganizacoes.Where(o => !idsOrganizacoesPatriarcas.Contains(o.Id))
                                                                .Include(o => o.Esfera)
                                                                .Include(o => o.Poder)
                                                                .Include(o => o.IdentificadorExterno)
                                                                .OrderBy(o => o.RazaoSocial)
                                                                .ToList();

                List<OrganizacaoModeloNegocio> organizacoesFilhasNegocio = Mapper.Map<List<Organizacao>, List<OrganizacaoModeloNegocio>>(organizacoesFilhas);

                MontarOrganograma(organizacoesPatriarcasNegocio, organizacoesFilhasNegocio);

                List<int> idsOrganizacoes = IdsOrganizacoesOrganograma(organizacoesPatriarcasNegocio);

                var unidades = repositorioUnidades.Where(u => idsOrganizacoes.Contains(u.IdOrganizacao))
                                                  .Include(o => o.Organizacao)
                                                  .Include(o => o.UnidadePai)
                                                  .Include(o => o.IdentificadorExterno)
                                                  .OrderBy(u => u.Nome)
                                                  .ToList();

                List<UnidadeModeloNegocio> umns = Mapper.Map<List<Unidade>, List<UnidadeModeloNegocio>>(unidades);

                MontarOrganograma(organizacoesPatriarcasNegocio, umns);
            }

            return organizacoesPatriarcasNegocio;
        }

        public List<OrganizacaoModeloNegocio> PesquisarPorUsuario(bool filhas)
        {
            List<Organizacao> organizacoes = repositorioOrganizacoes.Where(o => _currentUser.UserGuidsOrganizacao.Contains(o.IdentificadorExterno.Guid))
                                                             .Include(o => o.OrganizacoesFilhas)
                                                             .Include(o => o.Esfera)
                                                             .Include(o => o.Poder)
                                                             .Include(o => o.IdentificadorExterno)
                                                             .ToList();

            List<Organizacao> organizacoesFilhas = null;
            if (filhas && organizacoes != null && organizacoes.Count > 0)
            {
                organizacoesFilhas = new List<Organizacao>();
                foreach (var organizacao in organizacoes)
                {
                    organizacoesFilhas.AddRange(ObterOrganizacoesFilhas(organizacao));
                }
            }

            if (organizacoesFilhas != null)
                organizacoes = organizacoesFilhas.Distinct()
                                                 .OrderBy(o => o.RazaoSocial)
                                                 .ToList();
            else
                organizacoes = organizacoes.Distinct()
                                           .OrderBy(o => o.RazaoSocial)
                                           .ToList();

            return Mapper.Map<List<Organizacao>, List<OrganizacaoModeloNegocio>>(organizacoes);
        }
        #endregion

        #region Integração com o SIARHES
        public async Task IntegrarSiarhes()
        {
            IntegracaoSiarhesNegocio isn = new IntegracaoSiarhesNegocio();

            await isn.Integrar(repositorios, _clientAccessToken.AccessToken);
        }
        #endregion

        #region Funções Auxiliares

        private Organizacao BuscaObjetoDominio(OrganizacaoModeloNegocio organizacaoNegocio)
        {
            Guid g = new Guid(organizacaoNegocio.Guid);

            Organizacao organizacao = repositorioOrganizacoes.Where(o => o.IdentificadorExterno.Guid.Equals(g))
                                                             .Include(un => un.IdentificadorExterno)
                                                             .Include(e => e.Esfera)
                                                             .Include(p => p.Poder)
                                                             .Include(to => to.TipoOrganizacao)
                                                             .Single();

            PreencheOrganizacaoPai(organizacao);

            Mapper.Map(organizacao, organizacaoNegocio);
            return organizacao;
        }

        private Organizacao PreparaInsercao(OrganizacaoModeloNegocio organizacaoNegocio)
        {
            organizacaoNegocio.Guid = Guid.NewGuid().ToString("D");

            if (organizacaoNegocio.OrganizacaoPai != null)
            {
                var organizacaoPai = Pesquisar(organizacaoNegocio.OrganizacaoPai.Guid);
                organizacaoNegocio.OrganizacaoPai.Id = organizacaoPai.Id;
                organizacaoNegocio.Esfera = new EsferaOrganizacaoModeloNegocio { Id = organizacaoPai.Esfera.Id };
                organizacaoNegocio.Poder = new PoderModeloNegocio { Id = organizacaoPai.Poder.Id };
            }

            if (organizacaoNegocio.Endereco != null)
                organizacaoNegocio.Endereco.Municipio.Id = BuscarIdMunicipio(organizacaoNegocio.Endereco.Municipio.Guid);

            Organizacao organizacao = new Organizacao();

            organizacao = Mapper.Map<OrganizacaoModeloNegocio, Organizacao>(organizacaoNegocio);

            return organizacao;
        }

        private void PreencheOrganizacaoPai(Organizacao organizacao)
        {
            if (organizacao.IdOrganizacaoPai.HasValue)
            {
                organizacao.OrganizacaoPai = repositorioOrganizacoes.Where(op => op.Id == organizacao.IdOrganizacaoPai.Value)
                                                                    .Include(op => op.IdentificadorExterno)
                                                                    .Single();
            }
        }

        private int BuscarIdOrganizacaoPai(string guidOrganizacaoPai)
        {
            Guid guid = new Guid(guidOrganizacaoPai);
            return repositorioOrganizacoes.Where(o => o.IdentificadorExterno.Guid.Equals(guid)).Single().Id;
        }

        private void ExcluiContatos(Organizacao organizacao)
        {
            if (organizacao.ContatosOrganizacao != null)
            {
                foreach (var contatoOrganizacao in organizacao.ContatosOrganizacao)
                {
                    repositorioContatosOrganizacoes.Remove(contatoOrganizacao);
                    repositorioContatos.Remove(contatoOrganizacao.Contato);
                }

            }
        }

        private int BuscarIdMunicipio(string guidMunicipio)
        {
            Guid guid = new Guid(guidMunicipio);
            return repositorioMunicipios.Where(o => o.IdentificadorExterno.Guid.Equals(guid)).Single().Id;
        }

        private void ExcluiEndereco(Organizacao organizacao)
        {
            if (organizacao.Endereco != null)
            {
                repositorioEnderecos.Remove(organizacao.Endereco);
            }
        }

        private void ExcluiEmails(Organizacao organizacao)
        {
            if (organizacao.EmailsOrganizacao != null)
            {
                foreach (var emailOrganizacao in organizacao.EmailsOrganizacao)
                {
                    repositorioEmailsOrganizacoes.Remove(emailOrganizacao);
                    repositorioEmails.Remove(emailOrganizacao.Email);
                }

            }

        }

        private void ExcluiSites(Organizacao organizacao)
        {
            if (organizacao.SitesOrganizacao != null)
            {
                foreach (var siteOrganizacao in organizacao.SitesOrganizacao)
                {
                    repositorioSitesOrganizacoes.Remove(siteOrganizacao);
                    repositorioSites.Remove(siteOrganizacao.Site);
                }

            }
        }

        private void ExcluirIdentificadorExterno(Organizacao organizacao)
        {
            repositorioIdentificadoresExternos.Remove(organizacao.IdentificadorExterno);
        }

        private int ObterOrganizacaoPatriarca(Organizacao organizacao)
        {
            int idOrganizacaoPatriarca = organizacao.Id;

            if (organizacao.IdOrganizacaoPai.HasValue)
            {
                Organizacao organizacaoPai = repositorioOrganizacoes.Where(o => o.Id == organizacao.IdOrganizacaoPai.Value)
                                                                    .SingleOrDefault();

                idOrganizacaoPatriarca = ObterOrganizacaoPatriarca(organizacaoPai);
            }

            return idOrganizacaoPatriarca;
        }

        private List<Organizacao> ObterOrganizacoesFilhas(Organizacao organizacao)
        {
            if (organizacao == null) throw new ArgumentNullException("organizacao");

            List<Organizacao> organizacoesFilhas = new List<Organizacao>();

            if (organizacao.OrganizacoesFilhas != null && organizacao.OrganizacoesFilhas.Count > 0)
            {
                foreach (Organizacao organizacaoFilha in organizacao.OrganizacoesFilhas)
                {
                    Organizacao org = repositorioOrganizacoes.Where(o => o.Id == organizacaoFilha.Id)
                                                             .Include(o => o.OrganizacoesFilhas)
                                                             .Include(o => o.Esfera)
                                                             .Include(o => o.Poder)
                                                             .Include(o => o.IdentificadorExterno)
                                                             .Single();

                    organizacoesFilhas.AddRange(ObterOrganizacoesFilhas(org));
                }
            }

            organizacoesFilhas.Add(organizacao);

            return organizacoesFilhas.OrderBy(o => o.RazaoSocial).ToList();
        }

        private void MontarOrganograma(OrganizacaoModeloNegocio organizacao, List<OrganizacaoModeloNegocio> organizacoes)
        {
            int quantidadeOrganizacoes = organizacoes.Count;

            organizacao.OrganizacoesFilhas = organizacoes.Where(o => o.OrganizacaoPai != null
                                                                  && o.OrganizacaoPai.Id == organizacao.Id)
                                                         .ToList();

            if (organizacao.OrganizacoesFilhas.Count > 0)
                organizacoes.RemoveAll(o => o.OrganizacaoPai != null && o.OrganizacaoPai.Id == organizacao.Id);
            else
                organizacao.OrganizacoesFilhas = null;

            if (organizacoes.Count < quantidadeOrganizacoes)
            {
                foreach (var org in organizacao.OrganizacoesFilhas)
                {
                    MontarOrganograma(org, organizacoes);
                }
            }
        }

        private void MontarOrganograma(List<OrganizacaoModeloNegocio> organizacoesPatriarcas, List<OrganizacaoModeloNegocio> organizacoesFilhas)
        {
            if (organizacoesPatriarcas != null && organizacoesPatriarcas.Count > 0)
            {
                foreach (var organizacaoPatriarca in organizacoesPatriarcas)
                {
                    MontarOrganograma(organizacaoPatriarca, organizacoesFilhas);
                }
            }
        }

        private void MontarOrganograma(OrganizacaoModeloNegocio organizacao, List<UnidadeModeloNegocio> unidades)
        {
            var unidadesOrganizacao = unidades.Where(u => u.Organizacao != null
                                                       && u.Organizacao.Id == organizacao.Id)
                                              .ToList();

            if (unidadesOrganizacao != null && unidadesOrganizacao.Count > 0)
            {
                unidades.RemoveAll(u => u.Organizacao != null && u.Organizacao.Id == organizacao.Id);

                var unidadesSemPai = unidadesOrganizacao.Where(u => u.UnidadePai == null)
                                                        .ToList();

                if (unidadesSemPai != null && unidadesSemPai.Count > 0)
                {
                    unidadesOrganizacao.RemoveAll(u => u.UnidadePai == null);

                    organizacao.Unidades = unidadesSemPai;

                    foreach (var u in organizacao.Unidades)
                    {
                        MontarOrganograma(u, unidadesOrganizacao);
                    }
                }
            }

            if (organizacao.OrganizacoesFilhas != null && organizacao.OrganizacoesFilhas.Count > 0)
            {
                foreach (var org in organizacao.OrganizacoesFilhas)
                {
                    MontarOrganograma(org, unidades);
                }
            }
        }

        private void MontarOrganograma(List<OrganizacaoModeloNegocio> organizacoes, List<UnidadeModeloNegocio> unidades)
        {
            if (organizacoes != null && organizacoes.Count > 0)
            {
                foreach (var org in organizacoes)
                {
                    MontarOrganograma(org, unidades);
                }
            }
        }

        private void MontarOrganograma(UnidadeModeloNegocio unidade, List<UnidadeModeloNegocio> unidades)
        {
            int quantidadeUnidades = unidades.Count;

            unidade.UnidadesFilhas = unidades.Where(u => u.UnidadePai != null
                                                      && u.UnidadePai.Id == unidade.Id)
                                             .ToList();

            if (unidade.UnidadesFilhas.Count > 0)
                unidades.RemoveAll(u => u.UnidadePai != null && u.UnidadePai.Id == unidade.Id);
            else
                unidade.UnidadesFilhas = null;

            if (unidades.Count < quantidadeUnidades)
            {
                foreach (var uni in unidade.UnidadesFilhas)
                {
                    MontarOrganograma(uni, unidades);
                }
            }
        }

        private List<int> IdsOrganizacoesOrganograma(OrganizacaoModeloNegocio organizacao)
        {
            List<int> idsOrganizacoes = null;

            if (organizacao != null)
            {
                idsOrganizacoes = new List<int>();

                idsOrganizacoes.Add(organizacao.Id);

                //var idsOrganizacoesFilhas = organizacao.OrganizacoesFilhas.Select(of => of.Id)
                //                                                          .ToList();

                if (organizacao.OrganizacoesFilhas != null && organizacao.OrganizacoesFilhas.Count > 0)
                {
                    foreach (var org in organizacao.OrganizacoesFilhas)
                    {
                        idsOrganizacoes.AddRange(IdsOrganizacoesOrganograma(org));
                    }
                }
            }

            return idsOrganizacoes;
        }

        private List<int> IdsOrganizacoesOrganograma(List<OrganizacaoModeloNegocio> organizacoes)
        {
            List<int> idsOrganizacoes = null;

            if (organizacoes != null && organizacoes.Count > 0)
            {
                idsOrganizacoes = new List<int>();

                foreach (var org in organizacoes)
                {
                    idsOrganizacoes.AddRange(IdsOrganizacoesOrganograma(org));
                }
            }

            return idsOrganizacoes;
        }

        private void InserirHistorico(Organizacao organizacao, string obsFimVigencia, DateTime? fimVigencia)
        {
            Organizacao organizacaoSimples = JsonData.DeserializeObject<Organizacao>(JsonData.SerializeObject(organizacao));

            organizacaoSimples.Esfera = null;
            organizacaoSimples.OrganizacaoPai = null;
            organizacaoSimples.OrganizacoesFilhas = null;
            organizacaoSimples.Poder = null;
            organizacaoSimples.TipoOrganizacao = null;
            organizacaoSimples.Unidades = null;

            string json = JsonData.SerializeObject(organizacaoSimples);

            Historico historico = new Historico
            {
                Json = json,
                InicioVigencia = organizacao.InicioVigencia,
                FimVigencia = fimVigencia.HasValue ? fimVigencia.Value : DateTime.Now,
                ObservacaoFimVigencia = obsFimVigencia,
                IdIdentificadorExterno = organizacao.IdentificadorExterno.Id
            };

            repositorioHistoricos.Add(historico);
        }
        #endregion
    }
}
