using Organograma.Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apresentacao.Base
{
    public interface IOrganizacaoWorkService : IBaseWorkService
    {
        List<OrganizacaoModeloGet> Listar(string esfera, string poder, string uf, int codIbgeMunicipio);
        OrganizacaoModeloGet Pesquisar(string id);
        OrganizacaoModeloPut InserirFilha(OrganizacaoFilhaModeloPost organizacaoPost);
        OrganizacaoModeloPut InserirPatriarca(OrganizacaoModeloPost organizacaoPost);
        void Alterar(string guid, OrganizacaoModeloPatch organizacaoPatch);
        void Excluir(string guid);
        SiteModeloPatch InserirSite(int idOrganizacao, SiteModelo sitePost);
        OrganizacaoModeloGet PesquisarPatriarca(string guid);
        List<OrganizacaoModeloGet> PesquisarFilhas(string guid);
        OrganizacaoModeloGetPorSigla PesquisarPorSigla(string sigla, bool patriarca);
        OrganizacaoOrganograma PesquisarOrganograma(string guid, bool filhas);
        List<OrganizacaoOrganograma> PesquisarOrganograma();
        List<OrganizacaoOrganogramaAcessoCidadao> PesquisarOrganogramaAcessoCidadao();
        List<OrganizacaoModeloGet> PesquisarPorUsuario(bool filhas);
        Task IntegarSiarhes();
    }

}
