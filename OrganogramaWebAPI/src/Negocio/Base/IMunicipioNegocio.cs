using Organograma.Negocio.Modelos;
using System.Collections.Generic;

namespace Organograma.Negocio.Base
{
    public interface IMunicipioNegocio
    {
        List<MunicipioModeloNegocio> Listar();
        MunicipioModeloNegocio Pesquisar(int id);
        MunicipioModeloNegocio Inserir(MunicipioModeloNegocio municipioNegocio);
    }
}
