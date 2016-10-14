using Organograma.Apresentacao.Modelos;
using System.Collections.Generic;

namespace Apresentacao.Base
{
    public interface IOrganizacaoWorkService
    {
        List<OrganizacaoModeloGet> Listar();
        OrganizacaoModeloGet Pesquisar(int id);
        OrganizacaoModeloGet Inserir(OrganizacaoModeloPost organizacaoPost);
        void Alterar(int id, OrganizacaoModeloPut organizacaoPut);
        void Excluir(int id);
    
    }

}
