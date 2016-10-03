using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Negocio.Modelos
{
    public class TipoOrganizacaoModeloNegocio
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime? FimVigencia { get; set; }
        public string ObservacaoFimVigencia { get; set; }
    }
}
