using System;
using System.Collections.Generic;

namespace EntityFrameworkReverse.Models
{
    public partial class PerfisAcesso
    {
        public int Idperfilacesso { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Iniciovigencia { get; set; }
        public DateTime Fimvigencia { get; set; }
    }
}
