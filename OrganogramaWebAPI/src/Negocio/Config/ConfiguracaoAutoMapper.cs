using AutoMapper;
using Organograma.Dominio.Modelos;
using Organograma.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organograma.Negocio.Config
{
    public static class ConfiguracaoAutoMapper
    {
        public static NegocioProfile GetNegocioProfile()
        {
            return new NegocioProfile();
        }
    }

    public class NegocioProfile : Profile
    {
        public NegocioProfile()
        {
            #region Mapeamento de EsferaOrganizacao
            CreateMap<EsferaOrganizacao, EsferaOrganizacaoModeloNegocio>();
            CreateMap<EsferaOrganizacaoModeloNegocio, EsferaOrganizacao>();
            #endregion

            CreateMap<Municipio, MunicipioModeloNegocio>().ReverseMap();
            CreateMap<Poder, PoderModeloNegocio>().ReverseMap();
            
            

            CreateMap<EnderecoModeloNegocio, Endereco>().ReverseMap();
            CreateMap<SiteModeloNegocio, Site>().ReverseMap();
            CreateMap<ContatoModeloNegocio, Contato>().ReverseMap();

            CreateMap<TipoOrganizacao, TipoOrganizacaoModeloNegocio>().ReverseMap();
            CreateMap<TipoUnidade, TipoUnidadeModeloNegocio>().ReverseMap();

            CreateMap<Endereco, EnderecoModeloNegocio>();
            CreateMap<Organizacao, OrganizacaoModeloNegocio>();
            CreateMap<EnderecoModeloNegocio, Endereco>().ForMember(dest => dest.IdMunicipio, opt => opt.MapFrom(s => s.Municipio.Id));


            CreateMap<EmailModeloNegocio, Email>();
            CreateMap<EmailModeloNegocio, EmailOrganizacao>().ForMember(dest => dest.Email, opt => opt.MapFrom(s => s));
            CreateMap<OrganizacaoModeloNegocio, Organizacao>()
                .ForMember(dest => dest.IdOrganizacaoPai, opt => opt.MapFrom(s => s.OrganizacaoPai != null? s.OrganizacaoPai.Id : (int?)null))
                .ForMember(dest => dest.IdEsfera, opt => opt.MapFrom(s => s.Esfera.Id))
                .ForMember(dest => dest.IdPoder, opt => opt.MapFrom(s => s.Poder.Id))
                .ForMember(dest => dest.IdTipoOrganizacao, opt => opt.MapFrom(s => s.TipoOrganizacao.Id))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => s.Endereco))
                .ForMember(dest => dest.EmailsOrganizacao, opt => opt.MapFrom(s => Mapper.Map<List<EmailModeloNegocio>, List<EmailOrganizacao>>(s.Emails)));



            CreateMap<UnidadeModeloNegocio, Unidade>()
                .BeforeMap((s, d) =>
                {
                    d.EmailsUnidade = new HashSet<EmailUnidade>();

                    foreach (var email in s.Emails)
                    {
                        EmailUnidade eu = new EmailUnidade();
                        eu.Email = Mapper.Map<EmailModeloNegocio, Email>(email);
                        eu.IdEmail = eu.Email.Id;
                        eu.IdUnidade = s.Id;

                        d.EmailsUnidade.Add(eu);
                    }
                })
                .ForMember(dest => dest.IdOrganizacao, opt => opt.MapFrom(s => s.Organizacao.Id))
                .ForMember(dest => dest.IdTipoUnidade, opt => opt.MapFrom(s => s.TipoUnidade.Id))
                .ForMember(dest => dest.IdEndereco, opt => opt.MapFrom(s => s.Endereco != null ? s.Endereco.Id : default(int)))
                .ForMember(dest => dest.IdUnidadePai, opt => opt.MapFrom(s => s.UnidadePai != null ? s.UnidadePai.Id : default(int)))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => s.Endereco))
                .ForMember(dest => dest.Organizacao, opt => opt.MapFrom(s => s.Organizacao))
                .ForMember(dest => dest.TipoUnidade, opt => opt.MapFrom(s => s.TipoUnidade))
                .ForMember(dest => dest.UnidadePai, opt => opt.MapFrom(s => s.UnidadePai != null ? s.UnidadePai : null))
                .ForMember(dest => dest.EmailsUnidade, opt => opt.Ignore());

        }
    }
}
