using Organograma.Infraestrutura.Comum;
using Organograma.Negocio.Modelos;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Organograma.Negocio.Validacao
{
    //TODO: Implemetar esta classe
    public class EmailValidacao
    {
        private string padraoEmail = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        internal void Preenchido(List<EmailModeloNegocio> emails)
        {
            foreach (var email in emails)
            {
                Preenchido(email);
            }
        }

        internal void Valido(List<EmailModeloNegocio> emails)
        {
            foreach (var email in emails)
            {
                Valido(email);
            }
        }

        internal void Preenchido (EmailModeloNegocio email)
        {
            if (string.IsNullOrEmpty(email.Endereco))
            {
                throw new OrganogramaRequisicaoInvalidaException("Endereço do email não preenchido");
            }
        }

        internal void Valido (EmailModeloNegocio email)
        {
            Regex emailRegex = new Regex(padraoEmail);

            if (!emailRegex.IsMatch(email.Endereco))
            {
                throw new OrganogramaRequisicaoInvalidaException("Email \"" + email.Endereco + "\" inválido.");
            }

        }

    }
}
