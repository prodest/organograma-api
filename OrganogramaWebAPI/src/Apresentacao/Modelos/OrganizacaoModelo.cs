using System.Collections.Generic;

namespace Organograma.Apresentacao.Modelos
{
    public class OrganizacaoModeloPost
    {

        public OrganizacaoModeloPost ()
        {
            Endereco = new EnderecoModelo();
            Contatos = new List<ContatoModelo>();
            Emails = new List<EmailModelo>();
            Sites = new List<SiteModelo>();
        }

        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Sigla { get; set; }
        public int IdEsfera { get; set; }
        public int IdPoder { get; set; }
        public int IdTipoOrganizacao { get; set; }
        public int IdOrganizacaoPai { get; set; }
        public EnderecoModelo Endereco { get; set; }
        public List<ContatoModelo> Contatos { get; set; }
        public List<EmailModelo> Emails { get; set; }
        public List<SiteModelo> Sites { get; set; }



    }

    public class OrganizacaoModeloPut : OrganizacaoModeloPost
    {
        public int Id { get; set; }
    }

    public class OrganizacaoModeloGet : OrganizacaoModeloPut { }

}
