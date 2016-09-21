using System;
using System.Collections.Generic;

namespace EntityFrameworkReverse.Models
{
    public partial class TiposRelacao
    {
        public int Idtiporelacao { get; set; }
        public string Nome { get; set; }
        public decimal? Idtiporelacaopai { get; set; }
        public DateTime Iniciovigencia { get; set; }
        public DateTime? Fimvigencia { get; set; }
        public string Obsfimvigencia { get; set; }
        public string Restrito { get; set; }
    }
}
