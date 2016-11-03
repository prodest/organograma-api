using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Infraestrutura.Comum;
using Organograma.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Organograma.Negocio.Validacao
{
    public class EsferaOrganizacaoValidacao
    {
        IRepositorioGenerico<EsferaOrganizacao> repositorioEsferasOrganizacoes;

        public EsferaOrganizacaoValidacao(IRepositorioGenerico<EsferaOrganizacao> repositorioEsferasOrganizacoes)
        {
            this.repositorioEsferasOrganizacoes = repositorioEsferasOrganizacoes;
        }

        internal void EsferaOrganizacaoValido(EsferaOrganizacaoModeloNegocio esferaOrganizacao)
        {
            if (esferaOrganizacao == null)
                throw new OrganogramaRequisicaoInvalidaException("Esfera de organizações não pode ser nulo.");
        }

        internal void IdValido(int id)
        {
            if (id <= default(int))
                throw new OrganogramaRequisicaoInvalidaException("Identificador da esfera de organização inválido.");
        }

        internal void IdValido(EsferaOrganizacaoModeloNegocio esfera)
        {
            if (esfera != null)
            {
                IdValido(esfera.Id);
            }
        }

        internal void IdAlteracaoValido(int id, EsferaOrganizacaoModeloNegocio esferaOrganizacao)
        {
            if (id != esferaOrganizacao.Id)
                throw new Exception("Identificadores da esfera de organizações não podem ser diferentes.");
        }

        internal void DescricaoValida(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new OrganogramaRequisicaoInvalidaException("O campo descrição não pode ser vazio ou nulo.");
        }

        internal void DescricaoExistente(string descricao)
        {
            var esferaOrganizacao = repositorioEsferasOrganizacoes.SingleOrDefault(td => td.Descricao.ToUpper().Equals(descricao.ToUpper()));

            if (esferaOrganizacao != null)
                throw new OrganogramaRequisicaoInvalidaException("Já existe uma esfera de organizações com esta descrição.");
        }

        internal void NaoEncontrado(EsferaOrganizacao esferaOrganizacao)
        {
            if (esferaOrganizacao == null)
                throw new OrganogramaNaoEncontradoException("Esfera de organização não encontrada.");
        }

        internal void NaoEncontrado(List<EsferaOrganizacao> esferasOrganizacoes)
        {
            if (esferasOrganizacoes == null || esferasOrganizacoes.Count <= 0)
                throw new OrganogramaNaoEncontradoException("Esfera de organização não encontrada.");
        }

        internal void IdPreenchido(EsferaOrganizacaoModeloNegocio esfera)
        {
            if (esfera != null)
            {
                if (esfera.Id == default(int))
                    throw new OrganogramaRequisicaoInvalidaException("Esfera da organização não preenchido.");
            }

        }

        internal void Existe(EsferaOrganizacaoModeloNegocio esfera)
        {

            

            if (repositorioEsferasOrganizacoes.Where(e => e.Id == esfera.Id).SingleOrDefault() == null)
            {
                throw new OrganogramaNaoEncontradoException("Esfera de organização não existe");
            }
        }


    }
}
