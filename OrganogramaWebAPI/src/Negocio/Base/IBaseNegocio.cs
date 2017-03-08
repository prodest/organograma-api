using System;
using System.Collections.Generic;
using System.Linq;

namespace Organograma.Negocio.Base
{
    public interface IBaseNegocio
    {
        List<KeyValuePair<string, string>> Usuario { get; set; }
        string UsuarioCpf { get; }
        string UsuarioNome { get; }
        List<Guid> UsuarioGuidOrganizacoes { get; }
        List<Guid> UsuarioGuidOrganizacoesPatriarca { get; }
    }
}