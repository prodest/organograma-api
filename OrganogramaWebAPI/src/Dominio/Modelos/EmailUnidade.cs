using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class EmailUnidade
    {
        public int Id { get; set; }
        public int IdEmail { get; set; }
        public int IdUnidade { get; set; }

        public virtual Email Email { get; set; }
        public virtual Unidade Unidade { get; set; }
    }
}
