using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Infraestrutura.Comum;
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

        internal void IdValido(int id)
        {
            if (id <= 0)
                throw new OrganogramaRequisicaoInvalidaException("Identificador do município inválido.");
        }

        internal void IdValido(MunicipioModeloNegocio municipio)
        {
            NaoNulo(municipio);

            if (municipio.Id <= 0)
                throw new OrganogramaRequisicaoInvalidaException("Identificador do município inválido.");
        }

        internal void IdPreenchido(MunicipioModeloNegocio municipio)
        {
            NaoNulo(municipio);

            if (municipio.Id == default(int))
                throw new OrganogramaRequisicaoInvalidaException("O id do munícipio deve ser preenchido.");
        }

        internal void MunicipioValido (MunicipioModeloNegocio municipio)
        {
            if(municipio.CodigoIbge == default(int) || string.IsNullOrEmpty(municipio.Nome) || string.IsNullOrEmpty(municipio.Uf)) {
                throw new OrganogramaRequisicaoInvalidaException("Dados inválidos: Código IBGE, nome e uf devem estar preenchidos.");
            }
        }

        internal void CodigoIbgeExistente (MunicipioModeloNegocio municipio)
        {
            //O id do registro a ser alterado deve ser desconsiderado (na inserção, o id é 0).
            //Em razão de erro de integridade do banco de dados (codigo IBGE deve ser único), o método ToList() está sendo utilizado no lugar de SingleOrDefault();
            var resultado = repositorioMunicipios.Where(q => q.CodigoIbge == municipio.CodigoIbge).Where(q => q.Id != municipio.Id).ToList();
                       
            if (resultado.Count != 0)
            {
                throw new OrganogramaRequisicaoInvalidaException("Já existe um município cadastrado com código IBGE informado.");
            }
        }

        internal void MunicipioNaoExistente(Municipio municipioDominio)
        {
            if (municipioDominio == null) {
                throw new OrganogramaNaoEncontradoException("Muicípio não encontrado.");
            }

        }

        internal void NomeUfExistente(MunicipioModeloNegocio municipio, int idDesconsiderado = 0)
        {
            //O id do registro a ser alterado deve ser desconsiderado (na inserção, o id é 0).
            var resultado = repositorioMunicipios.Where(q => q.Nome.ToUpper() == municipio.Nome.ToUpper()).Where(q => q.Uf.ToUpper() == municipio.Uf.ToUpper()).Where(q => q.Id != municipio.Id).SingleOrDefault();

            if (resultado != null)
            {
                throw new OrganogramaRequisicaoInvalidaException("Já existe um município cadastrado com o Nome e Uf informados");
            }
        }

        internal void PreenchimentoCompleto(MunicipioModeloNegocio municipioNegocio)
        {

            if(municipioNegocio.CodigoIbge == 0 || string.IsNullOrEmpty(municipioNegocio.Nome) || string.IsNullOrEmpty(municipioNegocio.Uf)) {
                throw new OrganogramaRequisicaoInvalidaException("Todos os campos do município devem ser preenchidos");
            }

         }

        internal void MunicipioNaoExistente(List<Municipio> municipios)
        {
            if (municipios.Count <= 0 || municipios == null)
            {
                throw new OrganogramaRequisicaoInvalidaException("Não há municípios cadastrados.");
            }
        }

        internal void IdAlteracaoValido(int id, MunicipioModeloNegocio municipioNegocio)
        {
            if (id != municipioNegocio.Id)
                throw new Exception("Identificadores do municipio não podem ser diferentes.");
        }

        internal void NaoNulo(MunicipioModeloNegocio municipio)
        {
            if (municipio == null)
                throw new OrganogramaRequisicaoInvalidaException("Município não pode ser nulo.");
        }

        internal void Existe(MunicipioModeloNegocio municipio)
        {
            var mun = repositorioMunicipios.Where(m => m.Id == municipio.Id)
                                           .SingleOrDefault();

            if(mun == null)
                throw new OrganogramaNaoEncontradoException("Município não encontrado.");
        }
    }

}
