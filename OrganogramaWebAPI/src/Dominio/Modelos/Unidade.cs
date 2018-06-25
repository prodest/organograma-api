using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Unidade
    {
        public Unidade()
        {
            ContatosUnidade = new HashSet<ContatoUnidade>();
            EmailsUnidade = new HashSet<EmailUnidade>();
            SitesUnidade = new HashSet<SiteUnidade>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeCurto { get; set; }
        public string Sigla { get; set; }
        public int IdOrganizacao { get; set; }
        public int IdTipoUnidade { get; set; }
        public int? IdEndereco { get; set; }
        public int? IdUnidadePai { get; set; }
        public int? IdAntigo { get; set; }
        public DateTime InicioVigencia { get; set; }

        public virtual ICollection<ContatoUnidade> ContatosUnidade { get; set; }
        public virtual ICollection<EmailUnidade> EmailsUnidade { get; set; }
        public virtual IdentificadorExterno IdentificadorExterno { get; set; }
        public virtual ICollection<SiteUnidade> SitesUnidade { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual Organizacao Organizacao { get; set; }
        public virtual TipoUnidade TipoUnidade { get; set; }
        public virtual Unidade UnidadePai { get; set; }
        public virtual ICollection<Unidade> UnidadesFilhas { get; set; }
    }
}
