using Organograma.Apresentacao.Modelos;
using System.Collections.Generic;

namespace Apresentacao.Municipio.Base
{
    public interface IMunicipioWorkService
    {
        List<MunicipioModeloApresentacao> ConsultarMunicipios();
    }
}
