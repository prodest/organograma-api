using System;
using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class Endereco
    {
        public Endereco()
        {
            Organizacao = new HashSet<Organizacao>();
        }

        public int Id { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public int Cep { get; set; }
        public int IdMunicipio { get; set; }

        public virtual ICollection<Organizacao> Organizacao { get; set; }
        public virtual Municipio IdMunicipioNavigation { get; set; }
    }
}
