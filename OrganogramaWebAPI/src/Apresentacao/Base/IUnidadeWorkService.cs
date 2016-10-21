using Organograma.Apresentacao.Modelos;
using System.Collections.Generic;

namespace Organograma.Apresentacao.Base
{
    public interface IUnidadeWorkService
    {
        List<UnidadeModelo> Listar();

        UnidadeModelo Pesquisar(int id);

        UnidadeModelo Inserir(UnidadeModeloPost esferaOrganizacao);

        //TODO:Refazer a estrutura que será recebida na alteração
        void Alterar(int id, UnidadeModelo esferaOrganizacao);

        void Excluir(int id);
    }
}
