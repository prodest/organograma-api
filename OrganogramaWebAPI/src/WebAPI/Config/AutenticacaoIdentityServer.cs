using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.WebAPI.Config
{
    public class AutenticacaoIdentityServer
    {
        public string Authority { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public string ScopeName { get; set; }
        public bool AutomaticAuthenticate { get; set; }
    }
}
