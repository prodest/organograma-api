using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class FunionalidadesPerfisacesso
    {
        public int Idfuncionalidade { get; set; }
        public decimal? Idperfilacesso { get; set; }
        public DateTime? Iniciovigencia { get; set; }
        public DateTime? Fimvigencia { get; set; }
    }
}
