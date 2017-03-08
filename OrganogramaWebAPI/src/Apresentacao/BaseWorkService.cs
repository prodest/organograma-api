using Organograma.Apresentacao.Base;
using System.Collections.Generic;
using System.Linq;

namespace Organograma.Apresentacao
{
    public abstract class BaseWorkService : IBaseWorkService
    {
        private List<KeyValuePair<string, string>> usuario;
        public List<KeyValuePair<string, string>> Usuario
        {
            get
            {
                return usuario;
            }

            set
            {
                usuario = value;
                RaiseUsuarioAlterado();
            }
        }

        public abstract void RaiseUsuarioAlterado();
    }
}
