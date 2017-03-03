
using System;
using System.Collections.Generic;

namespace Organograma.Negocio.Modelos
{
    public class OrganizacaoModeloNegocio
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Sigla { get; set; }
        public string Guid { get; set; }
        public EsferaOrganizacaoModeloNegocio Esfera { get; set; }
        public PoderModeloNegocio Poder { get; set; }
        public TipoOrganizacaoModeloNegocio TipoOrganizacao { get; set; }
        public OrganizacaoModeloNegocio OrganizacaoPai { get; set; }
        public EnderecoModeloNegocio Endereco { get; set; }
        public List<OrganizacaoModeloNegocio> OrganizacoesFilhas { get; set; }
        public List<UnidadeModeloNegocio> Unidades { get; set; }
        public List<ContatoModeloNegocio> Contatos { get; set; }
        public List<EmailModeloNegocio> Emails { get; set; }
        public List<SiteModeloNegocio> Sites { get; set; }
    }
}
