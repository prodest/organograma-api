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

            CreateMap<EnderecoModeloNegocio, Endereco>().ReverseMap();
            CreateMap<SiteModeloNegocio, Site>().ReverseMap();
            CreateMap<ContatoModeloNegocio, Contato>().ReverseMap();

            CreateMap<TipoOrganizacao, TipoOrganizacaoModeloNegocio>();
            CreateMap<TipoUnidade, TipoUnidadeModeloNegocio>().ReverseMap();

            CreateMap<Endereco, EnderecoModeloNegocio>().ReverseMap();
            CreateMap<Organizacao, OrganizacaoModeloNegocio>().ReverseMap();

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

            //.ForMember(dest => dest.ContatosUnidade, opt => opt.MapFrom(s => s.Contatos))

            //.ForMember(dest => dest.EmailsUnidade, opt => 

            //opt.MapFrom(s => new HashSet<EmailUnidade> {  } Mapper.Map<List<EmailModeloNegocio>, List<Email>>(s.Emails)))
            ;

            //cfg.CreateMap<OrganizacaoModeloPost, OrganizacaoModeloNegocio>()
            //     .ForMember(dest => dest.Endereco, opt => opt.MapFrom(s => Mapper.Map<EnderecoModelo, EnderecoModeloNegocio>(s.Endereco)))
            //     .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => Mapper.Map<List<EmailModelo>, List<EmailModeloNegocio>>(s.Emails)))
            //     .ForMember(dest => dest.Sites, opt => opt.MapFrom(s => Mapper.Map<List<SiteModelo>, List<SiteModeloNegocio>>(s.Sites)))
            //     .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => Mapper.Map<List<ContatoModelo>, List<ContatoModeloNegocio>>(s.Contatos)))
            //     .ForMember(dest => dest.Poder, opt => opt.MapFrom(s => Mapper.Map<PoderModeloNegocio>(new PoderModeloGet { Id = s.IdPoder })))
            //     .ForMember(dest => dest.Esfera, opt => opt.MapFrom(s => Mapper.Map<EsferaOrganizacaoModeloNegocio>(new EsferaOrganizacaoModelo { Id = s.IdEsfera })))
            //     .ForMember(dest => dest.OrganizacaoPai, opt => opt.MapFrom(s => s.IdOrganizacaoPai != default(int) ? new OrganizacaoModeloNegocio() { Id = s.IdOrganizacaoPai } : null))
            //     .ForMember(dest => dest.TipoOrganizacao, opt => opt.MapFrom(s => Mapper.Map<TipoOrganizacaoModeloNegocio>(new TipoOrganizacaoModelo { Id = s.IdTipoOrganizacao })));
        }
    }
}
