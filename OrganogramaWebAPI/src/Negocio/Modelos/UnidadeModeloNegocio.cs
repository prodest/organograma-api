using System;
using System.Collections.Generic;

namespace Organograma.Negocio.Modelos
{
    public partial class UnidadeModeloNegocio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }

        public TipoUnidadeModeloNegocio TipoUnidade { get; set; }
        public OrganizacaoModeloNegocio Organizacao { get; set; }
        public UnidadeModeloNegocio UnidadePai { get; set; }
        public EnderecoModeloNegocio Endereco { get; set; }
        public List<ContatoModeloNegocio> Contatos { get; set; }
        public List<EmailModeloNegocio> Emails { get; set; }
        public List<SiteModeloNegocio> Sites { get; set; }
        public List<UnidadeModeloNegocio> UnidadesFilhas { get; set; }
    }
}
