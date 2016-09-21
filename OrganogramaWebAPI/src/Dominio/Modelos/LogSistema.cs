using System;
using System.Collections.Generic;

namespace EntityFrameworkReverse.Models
{
    public partial class LogSistema
    {
        public int Idlogsistema { get; set; }
        public string Mensagem { get; set; }
        public string Rastro { get; set; }
        public string Url { get; set; }
        public DateTime? DataEvento { get; set; }
        public string LoginUsuario { get; set; }
        public string IpUsuario { get; set; }
        public string Browser { get; set; }
        public string Javascript { get; set; }
    }
}
