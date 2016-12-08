using Organograma.Apresentacao.Modelos;
using System.Collections.Generic;

namespace Apresentacao.Base
{
    public interface IOrganizacaoWorkService
    {
        List<OrganizacaoModeloGet> Listar(string esfera, string poder, string uf, int codIbgeMunicipio);
        OrganizacaoModeloGet Pesquisar(string id);
        OrganizacaoModeloPut Inserir(OrganizacaoModeloPost organizacaoPost);
        void Alterar(int id, OrganizacaoModeloPatch organizacaoPatch);
        void Excluir(int id);
        SiteModeloPatch InserirSite(int idOrganizacao, SiteModelo sitePost);
        OrganizacaoModeloGet PesquisarPatriarca(string guid);
        List<OrganizacaoModeloGet> PesquisarFilhas(string guid);
    }

}
