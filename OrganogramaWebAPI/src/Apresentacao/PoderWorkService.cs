using AutoMapper;
using Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Base;
using System.Collections.Generic;
using System;

namespace Apresentacao
{
    public class PoderWorkService : IPoderWorkService
    {

        private IPoderNegocio poderNegocio;

        public PoderWorkService(IPoderNegocio poderNegocio)
        {
            this.poderNegocio = poderNegocio;
        }

        public PoderModeloGet Pesquisar(int id)
        {
            return Mapper.Map<PoderModeloNegocio, PoderModeloGet>(poderNegocio.Pesquisar(id));
        }

        public List<PoderModeloGet> Listar()
        {
            return Mapper.Map<List<PoderModeloNegocio>, List<PoderModeloGet>>(poderNegocio.Listar());
        }

        public PoderModeloGet Inserir(PoderModeloPost PoderPost)
        {
            PoderModeloNegocio PoderModeloNegocio = new PoderModeloNegocio();
            PoderModeloNegocio = Mapper.Map<PoderModeloPost, PoderModeloNegocio>(PoderPost);
            return Mapper.Map<PoderModeloNegocio, PoderModeloGet>(poderNegocio.Inserir(PoderModeloNegocio));
           
        }

        public void Alterar(int id, PoderModeloPut Poder)
        {
            poderNegocio.Alterar(id, Mapper.Map<PoderModeloPut, PoderModeloNegocio>(Poder));
        }

        public void Excluir (int id)
        {
            poderNegocio.Excluir(id);
        }

    }
}
