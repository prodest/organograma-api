using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.WebAPI.Model
{
    public class OrganizacaoSiarhes
    {
        public string Empresa { get; set; }
        public string Codigo { get; set; }
        public string Codigo_Pai { get; set; }
        public string Cgc { get; set; }
        public string Razao { get; set; }
        public string Fantasia { get; set; }
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public string Numender { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Municipio { get; set; }
        public string Ddd { get; set; }
        public string Fone { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
    }
}
