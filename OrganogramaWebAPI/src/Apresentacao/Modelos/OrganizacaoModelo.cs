using System;
using System.Collections.Generic;

namespace Organograma.Apresentacao.Modelos
{
    
    public class OrganizacaoModeloPost
    {
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Sigla { get; set; }
        public int IdEsfera { get; set; }
        public int IdPoder { get; set; }
        public int IdTipoOrganizacao { get; set; }
        public string GuidOrganizacaoPai { get; set; }
        public EnderecoModelo Endereco { get; set; }
        public List<ContatoModelo> Contatos { get; set; }
        public List<EmailModelo> Emails { get; set; }
        public List<SiteModelo> Sites { get; set; }

    }

    public class OrganizacaoModeloPut : OrganizacaoModeloPost
    {
        public int Id { get; set; }
    }

    public class OrganizacaoModeloGet
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Sigla { get; set; }
        public List<ContatoModeloGet> Contatos { get; set; }
        public List<EmailModelo> Emails { get; set; }
        public EnderecoModeloGet Endereco { get; set; }
        public EsferaOrganizacaoModeloPost Esfera { get; set; }
        public PoderModeloPost Poder { get; set; }
        public OrganizacaoPaiModeloGet OrganizacaoPai { get; set; }
        public List<SiteModelo> Sites { get; set; }
        public TipoOrganizacaoModeloPost TipoOrganizacao { get; set; }
    }

    public class OrganizacaoModeloPatch
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Sigla { get; set; }
        public int? IdEsfera { get; set; }
        public int? IdPoder { get; set; }
        public int? IdTipoOrganizacao { get; set; }
        public int? IdOrganizacaoPai { get; set; }
    }

    public class OrganizacaoPaiModeloGet
    {
        public string Guid { get; set; }
        public string RazaoSocial { get; set; }
        public string Sigla { get; set; }
    }

    public class OrganizacaoUnidadeModeloGet
    {
        public string Guid { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Sigla { get; set; }
    }
}
