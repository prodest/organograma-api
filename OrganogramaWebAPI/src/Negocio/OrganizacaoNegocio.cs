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
        SiteValidacao siteValidacao;
        

        public OrganizacaoNegocio(IOrganogramaRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioOrganizacoes = repositorios.Organizacoes;
            validacao = new OrganizacaoValidacao(repositorioOrganizacoes);
            cnpjValidacao = new CnpjValidacao(repositorioOrganizacoes);
            contatoValidacao = new ContatoValidacao(repositorios.Contatos, repositorios.TiposContatos);
            emailValidacao = new EmailValidacao();
            enderecoValidacao = new EnderecoValidacao(repositorios.Enderecos, repositorios.Municipios);
            siteValidacao = new SiteValidacao();
            
            
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
            siteValidacao.Preenchido(organizacaoNegocio.Sites);

            validacao.Valido(organizacaoNegocio);
            validacao.PaiValido(organizacaoNegocio.OrganizacaoPai);
            contatoValidacao.Valido(organizacaoNegocio.Contatos);
            emailValidacao.Valido(organizacaoNegocio.Emails);
            enderecoValidacao.Valido(organizacaoNegocio.Endereco);
            siteValidacao.Valido(organizacaoNegocio.Sites);


            
            throw new NotImplementedException();
        }

        public List<OrganizacaoModeloNegocio> Listar()
        {
            throw new NotImplementedException();
        }

        public OrganizacaoModeloNegocio Pesquisar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
