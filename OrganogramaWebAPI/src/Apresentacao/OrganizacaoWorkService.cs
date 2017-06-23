using Apresentacao.Base;
using AutoMapper;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Base;
using Organograma.Negocio.Modelos;
using System.Collections.Generic;

namespace Organograma.Apresentacao
{
    public class OrganizacaoWorkService : BaseWorkService, IOrganizacaoWorkService
    {
        private IOrganizacaoNegocio organizacaoNegocio;

        public OrganizacaoWorkService(IOrganizacaoNegocio organizacaoNegocio)
        {
            this.organizacaoNegocio = organizacaoNegocio;
        }

        #region Alterar
        public void Alterar(string guid, OrganizacaoModeloPatch organizacaoPatch)
        {
            organizacaoNegocio.Alterar(guid, Mapper.Map<OrganizacaoModeloPatch, OrganizacaoModeloNegocio>(organizacaoPatch));
        }

        #endregion

        #region Excluir
        public void Excluir(string guid)
        {
            organizacaoNegocio.Excluir(guid);
        }
        #endregion

        #region Inserir
        public OrganizacaoModeloPut InserirFilha(OrganizacaoFilhaModeloPost organizacaoPost)
        {
            OrganizacaoModeloNegocio organizacaoModeloNegocio = new OrganizacaoModeloNegocio();
            Mapper.Map(organizacaoPost, organizacaoModeloNegocio);

            return Mapper.Map<OrganizacaoModeloNegocio, OrganizacaoModeloPut>(organizacaoNegocio.InserirFilha(organizacaoModeloNegocio));

        }

        public OrganizacaoModeloPut InserirPatriarca(OrganizacaoModeloPost organizacaoPost)
        {
            OrganizacaoModeloNegocio organizacaoModeloNegocio = new OrganizacaoModeloNegocio();
            Mapper.Map(organizacaoPost, organizacaoModeloNegocio);

            return Mapper.Map<OrganizacaoModeloNegocio, OrganizacaoModeloPut>(organizacaoNegocio.InserirPatriarca(organizacaoModeloNegocio));

        }

        public SiteModeloPatch InserirSite(int idOrganizacao, SiteModelo sitePost)
        {
            SiteModeloNegocio siteModeloNegocio = new SiteModeloNegocio();
            Mapper.Map(sitePost, siteModeloNegocio);

            SiteModeloPatch site = Mapper.Map<SiteModeloNegocio, SiteModeloPatch>(organizacaoNegocio.InserirSite(siteModeloNegocio));

            return site;


        }

        #endregion

        #region Listar
        public List<OrganizacaoModeloGet> Listar(string esfera, string poder, string uf, int codIbgeMunicipio)
        {
            return Mapper.Map<List<OrganizacaoModeloNegocio>, List<OrganizacaoModeloGet>>(organizacaoNegocio.Listar(esfera, poder, uf, codIbgeMunicipio));
        }

        #endregion

        #region Pesquisar
        public OrganizacaoModeloGet Pesquisar(string guid)
        {
            return Mapper.Map<OrganizacaoModeloNegocio, OrganizacaoModeloGet>(organizacaoNegocio.Pesquisar(guid));
        }

        public OrganizacaoModeloGetPorSigla PesquisarPorSigla(string sigla, bool patriarca)
        {
            var organizacao = Mapper.Map<OrganizacaoModeloNegocio, OrganizacaoModeloGetPorSigla>(organizacaoNegocio.PesquisarPorSigla(sigla));

            if (patriarca)
            {
                var organizacaoPatriarca = organizacaoNegocio.PesquisarPatriarca(organizacao.Guid);

                organizacao.GuidPatriarca = organizacaoPatriarca.Guid;
            }

            return organizacao;
        }

        public OrganizacaoModeloGet PesquisarPatriarca(string guid)
        {
            return Mapper.Map<OrganizacaoModeloNegocio, OrganizacaoModeloGet>(organizacaoNegocio.PesquisarPatriarca(guid));
        }

        public List<OrganizacaoModeloGet> PesquisarFilhas(string guid)
        {
            return Mapper.Map<List<OrganizacaoModeloNegocio>, List<OrganizacaoModeloGet>>(organizacaoNegocio.PesquisarFilhas(guid));
        }

        public OrganizacaoOrganograma PesquisarOrganograma(string guid, bool filhas)
        {
            var org = organizacaoNegocio.PesquisarOrganograma(guid, filhas);
            return Mapper.Map<OrganizacaoModeloNegocio, OrganizacaoOrganograma>(org);
        }

        public List<OrganizacaoOrganograma> PesquisarOrganograma()
        {
            var org = organizacaoNegocio.PesquisarOrganograma();
            return Mapper.Map<List<OrganizacaoModeloNegocio>, List<OrganizacaoOrganograma>>(org);
        }

        public List<OrganizacaoModeloGet> PesquisarPorUsuario(bool filhas)
        {
            List<OrganizacaoModeloNegocio> organizacoes = organizacaoNegocio.PesquisarPorUsuario(filhas);

            return Mapper.Map<List<OrganizacaoModeloNegocio>, List<OrganizacaoModeloGet>>(organizacoes);
        }
        #endregion

        #region Integração com o SIARHES
        public void IntegarSiarhes()
        {
            organizacaoNegocio.IntegrarSiarhes();
        }
        #endregion
    }
}
