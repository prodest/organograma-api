using AutoMapper;
using Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Base;
using System.Collections.Generic;
using System;

namespace Apresentacao
{
    public class MunicipioWorkService : IMunicipioWorkService
    {

        private IMunicipioNegocio municipioNegocio;

        public MunicipioWorkService(IMunicipioNegocio municipioNegocio)
        {
            this.municipioNegocio = municipioNegocio;
        }

        public MunicipioModeloGet Pesquisar(string guid)
        {
            return Mapper.Map<MunicipioModeloNegocio, MunicipioModeloGet>(municipioNegocio.Pesquisar(guid));
        }

        public List<MunicipioModeloGet> Listar(string uf)
        {
            return Mapper.Map<List<MunicipioModeloNegocio>, List<MunicipioModeloGet>>(municipioNegocio.Listar(uf));
        }

        public MunicipioModeloGet Inserir(MunicipioModeloPost municipioPost)
        {
            MunicipioModeloNegocio municipioModeloNegocio = new MunicipioModeloNegocio();
            municipioModeloNegocio = Mapper.Map<MunicipioModeloPost, MunicipioModeloNegocio>(municipioPost);
            return Mapper.Map<MunicipioModeloNegocio, MunicipioModeloGet>(municipioNegocio.Inserir(municipioModeloNegocio));
           
        }

        public void Alterar(string guid, MunicipioModeloPut municipio)
        {
            municipioNegocio.Alterar(guid, Mapper.Map<MunicipioModeloPut, MunicipioModeloNegocio>(municipio));
        }

        public void Excluir (string guid)
        {
            municipioNegocio.Excluir(guid);
        }

    }
}
