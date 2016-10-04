using Organograma.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Negocio.Base
{
    public interface ITipoUnidadeNegocio
    {
        List<TipoUnidadeModeloNegocio> Listar();

        TipoUnidadeModeloNegocio Pesquisar(int id);

        TipoUnidadeModeloNegocio Incluir(TipoUnidadeModeloNegocio tipoUnidade);

        void Alterar(int id, TipoUnidadeModeloNegocio tipoUnidade);

        void Excluir(int id);
    }
}
