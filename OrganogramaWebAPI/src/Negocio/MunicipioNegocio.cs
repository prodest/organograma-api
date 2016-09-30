using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Base;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;


namespace Organograma.Negocio
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
            List<Municipio> municipiosDominio = new List<Municipio>();
            List<MunicipioModeloNegocio> municipiosNegocio = new List<MunicipioModeloNegocio>();

            municipiosDominio = repositorioMunicipios.ToList();

            municipiosNegocio = Mapper.Map<List<Municipio>, List<MunicipioModeloNegocio>>(municipiosDominio);

            return municipiosNegocio;
        }
    }
}
