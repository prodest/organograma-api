using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Negocio.Commom.Base
{
    public interface ICurrentUserProvider
    {
        string UserCpf { get; }
        string UserNome { get; }
        List<Guid> UserGuidsOrganizacao { get; }
        List<Guid> UserGuidsOrganizacaoPatriarca { get; }
    }
}
