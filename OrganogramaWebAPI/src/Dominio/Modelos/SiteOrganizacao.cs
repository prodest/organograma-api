using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class SiteOrganizacao
    {
        public int Id { get; set; }
        public int IdSite { get; set; }
        public int IdOrganizacao { get; set; }

        public virtual Organizacao IdOrganizacaoNavigation { get; set; }
        public virtual Site IdSiteNavigation { get; set; }
    }
}
