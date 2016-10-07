using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Apresentacao.Modelos
{
    public class EsferaOrganizacaoModelo : EsferaOrganizacaoModeloPost
    {
        public int Id { get; set; }
    }

    public class EsferaOrganizacaoModeloPost
    {
        public string Descricao { get; set; }

    }
}
