using Organograma.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Organograma.Negocio.Modelos;
using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Negocio.Validacao;
using AutoMapper;

namespace Organograma.Negocio
{
    public class EsferaOrganizacaoNegocio : IEsferaOrganizacaoNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<EsferaOrganizacao> repositorioEsferasOrganizacoes;
        private EsferaOrganizacaoValidacao validacao;

        public EsferaOrganizacaoNegocio(IOrganogramaRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioEsferasOrganizacoes = repositorios.EsferasOrganizacoes;
            validacao = new EsferaOrganizacaoValidacao(repositorioEsferasOrganizacoes);
        }

        public void Alterar(int id, EsferaOrganizacaoModeloNegocio esferaOrganizacao)
        {
            validacao.EsferaOrganizacaoValido(esferaOrganizacao);

            validacao.IdValido(id);
            validacao.IdValido(esferaOrganizacao.Id);

            validacao.IdAlteracaoValido(id, esferaOrganizacao);

            validacao.DescricaoValida(esferaOrganizacao.Descricao);

            validacao.DescricaoExistente(esferaOrganizacao.Descricao);

            EsferaOrganizacao eo = repositorioEsferasOrganizacoes.Where(e => e.Id == esferaOrganizacao.Id).SingleOrDefault();

            validacao.NaoEncontrado(eo);

            eo.Descricao = esferaOrganizacao.Descricao;

            unitOfWork.Save();
        }

        public void Excluir(int id)
        {
            validacao.IdValido(id);

            var esferaOrganizacao = repositorioEsferasOrganizacoes.SingleOrDefault(eo => eo.Id == id);
            validacao.NaoEncontrado(esferaOrganizacao);

            repositorioEsferasOrganizacoes.Remove(esferaOrganizacao);

            unitOfWork.Save();
        }

        public EsferaOrganizacaoModeloNegocio Inserir(EsferaOrganizacaoModeloNegocio esferaOrganizacao)
        {
            validacao.EsferaOrganizacaoValido(esferaOrganizacao);

            validacao.DescricaoValida(esferaOrganizacao.Descricao);

            validacao.DescricaoExistente(esferaOrganizacao.Descricao);

            var eo = Mapper.Map<EsferaOrganizacaoModeloNegocio, EsferaOrganizacao>(esferaOrganizacao);

            repositorioEsferasOrganizacoes.Add(eo);

            unitOfWork.Save();

            return Mapper.Map<EsferaOrganizacao, EsferaOrganizacaoModeloNegocio>(eo);
        }

        public List<EsferaOrganizacaoModeloNegocio> Listar()
        {
            var esferasOrganizacoes = repositorioEsferasOrganizacoes.ToList();

            validacao.NaoEncontrado(esferasOrganizacoes);

            return Mapper.Map<List<EsferaOrganizacao>, List<EsferaOrganizacaoModeloNegocio>>(esferasOrganizacoes);
        }

        public EsferaOrganizacaoModeloNegocio Pesquisar(int id)
        {
            var esferaOrganizacao = repositorioEsferasOrganizacoes.OrderBy(eo => eo.Descricao)
                                                                  .SingleOrDefault(eo => eo.Id == id);

            validacao.NaoEncontrado(esferaOrganizacao);

            return Mapper.Map<EsferaOrganizacao, EsferaOrganizacaoModeloNegocio>(esferaOrganizacao); ;
        }
    }
}
