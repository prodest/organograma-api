using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Negocio.Modelos
{
    public class MunicipioModeloNegocio
    {
        public decimal Idmunicipio { get; set; }
        public decimal? Codigoibge { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }
        public DateTime Iniciovigencia { get; set; }
        public DateTime? Fimvigencia { get; set; }
        public string Obsfimvigencia { get; set; }
    }
}
