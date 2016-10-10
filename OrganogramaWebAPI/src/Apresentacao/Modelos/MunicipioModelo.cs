using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Apresentacao.Modelos
{
    public class MunicipioModeloPost
    {
        public int CodigoIbge { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }
    }

    public class MunicipioModeloPut : MunicipioModeloPost
    {
        public int Id { get; set; }
    }

    public class MunicipioModeloDelete : MunicipioModeloPut
    {
    }

    public class MunicipioModeloGet : MunicipioModeloPut
    {
        public string InicioVigencia { get; set; }
        public string FimVigencia { get; set; }
        public string ObservacaoFimVigencia { get; set; }
    }


}
