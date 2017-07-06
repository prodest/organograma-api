using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Municipio
    {
        public Municipio()
        {
            Enderecos = new HashSet<Endereco>();
            HistoricosMunicipio = new HashSet<HistoricoMunicipio>();
        }

        public int Id { get; set; }
        public int CodigoIbge { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }

        public virtual ICollection<Endereco> Enderecos { get; set; }
        public virtual ICollection<HistoricoMunicipio> HistoricosMunicipio { get; set; }
        public virtual IdentificadorExterno IdentificadorExterno { get; set; }
    }
}
