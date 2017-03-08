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

        public override void RaiseUsuarioAlterado()
        {
            organizacaoNegocio.Usuario = Usuario;
        }

        #region Alterar
        public void Alterar(int id, OrganizacaoModeloPatch organizacaoPatch)
        {
            organizacaoNegocio.Alterar(id, Mapper.Map<OrganizacaoModeloPatch, OrganizacaoModeloNegocio>(organizacaoPatch));
        }

        #endregion

        #region Excluir
        public void Excluir(string guid)
        {
            organizacaoNegocio.Excluir(guid);
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
        }

        public OrganizacaoOrganograma PesquisarOrganograma(string guid, bool filhas)
        {
            var org = organizacaoNegocio.PesquisarOrganograma(guid, filhas);
            return Mapper.Map<OrganizacaoModeloNegocio, OrganizacaoOrganograma>(org);
        }

        public List<OrganizacaoModeloGet> PesquisarPorUsuario(bool filhas)
        {
            List<OrganizacaoModeloNegocio> organizacoes = organizacaoNegocio.PesquisarPorUsuario(filhas);

            return Mapper.Map<List<OrganizacaoModeloNegocio>, List<OrganizacaoModeloGet>>(organizacoes);
        }
        #endregion
    }
}
