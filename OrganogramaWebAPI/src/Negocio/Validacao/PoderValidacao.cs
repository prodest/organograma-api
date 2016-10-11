using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Infraestrutura.Comum;
using Organograma.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Organograma.Negocio.Validacao
{
    public class PoderValidacao
    {
        IRepositorioGenerico<Poder> repositorioPoderes;

        public PoderValidacao(IRepositorioGenerico<Poder> repositorioPoderes)
        {
            this.repositorioPoderes = repositorioPoderes;
        }

        internal void IdValido(int id)
        {
            if (id == default(int))
                throw new OrganogramaRequisicaoInvalidaException("Identificador do poder inválido.");
        }

        internal void IdAlteracaoValido(int id, PoderModeloNegocio poderOrganizacao)
        {
            if (id != poderOrganizacao.Id)
                throw new Exception("Identificadores do poder não podem ser diferentes.");
        }

        internal void DescricaoValida(PoderModeloNegocio poder)
        {
            if (string.IsNullOrWhiteSpace(poder.Descricao))
                throw new OrganogramaRequisicaoInvalidaException("O campo descrição não pode ser vazio ou nulo.");
        }

        internal void DescricaoExistente(PoderModeloNegocio poder)
        {
            //O registro a ser alterado deve ser desconsiderado na validação de duplicidade (quando for inserção, a id é 0. Então a segunda condição é sempre verdadeira)
            Poder poderDominio = repositorioPoderes.Where(p => p.Descricao.ToUpper().Equals(poder.Descricao.ToUpper())).Where(p => p.Id != poder.Id).SingleOrDefault();

            if (poderDominio != null)
                throw new OrganogramaRequisicaoInvalidaException("Já existe um poder com esta descrição.");
        }

        internal void PoderValido(PoderModeloNegocio poder)
        {
            if (poder == null)
                throw new OrganogramaRequisicaoInvalidaException("O poder não pode ser nulo.");
        }


        internal void NaoEncontrado(Poder poder)
        {
            if (poder == null)
                throw new OrganogramaNaoEncontradoException("Poder não encontrado.");
        }

        internal void NaoEncontrado(List<Poder> poderes)
        {
            if (poderes == null || poderes.Count <= 0)
                throw new OrganogramaNaoEncontradoException("Poderes não encontrados.");
        }
    }
}
