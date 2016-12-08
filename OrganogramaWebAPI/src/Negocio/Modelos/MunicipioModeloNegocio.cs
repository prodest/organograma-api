using System;

namespace Organograma.Negocio.Modelos
{
    public class MunicipioModeloNegocio
    {
        public int Id { get; set; }
        public int CodigoIbge { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }
        public string Guid { get; set; }
        public DateTime? InicioVigencia { get; set; }
        public DateTime? FimVigencia { get; set; }
        public string ObservacaoFimVigencia { get; set; }
        
    }
}
