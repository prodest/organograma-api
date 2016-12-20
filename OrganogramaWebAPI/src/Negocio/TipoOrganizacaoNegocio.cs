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
    public class TipoOrganizacaoNegocio : ITipoOrganizacaoNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<TipoOrganizacao> repositorioTiposOrganizacoes;
        private TipoOrganizacaoValidacao validacao;

        public TipoOrganizacaoNegocio(IOrganogramaRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioTiposOrganizacoes = repositorios.TiposOrganizacoes;
            validacao = new TipoOrganizacaoValidacao(repositorioTiposOrganizacoes);
        }

        public void Alterar(int id, TipoOrganizacaoModeloNegocio tipoOrganizacao)
        {
            validacao.TipoOrganizacaoValido(tipoOrganizacao);

            validacao.IdValido(id);
            validacao.IdValido(tipoOrganizacao.Id);

            validacao.IdAlteracaoValido(id, tipoOrganizacao);

            validacao.IdExistente(id);

            validacao.DescricaoValida(tipoOrganizacao.Descricao);

            validacao.DescricaoExistente(tipoOrganizacao.Descricao);

            TipoOrganizacao td = repositorioTiposOrganizacoes.Where(t => t.Id == tipoOrganizacao.Id).Single();

            td.Descricao = tipoOrganizacao.Descricao;

            unitOfWork.Save();
        }

        public void Excluir(int id)
        {
            validacao.IdExistente(id);

            var tipoDocumental = repositorioTiposOrganizacoes.Single(td => td.Id == id);

            repositorioTiposOrganizacoes.Remove(tipoDocumental);

            unitOfWork.Save();
        }

        public TipoOrganizacaoModeloNegocio Inserir(TipoOrganizacaoModeloNegocio tipoOrganizacao)
        {
            validacao.TipoOrganizacaoValido(tipoOrganizacao);

            validacao.DescricaoValida(tipoOrganizacao.Descricao);

            validacao.DescricaoExistente(tipoOrganizacao.Descricao);

            TipoOrganizacao td = new TipoOrganizacao();

            td.Descricao = tipoOrganizacao.Descricao;
            td.InicioVigencia = DateTime.Now;

            repositorioTiposOrganizacoes.Add(td);

            unitOfWork.Save();

            return Mapper.Map<TipoOrganizacao, TipoOrganizacaoModeloNegocio>(td);
        }

        public List<TipoOrganizacaoModeloNegocio> Listar()
        {
            var tiposOrganizacoes = repositorioTiposOrganizacoes.OrderBy(to => to.Descricao).ToList();

            return Mapper.Map<List<TipoOrganizacao>, List<TipoOrganizacaoModeloNegocio>>(tiposOrganizacoes);
        }

        public TipoOrganizacaoModeloNegocio Pesquisar(int id)
        {
            var tipoOrganizacao = repositorioTiposOrganizacoes.SingleOrDefault(td => td.Id == id);

            validacao.NaoEncontrado(tipoOrganizacao);

            return Mapper.Map<TipoOrganizacao, TipoOrganizacaoModeloNegocio>(tipoOrganizacao); ;
        }
    }
}
