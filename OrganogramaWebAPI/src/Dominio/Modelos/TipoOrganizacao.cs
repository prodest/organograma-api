using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class TipoOrganizacao
    {
        public TipoOrganizacao()
        {
            Organizacoes = new HashSet<Organizacao>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime? FimVigencia { get; set; }
        public string ObservacaoFimVigencia { get; set; }

        public virtual ICollection<Organizacao> Organizacoes { get; set; }
    }
}
