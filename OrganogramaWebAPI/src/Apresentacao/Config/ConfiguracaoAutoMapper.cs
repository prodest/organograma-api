using AutoMapper;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Config;
using System;

namespace Organograma.Apresentacao.Config
{
    public static class ConfiguracaoAutoMapper
    {
        public static void CriarMapeamento()
        {

            Mapper.Initialize(cfg =>
            {
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

                #region Negócio   

                cfg.AddProfile<NegocioProfile>();

                #endregion
            });


        }
    }
}