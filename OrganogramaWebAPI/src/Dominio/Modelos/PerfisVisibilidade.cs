using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class PerfisVisibilidade
    {
        public int Idperfilvisibilidade { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Iniciovigencia { get; set; }
        public DateTime Fimvigencia { get; set; }
    }
}
