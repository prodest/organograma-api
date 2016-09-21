using System;
using System.Collections.Generic;

namespace EntityFrameworkReverse.Models
{
    public partial class UsuariosPerfisacesso
    {
        public int Idusuario { get; set; }
        public string Idperfilacesso { get; set; }
        public DateTime? Iniciovigencia { get; set; }
        public DateTime? Fimvigencia { get; set; }
    }
}
