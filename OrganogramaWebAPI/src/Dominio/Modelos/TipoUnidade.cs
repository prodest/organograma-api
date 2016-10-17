using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class TipoUnidade
    {
        public TipoUnidade()
        {
            Unidades = new HashSet<Unidade>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime? FimVigencia { get; set; }
        public string ObservacaoFimVigencia { get; set; }

        public virtual ICollection<Unidade> Unidades { get; set; }
    }
}
