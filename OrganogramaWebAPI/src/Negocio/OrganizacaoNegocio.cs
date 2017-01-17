using AutoMapper;
using Organograma.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using Organograma.Negocio.Modelos;
using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Negocio.Validacao;
using Microsoft.EntityFrameworkCore;

namespace Organograma.Negocio
{
    public class OrganizacaoNegocio : IOrganizacaoNegocio
    {

        IUnitOfWork unitOfWork;
        IRepositorioGenerico<Organizacao> repositorioOrganizacoes;
        IRepositorioGenerico<Contato> repositorioContatos;
        IRepositorioGenerico<ContatoOrganizacao> repositorioContatosOrganizacoes;
        IRepositorioGenerico<Email> repositorioEmails;
        IRepositorioGenerico<EmailOrganizacao> repositorioEmailsOrganizacoes;
        IRepositorioGenerico<Endereco> repositorioEnderecos;
        IRepositorioGenerico<Municipio> repositorioMunicipios;
        IRepositorioGenerico<Site> repositorioSites;
        IRepositorioGenerico<SiteOrganizacao> repositorioSitesOrganizacoes;

        OrganizacaoValidacao validacao;
        CnpjValidacao cnpjValidacao;
        ContatoValidacao contatoValidacao;
        EmailValidacao emailValidacao;
        EnderecoValidacao enderecoValidacao;
        EsferaOrganizacaoValidacao esferaValidacao;
        PoderValidacao poderValidacao;
        SiteValidacao siteValidacao;
        TipoOrganizacaoValidacao tipoOrganizacaoValidacao;

        public OrganizacaoNegocio(IOrganogramaRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioOrganizacoes = repositorios.Organizacoes;
            repositorioContatos = repositorios.Contatos;
            repositorioContatosOrganizacoes = repositorios.ContatosOrganizacoes;
            repositorioEmails = repositorios.Emails;
            repositorioEmailsOrganizacoes = repositorios.EmailsOrganizacoes;
            repositorioEnderecos = repositorios.Enderecos;
            repositorioMunicipios = repositorios.Municipios;
            repositorioSites = repositorios.Sites;
            repositorioSitesOrganizacoes = repositorios.SitesOrganizacoes;

            validacao = new OrganizacaoValidacao(repositorioOrganizacoes);
            cnpjValidacao = new CnpjValidacao(repositorioOrganizacoes);
            contatoValidacao = new ContatoValidacao(repositorios.Contatos, repositorios.TiposContatos);
            emailValidacao = new EmailValidacao();
            enderecoValidacao = new EnderecoValidacao(repositorios.Enderecos, repositorios.Municipios);
            esferaValidacao = new EsferaOrganizacaoValidacao(repositorios.EsferasOrganizacoes);
            poderValidacao = new PoderValidacao(repositorios.Poderes);
            siteValidacao = new SiteValidacao();
            tipoOrganizacaoValidacao = new TipoOrganizacaoValidacao(repositorios.TiposOrganizacoes);

        }

        #region Alterar
        public void Alterar(int id, OrganizacaoModeloNegocio organizacaoNegocio)
        {
            validacao.IdPreenchido(id);
            validacao.IdPreenchido(organizacaoNegocio);
            validacao.IdAlteracaoValido(id, organizacaoNegocio);

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

            Mapper.Map(organizacaoNegocio, organizacao);
            unitOfWork.Save();

        }

        #endregion

        #region Excluir
        public void Excluir(int id)
        {
            validacao.IdPreenchido(id);
            validacao.Existe(id);
            validacao.PossuiFilho(id);
            validacao.PossuiUnidade(id);

            Organizacao organizacao = repositorioOrganizacoes.Where(o => o.Id == id)
                .Include(i => i.Endereco)
                .Include(i => i.ContatosOrganizacao).ThenInclude(c => c.Contato)
                .Include(i => i.SitesOrganizacao).ThenInclude(s => s.Site)
                .Include(i => i.EmailsOrganizacao).ThenInclude(s => s.Email).Single();

            ExcluiContatos(organizacao);
            ExcluiEndereco(organizacao);
            ExcluiEmails(organizacao);
            ExcluiSites(organizacao);
            repositorioOrganizacoes.Remove(organizacao);
            unitOfWork.Save();
        }
        #endregion

        #region Inserir
        public OrganizacaoModeloNegocio Inserir(OrganizacaoModeloNegocio organizacaoNegocio)
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

            Organizacao organizacao = PreparaInsercao(organizacaoNegocio);
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

        #endregion

        #region Funções Auxiliares

        private Organizacao BuscaObjetoDominio(OrganizacaoModeloNegocio organizacaoNegocio)
        {
            Organizacao organizacao = repositorioOrganizacoes.Where(o => o.Id == organizacaoNegocio.Id)
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
            organizacaoNegocio.OrganizacaoPai.Id = BuscarIdOrganizacaoPai(organizacaoNegocio.OrganizacaoPai.Guid);
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


        #endregion
    }
}
