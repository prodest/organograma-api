using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Organograma.WebAPI.Config
{
    public class MensagemErro
    {
        public static string ObterMensagem(Exception e)
        {
            return ObterTexto(e);
        }

        private static string ObterTexto(Exception e)
        {
            StringBuilder retorno = new StringBuilder();

            //if (mensagem != null)
            //    retorno.AppendLine(mensagem);

            if (e != null)
            {
                if (e.InnerException != null)
                {
                    retorno.AppendLine(ObterTexto(e.InnerException));
                    retorno.AppendLine();
                    retorno.AppendLine();
                }

                retorno.AppendLine("---Ínicio da Exceção---");
                retorno.AppendLine(e.Message);
                retorno.AppendLine("---Fim da Exceção---");
            }

            return retorno.ToString();
        }
    }
}
