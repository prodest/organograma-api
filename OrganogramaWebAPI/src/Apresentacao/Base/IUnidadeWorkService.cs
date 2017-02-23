using Organograma.Apresentacao.Modelos;
using System.Collections.Generic;

namespace Organograma.Apresentacao.Base
{
    public interface IUnidadeWorkService
    {
        List<UnidadeModeloRetornoPost> Listar();

        UnidadeModeloGet Pesquisar(string guid);

        List<UnidadeModeloGet> PesquisarPorOrganizacao(string guidOrganziacao);

        UnidadeModeloRetornoPost Inserir(UnidadeModeloPost unidade);

        void Alterar(int id, UnidadeModeloPatch unidade);

        void Excluir(string guid);

        void ExcluirEmail(int id, List<EmailModelo> emails);
    }
}
