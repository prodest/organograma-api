using System;
using System.Collections.Generic;

namespace EntityFrameworkReverse.Models
{
    public partial class Contatos
    {
        public int Idparte { get; set; }
        public DateTime Iniciovigencia { get; set; }
        public decimal Idmunicipio { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public decimal Cep { get; set; }
        public string Email { get; set; }
        public string Homepage { get; set; }
        public decimal? Celular1ddd { get; set; }
        public decimal? Celular1 { get; set; }
        public decimal? Celular2ddd { get; set; }
        public decimal? Celular2 { get; set; }
        public decimal? Fone1ddd { get; set; }
        public decimal? Fone1 { get; set; }
        public decimal? Fone1ramal { get; set; }
        public decimal? Fone2ddd { get; set; }
        public decimal? Fone2 { get; set; }
        public decimal? Fone2ramal { get; set; }
        public decimal? Fone3ddd { get; set; }
        public decimal? Fone3 { get; set; }
        public decimal? Fone3ramal { get; set; }
        public decimal? Faxddd { get; set; }
        public decimal? Faxnumero { get; set; }
        public string Caixapostal { get; set; }
        public DateTime? Fimvigencia { get; set; }
        public string Obsfimvigencia { get; set; }
        public decimal Idcontato { get; set; }
    }
}
