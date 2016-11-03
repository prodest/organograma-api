using Organograma.Apresentacao.Modelos;
using System.Collections.Generic;

namespace Apresentacao.Base
{
    public interface IOrganizacaoWorkService
    {
        List<OrganizacaoModeloGet> Listar();
        OrganizacaoModeloGet Pesquisar(int id);
        OrganizacaoModeloPut Inserir(OrganizacaoModeloPost organizacaoPost);
        void Alterar(int id, OrganizacaoModeloPatch organizacaoPatch);
        void Excluir(int id);
        SiteModeloPatch InserirSite(int idOrganizacao, SiteModelo sitePost);
    }

}
