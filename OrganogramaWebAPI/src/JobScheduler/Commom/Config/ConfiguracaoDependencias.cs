using Apresentacao.Base;
using Microsoft.Extensions.DependencyInjection;
using Organograma.Apresentacao;
using System;
using System.Collections.Generic;

namespace Organograma.JobScheduler.Commom.Config
{
    public static class ConfiguracaoDependencias
    {
        public static Dictionary<Type, Type> ObterDependencias()
        {
            Dictionary<Type, Type> dependencias = new Dictionary<Type, Type>();
            dependencias = Apresentacao.Config.ConfiguracaoDepedencias.ObterDependencias();

            dependencias.Add(typeof(IOrganizacaoWorkService), typeof(OrganizacaoWorkService));

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
