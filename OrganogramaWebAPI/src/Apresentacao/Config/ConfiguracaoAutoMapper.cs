using AutoMapper;
using Organograma.Apresentacao.Modelos;
using Organograma.Negocio.Modelos;
using Organograma.Negocio.Config;

namespace Organograma.Apresentacao.Config
{
    public static class ConfiguracaoAutoMapper
    {
        public static void CriarMapeamento()
        {

            Mapper.Initialize(cfg =>
            {
                /* MunicipioNegocio -> MunicipioApresentacao   */
                cfg.CreateMap<MunicipioModeloNegocio, MunicipioModeloApresentacao>()
                 .ForMember(dest => dest.CodigoIbge, opt => opt.MapFrom(src => src.CodigoIbge))
                 .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                 .ForMember(dest => dest.Uf, opt => opt.MapFrom(src => src.Uf));

                cfg.CreateMap<TipoOrganizacaoModeloNegocio, TipoOrganizacaoModelo>()
                .ForMember(dest => dest.InicioVigencia, opt => opt.MapFrom(src => src.InicioVigencia.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.FimVigencia, opt => opt.MapFrom(src => src.FimVigencia.HasValue ? src.FimVigencia.Value.ToString("dd/MM/yyyy") : null));

                cfg.CreateMap<TipoOrganizacaoModeloPut, TipoOrganizacaoModeloNegocio>();
                cfg.CreateMap<TipoOrganizacaoModeloPost, TipoOrganizacaoModeloNegocio>();

                cfg.AddProfile<NegocioProfile>();
            });


        }
    }
}