using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Negocio.Base
{
    public interface IGuidOrganizacaoProvider
    {
        Guid Search(string sigla);
        Guid SearchPatriarca(Guid guid);
    }
}