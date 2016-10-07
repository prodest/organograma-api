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
            this.unitOfWork = repositorios.UnitOfWork;
            this.repositorioMunicipios = repositorios.Municipios;
            validacao = new MunicipioValidacao(repositorioMunicipios);
        }

        
        public MunicipioModeloNegocio Pesquisar(int id)
        {
            Municipio municipioDominio = new Municipio();
            MunicipioModeloNegocio municipioNegocio = new MunicipioModeloNegocio();

            //Obtem Município com o codigo Ibge informado desde que esteja vigente (data de fim de vigencia nula)
            municipioDominio = repositorioMunicipios.Where(q => q.CodigoIbge.Equals(id)).SingleOrDefault();

            municipioNegocio = Mapper.Map<Municipio, MunicipioModeloNegocio>(municipioDominio);

            return municipioNegocio;
        }

        public List<MunicipioModeloNegocio> Listar()
        {
            List<Municipio> municipiosDominio = new List<Municipio>();
            List<MunicipioModeloNegocio> municipiosNegocio = new List<MunicipioModeloNegocio>();

            municipiosDominio = repositorioMunicipios.ToList();

            municipiosNegocio = Mapper.Map<List<Municipio>, List<MunicipioModeloNegocio>>(municipiosDominio);

            return municipiosNegocio;
        }

        public MunicipioModeloNegocio Inserir(MunicipioModeloNegocio municipioNegocio)
        {
            validacao.MunicipioValido(municipioNegocio);
            validacao.CodigoIbgeExistente(municipioNegocio);
            validacao.NomeUfExistente(municipioNegocio);

            repositorioMunicipios.Add(PreparaMunicipioParaInsercao(municipioNegocio));
            unitOfWork.Save();

            return Pesquisar(municipioNegocio.CodigoIbge);
        }


        public Municipio PreparaMunicipioParaInsercao (MunicipioModeloNegocio municipioNegocio)
        {
            Municipio municipio = new Municipio();
            municipio = Mapper.Map<MunicipioModeloNegocio, Municipio>(municipioNegocio);
            municipio.InicioVigencia = DateTime.Now;

            return municipio;

        }


    }
}
