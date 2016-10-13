using System.Collections.Generic;

namespace Organograma.Dominio.Modelos
{
    public partial class EsferaOrganizacao
    {
        public EsferaOrganizacao()
        {
            Organizacao = new HashSet<Organizacao>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Organizacao> Organizacao { get; set; }
    }
}
