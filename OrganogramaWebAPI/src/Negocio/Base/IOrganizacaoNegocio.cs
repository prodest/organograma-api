using Organograma.Negocio.Modelos;
using System.Collections.Generic;

namespace Organograma.Negocio.Base
{
    public interface IOrganizacaoNegocio
    {
        List<OrganizacaoModeloNegocio> Listar();
        OrganizacaoModeloNegocio Pesquisar(int id);
        OrganizacaoModeloNegocio Inserir(OrganizacaoModeloNegocio OrganizacaoNegocio);

        void Alterar(int id, OrganizacaoModeloNegocio poderNegocio);

        void Excluir (int id);
        SiteModeloNegocio InserirSite(SiteModeloNegocio siteModeloNegocio);
    }
}
