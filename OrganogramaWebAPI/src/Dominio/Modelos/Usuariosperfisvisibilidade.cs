﻿using System;
using System.Collections.Generic;

namespace EntityFrameworkReverse.Models
{
    public partial class Usuariosperfisvisibilidade
    {
        public int Idusuarioperfilvisibilidade { get; set; }
        public string Idperfilvisibilidade { get; set; }
        public string Idusuario { get; set; }
        public DateTime? Iniciovigencia { get; set; }
        public DateTime? Fimvigencia { get; set; }
    }
}
