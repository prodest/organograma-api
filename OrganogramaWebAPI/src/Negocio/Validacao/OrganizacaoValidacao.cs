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
        private CnpjValidacao cnpjValidacao;

        public OrganizacaoValidacao(IRepositorioGenerico<Organizacao> repositorioOrganizacoes)
        {
            this.repositorioOrganizacoes = repositorioOrganizacoes;
            cnpjValidacao = new CnpjValidacao(repositorioOrganizacoes);
        }

        internal void IdPreenchido(OrganizacaoModeloNegocio organizacao)
        {
            if (organizacao.Id == default(int))
            {
                throw new OrganogramaRequisicaoInvalidaException("Id da organização não preenchido.");
            }
        }

        internal void IdValido(OrganizacaoModeloNegocio organizacao)
        {
            if (organizacao.Id < 0)
            {
                throw new OrganogramaRequisicaoInvalidaException("Id da organização não é valido");
            }
        }

        internal void IdPaiValido(OrganizacaoModeloNegocio organizacao)
        {
            if (organizacao.Id <= 0)
            {
                throw new OrganogramaRequisicaoInvalidaException("Id da organização pai não é valido");
            }
        }
        

        internal void Preenchido(OrganizacaoModeloNegocio organizacao)
        {
            if (string.IsNullOrEmpty(organizacao.Cnpj))
            {
                throw new OrganogramaRequisicaoInvalidaException("Cnpj não preenchido.");
            }

            if (string.IsNullOrEmpty(organizacao.RazaoSocial))
            {
                throw new OrganogramaRequisicaoInvalidaException("Razão Social não preenchida.");
            }

            if (string.IsNullOrEmpty(organizacao.Sigla))
            {
                throw new OrganogramaRequisicaoInvalidaException("Sigla não preenchida.");
            }
        }

        internal void Existe(OrganizacaoModeloNegocio organizacao)
        {
            if (repositorioOrganizacoes.Where(o => o.Id == organizacao.Id).SingleOrDefault() == null)
            {
                throw new OrganogramaNaoEncontradoException("Organização não existe.");
            };
        }

        internal void PaiExiste(OrganizacaoModeloNegocio organizacao)
        {
            if (repositorioOrganizacoes.Where(o => o.Id == organizacao.Id).SingleOrDefault() == null)
            {
                throw new OrganogramaNaoEncontradoException("Organização pai não existe.");
            };
        }

        internal void PaiValido(OrganizacaoModeloNegocio organizacaoPai)
        {
            
            if (organizacaoPai != null)
            {
                IdPaiValido(organizacaoPai);
                PaiExiste(organizacaoPai);
            }
                      
        }

        internal void Valido (OrganizacaoModeloNegocio organizacao)
        {
            cnpjValidacao.CnpjExiste(organizacao);
            cnpjValidacao.CnpjValido(organizacao.Cnpj);
            SiglaValida(organizacao);
        }

        private void SiglaValida(OrganizacaoModeloNegocio organizacao)
        {
            if (organizacao.Sigla.Length > 10)
            {
                throw new OrganogramaRequisicaoInvalidaException("Sigla deve possuir no máximo 10 caracteres");
            }
        }

        internal void NaoNulo(OrganizacaoModeloNegocio organizacao)
        {
            throw new NotImplementedException();
        }
    }
}
