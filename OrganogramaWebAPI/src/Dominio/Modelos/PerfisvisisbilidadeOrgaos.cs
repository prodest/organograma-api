using System;
using System.Collections.Generic;

namespace EntityFrameworkReverse.Models
{
    public partial class PerfisvisisbilidadeOrgaos
    {
        public int Idperfilvisibilidadeorgao { get; set; }
        public string Idperfilvisibilidade { get; set; }
        public string Idorgao { get; set; }
        public DateTime? Iniciovigencia { get; set; }
        public DateTime? Fimvigencia { get; set; }
    }
}
