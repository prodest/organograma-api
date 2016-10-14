
namespace Organograma.Negocio.Modelos
{
    public class OrganizacaoModeloNegocio
    {

        public OrganizacaoModeloNegocio()
        {
            Endereco = new EnderecoModeloNegocio();
            Email = new EmailModeloNegocio();
            Site = new SiteModeloNegocio();
        }

        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Sigla { get; set; }
        public int IdEsfera { get; set; }
        public int IdPoder { get; set; }
        public int IdTipoOrganizacao { get; set; }
        public int? IdOrganizacaoPai { get; set; }
        public EnderecoModeloNegocio Endereco { get; set; }
        public EmailModeloNegocio Email { get; set; }
        public SiteModeloNegocio Site { get; set; }
    }
}
