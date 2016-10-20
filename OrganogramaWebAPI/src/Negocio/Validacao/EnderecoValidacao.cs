using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Infraestrutura.Comum;
using Organograma.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Organograma.Negocio.Validacao
{
    public class EnderecoValidacao
    {
        private MunicipioValidacao municipioValidacao;
        IRepositorioGenerico<Endereco> repositorioEnderecos;

        public EnderecoValidacao(IRepositorioGenerico<Endereco> repositorioEnderecos, IRepositorioGenerico<Municipio> repositorioMunicipios)
        {
            this.repositorioEnderecos = repositorioEnderecos;
            municipioValidacao = new MunicipioValidacao(repositorioMunicipios);
        }

        /**
         * <summary>
         * Verificação de preencimento dos campos obrigatórios do endereço.
         * </summary>
         * <param name="endereco">Endereço a ser verificado. Se o endereço for nulo os campos obrigatórios não serão verificados o preenchimento.</param>
         */
        internal void Preenchido(EnderecoModeloNegocio endereco)
        {
            if (endereco != null)
            {
                LogradouroPreenchido(endereco.Logradouro);
                BairroPreenchido(endereco.Numero);
                CepPreenchido(endereco.Cep);

                municipioValidacao.IdPreenchido(endereco.Municipio);
            }
        }

        /**
         * <summary>
         * Validação de campos do endereço.
         * </summary>
         * <param name="endereco">Endereço a ser validado. Se o endereço for nulo os campos não serão validados.</param>
         */
        internal void Valido(EnderecoModeloNegocio endereco)
        {
            if (endereco != null)
            {
                municipioValidacao.IdValido(endereco.Municipio);
                municipioValidacao.Existe(endereco.Municipio);
            }
        }

        private void LogradouroPreenchido(string logradouro)
        {
            if (string.IsNullOrWhiteSpace(logradouro))
                throw new OrganogramaRequisicaoInvalidaException("O campo logradouro não pode ser vazio ou nulo.");
        }

        private void BairroPreenchido(string bairro)
        {
            if (string.IsNullOrWhiteSpace(bairro))
                throw new OrganogramaRequisicaoInvalidaException("O campo bairro não pode ser vazio ou nulo.");
        }

        private void CepPreenchido(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                throw new OrganogramaRequisicaoInvalidaException("O campo cep não pode ser vazio ou nulo.");
        }
    }
}
