using System;
using System.Collections.Generic;

namespace Organograma.Negocio.Modelos
{
    public class HistoricoUnidadeModeloNegocio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int IdOrganizacao { get; set; }
        public int IdTipoUnidade { get; set; }
        public int? IdUnidadePai { get; set; }
        public int? IdAntigo { get; set; }
        public DateTime InicioVigencia { get; set; }

        public List<HistoricoContatoModeloNegocio> Contatos { get; set; }
        public List<HistoricoEmailModeloNegocio> Emails { get; set; }
        public HistoricoIdentificadorExternoModeloNegocio IdentificadorExterno { get; set; }
        public List<HistoricoSiteModeloNegocio> Sites { get; set; }
        public HistoricoEnderecoModeloNegocio Endereco { get; set; }
    }
}
