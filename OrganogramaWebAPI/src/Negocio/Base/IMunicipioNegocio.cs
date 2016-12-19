using Organograma.Negocio.Modelos;
using System.Collections.Generic;

namespace Organograma.Negocio.Base
{
    public interface IMunicipioNegocio
    {
        List<MunicipioModeloNegocio> Listar(string uf);
        MunicipioModeloNegocio Pesquisar(string guid);
        MunicipioModeloNegocio Inserir(MunicipioModeloNegocio municipioNegocio);
        void Alterar(string guid, MunicipioModeloNegocio municipioNegocio);
        void Excluir (string guid);
    }
}
