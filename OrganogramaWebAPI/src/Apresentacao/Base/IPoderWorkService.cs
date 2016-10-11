using Organograma.Apresentacao.Modelos;
using System.Collections.Generic;

namespace Apresentacao.Base
{
    public interface IPoderWorkService
    {
        List<PoderModeloGet> Listar();
        PoderModeloGet Pesquisar(int id);
        PoderModeloGet Inserir(PoderModeloPost poderPost);
        void Alterar(int id, PoderModeloPut poderPut);
        void Excluir(int id);
    
    }

}
