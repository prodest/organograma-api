using System;
using System.Collections.Generic;

namespace EntityFrameworkReverse.Models
{
    public partial class Transicoes
    {
        public int Idparte { get; set; }
        public string Idsiarhes { get; set; }
        public string Idsep { get; set; }
        public string Descricao { get; set; }
        public int? Empresa { get; set; }
        public int? Subempresa { get; set; }
    }
}
