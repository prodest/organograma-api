using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Municipio
    {
        public Municipio()
        {
            Enderecos = new HashSet<Endereco>();
            IdentificadorExterno = new HashSet<IdentificadorExterno>();
        }

        public int Id { get; set; }
        public int CodigoIbge { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime? FimVigencia { get; set; }
        public string ObservacaoFimVigencia { get; set; }

        public virtual ICollection<Endereco> Enderecos { get; set; }
        public virtual ICollection<IdentificadorExterno> IdentificadorExterno { get; set; }
    }
}
