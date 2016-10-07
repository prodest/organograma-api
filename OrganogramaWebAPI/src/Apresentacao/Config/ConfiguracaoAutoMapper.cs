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

                #region Município

                cfg.CreateMap<MunicipioModeloNegocio, MunicipioModeloGet>()
                .ForMember(dest => dest.InicioVigencia, opt => opt.MapFrom(src => src.InicioVigencia.Value.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.FimVigencia, opt => opt.MapFrom(src => src.FimVigencia.HasValue ? src.FimVigencia.Value.ToString("dd/MM/yyyy") : null));

                cfg.CreateMap<MunicipioModeloPost, MunicipioModeloNegocio>();
                               

                #endregion

                #region Tipo Organização

                cfg.CreateMap<TipoOrganizacaoModeloNegocio, TipoOrganizacaoModelo>()
                .ForMember(dest => dest.InicioVigencia, opt => opt.MapFrom(src => src.InicioVigencia.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.FimVigencia, opt => opt.MapFrom(src => src.FimVigencia.HasValue ? src.FimVigencia.Value.ToString("dd/MM/yyyy") : null));

                cfg.CreateMap<TipoOrganizacaoModeloPut, TipoOrganizacaoModeloNegocio>();
                cfg.CreateMap<TipoOrganizacaoModeloPost, TipoOrganizacaoModeloNegocio>();

                #endregion

                #region Negócio   

                cfg.AddProfile<NegocioProfile>();

                #endregion
            });


        }
    }
}