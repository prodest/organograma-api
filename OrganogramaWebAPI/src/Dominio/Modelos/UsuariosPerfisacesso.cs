using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class UsuariosPerfisacesso
    {
        public int Idusuario { get; set; }
        public string Idperfilacesso { get; set; }
        public DateTime? Iniciovigencia { get; set; }
        public DateTime? Fimvigencia { get; set; }
    }
}
