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

        public void Alterar(int id, OrganizacaoModeloNegocio poderNegocio)
        {
            throw new NotImplementedException();
        }

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


            ExcluiRelacionamentos(organizacao);
            repositorioOrganizacoes.Remove(organizacao);
            unitOfWork.Save();
        }

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

        public List<OrganizacaoModeloNegocio> Listar()
        {
            throw new NotImplementedException();
        }

        public OrganizacaoModeloNegocio Pesquisar(int id)
        {
            OrganizacaoModeloNegocio organizacaoNegocio = new OrganizacaoModeloNegocio();

            Organizacao organizacao = repositorioOrganizacoes.Where(o => o.Id == id)
                .Include(c => c.ContatosOrganizacao).ThenInclude(co => co.Contato)
                .Include(eo => eo.EmailsOrganizacao).ThenInclude(e => e.Email)
                .Include(e => e.Endereco)
                .Include(e => e.Esfera)
                .Include(op => op.OrganizacaoPai)
                .Include(p => p.Poder)
                .Include(so => so.SitesOrganizacao).ThenInclude(s => s.Site)
                .Include(to => to.TipoOrganizacao)
                .SingleOrDefault();

            validacao.NaoEncontrado(organizacao);
            
            return Mapper.Map(organizacao,organizacaoNegocio);

        }

        private Organizacao PreparaInsercao(OrganizacaoModeloNegocio organizacaoNegocio)
        {
            Organizacao organizacao = new Organizacao();
            organizacao = Mapper.Map<OrganizacaoModeloNegocio, Organizacao>(organizacaoNegocio);
            return organizacao;
        }

        private void ExcluiRelacionamentos(Organizacao organizacao)
        {
            //Contatos
            if (organizacao.ContatosOrganizacao != null)
            {
                foreach (var contatoOrganizacao in organizacao.ContatosOrganizacao)
                {
                    repositorioContatosOrganizacoes.Remove(contatoOrganizacao);
                    repositorioContatos.Remove(contatoOrganizacao.Contato);
                }

            }
            //Endereco
            if (organizacao.Endereco != null)
            {
                repositorioEnderecos.Remove(organizacao.Endereco);

            }
            //Emails
            if (organizacao.EmailsOrganizacao != null)
            {
                foreach (var emailOrganizacao in organizacao.EmailsOrganizacao)
                {
                    repositorioEmailsOrganizacoes.Remove(emailOrganizacao);
                    repositorioEmails.Remove(emailOrganizacao.Email);
                }

            }
            //Sites
            if (organizacao.SitesOrganizacao != null)
            {
                foreach (var siteOrganizacao in organizacao.SitesOrganizacao)
                {
                    repositorioSitesOrganizacoes.Remove(siteOrganizacao);
                    repositorioSites.Remove(siteOrganizacao.Site);
                }

            }
        }
    }
}
