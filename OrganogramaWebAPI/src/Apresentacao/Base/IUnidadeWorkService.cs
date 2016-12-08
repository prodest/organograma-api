using Organograma.Apresentacao.Modelos;
using System.Collections.Generic;

namespace Organograma.Apresentacao.Base
{
    public interface IUnidadeWorkService
    {
        List<UnidadeModeloRetornoPost> Listar();

        UnidadeModeloGet Pesquisar(int id);

        List<UnidadeModeloGet> PesquisarPorOrganizacao(string guidOrganziacao);

        UnidadeModeloRetornoPost Inserir(UnidadeModeloPost unidade);

        void Alterar(int id, UnidadeModeloPatch unidade);

        void Excluir(int id);

        void ExcluirEmail(int id, List<EmailModelo> emails);
    }
}
