using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class SiteUnidade
    {
        public int Id { get; set; }
        public int IdSite { get; set; }
        public int IdUnidade { get; set; }

        public virtual Site Site { get; set; }
        public virtual Unidade Unidade { get; set; }
    }
}
