using Organograma.Negocio.Modelos;
using System.Collections.Generic;

namespace Organograma.Negocio.Base
{
    public interface IPoderNegocio
    {
        List<PoderModeloNegocio> Listar();
        PoderModeloNegocio Pesquisar(int id);
        PoderModeloNegocio Inserir(PoderModeloNegocio poderNegocio);

        void Alterar(int id, PoderModeloNegocio poderNegocio);

        void Excluir (int id);
    }
}
