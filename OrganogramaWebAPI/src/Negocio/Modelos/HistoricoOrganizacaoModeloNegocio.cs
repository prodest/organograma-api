using System;
using System.Collections.Generic;

namespace Organograma.Negocio.Modelos
{
    public class HistoricoOrganizacaoModeloNegocio
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Sigla { get; set; }
        public int IdEsfera { get; set; }
        public int IdPoder { get; set; }
        public int IdTipoOrganizacao { get; set; }
        public int? IdOrganizacaoPai { get; set; }
        public int? IdAntigo { get; set; }
        public int? IdEmpresaSiarhes { get; set; }
        public int? IdSubEmpresaSiarhes { get; set; }
        public DateTime InicioVigencia { get; set; }

        public List<HistoricoContatoModeloNegocio> Contatos { get; set; }
        public List<HistoricoEmailModeloNegocio> Emails { get; set; }
        public HistoricoIdentificadorExternoModeloNegocio IdentificadorExterno { get; set; }
        public List<HistoricoSiteModeloNegocio> Sites { get; set; }
        public HistoricoEnderecoModeloNegocio Endereco { get; set; }
    }
}