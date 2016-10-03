using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Comum;

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
                throw new TipoOrganizacaoException("Tipo de organização não pode ser nulo.");
        }

        internal void IdValido(int id)
        {
            if (id == default(int))
                throw new TipoOrganizacaoException("Identificador do tipo de organização inválido.");
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
                throw new TipoOrganizacaoException("Tipo de organização não encontrado.");
        }

        internal void DescricaoValida(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new TipoOrganizacaoException("O campo descrição não pode ser vazio ou nulo.");
        }

        internal void DescricaoExistente(string descricao)
        {
            var tipoOrganizacao = repositorioTiposOrganizacoes.SingleOrDefault(td => td.Descricao.ToUpper().Equals(descricao.ToUpper()));

            if (tipoOrganizacao != null)
                throw new TipoOrganizacaoException("Já existe um tipo de organização com esta descrição.");
        }
    }

    public class TipoOrganizacaoException : OrganogramaException
    {
        public TipoOrganizacaoException(string mensagem) : base(mensagem) { }

        public TipoOrganizacaoException(string mensagem, Exception ex) : base(mensagem, ex) { }
    }
}
