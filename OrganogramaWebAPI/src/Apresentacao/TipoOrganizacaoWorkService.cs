using Organograma.Apresentacao.Base;
using System.Collections.Generic;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Base;
using AutoMapper;
using Organograma.Negocio.Modelos;

namespace Organograma.Apresentacao
{
    public class TipoOrganizacaoWorkService : ITipoOrganizacaoWorkService
    {
        private ITipoOrganizacaoNegocio tipoOrganizacaoNegocio;

        public TipoOrganizacaoWorkService(ITipoOrganizacaoNegocio tipoOrganizacaoNegocio)
        {
            this.tipoOrganizacaoNegocio = tipoOrganizacaoNegocio;
        }

        public void Alterar(int id, TipoOrganizacaoModeloPut tipoOrganizacao)
        {
            TipoOrganizacaoModeloNegocio tomn = Mapper.Map<TipoOrganizacaoModeloPut, TipoOrganizacaoModeloNegocio>(tipoOrganizacao);

            tipoOrganizacaoNegocio.Alterar(id, tomn);
        }

        public void Excluir(int id)
        {
            tipoOrganizacaoNegocio.Excluir(id);
        }

        public TipoOrganizacaoModelo Inserir(TipoOrganizacaoModeloPost tipoOrganizacao)
        {
            TipoOrganizacaoModeloNegocio tomn = Mapper.Map<TipoOrganizacaoModeloPost, TipoOrganizacaoModeloNegocio>(tipoOrganizacao);

            tomn = tipoOrganizacaoNegocio.Inserir(tomn);

            return Mapper.Map<TipoOrganizacaoModeloNegocio, TipoOrganizacaoModelo>(tomn);
        }

        public List<TipoOrganizacaoModelo> Listar()
        {
            var tiposOrganizacoes = tipoOrganizacaoNegocio.Listar();

            return Mapper.Map<List<TipoOrganizacaoModeloNegocio>, List<TipoOrganizacaoModelo>>(tiposOrganizacoes);
        }

        public TipoOrganizacaoModelo Pesquisar(int id)
        {
            var tomn = tipoOrganizacaoNegocio.Pesquisar(id);

            return Mapper.Map<TipoOrganizacaoModeloNegocio, TipoOrganizacaoModelo>(tomn); ;
        }
    }
}
