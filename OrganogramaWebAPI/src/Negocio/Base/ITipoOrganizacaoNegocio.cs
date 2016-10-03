using Organograma.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Negocio.Base
{
    public interface ITipoOrganizacaoNegocio
    {
        List<TipoOrganizacaoModeloNegocio> Listar();

        TipoOrganizacaoModeloNegocio Pesquisar(int id);

        TipoOrganizacaoModeloNegocio Incluir(TipoOrganizacaoModeloNegocio tipoOrganizacao);

        void Alterar(int id, TipoOrganizacaoModeloNegocio tipoOrganizacao);

        void Excluir(int id);
    }
}
