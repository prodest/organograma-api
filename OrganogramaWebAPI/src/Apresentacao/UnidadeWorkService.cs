using AutoMapper;
using Organograma.Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Base;
using Organograma.Negocio.Modelos;
using System.Collections.Generic;
using System;

namespace Organograma.Apresentacao
{
    public class UnidadeWorkService : IUnidadeWorkService
    {
        private IUnidadeNegocio unidadeNegocio;

        public UnidadeWorkService(IUnidadeNegocio unidadeNegocio)
        {
            this.unidadeNegocio = unidadeNegocio;
        }

        public void Alterar(int id, UnidadeModeloPatch unidade)
        {
            UnidadeModeloNegocio umn = Mapper.Map<UnidadeModeloPatch, UnidadeModeloNegocio>(unidade);

            unidadeNegocio.Alterar(id, umn);
        }

        public void Excluir(int id)
        {
            unidadeNegocio.Excluir(id);
        }

        public UnidadeModeloRetornoPost Inserir(UnidadeModeloPost unidade)
        {
            UnidadeModeloNegocio umn = Mapper.Map<UnidadeModeloPost, UnidadeModeloNegocio>(unidade);

            umn = unidadeNegocio.Inserir(umn);

            return Mapper.Map<UnidadeModeloNegocio, UnidadeModeloRetornoPost>(umn);
        }

        public List<UnidadeModeloRetornoPost> Listar()
        {
            var unidades = unidadeNegocio.Listar();

            return Mapper.Map<List<UnidadeModeloNegocio>, List<UnidadeModeloRetornoPost>>(unidades);
        }

        public UnidadeModeloGet Pesquisar(int id)
        {
            var umn = unidadeNegocio.Pesquisar(id);

            return Mapper.Map<UnidadeModeloNegocio, UnidadeModeloGet>(umn); ;
        }

        public void ExcluirEmail(int id, List<EmailModelo> emails)
        {
            throw new NotImplementedException();
        }
    }
}
