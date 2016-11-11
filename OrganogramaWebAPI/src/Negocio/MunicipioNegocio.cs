using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Base;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System;
using Organograma.Negocio.Validacao;

namespace Organograma.Negocio
{
    public class MunicipioNegocio : IMunicipioNegocio
    {
        IUnitOfWork unitOfWork;
        IRepositorioGenerico<Municipio> repositorioMunicipios;
        MunicipioValidacao validacao;
        
        public MunicipioNegocio (IOrganogramaRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioMunicipios = repositorios.Municipios;
            validacao = new MunicipioValidacao(repositorioMunicipios);
        }

        
        public MunicipioModeloNegocio Pesquisar(int id)
        {
            Municipio municipioDominio = new Municipio();
            MunicipioModeloNegocio municipioNegocio = new MunicipioModeloNegocio();
                        
            municipioDominio = repositorioMunicipios.Where(q => q.Id.Equals(id)).SingleOrDefault();

            validacao.MunicipioNaoExistente(municipioDominio);

            municipioNegocio = Mapper.Map<Municipio, MunicipioModeloNegocio>(municipioDominio);

            return municipioNegocio;
        }

        public List<MunicipioModeloNegocio> Listar(string uf)
        {
            List<Municipio> municipiosDominio = new List<Municipio>();
           
            IQueryable<Municipio> query = repositorioMunicipios;

            if (!string.IsNullOrWhiteSpace(uf))
            {
                query = query.Where(m => m.Uf.ToUpper().Equals(uf.ToUpper()));
            }
            
            municipiosDominio = query.ToList();

            return Mapper.Map<List<Municipio>, List<MunicipioModeloNegocio>>(municipiosDominio);
        }

        public MunicipioModeloNegocio Inserir(MunicipioModeloNegocio municipioNegocio)
        {
            validacao.MunicipioValido(municipioNegocio);
            validacao.CodigoIbgeExistente(municipioNegocio);
            validacao.NomeUfExistente(municipioNegocio);

            Municipio municipio = PreparaMunicipioParaInsercao(municipioNegocio);

            repositorioMunicipios.Add(municipio);
            unitOfWork.Save();

            return Mapper.Map<Municipio, MunicipioModeloNegocio>(municipio);
        }

        public void Alterar (int id, MunicipioModeloNegocio municipioNegocio)
        {
            validacao.IdValido(municipioNegocio.Id);
            validacao.IdAlteracaoValido(id, municipioNegocio);
            validacao.PreenchimentoCompleto(municipioNegocio);
            validacao.NomeUfExistente(municipioNegocio);
            validacao.CodigoIbgeExistente(municipioNegocio);
            validacao.PreenchimentoCompleto(municipioNegocio);

            Municipio municipioDominio = repositorioMunicipios.Where(q => q.Id == municipioNegocio.Id).Single();

            validacao.MunicipioNaoExistente(municipioDominio);

            municipioNegocio.InicioVigencia = municipioDominio.InicioVigencia;            
            municipioDominio = Mapper.Map(municipioNegocio, municipioDominio);
            unitOfWork.Save();


        }

        public void Excluir (int id)
        {
            validacao.IdValido(id);

            Municipio municipio = repositorioMunicipios.Where(q => q.Id == id).SingleOrDefault();
            validacao.MunicipioNaoExistente(municipio);

            repositorioMunicipios.Remove(municipio);

            unitOfWork.Save();
        }

        private Municipio PreparaMunicipioParaInsercao (MunicipioModeloNegocio municipioNegocio)
        {
            Municipio municipio = new Municipio();
            municipioNegocio.InicioVigencia = DateTime.Now;
            municipio = Mapper.Map<MunicipioModeloNegocio, Municipio>(municipioNegocio);
            
            return municipio;

        }
               

    }
}
