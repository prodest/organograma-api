using Apresentacao.Municipio;
using Apresentacao.Municipio.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Organograma.WebAPI.Config
{
    public static class ConfiguracaoDependencias
    {
        public static Dictionary<Type, Type> ObterDependencias()
        {
            Dictionary<Type, Type> dependencias = new Dictionary<Type, Type>();

            dependencias.Add(typeof(IMunicipioWorkService), typeof(MunicipioWorkService));
            return dependencias;
        }

        public static void InjetarDependencias(IServiceCollection services)
        {
            Dictionary<Type, Type> dependencias = new Dictionary<Type, Type>();
            dependencias = ObterDependencias();

            foreach (var dep in dependencias)
            {
                services.AddTransient(dep.Key, dep.Value);
            }

        }


    }
}
