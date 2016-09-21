using System;
using System.Collections.Generic;

namespace EntityFrameworkReverse.Models
{
    public partial class FunionalidadesPerfisacesso
    {
        public int Idfuncionalidade { get; set; }
        public decimal? Idperfilacesso { get; set; }
        public DateTime? Iniciovigencia { get; set; }
        public DateTime? Fimvigencia { get; set; }
    }
}
