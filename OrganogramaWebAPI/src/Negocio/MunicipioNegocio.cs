using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Organograma.Dominio.Base;
using Organograma.Dominio.Modelos;
using Organograma.Infraestrutura.Comum;
using Organograma.Negocio.Base;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Organograma.Negocio
{
    public class MunicipioNegocio : IMunicipioNegocio
    {
        IUnitOfWork unitOfWork;
        IRepositorioGenerico<Municipio> repositorioMunicipios;
        IRepositorioGenerico<Historico> repositorioHistoricos;
        IRepositorioGenerico<IdentificadorExterno> repositorioIdentificadoresExternos;
        MunicipioValidacao validacao;
        
        public MunicipioNegocio (IOrganogramaRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioMunicipios = repositorios.Municipios;
            repositorioHistoricos = repositorios.Historicos;
            repositorioIdentificadoresExternos = repositorios.IdentificadoresExternos;
            validacao = new MunicipioValidacao(repositorioMunicipios);
        }

        public MunicipioModeloNegocio Pesquisar(string guid)
        {
            validacao.GuidValido(guid);

            Municipio municipioDominio = new Municipio();
            MunicipioModeloNegocio municipioNegocio = new MunicipioModeloNegocio();

            Guid gMunicipio = new Guid(guid);

            municipioDominio = repositorioMunicipios.Where(m => m.IdentificadorExterno.Guid.Equals(gMunicipio))
                                                    .Include(m => m.IdentificadorExterno)
                                                    .SingleOrDefault();

            validacao.MunicipioNaoExistente(municipioDominio);

            municipioNegocio = Mapper.Map<Municipio, MunicipioModeloNegocio>(municipioDominio);

            return municipioNegocio;
        }

        public List<MunicipioModeloNegocio> Listar(string uf)
        {
            List<Municipio> municipiosDominio = new List<Municipio>();
           
            IQueryable<Municipio> query = repositorioMunicipios;

            if (!string.IsNullOrWhiteSpace(uf))
            {
                query = query.Where(m => m.Uf.ToUpper().Equals(uf.ToUpper()));
            }

            query = query.Include(m => m.IdentificadorExterno)
                         .OrderBy(m => m.Uf)
                         .ThenBy(m => m.Nome);
            
            municipiosDominio = query.ToList();

            return Mapper.Map<List<Municipio>, List<MunicipioModeloNegocio>>(municipiosDominio);
        }

        public MunicipioModeloNegocio Inserir(MunicipioModeloNegocio municipioNegocio)
        {
            validacao.MunicipioValido(municipioNegocio);
            validacao.CodigoIbgeExistente(municipioNegocio);
            validacao.NomeUfExistente(municipioNegocio);

            Municipio municipio = PreparaMunicipioParaInsercao(municipioNegocio);

            repositorioMunicipios.Add(municipio);
            unitOfWork.Save();

            return Mapper.Map<Municipio, MunicipioModeloNegocio>(municipio);
        }

        public void Alterar (string guid, MunicipioModeloNegocio municipioNegocio)
        {
            validacao.GuidValido(municipioNegocio.Guid);
            validacao.GuidAlteracaoValido(guid, municipioNegocio);
            validacao.PreenchimentoCompleto(municipioNegocio);
            validacao.NomeUfExistente(municipioNegocio);
            validacao.CodigoIbgeExistente(municipioNegocio);
            validacao.PreenchimentoCompleto(municipioNegocio);

            Guid gMunicipio = new Guid(municipioNegocio.Guid);

            Municipio municipioDominio = repositorioMunicipios.Where(q => q.IdentificadorExterno.Guid.Equals(gMunicipio))
                                                              .Include(m => m.IdentificadorExterno)
                                                              .Single();

            validacao.MunicipioNaoExistente(municipioDominio);

            DateTime agora = DateTime.Now;

            InserirHistorico(municipioDominio, "Edição", agora);

            municipioNegocio.Id = municipioDominio.Id;
            municipioNegocio.InicioVigencia = agora;

            municipioDominio = Mapper.Map(municipioNegocio, municipioDominio);

            unitOfWork.Save();
        }

        public void Excluir (string guid)
        {
            validacao.GuidValido(guid);

            Guid guidMunicipio = new Guid(guid);

            Municipio municipio = repositorioMunicipios.Where(m => m.IdentificadorExterno.Guid.Equals(guidMunicipio))
                                                       .Include(m => m.IdentificadorExterno)
                                                       .Include(m => m.Enderecos)
                                                       .SingleOrDefault();

            validacao.MunicipioNaoExistente(municipio);

            validacao.MunicipioPossuiEndereco(municipio);

            InserirHistorico(municipio, "Exclusão", null);

            repositorioMunicipios.Remove(municipio);

            unitOfWork.Save();
        }

        private Municipio PreparaMunicipioParaInsercao (MunicipioModeloNegocio municipioNegocio)
        {
            municipioNegocio.Guid = Guid.NewGuid().ToString("D");

            Municipio municipio = new Municipio();
            municipioNegocio.InicioVigencia = DateTime.Now;

            municipio = Mapper.Map<MunicipioModeloNegocio, Municipio>(municipioNegocio);
            
            return municipio;

        }

        private void InserirHistorico(Municipio municipio, string obsFimVigencia, DateTime? now)
        {
            string municipioJson = JsonData.SerializeObject(municipio);

            Historico historico = new Historico
            {
                Json = municipioJson,
                InicioVigencia = municipio.InicioVigencia,
                FimVigencia = now.HasValue ? now.Value : DateTime.Now,
                ObservacaoFimVigencia = obsFimVigencia,
                IdIdentificadorExterno = municipio.IdentificadorExterno.Id
            };

            repositorioHistoricos.Add(historico);
        }
    }
}
