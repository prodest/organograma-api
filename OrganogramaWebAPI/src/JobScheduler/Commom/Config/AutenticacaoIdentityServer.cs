using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.JobScheduler.Commom.Config
{
    public class AutenticacaoIdentityServer
    {
        public string Authority { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public List<string> AllowedScopes { get; set; }
        public bool AutomaticAuthenticate { get; set; }
    }
}
