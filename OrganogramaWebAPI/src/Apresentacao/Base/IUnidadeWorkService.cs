using Organograma.Apresentacao.Modelos;
using System.Collections.Generic;

namespace Organograma.Apresentacao.Base
{
    public interface IUnidadeWorkService
    {
        List<UnidadeModeloRetornoPost> Listar();

        UnidadeModeloGet Pesquisar(string guid);

        List<UnidadeSimplesModeloGet> PesquisarPorOrganizacao(string guidOrganziacao);

        UnidadeModeloRetornoPost Inserir(UnidadeModeloPost unidade);

        void Alterar(string guid, UnidadeModeloPatch unidade);

        void Excluir(string guid);

        void ExcluirEmail(int id, List<EmailModelo> emails);
    }
}
