using AutoMapper;
using Organograma.Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Base;
using Organograma.Negocio.Modelos;
using System.Collections.Generic;

namespace Organograma.Apresentacao
{
    public class UnidadeWorkService : IUnidadeWorkService
    {
        private IUnidadeNegocio unidadeNegocio;

        public UnidadeWorkService(IUnidadeNegocio unidadeNegocio)
        {
            this.unidadeNegocio = unidadeNegocio;
        }

        public void Alterar(int id, UnidadeModelo unidade)
        {
            UnidadeModeloNegocio eomn = Mapper.Map<UnidadeModelo, UnidadeModeloNegocio>(unidade);

            unidadeNegocio.Alterar(id, eomn);
        }

        public void Excluir(int id)
        {
            unidadeNegocio.Excluir(id);
        }

        public UnidadeModelo Inserir(UnidadeModeloPost unidade)
        {
            UnidadeModeloNegocio eomn = Mapper.Map<UnidadeModeloPost, UnidadeModeloNegocio>(unidade);

            eomn = unidadeNegocio.Inserir(eomn);

            return Mapper.Map<UnidadeModeloNegocio, UnidadeModelo>(eomn);
        }

        public List<UnidadeModelo> Listar()
        {
            var unidades = unidadeNegocio.Listar();

            return Mapper.Map<List<UnidadeModeloNegocio>, List<UnidadeModelo>>(unidades);
        }

        public UnidadeModelo Pesquisar(int id)
        {
            var unidade = unidadeNegocio.Pesquisar(id);

            return Mapper.Map<UnidadeModeloNegocio, UnidadeModelo>(unidade); ;
        }
    }
}
