using System;
using System.Collections.Generic;

namespace Organograma.Apresentacao.Modelos
{
    public partial class UnidadeModeloRetornoPost : UnidadeModeloPost
    {
        public string Guid { get; set; }
    }

    public partial class UnidadeModeloPost
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string GuidOrganizacao { get; set; }
        public int IdTipoUnidade { get; set; }
        public string GuidUnidadePai { get; set; }

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
        public string NomeCurto { get; set; }

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
        public string NomeCurto { get; set; }
    }

    public class UnidadeModeloPatch
    {
        public string Guid { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int? IdTipoUnidade { get; set; }
        public int? IdUnidadePai { get; set; }
    }

    public class UnidadeOrganograma
    {
        public string Guid { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string NomeCurto { get; set; }
        public List<UnidadeOrganograma> UnidadesFilhas { get; set; }
    }

    public class UnidadeOrganogramaAcessoCidadao
    {
        public string Guid { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string NomeCurto { get; set; }
        public List<UnidadeOrganogramaAcessoCidadao> UnidadesFilhas { get; set; }
    }

    public class UnidadeSimplesModeloGet
    {
        public string Guid { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string NomeCurto { get; set; }

        public TipoUnidadeModeloPut TipoUnidade { get; set; }
        public UnidadePaiModeloGet UnidadePai { get; set; }
    }

    public class ResponsavelUnidadeModeloGet
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
    }
}