using Organograma.Dominio.Base;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Municipio.Base;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;


namespace Organograma.Negocio.Municipio
{
    public class MunicipioNegocio : IMunicipioNegocio
    {
        IUnitOfWork unitOfWork;
        IRepositorioGenerico<Dominio.Modelos.Municipio> repositorioMunicipios;
        
        public MunicipioNegocio (IOrganogramaRepositorios repositorios)
        {
            this.unitOfWork = repositorios.UnitOfWork;
            this.repositorioMunicipios = repositorios.Municipios;
        }


        public List<MunicipioModeloNegocio> ConsultaMunicipios()
        {
            List<Dominio.Modelos.Municipio> municipiosDominio = new List<Dominio.Modelos.Municipio>();
            List<MunicipioModeloNegocio> municipiosNegocio = new List<MunicipioModeloNegocio>();

            municipiosDominio = repositorioMunicipios.ToList();

            municipiosNegocio = Mapper.Map<List<Dominio.Modelos.Municipio>, List<MunicipioModeloNegocio>>(municipiosDominio);

            return municipiosNegocio;
        }
    }
}
