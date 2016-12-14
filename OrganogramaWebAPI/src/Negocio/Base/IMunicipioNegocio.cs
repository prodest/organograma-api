using Organograma.Negocio.Modelos;
using System.Collections.Generic;

namespace Organograma.Negocio.Base
{
    public interface IMunicipioNegocio
    {
        List<MunicipioModeloNegocio> Listar(string uf);
        MunicipioModeloNegocio Pesquisar(string guid);
        MunicipioModeloNegocio Inserir(MunicipioModeloNegocio municipioNegocio);

        void Alterar(int id, MunicipioModeloNegocio municipioNegocio);

        void Excluir (int id);
    }
}
