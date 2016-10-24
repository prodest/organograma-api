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

            CreateMap<EmailModeloNegocio, Email>();
            CreateMap<EmailModeloNegocio, EmailOrganizacao>().ForMember(dest => dest.Email, opt => opt.MapFrom(s => s));
            CreateMap<EmailModeloNegocio, EmailUnidade>().ForMember(dest => dest.Email, opt => opt.MapFrom(s => s));

            CreateMap<EnderecoModeloNegocio, Endereco>().ReverseMap();
            CreateMap<SiteModeloNegocio, Site>().ReverseMap();
            CreateMap<ContatoModeloNegocio, Contato>().ReverseMap();

            CreateMap<TipoOrganizacao, TipoOrganizacaoModeloNegocio>().ReverseMap();
            CreateMap<TipoUnidade, TipoUnidadeModeloNegocio>().ReverseMap();

            CreateMap<Endereco, EnderecoModeloNegocio>();
            CreateMap<Organizacao, OrganizacaoModeloNegocio>();
            CreateMap<EnderecoModeloNegocio, Endereco>().ForMember(dest => dest.IdMunicipio, opt => opt.MapFrom(s => s.Municipio.Id));


            CreateMap<OrganizacaoModeloNegocio, Organizacao>()
                .ForMember(dest => dest.IdOrganizacaoPai, opt => opt.MapFrom(s => s.OrganizacaoPai != null? s.OrganizacaoPai.Id : (int?)null))
                .ForMember(dest => dest.IdEsfera, opt => opt.MapFrom(s => s.Esfera.Id))
                .ForMember(dest => dest.IdPoder, opt => opt.MapFrom(s => s.Poder.Id))
                .ForMember(dest => dest.IdTipoOrganizacao, opt => opt.MapFrom(s => s.TipoOrganizacao.Id))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => s.Endereco))
                .ForMember(dest => dest.EmailsOrganizacao, opt => opt.MapFrom(s => Mapper.Map<List<EmailModeloNegocio>, List<EmailOrganizacao>>(s.Emails)));

            CreateMap<UnidadeModeloNegocio, Unidade>()
                .ForMember(dest => dest.IdOrganizacao, opt => opt.MapFrom(s => s.Organizacao != null ? s.Organizacao.Id : default(int)))
                .ForMember(dest => dest.IdTipoUnidade, opt => opt.MapFrom(s => s.TipoUnidade != null ? s.TipoUnidade.Id : default(int)))
                .ForMember(dest => dest.IdEndereco, opt => opt.MapFrom(s => s.Endereco != null ? s.Endereco.Id : default(int)))
                .ForMember(dest => dest.IdUnidadePai, opt => opt.MapFrom(s => s.UnidadePai != null ? s.UnidadePai.Id : default(int)))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => s.Endereco != null ? Mapper.Map<EnderecoModeloNegocio, Endereco>(s.Endereco) : null))
                
                .ForMember(dest => dest.Organizacao, opt => opt.MapFrom(s => s.Organizacao != null ? Mapper.Map<OrganizacaoModeloNegocio,Organizacao>(s.Organizacao) : null))
                
                .ForMember(dest => dest.TipoUnidade, opt => opt.MapFrom(s => s.TipoUnidade!= null ? Mapper.Map<TipoUnidadeModeloNegocio, TipoUnidade>(s.TipoUnidade) : null))
                .ForMember(dest => dest.UnidadePai, opt => opt.MapFrom(s => s.UnidadePai != null ? Mapper.Map<UnidadeModeloNegocio, Unidade>(s.UnidadePai) : null))
                .ForMember(dest => dest.EmailsUnidade, opt => opt.MapFrom(s => s.Emails != null ? Mapper.Map<List<EmailModeloNegocio>, List<EmailUnidade>>(s.Emails) : null));
        }
    }
}
