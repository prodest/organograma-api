using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Apresentacao.Modelos
{
    public class TipoOrganizacaoModelo : TipoOrganizacaoModeloPut
    {
        public string ObservacaoFimVigencia { get; set; }
    }

    public class TipoOrganizacaoModeloPut : TipoOrganizacaoModeloPost
    {
        public int Id { get; set; }
    }

    public class TipoOrganizacaoModeloPost
    {
        public string Descricao { get; set; }

    }
}
