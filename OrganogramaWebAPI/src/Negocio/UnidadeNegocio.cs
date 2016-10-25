using Organograma.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Organograma.Negocio.Modelos;
using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Negocio.Validacao;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
        private IRepositorioGenerico<Site> repositorioSites;
        private IRepositorioGenerico<SiteUnidade> repositorioSitesUnidades;
        private UnidadeValidacao unidadeValidacao;
        private TipoUnidadeValidacao tipoUnidadeValidacao;
        private OrganizacaoValidacao organizacaoValidacao;
        private EnderecoValidacao enderecoValidacao;
        private ContatoValidacao contatoValidacao;
        private EmailValidacao emailValidacao;
        private SiteValidacao siteValidacao;


        public UnidadeNegocio(IOrganogramaRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioUnidades = repositorios.Unidades;
            repositorioEnderecos = repositorios.Enderecos;
            repositorioContatos = repositorios.Contatos;
            repositorioContatosUnidades = repositorios.ContatosUnidades;
            repositorioEmails = repositorios.Emails;
            repositorioEmailsUnidades = repositorios.EmailsUnidades;
            repositorioSites = repositorios.Sites;
            repositorioSitesUnidades = repositorios.SitesUnidades;


            unidadeValidacao = new UnidadeValidacao(repositorioUnidades, repositorios.TiposUnidades, repositorios.Organizacoes);
            tipoUnidadeValidacao = new TipoUnidadeValidacao(repositorios.TiposUnidades);
            organizacaoValidacao = new OrganizacaoValidacao(repositorios.Organizacoes);
            enderecoValidacao = new EnderecoValidacao(repositorios.Enderecos, repositorios.Municipios);
            contatoValidacao = new ContatoValidacao(repositorios.Contatos, repositorios.TiposContatos);
            emailValidacao = new EmailValidacao();
            siteValidacao = new SiteValidacao();
        }

        public void Alterar(int id, UnidadeModeloNegocio unidade)
        {
            unidadeValidacao.NaoNula(unidade);

            //unidadeValidacao.IdPreenchido(id);
            //unidadeValidacao.IdPreenchido(unidade.Id);

            unidadeValidacao.IdAlteracaoValido(id, unidade);

            //validacao.DescricaoValida(unidade.Descricao);

            //validacao.DescricaoExistente(unidade.Descricao);

            Unidade eo = repositorioUnidades.Where(e => e.Id == unidade.Id).SingleOrDefault();

            unidadeValidacao.NaoEncontrado(eo);

            //eo.Descricao = unidade.Descricao;

            unitOfWork.Save();
        }

        public void Excluir(int id)
        {
            unidadeValidacao.IdPreenchido(id);

            unidadeValidacao.IdValido(id);

            var unidade = repositorioUnidades.Where(un => un.Id == id)
                                             .Include(u => u.Endereco)
                                             .Include(u => u.ContatosUnidade).ThenInclude(cu => cu.Contato)
                                             .Include(u => u.EmailsUnidade).ThenInclude(eu => eu.Email)
                                             .Include(u => u.SitesUnidade).ThenInclude(su => su.Site)
                                             .SingleOrDefault();

            unidadeValidacao.NaoEncontrado(unidade);

            unidadeValidacao.PossuiFilho(id);

            if (unidade.Endereco != null)
                repositorioEnderecos.Remove(unidade.Endereco);

            foreach (var cu in unidade.ContatosUnidade)
            {
                repositorioContatos.Remove(cu.Contato);
                repositorioContatosUnidades.Remove(cu);
            }

            foreach (var eu in unidade.EmailsUnidade)
            {
                repositorioEmails.Remove(eu.Email);
                repositorioEmailsUnidades.Remove(eu);
            }

            foreach (var su in unidade.SitesUnidade)
            {
                repositorioSites.Remove(su.Site);
                repositorioSitesUnidades.Remove(su);
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
            organizacaoValidacao.IdPreenchido(unidade.Organizacao);

            unidadeValidacao.UnidadePaiPreenchida(unidade.UnidadePai);

            enderecoValidacao.Preenchido(unidade.Endereco);

            contatoValidacao.Preenchido(unidade.Contatos);

            emailValidacao.Preenchido(unidade.Emails);

            siteValidacao.Preenchido(unidade.Sites);

            #endregion

            #region Validação de Negócio

            unidadeValidacao.Valida(unidade);

            tipoUnidadeValidacao.Existe(unidade.TipoUnidade);

            organizacaoValidacao.Existe(unidade.Organizacao);

            unidadeValidacao.UnidadePaiValida(unidade.UnidadePai);

            enderecoValidacao.Valido(unidade.Endereco);

            contatoValidacao.Valido(unidade.Contatos);

            emailValidacao.Valido(unidade.Emails);

            siteValidacao.Valido(unidade.Sites);

            #endregion

            var unid = Mapper.Map<UnidadeModeloNegocio, Unidade>(unidade);

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

        public UnidadeModeloNegocio Pesquisar(int id)
        {
            var unidade = repositorioUnidades.Where(u => u.Id == id)
                                             .Include(u => u.TipoUnidade)
                                             .Include(u => u.Organizacao)
                                             .Include(u => u.UnidadePai)
                                             .Include(u => u.Endereco).ThenInclude(u => u.Municipio)
                                             .Include(u => u.ContatosUnidade).ThenInclude(u => u.Contato).ThenInclude(u => u.TipoContato)
                                             .Include(u => u.EmailsUnidade).ThenInclude(u => u.Email)
                                             .Include(u => u.SitesUnidade).ThenInclude(u => u.Site)
                                             .SingleOrDefault();

            unidadeValidacao.NaoEncontrado(unidade);

            return Mapper.Map<Unidade, UnidadeModeloNegocio>(unidade); ;
        }
    }
}
