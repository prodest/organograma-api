using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Site
    {
        public Site()
        {
            SitesOrganizacao = new HashSet<SiteOrganizacao>();
            SitesUnidade = new HashSet<SiteUnidade>();
        }

        public int Id { get; set; }
        public string Url { get; set; }

        public virtual ICollection<SiteOrganizacao> SitesOrganizacao { get; set; }
        public virtual ICollection<SiteUnidade> SitesUnidade { get; set; }
    }
}
