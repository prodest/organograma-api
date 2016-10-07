using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Negocio.Comum;
using Organograma.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Negocio.Validacao
{
    public class MunicipioValidacao
    {
        IRepositorioGenerico<Municipio> repositorioMunicipios;

        public MunicipioValidacao(IRepositorioGenerico<Municipio> repositorioMunicipios)
        {
            this.repositorioMunicipios = repositorioMunicipios;
        }

        internal void MunicipioValido (MunicipioModeloNegocio municipio)
        {
            if(municipio.CodigoIbge == 0 || string.IsNullOrEmpty(municipio.Nome) || string.IsNullOrEmpty(municipio.Uf)) {
                throw new MunicipioException("Dados inválidos: Código IBGE, nome e uf devem estar preenchidos.");
            }
        }

        internal void CodigoIbgeExistente (MunicipioModeloNegocio municipio)
        {
            var resultado = this.repositorioMunicipios.Where(q => q.CodigoIbge == municipio.CodigoIbge).SingleOrDefault();

            if(resultado != null)
            {
                throw new MunicipioException("Já existe um município cadastrado com código IBGE informado.");
            }
        }

        internal void NomeUfExistente(MunicipioModeloNegocio municipio)
        {
            var resultado = this.repositorioMunicipios.Where(q => q.Nome == municipio.Nome).Where(q => q.Uf == municipio.Uf).SingleOrDefault();

            if (resultado != null)
            {
                throw new MunicipioException("Já existe um município cadastrado com o Nome e Uf informados");
            }
        }

    }

    public class MunicipioException : OrganogramaException
    {
        public MunicipioException(string mensagem) : base(mensagem) { }

        public MunicipioException(string mensagem, Exception ex) : base(mensagem, ex) { }
    }
}
