using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Organograma.Negocio.Modelos;
using Organograma.Apresentacao.Modelos;

namespace Organograma.Apresentacao.Base
{
    public interface ITipoUnidadeWorkService
    {
        List<TipoUnidadeModelo> Listar();

        TipoUnidadeModelo Pesquisar(int id);

        TipoUnidadeModelo Inserir(TipoUnidadeModeloPost tipoUnidade);

        void Alterar(int id, TipoUnidadeModeloPut tipoUnidade);

        void Excluir(int id);
    }
}
