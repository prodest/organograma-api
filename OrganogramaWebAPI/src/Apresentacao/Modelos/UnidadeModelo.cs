using System;
using System.Collections.Generic;

namespace Organograma.Apresentacao.Modelos
{
    public partial class UnidadeModelo : UnidadeModeloPost
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

        public virtual EnderecoModelo Endereco { get; set; }
        public virtual List<ContatoModelo> Contatos { get; set; }
        public virtual List<EmailModelo> Emails { get; set; }
        public virtual List<SiteModelo> Sites { get; set; }
        
    }
}
