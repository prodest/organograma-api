using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Apresentacao.Modelos
{
    public class EnderecoModelo
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string GuidMunicipio { get; set; }
    }

    public class EnderecoModeloGet
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public MunicipioModeloGet Municipio { get; set; }
    }

    public class EnderecoModeloPut : EnderecoModelo
    {
        public bool? Excluir { get; set; }
    }
}
