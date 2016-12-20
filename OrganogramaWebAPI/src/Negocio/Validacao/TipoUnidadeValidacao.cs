using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Infraestrutura.Comum;
using Organograma.Negocio.Modelos;
using System;
using System.Linq;

namespace Organograma.Negocio.Validacao
{
    public class TipoUnidadeValidacao
    {
        IRepositorioGenerico<TipoUnidade> repositorioTiposUnidades;

        public TipoUnidadeValidacao(IRepositorioGenerico<TipoUnidade> repositorioTiposUnidades)
        {
            this.repositorioTiposUnidades = repositorioTiposUnidades;
        }

        internal void TipoUnidadeValido(TipoUnidadeModeloNegocio tipoUnidade)
        {
            if (tipoUnidade == null)
                throw new OrganogramaRequisicaoInvalidaException("Tipo de unidades não pode ser nulo.");
        }

        internal void IdValido(int id)
        {
            if (id == default(int))
                throw new OrganogramaRequisicaoInvalidaException("Identificador do tipo de unidades inválido.");
        }

        internal void IdAlteracaoValido(int id, TipoUnidadeModeloNegocio tipoUnidade)
        {
            if (id != tipoUnidade.Id)
                throw new Exception("Identificadores do tipo de unidades não podem ser diferentes.");
        }

        internal void IdExistente(int id)
        {
            var tipoUnidade = repositorioTiposUnidades.SingleOrDefault(td => td.Id == id);

            if (tipoUnidade == null)
                throw new OrganogramaRequisicaoInvalidaException("Tipo de unidades não encontrado.");
        }

        internal void DescricaoValida(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new OrganogramaRequisicaoInvalidaException("O campo descrição não pode ser vazio ou nulo.");
        }

        internal void DescricaoExistente(string descricao)
        {
            var tipoUnidade = repositorioTiposUnidades.SingleOrDefault(td => td.Descricao.ToUpper().Equals(descricao.ToUpper()));

            if (tipoUnidade != null)
                throw new OrganogramaRequisicaoInvalidaException("Já existe um tipo de unidades com esta descrição.");
        }

        internal void NaoNulo(TipoUnidadeModeloNegocio tipoUnidade)
        {
            if (tipoUnidade == null)
                throw new OrganogramaRequisicaoInvalidaException("Tipo de unidade não pode ser nulo.");
        }

        internal void IdPreenchido(TipoUnidadeModeloNegocio tipoUnidade)
        {
            if (tipoUnidade.Id == default(int))
                throw new OrganogramaRequisicaoInvalidaException("O id do tipo de unidade deve ser preenchido.");
        }

        internal void Existe(TipoUnidadeModeloNegocio tipoUnidade)
        {
            var tUnidade = repositorioTiposUnidades.Where(tu => tu.Id == tipoUnidade.Id)
                                                   .SingleOrDefault();

            if (tUnidade == null)
                throw new OrganogramaNaoEncontradoException("Tipo de unidade não encontrado.");
        }

        internal void NaoEncontrado(TipoUnidade tipoUnidade)
        {
            if (tipoUnidade == null)
                throw new OrganogramaNaoEncontradoException("Tipo de unidade não encontrado.");
        }
    }
}
