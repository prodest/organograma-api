using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Negocio.Modelos.Siarhes
{
    public class UnidadeSiarhes
    {
        public int Empresa { get; set; }
        public int Subempresa { get; set; }
        public string Setor { get; set; }
        public string Sigla { get; set; }
        public string NomeSetor { get; set; }
        public string NomeSetorLongo { get; set; }
        public string PaiSetor { get; set; }
        public DateTime DataIni { get; set; }
        public DateTime? DataFim { get; set; }
        public string TipoSetor { get; set; }
        public string Logradouro { get; set; }
        public string NumEnder { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Municipio { get; set; }
        public string Fone { get; set; }

        public UnidadeSiarhes UnidadePai { get; set; }
        public List<UnidadeSiarhes> UnidadesFilhas { get; set; }
    }
}
