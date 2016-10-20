using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Infraestrutura.Comum;
using Organograma.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Organograma.Negocio.Validacao
{
    public class UnidadeValidacao
    {
        IRepositorioGenerico<Unidade> repositorioUnidades;
        IRepositorioGenerico<TipoUnidade> repositorioTiposUnidades;
        IRepositorioGenerico<Organizacao> repositorioOrganizacoes;

        public UnidadeValidacao(IRepositorioGenerico<Unidade> repositorioUnidades,
                                IRepositorioGenerico<TipoUnidade> repositorioTiposUnidades,
                                IRepositorioGenerico<Organizacao> repositorioOrganizacoes)
        {
            this.repositorioUnidades = repositorioUnidades;
            this.repositorioTiposUnidades = repositorioTiposUnidades;
            this.repositorioOrganizacoes = repositorioOrganizacoes;
        }

        internal void NaoNula(UnidadeModeloNegocio unidade)
        {
            if (unidade == null)
                throw new OrganogramaRequisicaoInvalidaException("Unidade não pode ser nula.");
        }

        #region Verificações de preenchimento de campos obrigatórios

        internal void IdPreenchido(UnidadeModeloNegocio unidade)
        {
            if (unidade.Id == default(int))
                throw new OrganogramaRequisicaoInvalidaException("O id da unidade deve ser preenchido.");
        }

        internal void IdUnidadePaiPreenchido(UnidadeModeloNegocio unidadePai)
        {
            if (unidadePai.Id == default(int))
                throw new OrganogramaRequisicaoInvalidaException("O id da unidade pai deve ser preenchido.");
        }

        internal void Preenchida(UnidadeModeloNegocio unidade)
        {
            NomePreenchido(unidade.Nome);

            SiglaPreenchida(unidade.Sigla);
        }

        internal void NomePreenchido(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new OrganogramaRequisicaoInvalidaException("O nome da unidade deve ser preenchido.");
        }

        internal void SiglaPreenchida(string sigla)
        {
            if (string.IsNullOrWhiteSpace(sigla))
                throw new OrganogramaRequisicaoInvalidaException("A sigla da unidade deve ser preenchida.");
        }

        internal void UnidadePaiPreenchida(UnidadeModeloNegocio unidadePai)
        {
            if (unidadePai != null)
                IdUnidadePaiPreenchido(unidadePai);
        }

        #endregion

        #region Validações de negócio

        internal void IdValido(UnidadeModeloNegocio unidade)
        {
            if (unidade.Id <= 0)
                throw new OrganogramaRequisicaoInvalidaException("O id da unidade é inválido.");
        }

        internal void IdUnidadePaiValido(UnidadeModeloNegocio unidade)
        {
            if (unidade.Id <= 0)
                throw new OrganogramaRequisicaoInvalidaException("O id da unidade pai é inválido.");
        }

        internal void Valida(UnidadeModeloNegocio unidade)
        {
            NomeExiste(unidade);
            SiglaExiste(unidade);
        }

        internal void NomeExiste(UnidadeModeloNegocio unidade)
        {
            var uni = repositorioUnidades.Where(u => u.Nome.ToUpper().Equals(unidade.Nome.ToUpper())
                                                  && u.IdOrganizacao == unidade.Organizacao.Id
                                                  && u.Id != unidade.Id)
                                         .SingleOrDefault();

            if (uni != null)
                throw new OrganogramaRequisicaoInvalidaException("Já existe uma unidade com este nome.");
        }

        internal void SiglaExiste(UnidadeModeloNegocio unidade)
        {
            var uni = repositorioUnidades.Where(u => u.Sigla.ToUpper().Equals(unidade.Sigla.ToUpper())
                                                  && u.IdOrganizacao == unidade.Organizacao.Id
                                                  && u.Id != unidade.Id)
                                         .SingleOrDefault();

            if (uni != null)
                throw new OrganogramaRequisicaoInvalidaException("Já existe uma unidade com esta sigla.");
        }

        internal void UnidadePaiValida(UnidadeModeloNegocio unidadePai)
        {
            if (unidadePai != null)
            {
                IdUnidadePaiValido(unidadePai);
                UnidadePaiExiste(unidadePai);
            }
        }

        private void UnidadePaiExiste(UnidadeModeloNegocio unidadePai)
        {
            if (unidadePai != null)
            {
                var uniPai = repositorioUnidades.Where(u => u.Id == unidadePai.Id)
                                                .SingleOrDefault();

                if (uniPai != null)
                    throw new OrganogramaNaoEncontradoException("Unidade pai não encontrada.");
            }
        }

        #endregion

        internal void IdAlteracaoValido(int id, UnidadeModeloNegocio unidade)
        {
            if (id != unidade.Id)
                throw new Exception("Identificadores da unidade não podem ser diferentes.");
        }

        internal void NaoEncontrado(Unidade unidade)
        {
            if (unidade == null)
                throw new OrganogramaNaoEncontradoException("Unidade não encontrada.");
        }

        internal void NaoEncontrado(List<Unidade> unidades)
        {
            if (unidades == null || unidades.Count <= 0)
                throw new OrganogramaNaoEncontradoException("Unidade não encontrada.");
        }

        //internal void DescricaoExistente(string descricao)
        //{
        //    var unidade = repositorioUnidades.SingleOrDefault(td => td.Descricao.ToUpper().Equals(descricao.ToUpper()));

        //    if (unidade != null)
        //        throw new OrganogramaRequisicaoInvalidaException("Já existe uma unidade com esta descrição.");
        //}

    }
}
