﻿using AutoMapper;
using Organograma.Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Base;
using Organograma.Negocio.Modelos;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Organograma.Apresentacao
{
    public class UnidadeWorkService : IUnidadeWorkService
    {
        private IUnidadeNegocio unidadeNegocio;

        public UnidadeWorkService(IUnidadeNegocio unidadeNegocio)
        {
            this.unidadeNegocio = unidadeNegocio;
        }

        public void Alterar(string guid, UnidadeModeloPatch unidade)
        {
            UnidadeModeloNegocio umn = Mapper.Map<UnidadeModeloPatch, UnidadeModeloNegocio>(unidade);

            unidadeNegocio.Alterar(guid, umn);
        }

        public void Excluir(string guid)
        {
            unidadeNegocio.Excluir(guid);
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

        public UnidadeModeloGet Pesquisar(string guid)
        {
            var umn = unidadeNegocio.Pesquisar(guid);

            return Mapper.Map<UnidadeModeloNegocio, UnidadeModeloGet>(umn); ;
        }

        public List<UnidadeSimplesModeloGet> PesquisarPorOrganizacao(string guidOrganizacao)
        {
            var umn = unidadeNegocio.PesquisarPorOrganizacao(guidOrganizacao);

            return Mapper.Map<List<UnidadeModeloNegocio>, List<UnidadeSimplesModeloGet>>(umn); ;
        }

        public void ExcluirEmail(int id, List<EmailModelo> emails)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponsavelUnidadeModeloGet> PesquisarResponsavel(string guid)
        {
            var umn = await unidadeNegocio.PesquisarResponsavel(guid);

            return Mapper.Map<UnidadeModeloNegocio.Responsavel, ResponsavelUnidadeModeloGet>(umn);
        }
    }
}
