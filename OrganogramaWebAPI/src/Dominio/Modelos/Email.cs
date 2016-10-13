using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Email
    {
        public Email()
        {
            EmailOrganizacao = new HashSet<EmailOrganizacao>();
        }

        public int Id { get; set; }
        public string Endereco { get; set; }

        public virtual ICollection<EmailOrganizacao> EmailOrganizacao { get; set; }
    }
}
