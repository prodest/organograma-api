﻿using Organograma.Negocio.Modelos;
using System.Collections.Generic;

namespace Organograma.Negocio.Base
{
    public interface IOrganizacaoNegocio : IBaseNegocio
    {
        List<OrganizacaoModeloNegocio> Listar(string esfera, string poder, string uf, int codIbgeMunicipio);
        OrganizacaoModeloNegocio Pesquisar(string guid);
        OrganizacaoModeloNegocio Inserir(OrganizacaoModeloNegocio OrganizacaoNegocio);

        void Alterar(int id, OrganizacaoModeloNegocio poderNegocio);

        void Excluir (int id);
        SiteModeloNegocio InserirSite(SiteModeloNegocio siteModeloNegocio);
        OrganizacaoModeloNegocio PesquisarPatriarca(string guid);
        List<OrganizacaoModeloNegocio> PesquisarFilhas(string guid);
        OrganizacaoModeloNegocio PesquisarPorSigla(string sigla);
        OrganizacaoModeloNegocio PesquisarOrganograma(string guid, bool filhas);
        List<OrganizacaoModeloNegocio> PesquisarPorUsuario(bool filhas);
    }
}
