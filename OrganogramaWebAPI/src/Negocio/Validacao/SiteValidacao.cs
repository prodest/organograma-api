using Organograma.Infraestrutura.Comum;
using Organograma.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Organograma.Negocio.Validacao
{
    //TODO: Implemetar esta classe
    public class SiteValidacao
    {
        internal void Preenchido(List<SiteModeloNegocio> sites)
        {
            if (sites != null)
            {
                foreach (var site in sites)
                {
                    Preenchido(site);
                }
            }
        }

        internal void Valido(List<SiteModeloNegocio> sites)
        {
            if (sites != null)
            {
                foreach (var site in sites)
                {
                    Valido(site);
                }

                Repetido(sites);
            }
        }

        internal void Preenchido(SiteModeloNegocio site)
        {
            if (site != null)
            {
                if (string.IsNullOrEmpty(site.Url))
                {
                    throw new OrganogramaRequisicaoInvalidaException("Site não preenchido");
                }
            }
        }

        internal void Valido(SiteModeloNegocio site)
        {
            if (site != null)
            {
                if (!Uri.IsWellFormedUriString(site.Url, UriKind.Absolute))
                {
                    throw new OrganogramaRequisicaoInvalidaException("Site \"" + site.Url + "\" inválido");
                }
            }
        }

        private void Repetido(List<SiteModeloNegocio> sites)
        {
            var duplicados = sites.GroupBy(e => e.Url)
                                   .Where(g => g.Count() > 1)
                                   .Select(g => g.Key)
                                   .ToList(); ;

            if (duplicados != null && duplicados.Count > 0)
                throw new OrganogramaRequisicaoInvalidaException("Existe site duplicado.");
        }
    }
}
