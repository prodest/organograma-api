using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class IdentificadorExterno
    {
        public IdentificadorExterno()
        {
            HistoricosMunicipio = new HashSet<HistoricoMunicipio>();
        }

        public int Id { get; set; }
        public Guid Guid { get; set; }
        public int? IdOrganizacao { get; set; }
        public int? IdUnidade { get; set; }
        public int? IdMunicipio { get; set; }

        public virtual ICollection<HistoricoMunicipio> HistoricosMunicipio { get; set; }
        public virtual Municipio Municipio { get; set; }
        public virtual Organizacao Organizacao { get; set; }
        public virtual Unidade Unidade { get; set; }
    }
}
