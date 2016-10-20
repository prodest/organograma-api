using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class TipoContato
    {
        public TipoContato()
        {
            Contatos = new HashSet<Contato>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public byte QuantidadeDigitos { get; set; }

        public virtual ICollection<Contato> Contatos { get; set; }
    }
}
