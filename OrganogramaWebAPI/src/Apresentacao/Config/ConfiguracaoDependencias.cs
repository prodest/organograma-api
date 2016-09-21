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
            
            return dependencias;
        }
    }
}
