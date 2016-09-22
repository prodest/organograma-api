using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Municipio
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
