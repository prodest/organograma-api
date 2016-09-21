using System;
using System.Collections.Generic;

namespace EntityFrameworkReverse.Models
{
    public partial class Organizacoes
    {
        public int Idparte { get; set; }
        public DateTime? Iniciovigencia { get; set; }
        public string Idatividade { get; set; }
        public string Idnaturezajuridica { get; set; }
        public string Cnpj { get; set; }
        public string Razaosocial { get; set; }
        public string Nomefantasia { get; set; }
        public string Sigla { get; set; }
        public DateTime? Fimvigencia { get; set; }
        public DateTime? Obsfimvigencia { get; set; }
        public string Titular { get; set; }
        public string Poder { get; set; }
        public string Esfera { get; set; }
        public string Aceitaboletimeletronico { get; set; }
        public string Email { get; set; }
    }
}
