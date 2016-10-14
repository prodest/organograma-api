using System;
using System.Collections.Generic;
using Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Base;

namespace Organograma.Apresentacao
{
    public class OrganizacaoWorkService : IOrganizacaoWorkService
    {
        private IOrganizacaoNegocio organizacaoNegocio;

        public OrganizacaoWorkService(IOrganizacaoNegocio organizacaoNegocio)
        {
            this.organizacaoNegocio = organizacaoNegocio;
        }

        public void Alterar(int id, OrganizacaoModeloPut organizacaoPut)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public OrganizacaoModeloGet Inserir(OrganizacaoModeloPost organizacaoPost)
        {
            throw new NotImplementedException();
        }

        public List<OrganizacaoModeloGet> Listar()
        {
            throw new NotImplementedException();
        }

        public OrganizacaoModeloGet Pesquisar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
