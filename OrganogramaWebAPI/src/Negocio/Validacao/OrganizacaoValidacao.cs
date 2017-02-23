using Microsoft.EntityFrameworkCore;
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

        internal void GuidPreenchido(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
            {
                throw new OrganogramaRequisicaoInvalidaException("Guid da organização não preenchido.");
            }
        }

        internal void GuidPreenchido(OrganizacaoModeloNegocio organizacao)
        {
            GuidPreenchido(organizacao.Guid);
        }

        internal void IdPreenchido(int id)
        {
            if (id == default(int))
            {
                throw new OrganogramaRequisicaoInvalidaException("Id da organização não preenchido.");
            }
        }

        internal void IdPreenchido(OrganizacaoModeloNegocio organizacao)
        {
            IdPreenchido(organizacao.Id);
        }

        internal void IdValido(OrganizacaoModeloNegocio organizacao)
        {
            if (organizacao.Id < 0)
            {
                throw new OrganogramaRequisicaoInvalidaException("Id da organização não é valido");
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

        internal void PossuiFilho(int id)
        {
            if (repositorioOrganizacoes.Where(o => o.IdOrganizacaoPai == id).ToList().Count > 0)
            {
                throw new OrganogramaRequisicaoInvalidaException("Organização possui organizações filhas.");
            }

        }

        internal void PossuiUnidade(int id)
        {
            Organizacao organizacao = repositorioOrganizacoes.Where(o => o.Id == id).Include(i => i.Unidades).SingleOrDefault();

            if (organizacao != null)
            {
                if (organizacao.Unidades.ToList().Count > 0)
                {
                    throw new OrganogramaRequisicaoInvalidaException("Organização possui unidades");
                }
            }
        }

        internal void IdAlteracaoValido(int id, OrganizacaoModeloNegocio organizacaoNegocio)
        {
            if (id != organizacaoNegocio.Id)
            {
                throw new OrganogramaRequisicaoInvalidaException("Identificadores de Organização diferentes.");
            }
        }

        internal void Existe(int id)
        {
            if (repositorioOrganizacoes.Where(o => o.Id == id).SingleOrDefault() == null)
            {
                throw new OrganogramaNaoEncontradoException("Organização não existe.");
            }
        }

        internal void Existe(OrganizacaoModeloNegocio organizacao)
        {
            Existe(organizacao.Id);
        }

        internal void Existe(string guid)
        {
            Guid g = new Guid(guid);

            if (repositorioOrganizacoes.Where(o => o.IdentificadorExterno.Guid.Equals(g)).SingleOrDefault() == null)
            {
                throw new OrganogramaNaoEncontradoException("Organização não existe.");
            }
        }

        internal void ExistePorGuid(OrganizacaoModeloNegocio organizacao)
        {
            Existe(organizacao.Guid);
        }

        internal void PaiExiste(OrganizacaoModeloNegocio organizacaoPai)
        {

            if (organizacaoPai != null)
            {
                Guid guid = new Guid(organizacaoPai.Guid);

                if (repositorioOrganizacoes.Where(o => o.IdentificadorExterno.Guid.Equals(guid)).SingleOrDefault() == null)
                {
                    throw new OrganogramaNaoEncontradoException("Organização pai não existe.");
                }
            }
        }

        internal void PaiDiferente(OrganizacaoModeloNegocio organizacao)
        {
            if (organizacao != null && organizacao.OrganizacaoPai != null)
            {
                if (organizacao.Guid.Equals(organizacao.OrganizacaoPai.Guid))
                {
                    throw new OrganogramaNaoEncontradoException("A organização pai deve ser diferente.");
                }
            }
        }


        internal void PaiValido(OrganizacaoModeloNegocio organizacaoPai)
        {

            if (organizacaoPai != null)
            {
                GuidValido(organizacaoPai.Guid);
                PaiExiste(organizacaoPai);
                PaiDiferente(organizacaoPai);
            }

        }

        internal void Valido(OrganizacaoModeloNegocio organizacao)
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
            if (organizacao == null)
                throw new OrganogramaRequisicaoInvalidaException("Organização não pode ser nulo.");
        }

        internal void NaoEncontrado(Organizacao organizacao)
        {
            if (organizacao == null)
            {
                throw new OrganogramaNaoEncontradoException("Organização não encontrada.");
            }
        }

        internal void GuidValido(string guid)
        {
            try
            {
                Guid g = new Guid(guid);

                if (g.Equals(Guid.Empty))
                    throw new OrganogramaRequisicaoInvalidaException("Identificador inválido.");
            }
            catch (FormatException)
            {
                throw new OrganogramaRequisicaoInvalidaException("Formato do identificador inválido.");
            }
        }
    }
}
