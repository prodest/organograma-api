using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Contato
    {
        public Contato()
        {
            ContatosOrganizacao = new HashSet<ContatoOrganizacao>();
            ContatosUnidade = new HashSet<ContatoUnidade>();
        }

        public int Id { get; set; }
        public string Telefone { get; set; }
        public byte TipoTelefone { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<ContatoOrganizacao> ContatosOrganizacao { get; set; }
        public virtual ICollection<ContatoUnidade> ContatosUnidade { get; set; }
    }
}
