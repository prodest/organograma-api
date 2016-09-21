using System;
using System.Collections.Generic;

namespace EntityFrameworkReverse.Models
{
    public partial class Usuarios
    {
        public int Idusuario { get; set; }
        public string Idcontatousuario { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
        public DateTime? Iniciovigencia { get; set; }
        public DateTime? Fimvigencia { get; set; }
        public decimal? Idorgao { get; set; }
        public decimal? Idlocal { get; set; }
        public decimal? QtdTentativaAcesso { get; set; }
        public DateTime? DataBloqueio { get; set; }
        public decimal? Idtiposusuario { get; set; }
        public string Visibilidade { get; set; }
        public string Iplogin { get; set; }
        public string Sessionid { get; set; }
    }
}
