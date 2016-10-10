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

        public MunicipioModeloGet Pesquisar(int id)
        {
            return Mapper.Map<MunicipioModeloNegocio, MunicipioModeloGet>(municipioNegocio.Pesquisar(id));
        }

        public List<MunicipioModeloGet> Listar()
        {
            return Mapper.Map<List<MunicipioModeloNegocio>, List<MunicipioModeloGet>>(municipioNegocio.Listar());

        }

        public MunicipioModeloGet Inserir(MunicipioModeloPost municipioPost)
        {
            MunicipioModeloNegocio municipioModeloNegocio = new MunicipioModeloNegocio();
            municipioModeloNegocio = Mapper.Map<MunicipioModeloPost, MunicipioModeloNegocio>(municipioPost);
            return Mapper.Map<MunicipioModeloNegocio, MunicipioModeloGet>(municipioNegocio.Inserir(municipioModeloNegocio));
           
        }

        public void Alterar(int id, MunicipioModeloPut municipio)
        {
            municipioNegocio.Alterar(id, Mapper.Map<MunicipioModeloPut, MunicipioModeloNegocio>(municipio));
        }

        public void Excluir (int id)
        {
            municipioNegocio.Excluir(id);
        }

    }
}
