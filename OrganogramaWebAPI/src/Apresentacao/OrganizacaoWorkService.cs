using System;
using System.Collections.Generic;
using Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Base;
using AutoMapper;
using Organograma.Negocio.Modelos;

namespace Organograma.Apresentacao
{
    public class OrganizacaoWorkService : IOrganizacaoWorkService
    {
        private IOrganizacaoNegocio organizacaoNegocio;

        public OrganizacaoWorkService(IOrganizacaoNegocio organizacaoNegocio)
        {
            this.organizacaoNegocio = organizacaoNegocio;
        }

        #region Alterar
        public void Alterar(int id, OrganizacaoModeloPatch organizacaoPatch)
        {
            organizacaoNegocio.Alterar(id, Mapper.Map<OrganizacaoModeloPatch, OrganizacaoModeloNegocio>(organizacaoPatch));
        }

        #endregion

        #region Excluir
        public void Excluir(int id)
        {
            organizacaoNegocio.Excluir(id);
        }
        #endregion

        #region Inserir
        public OrganizacaoModeloPut Inserir(OrganizacaoModeloPost organizacaoPost)
        {
            OrganizacaoModeloNegocio organizacaoModeloNegocio = new OrganizacaoModeloNegocio();
            Mapper.Map(organizacaoPost, organizacaoModeloNegocio);

            return Mapper.Map<OrganizacaoModeloNegocio, OrganizacaoModeloPut>(organizacaoNegocio.Inserir(organizacaoModeloNegocio));

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
        public OrganizacaoModeloGet PesquisarPorSigla(string sigla)
        {
            return Mapper.Map<OrganizacaoModeloNegocio, OrganizacaoModeloGet>(organizacaoNegocio.PesquisarPorSigla(sigla));
        }

        public OrganizacaoModeloGet PesquisarPatriarca(string guid)
        {
            return Mapper.Map<OrganizacaoModeloNegocio, OrganizacaoModeloGet>(organizacaoNegocio.PesquisarPatriarca(guid));
        }

        public List<OrganizacaoModeloGet> PesquisarFilhas(string guid)
        {
            return Mapper.Map<List<OrganizacaoModeloNegocio>, List<OrganizacaoModeloGet>>(organizacaoNegocio.PesquisarFilhas(guid));

            #endregion
        }
    }
}
