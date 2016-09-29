using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class TipoUnidade
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime? FimVigencia { get; set; }
        public string ObservacaoFimVigencia { get; set; }
    }
}
