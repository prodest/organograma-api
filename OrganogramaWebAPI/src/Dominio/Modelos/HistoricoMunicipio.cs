using System;

namespace Organograma.Dominio.Modelos
{
    public partial class HistoricoMunicipio
    {
        public int Id { get; set; }
        public string Json { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime FimVigencia { get; set; }
        public string ObservacaoFimVigencia { get; set; }
        public int IdIdentificadorExterno { get; set; }
        public int? IdMunicipio { get; set; }

        public virtual IdentificadorExterno IdentificadorExterno { get; set; }
        public virtual Municipio Municipio { get; set; }
    }
}
