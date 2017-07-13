using System;

namespace Organograma.Dominio.Modelos
{
    public partial class Historico
    {
        public int Id { get; set; }
        public string Json { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime FimVigencia { get; set; }
        public string ObservacaoFimVigencia { get; set; }
        public int IdIdentificadorExterno { get; set; }

        public virtual IdentificadorExterno IdentificadorExterno { get; set; }
    }
}
