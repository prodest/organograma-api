using AutoMapper;
using Apresentacao.Municipio.Base;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Base;
using System.Collections.Generic;
using System;

namespace Apresentacao.Municipio
{
    public class MunicipioWorkService : IMunicipioWorkService
    {

        private IMunicipioNegocio consultaMunicipio;

        public MunicipioWorkService(IMunicipioNegocio consultaMunicipio)
        {
            this.consultaMunicipio = consultaMunicipio;
        }

        public MunicipioModeloApresentacao ConsultarMunicipioPorId(int id)
        {
            return Mapper.Map<MunicipioModeloNegocio, MunicipioModeloApresentacao>(consultaMunicipio.ConsultaMunicipiosPorId(id));
        }

        public List<MunicipioModeloApresentacao> ConsultarMunicipios()
        {
            return Mapper.Map<List<MunicipioModeloNegocio>, List<MunicipioModeloApresentacao>>(consultaMunicipio.ConsultaMunicipios());

        }
    }
}
