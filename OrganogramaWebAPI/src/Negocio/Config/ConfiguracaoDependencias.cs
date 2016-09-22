using System;
using System.Collections.Generic;
using Organograma.Dominio.Base;
using Organograma.Infraestrutura.Repositorios;

namespace Organograma.Negocio.Config
{
    public static class ConfiguracaoDependencias
    {
        public static Dictionary<Type, Type> ObterDependencias()
        {
            Dictionary<Type, Type> dependencias = new Dictionary<Type, Type>();
            dependencias.Add(typeof(IOrganogramaRepositorios), typeof(OrganogramaRepositorios));

            return dependencias;
        }
    }
}