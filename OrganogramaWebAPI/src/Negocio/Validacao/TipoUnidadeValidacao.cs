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
                throw new TipoUnidadeException("Tipo de unidades não pode ser nulo.");
        }

        internal void IdValido(int id)
        {
            if (id == default(int))
                throw new TipoUnidadeException("Identificador do tipo de unidades inválido.");
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
                throw new TipoUnidadeException("Tipo de unidades não encontrado.");
        }

        internal void DescricaoValida(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new TipoUnidadeException("O campo descrição não pode ser vazio ou nulo.");
        }

        internal void DescricaoExistente(string descricao)
        {
            var tipoUnidade = repositorioTiposUnidades.SingleOrDefault(td => td.Descricao.ToUpper().Equals(descricao.ToUpper()));

            if (tipoUnidade != null)
                throw new TipoUnidadeException("Já existe um tipo de unidades com esta descrição.");
        }
    }

    public class TipoUnidadeException : OrganogramaException
    {
        public TipoUnidadeException(string mensagem) : base(mensagem) { }

        public TipoUnidadeException(string mensagem, Exception ex) : base(mensagem, ex) { }
    }
}
