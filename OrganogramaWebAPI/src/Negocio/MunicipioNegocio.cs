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

        public MunicipioModeloNegocio ConsultaMunicipiosPorId(int id)
        {
            Municipio municipioDominio = new Municipio();
            MunicipioModeloNegocio municipioNegocio = new MunicipioModeloNegocio();

            municipioDominio = repositorioMunicipios.Where(q => q.Id.Equals(id)).SingleOrDefault();

            municipioNegocio = Mapper.Map<Municipio, MunicipioModeloNegocio>(municipioDominio);

            return municipioNegocio;
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
