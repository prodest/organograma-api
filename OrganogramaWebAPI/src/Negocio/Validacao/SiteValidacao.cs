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
            foreach (var site in sites)
            {
                Preenchido(site);
            }
        }

        internal void Valido(List<SiteModeloNegocio> sites)
        {
            foreach (var site in sites)
            {
                Valido(site);
            }
        }

        internal void Preenchido(SiteModeloNegocio site)
        {
            if (string.IsNullOrEmpty(site.Url))
            {
                throw new OrganogramaRequisicaoInvalidaException("Site não preenchido");
            }
        }

        internal void Valido(SiteModeloNegocio site)
        {
            if (!Uri.IsWellFormedUriString(site.Url, UriKind.Absolute))
            {
                throw new OrganogramaRequisicaoInvalidaException("Site \"" + site.Url + "\" inválido");
            }
        }

    }
}
