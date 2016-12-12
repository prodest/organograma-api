using Organograma.Negocio.Modelos;
using System.Collections.Generic;

namespace Organograma.Negocio.Base
{
    public interface IOrganizacaoNegocio
    {
        List<OrganizacaoModeloNegocio> Listar(string esfera, string poder, string uf, int codIbgeMunicipio);
        OrganizacaoModeloNegocio Pesquisar(string guid);
        OrganizacaoModeloNegocio Inserir(OrganizacaoModeloNegocio OrganizacaoNegocio);

        void Alterar(int id, OrganizacaoModeloNegocio poderNegocio);

        void Excluir (int id);
        SiteModeloNegocio InserirSite(SiteModeloNegocio siteModeloNegocio);
        OrganizacaoModeloNegocio PesquisarPatriarca(string guid);
        List<OrganizacaoModeloNegocio> PesquisarFilhas(string guid);
        OrganizacaoModeloNegocio PesquisarPorSigla(string sigla);
    }
}
