using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class ContatoUnidade
    {
        public int Id { get; set; }
        public int IdContato { get; set; }
        public int IdUnidade { get; set; }

        public virtual Contato Contato { get; set; }
        public virtual Unidade Unidade { get; set; }
    }
}
