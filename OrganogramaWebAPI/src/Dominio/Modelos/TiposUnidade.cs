using System;
using System.Collections.Generic;

namespace EntityFrameworkReverse.Models
{
    public partial class TiposUnidade
    {
        public int Idtipounidade { get; set; }
        public string Nome { get; set; }
        public DateTime Iniciovigencia { get; set; }
        public DateTime? Fimvigencia { get; set; }
        public DateTime? Obsfimvigencia { get; set; }
    }
}
