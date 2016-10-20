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

        public OrganizacaoNegocio(IOrganogramaRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioOrganizacoes = repositorios.Organizacoes;
            validacao = new OrganizacaoValidacao(repositorioOrganizacoes);
            cnpjValidacao = new CnpjValidacao(repositorioOrganizacoes);
            contatoValidacao = new ContatoValidacao(repositorios.Contatos, repositorios.TiposContato);
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
            validacao.Preenchido(organizacaoNegocio);
            validacao.Valido(organizacaoNegocio);
            validacao.PaiValido(organizacaoNegocio.OrganizacaoPai);
            contatoValidacao.Preenchidos(organizacaoNegocio.Contatos);
            contatoValidacao.Validos(organizacaoNegocio.Contatos);

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
