using AutoMapper;
using Organograma.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using Organograma.Negocio.Modelos;
using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Negocio.Validacao;

namespace Organograma.Negocio
{
    public class OrganizacaoNegocio : IOrganizacaoNegocio
    {

        IUnitOfWork unitOfWork;
        IRepositorioGenerico<Organizacao> repositorioOrganizacoes;
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
            throw new NotImplementedException();
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


            validacao.Valido(organizacaoNegocio);
            validacao.PaiValido(organizacaoNegocio.OrganizacaoPai);
            contatoValidacao.Valido(organizacaoNegocio.Contatos);
            emailValidacao.Valido(organizacaoNegocio.Emails);
            enderecoValidacao.Valido(organizacaoNegocio.Endereco);
            esferaValidacao.Existe(organizacaoNegocio.Esfera);
            poderValidacao.Existe(organizacaoNegocio.Poder);
            siteValidacao.Valido(organizacaoNegocio.Sites);
            tipoOrganizacaoValidacao.Existe(organizacaoNegocio.TipoOrganizacao);


            throw new NotImplementedException();

            Organizacao organizacao = PreparaInsercao(organizacaoNegocio);
            repositorioOrganizacoes.Add(organizacao);
            unitOfWork.Attach(organizacao.TipoOrganizacao);
            unitOfWork.Attach(organizacao.Esfera);
            unitOfWork.Attach(organizacao.Poder);
            unitOfWork.Save();

            //return organizacaoNegocio;
        }

        public List<OrganizacaoModeloNegocio> Listar()
        {
            throw new NotImplementedException();
        }

        public OrganizacaoModeloNegocio Pesquisar(int id)
        {
            throw new NotImplementedException();
        }

        private Organizacao PreparaInsercao(OrganizacaoModeloNegocio organizacaoNegocio)
        {
            Organizacao organizacao = new Organizacao();
            organizacao = Mapper.Map<OrganizacaoModeloNegocio, Organizacao>(organizacaoNegocio);
            return organizacao;
        }
    }
}
