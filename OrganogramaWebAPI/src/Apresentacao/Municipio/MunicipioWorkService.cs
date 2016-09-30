using AutoMapper;
using Apresentacao.Municipio.Base;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Base;
using System.Collections.Generic;

namespace Apresentacao.Municipio
{
    public class MunicipioWorkService : IMunicipioWorkService
    {

        private IMunicipioNegocio consultaMunicipio;

        public MunicipioWorkService (IMunicipioNegocio consultaMunicipio)
        {
            this.consultaMunicipio = consultaMunicipio;
        }


        public List<MunicipioModeloApresentacao> ConsultarMunicipios()
        {
            List<MunicipioModeloApresentacao> municipiosApresentacao;
            List<MunicipioModeloNegocio> municipiosNegocio;
            municipiosNegocio = consultaMunicipio.ConsultaMunicipios();

            municipiosApresentacao = Mapper.Map<List<MunicipioModeloNegocio>, List<MunicipioModeloApresentacao>>(municipiosNegocio);

            return municipiosApresentacao;
        }
    }
}
