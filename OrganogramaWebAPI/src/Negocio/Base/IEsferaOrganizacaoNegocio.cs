using Organograma.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Negocio.Base
{
    public interface IEsferaOrganizacaoNegocio
    {
        List<EsferaOrganizacaoModeloNegocio> Listar();

        EsferaOrganizacaoModeloNegocio Pesquisar(int id);

        EsferaOrganizacaoModeloNegocio Inserir(EsferaOrganizacaoModeloNegocio esferaOrganizacao);

        void Alterar(int id, EsferaOrganizacaoModeloNegocio esferaOrganizacao);

        void Excluir(int id);
    }
}
