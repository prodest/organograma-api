using Apresentacao;
using Apresentacao.Base;
using Microsoft.Extensions.DependencyInjection;
using Organograma.Apresentacao;
using Organograma.Apresentacao.Base;
using System;
using System.Collections.Generic;

namespace Organograma.WebAPI.Config
{
    public static class ConfiguracaoDependencias
    {
        public static Dictionary<Type, Type> ObterDependencias()
        {
            Dictionary<Type, Type> dependencias = new Dictionary<Type, Type>();
            dependencias = Apresentacao.Config.ConfiguracaoDepedencias.ObterDependencias();

            dependencias.Add(typeof(IEsferaOrganizacaoWorkService), typeof(EsferaOrganizacaoWorkService));
            dependencias.Add(typeof(IMunicipioWorkService), typeof(MunicipioWorkService));
            dependencias.Add(typeof(ITipoOrganizacaoWorkService), typeof(TipoOrganizacaoWorkService));
            dependencias.Add(typeof(ITipoUnidadeWorkService), typeof(TipoUnidadeWorkService));
            dependencias.Add(typeof(IPoderWorkService), typeof(PoderWorkService));
            dependencias.Add(typeof(IOrganizacaoWorkService), typeof(OrganizacaoWorkService));
            dependencias.Add(typeof(IUnidadeWorkService), typeof(UnidadeWorkService));

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
