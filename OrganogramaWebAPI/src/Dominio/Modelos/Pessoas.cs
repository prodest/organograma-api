using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Pessoas
    {
        public int Idparte { get; set; }
        public DateTime? Iniciovigencia { get; set; }
        public int Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime? Fimvigencia { get; set; }
        public string Obsfimvigencia { get; set; }
        public int Idunidade { get; set; }
    }
}
