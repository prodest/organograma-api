using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Negocio.Modelos
{
    public class HistoricoContatoModeloNegocio
    {
        public int Id { get; set; }
        public string Telefone { get; set; }
        public int IdTipoContato { get; set; }
        public string Nome { get; set; }
    }
}
