using AutoMapper;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Config;
using System;
using System.Collections.Generic;

namespace Organograma.Apresentacao.Config
{
    public static class ConfiguracaoAutoMapper
    {
        public static void CriarMapeamento()
        {

            Mapper.Initialize(cfg =>
            {
                cfg.AllowNullCollections = true;

                #region Mapeamento de EsferaOrganizacao
                cfg.CreateMap<EsferaOrganizacaoModeloNegocio, EsferaOrganizacaoModelo>();

                cfg.CreateMap<EsferaOrganizacaoModelo, EsferaOrganizacaoModeloNegocio>();
                cfg.CreateMap<EsferaOrganizacaoModeloPost, EsferaOrganizacaoModeloNegocio>();
                #endregion

                #region Município

                cfg.CreateMap<MunicipioModeloNegocio, MunicipioModeloGet>()
                .ForMember(dest => dest.InicioVigencia, opt => opt.MapFrom(src => src.InicioVigencia.HasValue ? src.InicioVigencia.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(dest => dest.FimVigencia, opt => opt.MapFrom(src => src.FimVigencia.HasValue ? src.FimVigencia.Value.ToString("dd/MM/yyyy") : null));

                cfg.CreateMap<MunicipioModeloPost, MunicipioModeloNegocio>();
                cfg.CreateMap<MunicipioModeloPut, MunicipioModeloNegocio>();


                #endregion

                #region Tipo Organização

                #region Mapeamento de TipoOrganizacao
                cfg.CreateMap<TipoOrganizacaoModeloNegocio, TipoOrganizacaoModelo>()
                .ForMember(dest => dest.InicioVigencia, opt => opt.MapFrom(src => src.InicioVigencia.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.FimVigencia, opt => opt.MapFrom(src => src.FimVigencia.HasValue ? src.FimVigencia.Value.ToString("dd/MM/yyyy") : null));

                cfg.CreateMap<TipoOrganizacaoModeloPut, TipoOrganizacaoModeloNegocio>();
                cfg.CreateMap<TipoOrganizacaoModeloPost, TipoOrganizacaoModeloNegocio>();
                #endregion

                #region Mapeamento de TipoUnidade
                cfg.CreateMap<TipoUnidadeModeloNegocio, TipoUnidadeModelo>()
                .ForMember(dest => dest.InicioVigencia, opt => opt.MapFrom(src => src.InicioVigencia.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.FimVigencia, opt => opt.MapFrom(src => src.FimVigencia.HasValue ? src.FimVigencia.Value.ToString("dd/MM/yyyy") : null));

                cfg.CreateMap<TipoUnidadeModeloPut, TipoUnidadeModeloNegocio>();
                cfg.CreateMap<TipoUnidadeModeloPost, TipoUnidadeModeloNegocio>();
                #endregion

                #endregion

                #region Poder

                cfg.CreateMap<PoderModeloPut, PoderModeloNegocio>();
                cfg.CreateMap<PoderModeloPost, PoderModeloNegocio>();
                cfg.CreateMap<PoderModeloNegocio, PoderModeloGet>();

                #endregion

                cfg.CreateMap<EmailModelo, EmailModeloNegocio>().ReverseMap();

                #region Endereco
                cfg.CreateMap<EnderecoModelo, EnderecoModeloNegocio>()
                   .ForMember(dest => dest.Municipio, opt => opt.MapFrom(s => s.IdMunicipio != default(int) ? new MunicipioModeloNegocio() { Id = s.IdMunicipio } : null));
                #endregion

                cfg.CreateMap<SiteModelo, SiteModeloNegocio>().ReverseMap();

                #region Contato
                cfg.CreateMap<ContatoModelo, ContatoModeloNegocio>()
                   .ForMember(dest => dest.TipoContato, opt => opt.MapFrom(s => new TipoContatoModeloNegocio { Id = s.IdTipoContato }));
                #endregion

                #region Organizacao

                cfg.CreateMap<OrganizacaoModeloPost, OrganizacaoModeloNegocio>()
                 .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => Mapper.Map<EnderecoModelo, EnderecoModeloNegocio>(s.Endereco)))
                 .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => Mapper.Map<List<EmailModelo>, List<EmailModeloNegocio>>(s.Emails)))
                 .ForMember(dest => dest.Sites, opt => opt.MapFrom(s => Mapper.Map<List<SiteModelo>, List<SiteModeloNegocio>>(s.Sites)))
                 .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => Mapper.Map<List<ContatoModelo>, List<ContatoModeloNegocio>>(s.Contatos)))
                 .ForMember(dest => dest.Poder, opt => opt.MapFrom(s => Mapper.Map<PoderModeloNegocio>(new PoderModeloGet { Id = s.IdPoder })))
                 .ForMember(dest => dest.Esfera, opt => opt.MapFrom(s => Mapper.Map<EsferaOrganizacaoModeloNegocio>(new EsferaOrganizacaoModelo { Id = s.IdEsfera })))
                 .ForMember(dest => dest.OrganizacaoPai, opt => opt.MapFrom(s => s.IdOrganizacaoPai != default(int) ? new OrganizacaoModeloNegocio() { Id = s.IdOrganizacaoPai } : null))
                 .ForMember(dest => dest.TipoOrganizacao, opt => opt.MapFrom(s => Mapper.Map<TipoOrganizacaoModeloNegocio>(new TipoOrganizacaoModelo { Id = s.IdTipoOrganizacao })));

                cfg.CreateMap<OrganizacaoModeloNegocio, OrganizacaoModeloGet>()
                 .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => Mapper.Map<EnderecoModeloNegocio, EnderecoModelo>(s.Endereco)))
                 .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => Mapper.Map<List<EmailModeloNegocio>, List<EmailModelo>>(s.Emails)))
                 .ForMember(dest => dest.Sites, opt => opt.MapFrom(s => Mapper.Map<List<SiteModeloNegocio>, List<SiteModelo>>(s.Sites)))
                 .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => Mapper.Map<List<ContatoModeloNegocio>, List<ContatoModelo>>(s.Contatos)));


                #endregion

                #region Unidade
                cfg.CreateMap<UnidadeModeloPost, UnidadeModeloNegocio>()
                   .ForMember(dest => dest.Organizacao, opt => opt.MapFrom(s => s.IdOrganizacao != default(int) ? new OrganizacaoModeloNegocio() { Id = s.IdOrganizacao } : null))
                   .ForMember(dest => dest.TipoUnidade, opt => opt.MapFrom(s => s.IdTipoUnidade != default(int) ? new TipoUnidadeModeloNegocio() { Id = s.IdTipoUnidade } : null))
                   .ForMember(dest => dest.UnidadePai, opt => opt.MapFrom(s => s.IdUnidadePai.HasValue && s.IdUnidadePai.Value != default(int) ? new UnidadeModeloNegocio() { Id = s.IdUnidadePai.Value } : null))
                   .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => s.Endereco != null ? Mapper.Map<EnderecoModelo, EnderecoModeloNegocio>(s.Endereco) : null))
                   .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => s.Contatos != null ? Mapper.Map<List<ContatoModelo>, List<ContatoModeloNegocio>>(s.Contatos) : null))
                   .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => s.Emails != null ? Mapper.Map<List<EmailModelo>, List<EmailModeloNegocio>>(s.Emails) : null))
                   .ForMember(dest => dest.Sites, opt => opt.MapFrom(s => s.Sites != null ? Mapper.Map<List<SiteModelo>, List<SiteModeloNegocio>>(s.Sites) : null))
                   ;
                #endregion

                #region Negócio   

                cfg.AddProfile<NegocioProfile>();

                #endregion
            });


        }
    }
}