using System.Collections.Generic;

namespace Organograma.Negocio.Modelos
{
    public class EnderecoModeloNegocio
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public bool? Excluir { get; set; }

        public virtual List<OrganizacaoModeloNegocio> Organizacoes { get; set; }
        public virtual List<UnidadeModeloNegocio> Unidades { get; set; }
        public virtual MunicipioModeloNegocio Municipio { get; set; }
    }
}
