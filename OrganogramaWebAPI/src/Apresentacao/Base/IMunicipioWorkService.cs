using Organograma.Apresentacao.Modelos;
using System.Collections.Generic;

namespace Apresentacao.Base
{
    public interface IMunicipioWorkService
    {
        List<MunicipioModeloGet> Listar(string uf);
        MunicipioModeloGet Pesquisar(string guid);
        MunicipioModeloGet Inserir(MunicipioModeloPost municipioPost);
        void Alterar(int id, MunicipioModeloPut municipioPut);
        void Excluir(int id);
    }

}
