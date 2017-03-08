using System.Collections.Generic;
using System.Linq;

namespace Organograma.Apresentacao.Base
{
    public interface IBaseWorkService
    {
        List<KeyValuePair<string, string>> Usuario { get; set; }
        void RaiseUsuarioAlterado();
    }
}
