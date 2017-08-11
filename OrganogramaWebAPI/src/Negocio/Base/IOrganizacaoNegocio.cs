using Organograma.Negocio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Organograma.Negocio.Base
{
    public interface IOrganizacaoNegocio : IBaseNegocio
    {
        List<OrganizacaoModeloNegocio> Listar(string esfera, string poder, string uf, int codIbgeMunicipio);
        OrganizacaoModeloNegocio Pesquisar(string guid);
        OrganizacaoModeloNegocio InserirFilha(OrganizacaoModeloNegocio OrganizacaoNegocio);
        OrganizacaoModeloNegocio InserirPatriarca(OrganizacaoModeloNegocio OrganizacaoNegocio);

        void Alterar(string guid, OrganizacaoModeloNegocio poderNegocio);

        void Excluir (string guid);
        SiteModeloNegocio InserirSite(SiteModeloNegocio siteModeloNegocio);
        OrganizacaoModeloNegocio PesquisarPatriarca(string guid);
        List<OrganizacaoModeloNegocio> PesquisarFilhas(string guid);
        OrganizacaoModeloNegocio PesquisarPorSigla(string sigla);
        OrganizacaoModeloNegocio PesquisarOrganograma(string guid, bool filhas);
        List<OrganizacaoModeloNegocio> PesquisarOrganograma();
        List<OrganizacaoModeloNegocio> PesquisarPorUsuario(bool filhas);
        Task IntegrarSiarhes();
    }
}
