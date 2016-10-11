using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Apresentacao.Modelos
{
    public class PoderModeloPost
    {
        public string Descricao { get; set; }
    }

    public class PoderModeloPut : PoderModeloPost
    {
        public int Id { get; set; }
    }

    public class PoderModeloGet : PoderModeloPut { }

}
