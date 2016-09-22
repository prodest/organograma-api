using System;

namespace Organograma.Infraestrutura.Repositorios.Modelos
{
    public class MunicipioRepositorio
    {
        public decimal Idmunicipio { get; set; }
        public decimal? Codigoibge { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }
        public DateTime Iniciovigencia { get; set; }
        public DateTime? Fimvigencia { get; set; }
        public string Obsfimvigencia { get; set; }
    }
}
