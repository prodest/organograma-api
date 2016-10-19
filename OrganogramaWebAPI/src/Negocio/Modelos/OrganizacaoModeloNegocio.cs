
using System.Collections.Generic;

namespace Organograma.Negocio.Modelos
{
    public class OrganizacaoModeloNegocio
    {

        
        public OrganizacaoModeloNegocio()
        {
            Endereco = new EnderecoModeloNegocio();
            Contatos = new List<ContatoModeloNegocio>();
            Emails = new List<EmailModeloNegocio>();
            Sites = new List<SiteModeloNegocio>();
            Esfera = new EsferaOrganizacaoModeloNegocio();
            Poder = new PoderModeloNegocio();
            TipoOrganizacao = new TipoOrganizacaoModeloNegocio();
            //Organizacao Pai não é instanciado no construtor propositalmente para evitar estouro de memória
        }
        
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Sigla { get; set; }
        public EsferaOrganizacaoModeloNegocio Esfera { get; set; }
        public PoderModeloNegocio Poder { get; set; }
        public TipoOrganizacaoModeloNegocio TipoOrganizacao { get; set; }
        public OrganizacaoModeloNegocio OrganizacaoPai { get; set; }
        public EnderecoModeloNegocio Endereco { get; set; }
        public List<ContatoModeloNegocio> Contatos { get; set; }
        public List<EmailModeloNegocio> Emails { get; set; }
        public List<SiteModeloNegocio> Sites { get; set; }
    }
}
