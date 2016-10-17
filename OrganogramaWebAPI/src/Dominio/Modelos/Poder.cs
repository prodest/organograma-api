using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Poder
    {
        public Poder()
        {
            Organizacoes = new HashSet<Organizacao>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Organizacao> Organizacoes { get; set; }
    }
}
