using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Negocio.Comum
{
    public class OrganogramaException : Exception
    {
        public OrganogramaException() : base() { }

        public OrganogramaException(string mensagem) : base(mensagem) { }

        public OrganogramaException(string mensagem, Exception ex) : base(mensagem, ex) { }
    }
}
