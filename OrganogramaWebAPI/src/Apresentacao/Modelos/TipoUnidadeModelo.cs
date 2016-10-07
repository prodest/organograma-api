using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Apresentacao.Modelos
{
    public class TipoUnidadeModelo : TipoUnidadeModeloPut
    {
        public string InicioVigencia { get; set; }
        public string FimVigencia { get; set; }
        public string ObservacaoFimVigencia { get; set; }
    }

    public class TipoUnidadeModeloPut : TipoUnidadeModeloPost
    {
        public int Id { get; set; }
    }

    public class TipoUnidadeModeloPost
    {
        public string Descricao { get; set; }

    }
}
