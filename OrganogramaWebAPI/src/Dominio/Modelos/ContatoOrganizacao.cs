using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class ContatoOrganizacao
    {
        public int Id { get; set; }
        public int IdContato { get; set; }
        public int IdOrganizacao { get; set; }

        public virtual Contato IdContatoNavigation { get; set; }
        public virtual Organizacao IdOrganizacaoNavigation { get; set; }
    }
}
