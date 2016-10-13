using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Site
    {
        public Site()
        {
            SiteOrganizacao = new HashSet<SiteOrganizacao>();
        }

        public int Id { get; set; }
        public string Url { get; set; }

        public virtual ICollection<SiteOrganizacao> SiteOrganizacao { get; set; }
    }
}
