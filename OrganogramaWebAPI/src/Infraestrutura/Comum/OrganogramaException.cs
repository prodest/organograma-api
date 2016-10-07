using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Infraestrutura.Comum
{
    public class OrganogramaException : Exception
    {
        public OrganogramaException(string mensagem) : base(mensagem) { }

        public OrganogramaException(string mensagem, Exception ex) : base(mensagem, ex) { }
    }

    public class OrganogramaNaoEncontradoException : OrganogramaException
    {
        public OrganogramaNaoEncontradoException(string mensagem) : base(mensagem) { }

        public OrganogramaNaoEncontradoException(string mensagem, Exception ex) : base(mensagem, ex) { }
    }

    public class OrganogramaRequisicaoInvalidaException : OrganogramaException
    {
        public OrganogramaRequisicaoInvalidaException(string mensagem) : base(mensagem) { }

        public OrganogramaRequisicaoInvalidaException(string mensagem, Exception ex) : base(mensagem, ex) { }
    }


}
