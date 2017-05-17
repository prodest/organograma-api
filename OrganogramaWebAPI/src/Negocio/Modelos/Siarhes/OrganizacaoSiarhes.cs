using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Negocio.Modelos.Siarhes
{
    public class OrganizacaoSiarhes
    {
        public int Empresa { get; set; }
        public int Codigo { get; set; }
        public int? Codigo_Pai { get; set; }
        public string Cgc { get; set; }
        public string Razao { get; set; }
        public string Fantasia { get; set; }
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public string NumEnder { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Municipio { get; set; }
        public int? Ddd { get; set; }
        public int? Fone { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }

        public OrganizacaoSiarhes OrganizacaoPai { get; set; }
        public List<OrganizacaoSiarhes> OrganizacoesFilhas { get; set; }
        public List<UnidadeSiarhes> Unidades { get; set; }
    }
}
