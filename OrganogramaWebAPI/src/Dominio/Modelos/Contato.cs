using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Contato
    {
        public Contato()
        {
            ContatoOrganizacao = new HashSet<ContatoOrganizacao>();
        }

        public int Id { get; set; }
        public long Telefone { get; set; }
        public byte TipoTelefone { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<ContatoOrganizacao> ContatoOrganizacao { get; set; }
    }
}
