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

        public void Alterar(int id, OrganizacaoModeloPatch organizacaoPatch)
        {
            organizacaoNegocio.Alterar(id, Mapper.Map<OrganizacaoModeloPatch, OrganizacaoModeloNegocio>(organizacaoPatch));
        }

        public void Excluir(int id)
        {
            organizacaoNegocio.Excluir(id);
        }

        public OrganizacaoModeloPut Inserir(OrganizacaoModeloPost organizacaoPost)
        {
            OrganizacaoModeloNegocio organizacaoModeloNegocio = new OrganizacaoModeloNegocio();
            Mapper.Map(organizacaoPost, organizacaoModeloNegocio);
                        
            return Mapper.Map<OrganizacaoModeloNegocio, OrganizacaoModeloPut>(organizacaoNegocio.Inserir(organizacaoModeloNegocio));
            
        }

        public List<OrganizacaoModeloGet> Listar()
        {
            throw new NotImplementedException();
        }

        public OrganizacaoModeloGet Pesquisar(int id)
        {
            return Mapper.Map<OrganizacaoModeloNegocio, OrganizacaoModeloGet>(organizacaoNegocio.Pesquisar(id));
        }
    }
}
