using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Organograma.Negocio.Modelos;
using Organograma.Apresentacao.Modelos;

namespace Organograma.Apresentacao.Base
{
    public interface IEsferaOrganizacaoWorkService
    {
        List<EsferaOrganizacaoModelo> Listar();

        EsferaOrganizacaoModelo Pesquisar(int id);

        EsferaOrganizacaoModelo Inserir(EsferaOrganizacaoModeloPost esferaOrganizacao);

        void Alterar(int id, EsferaOrganizacaoModelo esferaOrganizacao);

        void Excluir(int id);
    }
}
