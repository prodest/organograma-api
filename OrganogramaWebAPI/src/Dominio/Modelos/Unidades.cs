using System;
using System.Collections.Generic;

namespace EntityFrameworkReverse.Models
{
    public partial class Unidades
    {
        public int Idparte { get; set; }
        public DateTime? Iniociovigencia { get; set; }
        public string Tramitaprocesso { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public DateTime? Fimvigencia { get; set; }
        public string Obsfimvigencia { get; set; }
        public int? Idorganizacao { get; set; }
        public int? Idtipounidade { get; set; }
        public string Palavrachave { get; set; }
        public string Descricao { get; set; }
        public int? Aceitaboletimeletronico { get; set; }
        public string Email { get; set; }
    }
}
