using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Infraestrutura.Comum;
using Organograma.Negocio.Modelos;
using System;
using System.Linq;

namespace Organograma.Negocio.Validacao
{
    public class TipoOrganizacaoValidacao
    {
        IRepositorioGenerico<TipoOrganizacao> repositorioTiposOrganizacoes;

        public TipoOrganizacaoValidacao(IRepositorioGenerico<TipoOrganizacao> repositorioTiposOrganizacoes)
        {
            this.repositorioTiposOrganizacoes = repositorioTiposOrganizacoes;
        }

        internal void TipoOrganizacaoValido(TipoOrganizacaoModeloNegocio tipoOrganizacao)
        {
            if (tipoOrganizacao == null)
                throw new OrganogramaRequisicaoInvalidaException("Tipo de organização não pode ser nulo.");
        }

        internal void IdValido(int id)
        {
            if (id == default(int))
                throw new OrganogramaRequisicaoInvalidaException("Identificador do tipo de organização inválido.");
        }

        internal void IdValido(TipoOrganizacaoModeloNegocio tipoOrganizacao)
        {
            if (tipoOrganizacao != null)
            {
                IdValido(tipoOrganizacao.Id);
            }
        }

        internal void IdAlteracaoValido(int id, TipoOrganizacaoModeloNegocio tipoOrganizacao)
        {
            if (id != tipoOrganizacao.Id)
                throw new Exception("Identificadores do tipo de organização não podem ser diferentes.");
        }

        internal void IdExistente(int id)
        {
            var tipoOrganizacao = repositorioTiposOrganizacoes.SingleOrDefault(td => td.Id == id);

            if (tipoOrganizacao == null)
                throw new OrganogramaRequisicaoInvalidaException("Tipo de organização não encontrado.");
        }

        internal void DescricaoValida(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new OrganogramaRequisicaoInvalidaException("O campo descrição não pode ser vazio ou nulo.");
        }

        internal void DescricaoExistente(string descricao)
        {
            var tipoOrganizacao = repositorioTiposOrganizacoes.SingleOrDefault(td => td.Descricao.ToUpper().Equals(descricao.ToUpper()));

            if (tipoOrganizacao != null)
                throw new OrganogramaRequisicaoInvalidaException("Já existe um tipo de organização com esta descrição.");
        }

        internal void IdPreenchido(TipoOrganizacaoModeloNegocio tipoOrganizacao)
        {
            if (tipoOrganizacao != null)
            {
                if (tipoOrganizacao.Id == default(int))
                    throw new OrganogramaRequisicaoInvalidaException("Tipo da organização não preenchido.");
            }
        }

        internal void Existe(TipoOrganizacaoModeloNegocio tipoOrganizacao)
        {

            if (tipoOrganizacao != null)
            {
                if (repositorioTiposOrganizacoes.Where(e => e.Id == tipoOrganizacao.Id).SingleOrDefault() == null)
                {
                    throw new OrganogramaNaoEncontradoException("Tipo da organização não existe");
                }
            }
        }
    }
}
