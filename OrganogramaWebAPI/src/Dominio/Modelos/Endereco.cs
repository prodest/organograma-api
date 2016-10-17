using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Endereco
    {
        public Endereco()
        {
            Organizacoes = new HashSet<Organizacao>();
            Unidades = new HashSet<Unidade>();
        }

        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public int IdMunicipio { get; set; }

        public virtual ICollection<Organizacao> Organizacoes { get; set; }
        public virtual ICollection<Unidade> Unidades { get; set; }
        public virtual Municipio Municipio { get; set; }
    }
}
