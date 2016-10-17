using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Email
    {
        public Email()
        {
            EmailsOrganizacao = new HashSet<EmailOrganizacao>();
            EmailsUnidade = new HashSet<EmailUnidade>();
        }

        public int Id { get; set; }
        public string Endereco { get; set; }

        public virtual ICollection<EmailOrganizacao> EmailsOrganizacao { get; set; }
        public virtual ICollection<EmailUnidade> EmailsUnidade { get; set; }
    }
}
