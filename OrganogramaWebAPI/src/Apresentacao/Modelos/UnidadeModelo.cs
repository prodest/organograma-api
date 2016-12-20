using System;
using System.Collections.Generic;

namespace Organograma.Apresentacao.Modelos
{
    public partial class UnidadeModeloRetornoPost : UnidadeModeloPost
    {
        public int Id { get; set; }
    }

    public partial class UnidadeModeloPost
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int IdOrganizacao { get; set; }
        public int IdTipoUnidade { get; set; }
        public int? IdUnidadePai { get; set; }

        public EnderecoModelo Endereco { get; set; }
        public List<ContatoModelo> Contatos { get; set; }
        public List<EmailModelo> Emails { get; set; }
        public List<SiteModelo> Sites { get; set; }
    }

    public class UnidadeModeloGet
    {
        public string Guid { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }

        public TipoUnidadeModeloPut TipoUnidade { get; set; }
        public OrganizacaoUnidadeModeloGet Organizacao { get; set; }
        public UnidadePaiModeloGet UnidadePai { get; set; }
        public EnderecoModeloGet Endereco { get; set; }
        public List<ContatoModeloGet> Contatos { get; set; }
        public List<EmailModelo> Emails { get; set; }
        public List<SiteModelo> Sites { get; set; }
    }

    public class UnidadePaiModeloGet
    {
        public string Guid { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
    }

    public class UnidadeModeloPatch
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int? IdTipoUnidade { get; set; }
        public int? IdUnidadePai { get; set; }
    }

}
