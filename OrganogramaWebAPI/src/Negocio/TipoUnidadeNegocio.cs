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
    public class TipoUnidadeNegocio : ITipoUnidadeNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<TipoUnidade> repositorioTiposUnidades;
        private TipoUnidadeValidacao validacao;

        public TipoUnidadeNegocio(IOrganogramaRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioTiposUnidades = repositorios.TiposUnidades;
            validacao = new TipoUnidadeValidacao(repositorioTiposUnidades);
        }

        public void Alterar(int id, TipoUnidadeModeloNegocio tipoUnidade)
        {
            validacao.TipoUnidadeValido(tipoUnidade);

            validacao.IdValido(id);
            validacao.IdValido(tipoUnidade.Id);

            validacao.IdAlteracaoValido(id, tipoUnidade);

            validacao.IdExistente(id);

            validacao.DescricaoValida(tipoUnidade.Descricao);

            validacao.DescricaoExistente(tipoUnidade.Descricao);

            TipoUnidade td = repositorioTiposUnidades.Where(t => t.Id == tipoUnidade.Id).Single();

            td.Descricao = tipoUnidade.Descricao;

            unitOfWork.Save();
        }

        public void Excluir(int id)
        {
            validacao.IdExistente(id);

            var tipoUnidade = repositorioTiposUnidades.Single(td => td.Id == id);

            repositorioTiposUnidades.Remove(tipoUnidade);

            unitOfWork.Save();
        }

        public TipoUnidadeModeloNegocio Inserir(TipoUnidadeModeloNegocio tipoUnidade)
        {
            validacao.TipoUnidadeValido(tipoUnidade);

            validacao.DescricaoValida(tipoUnidade.Descricao);

            validacao.DescricaoExistente(tipoUnidade.Descricao);

            TipoUnidade td = new TipoUnidade();

            td.Descricao = tipoUnidade.Descricao;
            td.InicioVigencia = DateTime.Now;

            repositorioTiposUnidades.Add(td);

            unitOfWork.Save();

            return Mapper.Map<TipoUnidade, TipoUnidadeModeloNegocio>(td);
        }

        public List<TipoUnidadeModeloNegocio> Listar()
        {
            var tiposUnidades = repositorioTiposUnidades.OrderBy(tu => tu.Descricao).ToList();

            return Mapper.Map<List<TipoUnidade>, List<TipoUnidadeModeloNegocio>>(tiposUnidades);
        }

        public TipoUnidadeModeloNegocio Pesquisar(int id)
        {
            var tipoUnidade = repositorioTiposUnidades.SingleOrDefault(td => td.Id == id);

            validacao.NaoEncontrado(tipoUnidade);

            return Mapper.Map<TipoUnidade, TipoUnidadeModeloNegocio>(tipoUnidade); ;
        }
    }
}
