using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Infraestrutura.Comum;
using Organograma.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Organograma.Negocio.Validacao
{
    public class OrganizacaoValidacao
    {
        IRepositorioGenerico<Organizacao> repositorioOrganizacoes;

        public OrganizacaoValidacao(IRepositorioGenerico<Organizacao> repositorioOrganizacoes)
        {
            this.repositorioOrganizacoes = repositorioOrganizacoes;
        }
        

        internal void camposObrigatorios(OrganizacaoModeloNegocio organizacao)
        {
            if (string.IsNullOrEmpty(organizacao.Cnpj) || string.IsNullOrEmpty(organizacao.RazaoSocial) || string.IsNullOrEmpty(organizacao.Sigla) )
            {
                throw new OrganogramaRequisicaoInvalidaException("Cnpj, Razão social e Sigla da organização devem ser informados");
            }
        }
        
    }
}
