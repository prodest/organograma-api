﻿using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class ContatoOrganizacao
    {
        public int Id { get; set; }
        public int IdContato { get; set; }
        public int IdOrganizacao { get; set; }

        public virtual Contato Contato { get; set; }
        public virtual Organizacao Organizacao { get; set; }
    }
}
