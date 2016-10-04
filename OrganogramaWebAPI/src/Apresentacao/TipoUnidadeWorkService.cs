using AutoMapper;
using Organograma.Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Base;
using Organograma.Negocio.Modelos;
using System.Collections.Generic;

namespace Organograma.Apresentacao
{
    public class TipoUnidadeWorkService : ITipoUnidadeWorkService
    {
        private ITipoUnidadeNegocio tipoUnidadeNegocio;

        public TipoUnidadeWorkService(ITipoUnidadeNegocio tipoUnidadeNegocio)
        {
            this.tipoUnidadeNegocio = tipoUnidadeNegocio;
        }

        public void Alterar(int id, TipoUnidadeModeloPut tipoUnidade)
        {
            TipoUnidadeModeloNegocio tomn = Mapper.Map<TipoUnidadeModeloPut, TipoUnidadeModeloNegocio>(tipoUnidade);

            tipoUnidadeNegocio.Alterar(id, tomn);
        }

        public void Excluir(int id)
        {
            tipoUnidadeNegocio.Excluir(id);
        }

        public TipoUnidadeModelo Incluir(TipoUnidadeModeloPost tipoUnidade)
        {
            TipoUnidadeModeloNegocio tUnidade = Mapper.Map<TipoUnidadeModeloPost, TipoUnidadeModeloNegocio>(tipoUnidade);

            tUnidade = tipoUnidadeNegocio.Incluir(tUnidade);

            return Mapper.Map<TipoUnidadeModeloNegocio, TipoUnidadeModelo>(tUnidade);
        }

        public List<TipoUnidadeModelo> Listar()
        {
            var tiposUnidades = tipoUnidadeNegocio.Listar();

            return Mapper.Map<List<TipoUnidadeModeloNegocio>, List<TipoUnidadeModelo>>(tiposUnidades);
        }

        public TipoUnidadeModelo Pesquisar(int id)
        {
            var tipoUnidade = tipoUnidadeNegocio.Pesquisar(id);

            return Mapper.Map<TipoUnidadeModeloNegocio, TipoUnidadeModelo>(tipoUnidade); ;
        }
    }
}
