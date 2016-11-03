using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Infraestrutura.Comum;
using Organograma.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Organograma.Negocio.Validacao
{
    public class CnpjValidacao
    {
        IRepositorioGenerico<Organizacao> repositorioOrganizacoes;

        public CnpjValidacao(IRepositorioGenerico<Organizacao> repositorioOrganizacoes)
        {
            this.repositorioOrganizacoes = repositorioOrganizacoes;
        }

        internal void CnpjExiste(OrganizacaoModeloNegocio organizacaoNegocio)
        {
            Organizacao organizacao = repositorioOrganizacoes.Where(o => o.Cnpj == organizacaoNegocio.Cnpj && o.Id != organizacaoNegocio.Id).SingleOrDefault();

            if (organizacao != null)
            {
                throw new OrganogramaRequisicaoInvalidaException("O Cnpj informado já pertence a uma organização.");
            }

        }

        internal void CnpjValido(string cnpj)
        {

            if (!string.IsNullOrEmpty(cnpj))
            {

                if (cnpj.Length != 14)
                {
                    throw new OrganogramaRequisicaoInvalidaException("O Cnpj deve ser composto por 14 dígitos");
                }
                else
                {

                    try
                    {
                        long.Parse(cnpj);
                    }
                    catch (Exception)
                    {
                        throw new OrganogramaRequisicaoInvalidaException("O Cnpj deve ser composto apenas por números");
                    }

                    int[] digitos = new int[14];
                    int[] verificadores = new int[2];
                    int j, i, soma;
                    string sequencia, soNumero;
                    soNumero = Regex.Replace(cnpj, "[^0-9]", string.Empty);

                    //Verifica se todos os numeros são iguais
                    if (new string(soNumero[0], soNumero.Length) == soNumero)
                    {
                        throw new OrganogramaRequisicaoInvalidaException("O Cnpj informado é inválido");
                    }

                    sequencia = "6543298765432";
                    for (i = 0; i <= 13; i++)
                    {
                        digitos[i] = Convert.ToInt32(soNumero.Substring(i, 1));
                    }

                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 11 + i; j++)
                        {
                            soma += digitos[j] * Convert.ToInt32(sequencia.Substring(j + 1 - i, 1));
                        }

                        verificadores[i] = (soma * 10) % 11;

                        if (verificadores[i] == 10)
                        {
                            verificadores[i] = 0;
                        }
                    }
                    if (verificadores[0] != digitos[12] || verificadores[1] != digitos[13])
                    {
                        throw new OrganogramaRequisicaoInvalidaException("O Cnpj informado é inválido");
                    }

                }

            }
        }

    }
}
