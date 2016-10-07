using Organograma.Negocio;
using Organograma.Negocio.Base;
using System;
using System.Collections.Generic;

namespace Organograma.Apresentacao.Config
{
    public static class ConfiguracaoDepedencias
    {
        public static Dictionary<Type, Type> ObterDependencias()
        {
            Dictionary<Type, Type> dependencias = new Dictionary<Type, Type>();

            dependencias = Negocio.Config.ConfiguracaoDependencias.ObterDependencias();

            dependencias.Add(typeof(IEsferaOrganizacaoNegocio), typeof(EsferaOrganizacaoNegocio));
            dependencias.Add(typeof(IMunicipioNegocio), typeof(MunicipioNegocio));
            dependencias.Add(typeof(ITipoOrganizacaoNegocio), typeof(TipoOrganizacaoNegocio));
            dependencias.Add(typeof(ITipoUnidadeNegocio), typeof(TipoUnidadeNegocio));

            return dependencias;
        }
    }
}
