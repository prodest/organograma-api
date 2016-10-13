using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class EmailOrganizacao
    {
        public int Id { get; set; }
        public int IdEmail { get; set; }
        public int IdOrganizacao { get; set; }

        public virtual Email IdEmailNavigation { get; set; }
        public virtual Organizacao IdOrganizacaoNavigation { get; set; }
    }
}
