using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Organograma.Negocio.Modelos;
using Organograma.Apresentacao.Modelos;

namespace Organograma.Apresentacao.Base
{
    public interface ITipoOrganizacaoWorkService
    {
        List<TipoOrganizacaoModelo> Listar();

        TipoOrganizacaoModelo Pesquisar(int id);

        TipoOrganizacaoModelo Inserir(TipoOrganizacaoModeloPost tipoOrganizacao);

        void Alterar(int id, TipoOrganizacaoModeloPut tipoOrganizacao);

        void Excluir(int id);
    }
}
